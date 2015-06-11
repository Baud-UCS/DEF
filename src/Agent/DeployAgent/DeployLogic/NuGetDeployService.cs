using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.DeployLogic.Contracts;
using Baud.Deployment.DeployLogic.NuGetHelpers;
using NuGet;

namespace Baud.Deployment.DeployLogic
{
    public class NuGetDeployService : IDeployService
    {
        private const string TransformFilesExtension = ".pp";
        private const string ContentDirectoryName = "content";
        private const string ManifestFileName = "DeployManifest.xml";

        private const string SharedSite = "Shared";
        private const string ScriptPackageId = "DEF.DeployScripts";
        private const string DeployScriptFileName = "Deploy.ps1";

        private readonly Func<string, IFileSystem> _fileSystemFactory;
        private readonly IConfigurationProvider _configuration;
        private readonly IScriptService _script;
        private readonly ISitesService _sites;
        private readonly ISharedSettingsService _sharedSettings;

        public NuGetDeployService(IConfigurationProvider configuration, ISharedSettingsService sharedSettings, ISitesService sites, IScriptService script)
            : this(siteID => new PhysicalFileSystem(Path.Combine(configuration.PackagesRootPath, siteID)), configuration, sharedSettings, sites, script)
        {
        }

        internal NuGetDeployService(Func<string, IFileSystem> fileSystemFactory, IConfigurationProvider configuration, ISharedSettingsService sharedSettings, ISitesService sites, IScriptService script)
        {
            _fileSystemFactory = fileSystemFactory;
            _configuration = configuration;
            _sharedSettings = sharedSettings;
            _sites = sites;
            _script = script;
        }

        public Models.Installation DeployPackage(string siteID, Guid installationID, Stream packageStream)
        {
            var package = new ZipPackage(packageStream);

            return DeployPackage(siteID, installationID, package);
        }

        internal Models.Installation DeployPackage(string siteID, Guid installationID, NuGet.IPackage package)
        {
            var packageInfo = new Models.PackageInfo { ID = package.Id, Version = package.Version.ToString(), Name = package.Title };
            var installation = _sites.CreateInstallation(siteID, packageInfo, installationID);

            LogInstallationProgress(installation, Models.InstallationState.Pending, 1, Models.LogSeverity.Info, "Deployment started");

            try
            {
                var packageManager = CreatePackageManager(siteID);

                RemovePackage(installation, packageManager, package);

                TransformFiles(installation, packageManager, package);

                ProcessManifest(installation, packageManager, package);

                LogInstallationProgress(installation, Models.InstallationState.Success, 1, Models.LogSeverity.Info, "Deployment finished successfully");
            }
            catch (DeployScriptException ex)
            {
                LogInstallationProgress(installation, Models.InstallationState.Failure, 1, Models.LogSeverity.Error, "Deployment failed: " + ex.Message);
            }
            catch (Exception ex)
            {
                LogInstallationProgress(installation, Models.InstallationState.Failure, 1, Models.LogSeverity.Error, "Deployment failed: " + ex.ToString());
            }

            return installation;
        }

        private void RemovePackage(Models.Installation currentDeployment, IPackageManager manager, IPackage package)
        {
            LogInstallationProgress(currentDeployment, Models.InstallationState.Pending, 1, Models.LogSeverity.Info, "Removing old package files");

            // packageManager.UninstallPackage(package) cannot be used because it does not delete modified and new files
            var directory = manager.PathResolver.GetPackageDirectory(package);
            foreach (var file in manager.FileSystem.GetFiles(directory, "", true))
            {
                manager.FileSystem.DeleteFile(file);
            }
            manager.FileSystem.DeleteDirectory(directory, true);

            LogInstallationProgress(currentDeployment, Models.InstallationState.Pending, 2, Models.LogSeverity.Debug, "Old package files removed");
        }

        private void UnpackPackage(Models.Installation currentDeployment, IPackageManager manager, IPackage package)
        {
            LogInstallationProgress(currentDeployment, Models.InstallationState.Pending, 1, Models.LogSeverity.Info, "Unpacking package files");

            manager.InstallPackage(package, false, true);

            LogInstallationProgress(currentDeployment, Models.InstallationState.Pending, 2, Models.LogSeverity.Debug, "Package files unpacked");
        }

        private void TransformFiles(Models.Installation currentDeployment, IPackageManager manager, IPackage package)
        {
            LogInstallationProgress(currentDeployment, Models.InstallationState.Pending, 1, Models.LogSeverity.Info, "Tranforming configuration files");

            var projectSystem = CreateSiteProjectSystem(currentDeployment, manager.FileSystem, currentDeployment.SiteID);
            var processor = new NuGet.Preprocessor();

            var packageDirectory = manager.PathResolver.GetPackageDirectory(package);
            var files = package.GetFiles().Where(f => f.Path.EndsWith(TransformFilesExtension));
            foreach (var item in files)
            {
                string targetPath = Path.Combine(packageDirectory, item.Path.Replace(TransformFilesExtension, string.Empty));
                LogInstallationProgress(currentDeployment, Models.InstallationState.Pending, 2, Models.LogSeverity.Trace, string.Format("Transforming file {0}. Target path: {1}", item.Path, targetPath));
                processor.TransformFile(item, targetPath, projectSystem);
            }

            LogInstallationProgress(currentDeployment, Models.InstallationState.Pending, 2, Models.LogSeverity.Debug, "Configuration files transformed");
        }

