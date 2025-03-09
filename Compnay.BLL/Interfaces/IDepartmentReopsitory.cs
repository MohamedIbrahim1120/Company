using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compnay.BLL.Interfaces
{
    public interface IDepartmentReopsitory
    {
        IEnumerable<Department> GetAll();

        Department? Get(int id);

        int Add(Department model);

        int Update(Department model);

        int Delete(Department model);


    }
}
