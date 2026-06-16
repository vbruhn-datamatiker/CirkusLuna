using System;
using System.Collections.Generic;
using System.Text;

namespace CirkusLuna.ClassLibrary.Model
{
    public class CircusAnimal
    {
        private string _species;

        //Properties
        public string Species
        { 
            get { return _species; }
            set
            {
                if (value == string.Empty)
                {
                    throw new ArgumentException("Angiv en art!");
                }
                _species = value;
            }
        }
        public string Name { get; set; } = string.Empty;

        //Constructor
        public CircusAnimal(string species, string name)
        {
            Species = species;
            this.Name = name;
        }

        public string Perform()
        {
            return $"{Name} af arten {Species} demonstrerere sit cirkustrick over for publikum!";
        }

    }
}
