using CirkusLuna.ClassLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CirkusLuna.ClassLibrary.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList = new List<Employee>();

        //Employee Constructor
        public EmployeeRepository()
        {
            Employee employee1 = new Employee(1, "Benny", "Blæk", "blæk@cirkusluna.dk", "Direktør", "blæk");
            Employee employee2 = new Employee(2, "Dorte", "Hansen", "hansen@cirkusluna.dk", "Sekretær", "hansen");
            Employee employee3 = new Employee(3, "Manfred", "Manfredi", "manfredi@cirkusluna.dk", "Vært", "manfredi");
        }

        public List<Employee> GetAll()
        {
            return _employeeList;
        }
        public Employee GetById(int id)
        {

            for (int i = 0; i < _employeeList.Count; i++)
            {
                if (_employeeList[i].Id == id)
                {
                    return _employeeList[i];
                }
            }
            return null;
        }
    }
}