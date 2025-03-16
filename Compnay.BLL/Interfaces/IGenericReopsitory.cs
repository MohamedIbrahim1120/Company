using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compnay.BLL.Interfaces
{
    public interface IGenericReopsitory<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();

        T? Get(int id);

        int Add(T model);

        int Update(T model);

        int Delete(T model);
    }
}
