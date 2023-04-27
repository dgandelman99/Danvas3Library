using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Library.Danvas3.models
{
    public class Person
    {
        public string Name { get; set; }
        public Classification Classification { get; set; }

        public Dictionary<Assignment, int> Grades { get; set; }

        public int ID { get; private set; } // ID is now read-only
        public static int nextID = 1; // static field to keep track of the next ID to assign

        public int emplID { get; private set; } // for employees only
        public static int nextEmplID = 1; // static field to keep track of the next ID to assign

        public Person(string name, Classification classification)
        {
            Name = name;
            Classification = classification;
            if (classification != Classification.Instructor && classification != Classification.TA)
            {
                ID = nextID;
                nextID++;
            }
            else if (classification == Classification.Instructor || classification == Classification.TA)
            {
                emplID = nextEmplID;
                nextEmplID++;
            }
        }

        public string DisplayID
        {
            get
            {
                if (Classification == Classification.TA || Classification == Classification.Instructor)
                {
                    return $"EMPLID: {emplID}";
                }
                else
                {
                    return $"ID: {ID}";
                }
            }
        }


        public override string ToString()
        {
            if (Classification == Classification.Instructor || Classification == Classification.TA)
                return $"[EMPLID: {emplID}] {Name} ({Classification})";
            else return $"[ID: {ID}] {Name} ({Classification})";
        }


        public static Classification ConvertStringToClassification(string s)
        {
            switch (s.ToUpper())
            {
                case "F":
                    return Classification.Freshman;
                case "O":
                    return Classification.Sophomore;
                case "J":
                    return Classification.Junior;
                case "S":
                    return Classification.Senior;
                case "T":
                    return Classification.TA;
                case "I":
                    return Classification.Instructor;
                default:
                    throw new ArgumentException("Invalid classification string.");
            }

        }
    }
    public enum Classification
    {
        Freshman, Sophomore, Junior, Senior, TA, Instructor
    }
    public class Student : Person
    {
        public Student(string name, Classification classification) : base(name, classification)
        {
        }

    }


    public class TA : Person
    {
        public TA(string name) : base(name, Classification.TA)
        { 
          
        }
    }

    public class Instructor : Person
    {
        public Instructor(string name) : base(name, Classification.Instructor)
        {
 
        }
    }

}
