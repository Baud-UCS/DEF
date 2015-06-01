using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.Database.Contracts
{
    public interface IRepositoryProvider<TDbContext>
    {
        TDbContext DbContext { get; set; }

        T GetRepository<T>();
    }
}