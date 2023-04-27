using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library.Danvas3.models
{
    public class Module
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ContentItem> Content { get; set; }
        public int ID { get; set; }

        public Module()
        {
            ID = Course.GetNextModuleId(); // assign the next available id for particular course
            Content = new List<ContentItem>();  
        }


        public override string ToString()
        {
            string output = "Module [" + ID + "]: " + Name;

            /*
            foreach(var item in Content)
            {
                output += "/n";
                output += item.ToString();
            }
            */
            return output;
        }
    }
}
