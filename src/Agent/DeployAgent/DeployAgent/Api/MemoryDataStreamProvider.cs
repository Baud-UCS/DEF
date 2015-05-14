using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.DeployAgent.Api
{
    public class MemoryDataStreamProvider : MultipartStreamProvider
    {
        private Stream _stream;

        public MemoryDataStreamProvider(Stream stream)
        {
            _stream = stream;
        }

        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            return _stream;
        }
    }
}