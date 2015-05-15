using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.DeployLogic.Contracts;
using Biggy.Core;
using Biggy.Data.Json;
using Biggy.Extensions;

namespace Baud.Deployment.DeployLogic
{
    // because of possible concurrency, the service should be used as singleton
    public class BiggySharedSettingsService : ISharedSettingsService
    {
        private readonly BiggyList<Models.SharedConfiguration> _configurationList;

        public BiggySharedSettingsService()
            : this(new JsonDbCore())
        {
        }

        internal BiggySharedSettingsService(string dbDirectory, string dbName)
            : this(new JsonDbCore(dbDirectory, dbName))
        {
        }

        private BiggySharedSettingsService(JsonDbCore dbCore)
        {
            var store = new JsonStore<Models.SharedConfiguration>(dbCore);
            _configurationList = new BiggyList<Models.SharedConfiguration>(store);
        }

        public IReadOnlyDictionary<string, string> GetSharedParameters()
        {
            var configuration = GetCurrentConfiguration();

            if (configuration != null)
            {
                return new ReadOnlyDictionary<string, string>(configuration.SharedParameters);
            }
            else
            {
                return new ReadOnlyDictionary<string, string>(new Dictionary<string, string>(0));
            }
        }

        public void SetSharedParameter(string key, string value)
        {
            var configuration = GetCurrentConfiguration() ?? CreateNewConfiguration();

            configuration.SharedParameters[key] = value;
            SaveConfiguration(configuration);
        }

        public void RemoveSharedParameter(string key)
        {
            var configuration = GetCurrentConfiguration() ?? CreateNewConfiguration();

            configuration.SharedParameters.Remove(key);
            SaveConfiguration(configuration);
        }

        private Models.SharedConfiguration GetCurrentConfiguration()
        {
            return _configurationList.FirstOrDefault(x => x.IsDefault);
        }

        private Models.SharedConfiguration CreateNewConfiguration()
        {
            // TODO lock
            foreach (var existingConfiguration in _configurationList.Where(x => x.IsDefault))
            {
                existingConfiguration.IsDefault = false;
                SaveConfiguration(existingConfiguration);
            }

            var newConfiguration = new Models.SharedConfiguration { IsDefault = true };
            _configurationList.Add(newConfiguration);

            return newConfiguration;
        }

        private void SaveConfiguration(Models.SharedConfiguration configuration)
        {
            _configurationList.Store.Update(configuration);
        }
    }
}