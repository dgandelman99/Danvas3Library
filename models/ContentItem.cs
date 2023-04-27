using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Danvas3.models
{
    public class ContentItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public int ID { get; set; }

        public override string ToString()
        {
            string itemName = this.GetType().Name;
            return $"[{ID}] - {itemName} {Name}";
        }
    }

    public class AssignmentItem : ContentItem
    {
        public Assignment Assignment { get; set; }
    }

    public class FileItem : ContentItem
    {
        public string FilePath { get; set; }
    }

    public class PageItem : ContentItem
    {
        public string HTMLBody { get; set; }
    }

}
