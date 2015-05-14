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

        public NuGetDeployService(IConfigurationProvider configuration, ISitesService sites, IScriptService script)
            : this(siteID => new PhysicalFileSystem(Path.Combine(configuration.PackagesRootPath, siteID)), configuration, sites, script)
        {
        }

        internal NuGetDeployService(Func<string, IFileSystem> fileSystemFactory, IConfigurationProvider configuration, ISitesService sites, IScriptService script)
        {
            _fileSystemFactory = fileSystemFactory;
            _configuration = configuration;
            _sites = sites;
            _script = script;
        }

        public void DeployPackage(string siteID, Stream packageStream)
        {
            var package = new ZipPackage(packageStream);

            DeployPackage(siteID, package);
        }

        internal void DeployPackage(string siteID, NuGet.IPackage package)
        {
            var packageManager = CreatePackageManager(siteID);

            RemovePackage(packageManager, package);
            packageManager.InstallPackage(package, false, true);

            TransformFiles(packageManager, siteID, package);

            ProcessManifest(packageManager, package);
        }

        private void RemovePackage(IPackageManager manager, IPackage package)
        {
            // packageManager.UninstallPackage(package) cannot be used because it does not delete modified and new files

            var directory = manager.PathResolver.GetPackageDirectory(package);
            foreach (var file in manager.FileSystem.GetFiles(directory, "", true))
            {
                manager.FileSystem.DeleteFile(file);
            }
            manager.FileSystem.DeleteDirectory(directory, true);
        }

        private void TransformFiles(IPackageManager manager, string siteID, IPackage package)
        {
            var processor = new NuGet.Preprocessor();
            var projectSystem = CreateSiteProjectSystem(manager.FileSystem, siteID);

            var packageDirectory = manager.PathResolver.GetPackageDirectory(package);
            var files = package.GetFiles().Where(f => f.Path.EndsWith(TransformFilesExtension));
            foreach (var item in files)
            {
                string targetPath = Path.Combine(packageDirectory, item.Path.Replace(TransformFilesExtension, string.Empty));
                processor.TransformFile(item, targetPath, projectSystem);
            }
        }

        private void ProcessManifest(IPackageManager manager, IPackage package)
        {
            var contentDirectory = Path.Combine(manager.PathResolver.GetPackageDirectory(package), ContentDirectoryName);
            var manifestPath = Path.Combine(contentDirectory, ManifestFileName);
            if (!manager.FileSystem.FileExists(manifestPath))
            {
                // no deploy manifest found
                return;
            }

            var deployScriptFullPath = GetDeployScriptFullPath();
            if (deployScriptFullPath == null)
            {
                // no deploy script found
                return;
            }

            string output;
            string errorOutput;
            _script.Run(deployScriptFullPath, manifestPath, out output, out errorOutput);
        }

        private IProjectSystem CreateSiteProjectSystem(IFileSystem fileSystem, string siteID)
        {
            var siteParameters = _sites.GetSiteParameters(siteID);
            var sharedParameters = _sites.GetSharedParameters();

            return new NuGetHelpers.SiteProjectSystem(fileSystem, siteParameters, sharedParameters);
        }

        private PackageManager CreatePackageManager(string siteID)
        {
            var fileSystem = _fileSystemFactory(siteID);

            // TODO instead of NullPackageRepository, use repository on top of SharedSite - this enables packages to depend for example on deploy script versions
            return new PackageManager(new NullPackageRepository(), new DefaultPackagePathResolver(fileSystem), fileSystem);
        }

        private string GetDeployScriptFullPath()
        {
            var manager = CreatePackageManager(SharedSite);

            var package = manager.LocalRepository.GetPackages().OrderByDescending(x => x.Version).FirstOrDefault(p => p.Id == ScriptPackageId);
            if (package == null)
            {
                return null;
            }

            var script = package.GetFiles().SingleOrDefault(f => f.EffectivePath == DeployScriptFileName);
            if (script == null)
            {
                return null;
            }

            var packageRootPath = manager.PathResolver.GetInstallPath(package);
            return Path.Combine(packageRootPath, script.Path);
        }
    }
}