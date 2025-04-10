﻿using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compnay.BLL.Interfaces
{
    public interface IEmployeeReopsitory : IGenericReopsitory<Employee>
    {
        //IEnumerable<Employee> GetAll();

        //Employee? Get(int id);

        //int Add(Employee model);

        //int Update(Employee model);

        //int Delete(Employee model);

        Task<List<Employee>> GetByNameAsync(string name);

    }
}
