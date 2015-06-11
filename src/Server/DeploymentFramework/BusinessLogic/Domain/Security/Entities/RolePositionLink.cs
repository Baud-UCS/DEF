using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.BusinessLogic.Domain.Security.Entities
{
    public class RolePositionLink : EntityBase
    {
        public short RoleID { get; set; }
        public short PositionID { get; set; }

        public Role Role { get; set; }
        public Position Position { get; set; }
    }
}