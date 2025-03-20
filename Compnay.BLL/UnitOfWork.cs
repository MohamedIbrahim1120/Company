using Company.DAL.Data.Contexts;
using Compnay.BLL.Interfaces;
using Compnay.BLL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compnay.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyDbContext _context;

        public IDepartmentReopsitory DepartmentReopsitory { get; } // Null

        public IEmployeeReopsitory EmployeeReopsitory { get; } // Null

        public UnitOfWork(CompanyDbContext context)
        {
            _context = context;

            DepartmentReopsitory = new DepartmentReopsitory(_context);
            EmployeeReopsitory = new EmployeeReopsitory(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
