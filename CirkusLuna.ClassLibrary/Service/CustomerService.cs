using CirkusLuna.ClassLibrary.Model;
using CirkusLuna.ClassLibrary.Repository;

namespace CirkusLuna.ClassLibrary.Service
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // Returnerer alle kunder
        public List<Customer> GetAll()
        {
            return _customerRepository.GetAll();
        }

        // Finder kunde på ID
        public Customer GetById(int id)
        {
            return _customerRepository.GetById(id);
        }

        // Opretter ny kunde og tilføjer til repository
        public Customer AddCustomer(string firstName, string lastName, string email, string phoneNumber)
        {
            int customerId = _customerRepository.GetAll().Count + 1;
            Customer newCustomer = new Customer(customerId, firstName, lastName, email, phoneNumber, false);
            _customerRepository.Add(newCustomer);
            return newCustomer;
        }

        // Opdaterer kundens navn
        public void UpdateName(int id, string firstName, string lastName)
        {
            Customer customer = _customerRepository.GetById(id);
            customer.FirstName = firstName;
            customer.LastName = lastName;
            _customerRepository.Update(customer);
        }

        // Opdaterer kundens email
        public void UpdateEmail(int id, string email)
        {
            Customer customer = _customerRepository.GetById(id);
            customer.Email = email;
            _customerRepository.Update(customer);
        }

        // Opdaterer kundens telefonnummer
        public void UpdatePhoneNumber(int id, string phoneNumber)
        {
            Customer customer = _customerRepository.GetById(id);
            customer.PhoneNumber = phoneNumber;
            _customerRepository.Update(customer);
        }

        // Sletter kunde på ID
        public void DeleteCustomer(int id)
        {
            _customerRepository.Delete(id);
        }
    }
}