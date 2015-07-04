using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Domain.Security.Contracts;
using Baud.Deployment.BusinessLogic.Domain.Security.Entities;
using Baud.Deployment.BusinessLogic.Domain.Security.Queries;

namespace Baud.Deployment.Database.Security
{
    public class PositionsRepository : RepositoryBase<SecurityDbContext>, IPositionsRepository
    {
        public PositionsRepository(SecurityDbContext context)
            : base(context)
        {
        }

        public IQueryable<Position> GetPositions()
        {
            return Context.Positions;
        }

        public Position GetPositionDetail(short id)
        {
            return Context.Positions.FilterByID(id).FirstOrDefault();
        }

        public void UpdatePosition(short id, Position position)
        {
            position.ID = id;

            Context.AttachAsModified(position,
                x => x.Name,
                x => x.IsActive);
        }
    }
}
