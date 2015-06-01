using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Contracts;

namespace Baud.Deployment.BusinessLogic.Providers
{
    public class RealDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }

        public DateTime UtcNow
        {
            get { return DateTime.UtcNow; }
        }

        public DateTimeOffset NowOffset
        {
            get { return DateTimeOffset.Now; }
        }

        public DateTimeOffset UtcNowOffset
        {
            get { return DateTimeOffset.UtcNow; }
        }
    }
}