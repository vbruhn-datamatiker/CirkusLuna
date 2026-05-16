using System;
using System.Collections.Generic;
using System.Text;

namespace CirkusLuna.ClassLibrary.Model
{
    public class Employee : Person
    {
        public string Role { get; set; } = string.Empty;
        public string Password { get; set; }


        public Employee(int id, string firstName, string lastName, string email, string role, string password) : base(id, firstName, lastName, email)
        {
            Role = role;
            Password = password;
        }
    }
}
