using Company.DAL.Data.Contexts;
using Company.DAL.Models;
using Compnay.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compnay.BLL.Repositories
{
    public class DepartmentReopsitory : IDepartmentReopsitory
    {
        private readonly CompanyDbContext _context; // NULL

        // ASk CLR Create Object From CompanyDbContext

        public DepartmentReopsitory(CompanyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.ToList();
        }


        public Department? Get(int id)
        {
            return _context.Departments.FirstOrDefault(x => x.Id == id);
        }

        public int Add(Department model)
        {
            _context.Departments.Add(model);
            return _context.SaveChanges();
        }

        public int Update(Department model)
        {
            _context.Departments.Update(model);
            return _context.SaveChanges();
        }

        public int Delete(Department model)
        {
            _context.Departments.Remove(model);
            return _context.SaveChanges();
        }

     
      
    }
}
