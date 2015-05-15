using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NuGet;

namespace Baud.Deployment.DeployLogic.NuGetHelpers
{
    public class SiteProjectSystem : IProjectSystem
    {
        private IFileSystem _fileSystem;
        private IReadOnlyDictionary<string, string> _siteParameters;
        private IReadOnlyDictionary<string, string> _sharedParameters;

        public SiteProjectSystem(IFileSystem fileSystem, IReadOnlyDictionary<string, string> siteParameters, IReadOnlyDictionary<string, string> sharedParameters)
        {
            _fileSystem = fileSystem;
            _siteParameters = siteParameters;
            _sharedParameters = sharedParameters;
        }

        public dynamic GetPropertyValue(string propertyName)
        {
            string propertyValue;

            if (!_siteParameters.TryGetValue(propertyName, out propertyValue) && !_sharedParameters.TryGetValue(propertyName, out propertyValue))
            {
                propertyValue = string.Concat("!KeyMissing:", propertyName, "!");
            }

            return propertyValue;
        }

        #region IFileSystem implementations

        public string Root
        {
            get { return _fileSystem.Root; }
        }
        public NuGet.ILogger Logger
        {
            get
            {
                return _fileSystem.Logger;
            }
            set
            {
                _fileSystem.Logger = value;
            }
        }

        public bool FileExistsInProject(string path)
        {
            return _fileSystem.FileExists(path);
        }

        public void AddFile(string path, Action<System.IO.Stream> writeToStream)
        {
            _fileSystem.AddFile(path, writeToStream);
        }

        public void AddFile(string path, System.IO.Stream stream)
        {
            _fileSystem.AddFile(path, stream);
        }

        public void AddFiles(IEnumerable<NuGet.IPackageFile> files, string rootDir)
        {
            _fileSystem.AddFiles(files, rootDir);
        }

        public System.IO.Stream CreateFile(string path)
        {
            return _fileSystem.CreateFile(path);
        }

        public void DeleteDirectory(string path, bool recursive)
        {
            _fileSystem.DeleteDirectory(path, recursive);
        }

        public void DeleteFile(string path)
        {
            _fileSystem.DeleteFile(path);
        }

        public void DeleteFiles(IEnumerable<NuGet.IPackageFile> files, string rootDir)
        {
            _fileSystem.DeleteFiles(files, rootDir);
        }

        public bool DirectoryExists(string path)
        {
            return _fileSystem.DirectoryExists(path);
        }

        public bool FileExists(string path)
        {
            return _fileSystem.FileExists(path);
        }

        public DateTimeOffset GetCreated(string path)
        {
            return _fileSystem.GetCreated(path);
        }

        public IEnumerable<string> GetDirectories(string path)
        {
            return _fileSystem.GetDirectories(path);
        }

        public IEnumerable<string> GetFiles(string path, string filter, bool recursive)
        {
            return _fileSystem.GetFiles(path, filter, recursive);
        }

        public string GetFullPath(string path)
        {
            return _fileSystem.GetFullPath(path);
        }

        public DateTimeOffset GetLastAccessed(string path)
        {
            return _fileSystem.GetLastAccessed(path);
        }

        public DateTimeOffset GetLastModified(string path)
        {
            return _fileSystem.GetLastModified(path);
        }

        public void MakeFileWritable(string path)
        {
            _fileSystem.MakeFileWritable(path);
        }

        public void MoveFile(string source, string destination)
        {
            _fileSystem.MoveFile(source, destination);
        }

        public System.IO.Stream OpenFile(string path)
        {
            return _fileSystem.OpenFile(path);
        }

        #endregion IFileSystem implementations

        #region Not supported functions

        public string ProjectName
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsBindingRedirectSupported
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public System.Runtime.Versioning.FrameworkName TargetFramework
        {
            get { throw new NotSupportedException(); }
        }

        public void AddFrameworkReference(string name)
        {
            throw new NotSupportedException();
        }

        public void AddImport(string targetFullPath, NuGet.ProjectImportLocation location)
        {
            throw new NotSupportedException();
        }

        public void AddReference(string referencePath, System.IO.Stream stream)
        {
            throw new NotSupportedException();
        }

        public bool IsSupportedFile(string path)
        {
            throw new NotSupportedException();
        }

        public bool ReferenceExists(string name)
        {
            throw new NotSupportedException();
        }

        public void RemoveImport(string targetFullPath)
        {
            throw new NotSupportedException();
        }

        public void RemoveReference(string name)
        {
            throw new NotSupportedException();
        }

        public string ResolvePath(string path)
        {
            throw new NotSupportedException();
        }

        #endregion Not supported functions
    }
}