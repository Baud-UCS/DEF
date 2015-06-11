using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.BusinessLogic.Domain.Deployment.Enums
{
    public enum InstallationState : byte
    {
        Waiting,
        Pending,
        Failure,
        Success
    }
}