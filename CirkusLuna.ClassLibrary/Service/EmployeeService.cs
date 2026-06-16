using CirkusLuna.ClassLibrary.Model;
using CirkusLuna.ClassLibrary.Repository;

namespace CirkusLuna.ClassLibrary.Service
{
    public class EmployeeService : IEmployeeService
    {
        //Reference til employeeRepository
        private IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // Returnerer alle employees
        public List<Employee> GetAll()
        {
            return _employeeRepository.GetAll();
        }

        // Finder og returnerer employee på ID
        public Employee GetById(int id)
        {
            return _employeeRepository.GetById(id);
        }

        // Login logik - finder employee på password, returnerer null hvis ikke fundet
        public Employee Login(string password)
        {
            foreach (Employee employee in _employeeRepository.GetAll())
            {
                if (employee.Password == password)
                    return employee;
            }
            return null;
        }

        // Opretter ny employee - password sættes automatisk til efternavn
        public Employee AddEmployee(string firstName, string lastName, string email, string role)
        {
            int newEmployeeId = _employeeRepository.GetAll().Count + 1;
            string password = lastName;
            Employee newEmployee = new Employee(newEmployeeId, firstName, lastName, email, role, password);
            _employeeRepository.Add(newEmployee);
            return newEmployee;
        }

        // Sletter employee på ID
        public void DeleteEmployee(int id)
        {
            _employeeRepository.Delete(id);
        }

        public Employee GetByLastName(string lastName)
        { 
            return _employeeRepository.GetByLastName(lastName);
        }
    }
}