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

        public Models.Deployment DeployPackage(string siteID, Guid deploymentID, Stream packageStream)
        {
            var package = new ZipPackage(packageStream);

            return DeployPackage(siteID, deploymentID, package);
        }

        internal Models.Deployment DeployPackage(string siteID, Guid deploymentID, NuGet.IPackage package)
        {
            var packageInfo = new Models.PackageInfo { ID = package.Id, Version = package.Version.ToString(), Name = package.Title };
            var deployment = _sites.CreateDeployment(siteID, packageInfo, deploymentID);

            LogDeploymentProgress(deployment, Models.DeploymentState.Pending, 1, Models.LogSeverity.Info, "Deployment started");

            try
            {
                var packageManager = CreatePackageManager(siteID);

                RemovePackage(deployment, packageManager, package);

                TransformFiles(deployment, packageManager, package);

                ProcessManifest(deployment, packageManager, package);

                LogDeploymentProgress(deployment, Models.DeploymentState.Success, 1, Models.LogSeverity.Info, "Deployment finished successfully");
            }
            catch (DeployScriptException ex)
            {
                LogDeploymentProgress(deployment, Models.DeploymentState.Failure, 1, Models.LogSeverity.Error, "Deployment failed: " + ex.Message);
            }
            catch (Exception ex)
            {
                LogDeploymentProgress(deployment, Models.DeploymentState.Failure, 1, Models.LogSeverity.Error, "Deployment failed: " + ex.ToString());
            }

            return deployment;
        }

        private void RemovePackage(Models.Deployment currentDeployment, IPackageManager manager, IPackage package)
        {
            LogDeploymentProgress(currentDeployment, Models.DeploymentState.Pending, 1, Models.LogSeverity.Info, "Removing old package files");

            // packageManager.UninstallPackage(package) cannot be used because it does not delete modified and new files
            var directory = manager.PathResolver.GetPackageDirectory(package);
            foreach (var file in manager.FileSystem.GetFiles(directory, "", true))
            {
                manager.FileSystem.DeleteFile(file);
            }
            manager.FileSystem.DeleteDirectory(directory, true);

            LogDeploymentProgress(currentDeployment, Models.DeploymentState.Pending, 2, Models.LogSeverity.Debug, "Old package files removed");
        }

        private void UnpackPackage(Models.Deployment currentDeployment, IPackageManager manager, IPackage package)
        {
            LogDeploymentProgress(currentDeployment, Models.DeploymentState.Pending, 1, Models.LogSeverity.Info, "Unpacking package files");

            manager.InstallPackage(package, false, true);

            LogDeploymentProgress(currentDeployment, Models.DeploymentState.Pending, 2, Models.LogSeverity.Debug, "Package files unpacked");
        }

        private void TransformFiles(Models.Deployment currentDeployment, IPackageManager manager, IPackage package)
        {
            LogDeploymentProgress(currentDeployment, Models.DeploymentState.Pending, 1, Models.LogSeverity.Info, "Tranforming configuration files");

            var projectSystem = CreateSiteProjectSystem(currentDeployment, manager.FileSystem, currentDeployment.SiteID);
            var processor = new NuGet.Preprocessor();

            var packageDirectory = manager.PathResolver.GetPackageDirectory(package);
            var files = package.GetFiles().Where(f => f.Path.EndsWith(TransformFilesExtension));
            foreach (var item in files)
            {
                string targetPath = Path.Combine(packageDirectory, item.Path.Replace(TransformFilesExtension, string.Empty));
                LogDeploymentProgress(currentDeployment, Models.DeploymentState.Pending, 2, Models.LogSeverity.Trace, string.Format("Transforming file {0}. Target path: {1}", item.Path, targetPath));
                processor.TransformFile(item, targetPath, projectSystem);
            }

            LogDeploymentProgress(currentDeployment, Models.DeploymentState.Pending, 2, Models.LogSeverity.Debug, "Configuration files transformed");
        }

        private void ProcessManifest(Models.Deployment currentDeployment, IPackageManager manager, IPackage package)
        {
            LogDeploymentProgress(currentDeployment, Models.DeploymentState.Pending, 1, Models.LogSeverity.Info, "Processing deploy manifest");

            var packageDirectory = manager.PathResolver.GetInstallPath(package);
            var manifestPath = manager.FileSystem.GetFiles(packageDirectory, ManifestFileName, true).FirstOrDefault();

            if (manifestPath == null)
            {
                LogDeploymentProgress(currentDeployment, Models.DeploymentState.Pending, 2, Models.LogSeverity.Warning, string.Format("No deploy manifest {0} found", ManifestFileName));
                return;
            }

            var manifestFullPath = manager.FileSystem.GetFullPath(manifestPath);

            LogDeploymentProgress(currentDeployment, Models.DeploymentState.Pending, 2, Models.LogSeverity.Debug, "Deploy manifest found: " + manifestFullPath);

            var deployScriptFullPath = GetDeployScriptFullPath(currentDeployment);
            if (deployScriptFullPath == null)
            {
                LogDeploymentProgress(currentDeployment, Models.DeploymentState.Pending, 2, Models.LogSeverity.Warning, "No deploy script found");
                return;
            }

            LogDeploymentProgress(currentDeployment, Models.DeploymentState.Pending, 2, Models.LogSeverity.Debug, "Running deploy script " + deployScriptFullPath);

            string output;
            string errorOutput;
            var returnValue = _script.Run(packageDirectory, deployScriptFullPath, manifestFullPath, out output, out errorOutput);

            if (returnValue == 0)
            {
                LogDeploymentProgress(currentDeployment, Models.DeploymentState.Pending, 3, Models.LogSeverity.Debug, "Deploy script finished successfully");
                LogDeploymentProgress(currentDeployment, Models.DeploymentState.Pending, 4, Models.LogSeverity.Trace, output);
            }
            else
            {
                var outputMessage = output + Environment.NewLine + errorOutput;
                LogDeploymentProgress(currentDeployment, Models.DeploymentState.Failure, 3, Models.LogSeverity.Error, "Error while running deploy script");
                LogDeploymentProgress(currentDeployment, Models.DeploymentState.Failure, 4, Models.LogSeverity.Error, outputMessage);
                throw new DeployScriptException(outputMessage);
            }
        }

        private IProjectSystem CreateSiteProjectSystem(Models.Deployment currentDeployment, IFileSystem fileSystem, string siteID)
        {
            LogDeploymentProgress(currentDeployment, Models.DeploymentState.Pending, 2, Models.LogSeverity.Debug, "Loading parameters for site " + siteID);
            var siteParameters = _sites.GetSiteParameters(siteID);

            LogDeploymentProgress(currentDeployment, Models.DeploymentState.Pending, 2, Models.LogSeverity.Debug, "Loading shared parameters");
            var sharedParameters = _sharedSettings.GetSharedParameters();

            return new NuGetHelpers.SiteProjectSystem(fileSystem, siteParameters, sharedParameters);
        }

        private PackageManager CreatePackageManager(string siteID)
        {
            var fileSystem = _fileSystemFactory(siteID);

            // TODO instead of NullPackageRepository, use repository on top of SharedSite - this enables packages to depend for example on deploy script versions
            return new PackageManager(new NullPackageRepository(), new DefaultPackagePathResolver(fileSystem), fileSystem);
        }

        private string GetDeployScriptFullPath(Models.Deployment currentDeployment)
        {
            LogDeploymentProgress(currentDeployment, Models.DeploymentState.Pending, 2, Models.LogSeverity.Debug, "Looking for deploy script");

            var manager = CreatePackageManager(SharedSite);

            var package = manager.LocalRepository.GetPackages().OrderByDescending(x => x.Version).FirstOrDefault(p => p.Id == ScriptPackageId);
            if (package == null)
            {
                LogDeploymentProgress(currentDeployment, Models.DeploymentState.Pending, 3, Models.LogSeverity.Warning, string.Format("No package with deploy scripts {0} found in shared site {1}", ScriptPackageId, SharedSite));
                return null;
            }

            var script = package.GetFiles().FirstOrDefault(f => f.EffectivePath == DeployScriptFileName);
            if (script == null)
            {
                LogDeploymentProgress(currentDeployment, Models.DeploymentState.Pending, 3, Models.LogSeverity.Warning, string.Format("Package with deploy scripts {0} in shared site {1} does not contain deploy script {2}", ScriptPackageId, SharedSite, DeployScriptFileName));
                return null;
            }

            LogDeploymentProgress(currentDeployment, Models.DeploymentState.Pending, 3, Models.LogSeverity.Debug, string.Format("Deploy script {2} found in site {1}, package {0}", ScriptPackageId, SharedSite, script.Path));

            var packageRootPath = manager.PathResolver.GetInstallPath(package);
            return Path.Combine(packageRootPath, script.Path);
        }

        private void LogDeploymentProgress(Models.Deployment deployment, Models.DeploymentState state, int logLevel, Models.LogSeverity severity, string message)
        {
            //// TODO publish event to notify other loggers (i.e., SignalR real time logger)

            //// TODO log to standard log?

            _sites.LogDeploymentProgress(deployment.SiteID, deployment.PackageID, deployment.ID, state, logLevel, severity, message);
        }
    }
}