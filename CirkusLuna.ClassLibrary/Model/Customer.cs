using System;
using System.Collections.Generic;
using System.Text;

namespace CirkusLuna.ClassLibrary.Model
{
    public class Customer : Person //Colon means inherits from the class Person
    {
        //customer-specific properties go here like phone number
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsVip { get; set; }




        //Constructor
        // base() calls Person's constructor

        public Customer(int id, string firstName, string lastName, string email, string phoneNumber, bool isVip)
            : base(id, firstName, lastName, email)
        {
            PhoneNumber = phoneNumber;
            IsVip = isVip;

        }
    }
}
