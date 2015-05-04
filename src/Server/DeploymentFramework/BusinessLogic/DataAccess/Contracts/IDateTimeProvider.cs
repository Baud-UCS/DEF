using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.BusinessLogic.DataAccess.Contracts
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
        DateTimeOffset NowOffset { get; }
        DateTimeOffset UtcNowOffset { get; }
    }
}