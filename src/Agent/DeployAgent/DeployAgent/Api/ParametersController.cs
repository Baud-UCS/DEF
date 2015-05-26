using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Baud.Deployment.DeployLogic;
using Baud.Deployment.DeployLogic.Contracts;
using Newtonsoft.Json.Linq;

namespace Baud.Deployment.DeployAgent.Api
{
    [RoutePrefix("api/parameters")]
    public class ParametersController : ApiController
    {
        private readonly ISharedSettingsService _settingsService;

        // TODO inject through dependecy injection
        public ParametersController()
        {
            _settingsService = new BiggySharedSettingsService();
        }

        public ParametersController(ISharedSettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [Route("")]
        public IReadOnlyDictionary<string, string> Get()
        {
            return _settingsService.GetSharedParameters();
        }

        [HttpGet]
        [Route("{key}")]
        public string GetSingle(string key)
        {
            string value;
            if (_settingsService.GetSharedParameters().TryGetValue(key, out value))
            {
                return value;
            }
            else
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }
        }

        [Route("")]
        public IHttpActionResult Post(IDictionary<string, string> values)
        {
            foreach (var item in values)
            {
                _settingsService.SetSharedParameter(item.Key, item.Value);
            }

            return Ok();
        }

        [Route("{key}")]
        public IHttpActionResult Put(string key, [FromBody]string value)
        {
            _settingsService.SetSharedParameter(key, value);

            return Ok();
        }

        [Route("{key}")]
        public IHttpActionResult Delete(string key)
        {
            _settingsService.RemoveSharedParameter(key);

            return Ok();
        }
    }
}