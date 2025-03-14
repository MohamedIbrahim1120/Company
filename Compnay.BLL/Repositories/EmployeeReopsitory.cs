﻿using Company.DAL.Data.Contexts;
using Company.DAL.Models;
using Compnay.BLL.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compnay.BLL.Repositories
{
    public class EmployeeReopsitory : IEmployeeReopsitory
    {
        private readonly CompanyDbContext _context;

        public EmployeeReopsitory(CompanyDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        public Employee? Get(int id)
        {
            return _context.Employees.Find(id);
        }

        public int Add(Employee model)
        {
            _context.Employees.Add(model);
            return _context.SaveChanges();
        }

        public int Update(Employee model)
        {
            _context.Employees.Update(model);
            return _context.SaveChanges();
        }

        public int Delete(Employee model)
        {
            _context.Employees.Remove(model);
            return _context.SaveChanges();
        }
   
       
    }
}
