using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Library.Danvas3.models;

namespace LearningSystemGUI
{
    public class DataStorage
    {
        /* public static List<Course> courses = new List<Course>();
        public static List<Person> people = new List<Person>();
        public static HashSet<string> courseCodes = new HashSet<string>(); */

        /*
        // Singleton implementation
        private static DataStorage _instance;
        public static DataStorage Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DataStorage();
                }
                return _instance;
            }
        }
        */

        public List<Course> courses { get; set; }
        public int TotalCourses;
        public List<Person> people { get; set; }

        public int TotalPeople;
        public HashSet<string> courseCodes { get; set; }

        public DataStorage()
        {
            courses = new List<Course>();
            courseCodes = new HashSet<string>();
            people = new List<Person>();
            TotalCourses = 0;
            TotalPeople = 0;
            Person student = new Person("placeholderPerson", Person.ConvertStringToClassification("S"));
            Person staff = new Person("placeholderTA", Classification.TA);
            Person staff2 = new Person("placeholderInstructor", Classification.Instructor);
            AddPerson(student);
            AddPerson(staff);
            AddPerson(staff2);
            Course course = new Course
            {
                Code = "XXXX",
                Name = "placeholderCourse",
                Description = "nothing to see here",
                CreditHours = 3
            };
            AddCourse(course);
        }


        public void AddCourse(Course course)
        {
            courses.Add(course);
            AddCourseCode(course.Code);
            TotalCourses+=1;
        }

        public void UpdateCourse(Course updatedCourse)
        {
            for (int i = 0; i < courses.Count; i++)
            {
                if (courses[i].Code == updatedCourse.Code)
                {
                    courses[i] = updatedCourse;
                    break;
                }
            }
        }


        public void AddPerson(Person person)
        {
            people.Add(person);
            TotalPeople+=1;
        }

        public void UpdatePerson(Person person)
        {
            int index = people.FindIndex(p => p.ID == person.ID);
            if (index >= 0)
            {
                people[index] = person;
            }
        }


        public void AddCourseCode(String code)
        {
            if (IsCodeUnique(code))
            {
                courseCodes.Add(code);
            }
        }

        public void DeleteCourse(Course course)
        {
            courses.Remove(course);
            TotalCourses -= 1;
        }

        public void DeletePerson(Person person)
        {
            people.Remove(person);
            TotalPeople -= 1;
        }

        public void DeleteCourseCode(String code)
        {
            courseCodes.Remove(code);
        }

        public bool IsCodeUnique(String code)
        {
            //if (TotalCourses == 0) AddCourseCode("XXXX");
            return !courseCodes.Contains(code);
        }
    }
}

