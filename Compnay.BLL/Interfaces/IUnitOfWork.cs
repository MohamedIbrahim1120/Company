using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compnay.BLL.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IDepartmentReopsitory DepartmentReopsitory { get;  }

        public IEmployeeReopsitory EmployeeReopsitory { get; }

        Task<int> CompleteAsync();



    }
}
