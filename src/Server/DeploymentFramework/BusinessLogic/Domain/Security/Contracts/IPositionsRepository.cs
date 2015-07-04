using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Domain.Security.Entities;

namespace Baud.Deployment.BusinessLogic.Domain.Security.Contracts
{
    public interface IPositionsRepository
    {
        IQueryable<Position> GetPositions();

        Position GetPositionDetail(short id);

        void UpdatePosition(short id, Position position);
    }
}
