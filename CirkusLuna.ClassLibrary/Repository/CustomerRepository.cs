using CirkusLuna.ClassLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CirkusLuna.ClassLibrary.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private List<Customer> _customerList = new List<Customer>();

        public CustomerRepository()
        {
            Customer customer1 = new Customer(1, "Gunner", "Gunnersen", "gumhmail@mail.com", "56345678", false);
            Customer customer2 = new Customer(2, "Åge", "Ågesen", "åmhmail@mail.com", "35345678", false);
            Customer customer3 = new Customer(3, "Viggo", "Viggosen", "vigmhmail@mail.com", "20345678", false);
            Customer customer4 = new Customer(4, "Maja", "Majasen", "majmhmail@mail.com", "89345678", false);
            Customer customer5 = new Customer(5, "Shen", "Hana", "shemhmail@mail.com", "12995678", true);

            _customerList.Add(customer1);
            _customerList.Add(customer2);
            _customerList.Add(customer3);
            _customerList.Add(customer4);
            _customerList.Add(customer5);



        }
        public List<Customer> GetAll()
        {
            return _customerList;
        }
        public Customer GetById(int id)
        {

            for (int i = 0; i < _customerList.Count; i++)
            {
                if (_customerList[i].Id == id)
                {
                    return _customerList[i];
                }
            }
            return null;
        }

        public void Add(Customer customer)
        {
            _customerList.Add(customer);

        }
        public void Update(Customer customer)
        {
            for (int i = 0; i < _customerList.Count; i++)
            {
                if (_customerList[i].Id == customer.Id)
                {
                    _customerList[i].FirstName = customer.FirstName;
                    _customerList[i].LastName = customer.LastName;
                    _customerList[i].Email = customer.Email;
                    _customerList[i].PhoneNumber = customer.PhoneNumber;
                    _customerList[i].IsVip = customer.IsVip;
                    break; //no point continuing the loop
                }
            }

        }
        public void Delete(int id)
        {
            _customerList.Remove(GetById(id));

        }

    }
}
