using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.BusinessLogic.Domain.Security.Entities
{
    public class UserPositionLink : EntityBase
    {
        public short UserID { get; set; }
        public short PositionID { get; set; }

        public Position Position { get; set; }
        public User User { get; set; }
    }
}