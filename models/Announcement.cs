using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Danvas3.models
{
    public class Announcement
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }

        public Announcement()
        {
            ID = Course.GetNextAnnouncementId();
        }

        public override string ToString()
        {
            return $"ID: {ID}, Title: {Title}, Content: {Content}, Publish Date: {PublishDate}";
        }
    }
}
