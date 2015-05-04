using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.BusinessLogic.DataAccess
{
    public abstract class RepositoryBase<TDbContext>
    {
        protected TDbContext Context { get; set; }

        public RepositoryBase(TDbContext context)
        {
            Context = context;
        }
    }
}