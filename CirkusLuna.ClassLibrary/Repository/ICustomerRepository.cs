using CirkusLuna.ClassLibrary.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CirkusLuna.ClassLibrary.Repository
{
    public interface ICustomerRepository
    {
        // EL FAMIOSO DEL CLAE!!!... eller bare CRUD på engelsk
        List<Customer> GetAll();
        Customer GetById(int id);
        void Delete(int id);
        void Add(Customer customer);
        void Update(Customer customer);

    }
}