        private void ProcessManifest(Models.Installation currentDeployment, IPackageManager manager, IPackage package)
        {
            LogInstallationProgress(currentDeployment, Models.InstallationState.Pending, 1, Models.LogSeverity.Info, "Processing deploy manifest");

            var packageDirectory = manager.PathResolver.GetInstallPath(package);
            var manifestPath = manager.FileSystem.GetFiles(packageDirectory, ManifestFileName, true).FirstOrDefault();

            if (manifestPath == null)
            {
                LogInstallationProgress(currentDeployment, Models.InstallationState.Pending, 2, Models.LogSeverity.Warning, string.Format("No deploy manifest {0} found", ManifestFileName));
                return;
            }

            var manifestFullPath = manager.FileSystem.GetFullPath(manifestPath);

            LogInstallationProgress(currentDeployment, Models.InstallationState.Pending, 2, Models.LogSeverity.Debug, "Deploy manifest found: " + manifestFullPath);

            var deployScriptFullPath = GetDeployScriptFullPath(currentDeployment);
            if (deployScriptFullPath == null)
            {
                LogInstallationProgress(currentDeployment, Models.InstallationState.Pending, 2, Models.LogSeverity.Warning, "No deploy script found");
                return;
            }

            LogInstallationProgress(currentDeployment, Models.InstallationState.Pending, 2, Models.LogSeverity.Debug, "Running deploy script " + deployScriptFullPath);

            string output;
            string errorOutput;
            var returnValue = _script.Run(packageDirectory, deployScriptFullPath, manifestFullPath, out output, out errorOutput);

            if (returnValue == 0)
            {
                LogInstallationProgress(currentDeployment, Models.InstallationState.Pending, 3, Models.LogSeverity.Debug, "Deploy script finished successfully");
                LogInstallationProgress(currentDeployment, Models.InstallationState.Pending, 4, Models.LogSeverity.Trace, output);
            }
            else
            {
                var outputMessage = output + Environment.NewLine + errorOutput;
                LogInstallationProgress(currentDeployment, Models.InstallationState.Failure, 3, Models.LogSeverity.Error, "Error while running deploy script");
                LogInstallationProgress(currentDeployment, Models.InstallationState.Failure, 4, Models.LogSeverity.Error, outputMessage);
                throw new DeployScriptException(outputMessage);
            }
        }

        private IProjectSystem CreateSiteProjectSystem(Models.Installation currentDeployment, IFileSystem fileSystem, string siteID)
        {
            LogInstallationProgress(currentDeployment, Models.InstallationState.Pending, 2, Models.LogSeverity.Debug, "Loading parameters for site " + siteID);
            var siteParameters = _sites.GetSiteParameters(siteID);

            LogInstallationProgress(currentDeployment, Models.InstallationState.Pending, 2, Models.LogSeverity.Debug, "Loading shared parameters");
            var sharedParameters = _sharedSettings.GetSharedParameters();

            return new NuGetHelpers.SiteProjectSystem(fileSystem, siteParameters, sharedParameters);
        }

        private PackageManager CreatePackageManager(string siteID)
        {
            var fileSystem = _fileSystemFactory(siteID);

            // TODO instead of NullPackageRepository, use repository on top of SharedSite - this enables packages to depend for example on deploy script versions
            return new PackageManager(new NullPackageRepository(), new DefaultPackagePathResolver(fileSystem), fileSystem);
        }

        private string GetDeployScriptFullPath(Models.Installation currentDeployment)
        {
            LogInstallationProgress(currentDeployment, Models.InstallationState.Pending, 2, Models.LogSeverity.Debug, "Looking for deploy script");

            var manager = CreatePackageManager(SharedSite);

            var package = manager.LocalRepository.GetPackages().OrderByDescending(x => x.Version).FirstOrDefault(p => p.Id == ScriptPackageId);
            if (package == null)
            {
                LogInstallationProgress(currentDeployment, Models.InstallationState.Pending, 3, Models.LogSeverity.Warning, string.Format("No package with deploy scripts {0} found in shared site {1}", ScriptPackageId, SharedSite));
                return null;
            }

            var script = package.GetFiles().FirstOrDefault(f => f.EffectivePath == DeployScriptFileName);
            if (script == null)
            {
                LogInstallationProgress(currentDeployment, Models.InstallationState.Pending, 3, Models.LogSeverity.Warning, string.Format("Package with deploy scripts {0} in shared site {1} does not contain deploy script {2}", ScriptPackageId, SharedSite, DeployScriptFileName));
                return null;
            }

            LogInstallationProgress(currentDeployment, Models.InstallationState.Pending, 3, Models.LogSeverity.Debug, string.Format("Deploy script {2} found in site {1}, package {0}", ScriptPackageId, SharedSite, script.Path));

            var packageRootPath = manager.PathResolver.GetInstallPath(package);
            return Path.Combine(packageRootPath, script.Path);
        }

        private void LogInstallationProgress(Models.Installation deployment, Models.InstallationState state, int logLevel, Models.LogSeverity severity, string message)
        {
            //// TODO publish event to notify other loggers (i.e., SignalR real time logger)

            //// TODO log to standard log?

            _sites.LogInstallationProgress(deployment.SiteID, deployment.PackageID, deployment.ID, state, logLevel, severity, message);
        }
    }
}