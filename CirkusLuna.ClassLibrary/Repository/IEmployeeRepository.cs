using CirkusLuna.ClassLibrary.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CirkusLuna.ClassLibrary.Repository
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll();
        Employee GetById(int id);
        void Add(Employee employee);
        void Update(Employee employee);
        void Delete(int id);

        Employee GetByLastName(string lastName);
    }
}
