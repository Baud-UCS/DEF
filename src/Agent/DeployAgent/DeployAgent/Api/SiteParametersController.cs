using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Baud.Deployment.DeployLogic;
using Baud.Deployment.DeployLogic.Contracts;

namespace Baud.Deployment.DeployAgent.Api
{
    [RoutePrefix("api/sites/{site}/parameters")]
    public class SiteParametersController : ApiController
    {
        private readonly ISitesService _sites;

        // TODO inject through dependecy injection
        public SiteParametersController()
        {
            _sites = new BiggySitesService();
        }

        public SiteParametersController(ISitesService sites)
        {
            _sites = sites;
        }

        [Route("")]
        public IReadOnlyDictionary<string, string> Get(string site)
        {
            try
            {
                var parameters = _sites.GetSiteParameters(site);
                return parameters;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("{key}")]
        public string GetSingle(string site, string key)
        {
            try
            {
                var parameters = _sites.GetSiteParameters(site);

                string value;
                if (parameters.TryGetValue(key, out value))
                {
                    return value;
                }
                else
                {
                    throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }
        }

        [Route("")]
        public IHttpActionResult Post(string site, IDictionary<string, string> values)
        {
            foreach (var item in values)
            {
                _sites.SetSiteParameter(site, item.Key, item.Value);
            }

            return Ok();
        }

        [Route("{key}")]
        public IHttpActionResult Put(string site, string key, [FromBody]string value)
        {
            _sites.SetSiteParameter(site, key, value);

            return Ok();
        }

        [Route("{key}")]
        public IHttpActionResult Delete(string site, string key)
        {
            _sites.RemoveSiteParameter(site, key);

            return Ok();
        }
    }
}