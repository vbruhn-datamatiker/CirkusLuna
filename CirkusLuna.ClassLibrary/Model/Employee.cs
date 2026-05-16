using System;
using System.Collections.Generic;
using System.Text;

namespace CirkusLuna.ClassLibrary.Model
{
    public class Employee : Person
    {
        public string Role { get; set; } = string.Empty;

        public DateOnly BirthDate
        {
            get; set;
        }
        public DateTime HireDate
        {
            get; set;
        }

        public double Salary
        {
            get; set;
        }

        public Employee(int id, string firstName, string lastName, string email, string role, DateOnly birthDate, DateTime hireDate, double salary) : base(id, firstName, lastName, email)
        {
            Role = role;
            BirthDate = birthDate;
            HireDate = hireDate;
            Salary = salary;
        }
    }
}
