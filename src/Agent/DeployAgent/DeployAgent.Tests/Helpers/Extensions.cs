using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Baud.Deployment.DeployAgent.Tests
{
    public static class Extensions
    {
        public static async Task<T> ReadAsJson<T>(this HttpContent content)
        {
            var serialized = await content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}