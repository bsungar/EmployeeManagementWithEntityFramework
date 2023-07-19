using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace MVVM.Models
{
    public class EmployeeServices
    {
        public EmployeeServices()
        {
        }

        public List<Employee> GetAll()
        {
            using EmployeeContext context = new EmployeeContext();
            return context.Employees.ToList();
        }

        public bool AddEmployee(Employee objnewemployee)
        {
            using EmployeeContext context = new EmployeeContext();
            var entity = context.Add(objnewemployee);
            context.SaveChanges();
            return entity != null;
        }

        public bool UpdateEmployee(Employee objupemployee)
        {
            using EmployeeContext context = new EmployeeContext();
            bool isUpdated = false; 

            if (objupemployee.Age > 21 && objupemployee.Age <= 51)
            {
                var entity = context.Update(objupemployee);
                context.SaveChanges();
                isUpdated = true; 
            }
            else
            {
                MessageBox.Show("Invalid Age.");
            }

            return isUpdated;
        }

        public bool DeleteEmployee(Employee objdelemployee)
        {
            using EmployeeContext context = new EmployeeContext();
            var entity = context.Remove(objdelemployee);
            context.SaveChanges();

            return entity != null;
        }

        public Employee Search(int? id)
        {
            using EmployeeContext context = new EmployeeContext();
            return context.Employees.FirstOrDefault(i => i.Id == id);
        }
    }
}
