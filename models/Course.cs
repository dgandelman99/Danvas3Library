using System.Reflection;
using System.Collections.Generic;
using System;

namespace Library.Danvas3.models
{
    public class Course
    {

        private string code;
        public string Code
        {
            get { code = code.ToUpper().Replace(" ", ""); return code; }
            set { code = value.ToUpper().Replace(" ", ""); } // convert to uppercase and remove spaces
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public int CreditHours { get; set; }

        public Semester Semester { get; set; }


        // Now, when you create a new Course object, you can use this helper method
        // to determine the semester based on the start date as such:
        //      var course = new Course
        //      {
        //          CourseName = "Math",
        //          StartDate = new DateTime(2023, 3, 1),
        //          RoomLocation = "A101"
        //      };

    public static Semester GetSemester(DateTime date)
        {
            if (date.Month >= 1 && date.Month <= 4)
            {
                return Semester.Spring;
            }
            else if (date.Month >= 5 && date.Month <= 7)
            {
                return Semester.Summer;
            }
            else
            {
                return Semester.Fall;
            }
        }
        public DateTime StartDate { get; set; }
        public string RoomLocation { get; set; }

        public List<Person> Roster { get; set; }
        public List<Person> Faculty { get; set; }
        public List<Assignment> Assignments { get; set; }
        public List<Module> Modules { get; set; }
        public List<AssignmentGroup> AssignmentGroups { get; set; }
        public List<Announcement> Announcements { get; set; }

        public Course()
        {
            Roster = new List<Person>(); // initialize roster 
            Assignments = new List<Assignment>(); // initialize assignment list
            Modules = new List<Module>(); // initialize module list
            AssignmentGroups = new List<AssignmentGroup>(); // initialize assignment group list
            Announcements = new List<Announcement>(); // initialize announcement group list
            Faculty = new List<Person>(); // initialize faculty group list
        }

        public void AddModule(Module module)
        {
            Modules.Add(module);
        }

        public void RemoveModule(Module module)
        {
            Modules.Remove(module);
        }

        public void UpdateModule(Module moduleToUpdate, Module newModule)
        {
            var index = Modules.IndexOf(moduleToUpdate);

            if (index != -1)
            {
                Modules[index] = newModule;
            }
        }

        private static int nextID = 1; // static variable to keep track of the last assignment id used

        public static int GetNextAssignmentId()
        {
            int ID = nextID;
            nextID++;
            return ID;
        }

        private static int nextGroupID = 1; // static variable to keep track of the last assignment id used

        public static int GetNextGroupId()
        {
            int ID = nextGroupID;
            nextGroupID++;
            return ID;
        }

        private static int nextModuleID = 1; // static variable to keep track of the last module id used

        public static int GetNextModuleId()
        {
            int ID = nextModuleID;
            nextModuleID++;
            return ID;
        }

        private static int nextAnnouncementID = 1; // static variable to keep track of the last module id used

        public static int GetNextAnnouncementId()
        {
            int ID = nextAnnouncementID;
            nextAnnouncementID++;
            return ID;
        }


        public void RemoveAssignment(int id)
        {
            Assignment assignmentToDelete = Assignments?.Find(a => a?.AssignmentId == id);

            if (assignmentToDelete != null)
            {
                Assignments?.Remove(assignmentToDelete);
                Console.WriteLine($"Assignment with id {id} has been deleted.");
                Console.ReadKey();

            }
            else
            {
                Console.WriteLine($"No assignment with id {id} was found.");
                Console.ReadKey();
            }

        }

        public override string ToString()
        {
            return $"[{Code}] - {Name}";
        }


        public void AddAssignmentGroup(AssignmentGroup group)
        {
            AssignmentGroups.Add(group);
        }

        public void RemoveAssignmentGroup(AssignmentGroup group)
        {
            AssignmentGroups.Remove(group);
        }

        public void UpdateAssignmentGroup(AssignmentGroup groupToUpdate, AssignmentGroup newGroup)
        {
            var index = AssignmentGroups.IndexOf(groupToUpdate);

            if (index != -1)
            {
                AssignmentGroups[index] = newGroup;
            }
        }

        public double GetWeightedAverage(Person student)
        {
            double weightedSum = 0;
            double totalWeight = 0;
            int unweightedTotalGroupPoints = 0;
            int unweightedMaxGroupPoints = 0;

            foreach (var assignmentGroup in AssignmentGroups)
            {
                if (assignmentGroup.Name == "Unweighted")
                {
                    foreach (var assignment in assignmentGroup.Assignments)
                    {
                        if (assignment.Grades.TryGetValue(student, out int grade))
                        {
                            unweightedTotalGroupPoints += grade;
                            unweightedMaxGroupPoints += assignment.TotalAvailablePoints;
                        }
                    }
                }
                else
                {
                    int totalGroupPoints = 0;
                    int maxGroupPoints = 0;

                    foreach (var assignment in assignmentGroup.Assignments)
                    {
                        if (assignment.Grades.TryGetValue(student, out int grade))
                        {
                            totalGroupPoints += grade;
                            maxGroupPoints += assignment.TotalAvailablePoints;
                        }
                    }

                    if (maxGroupPoints != 0)
                    {
                        double groupAverage = (double)totalGroupPoints / maxGroupPoints;
                        weightedSum += groupAverage * assignmentGroup.Weight;
                        totalWeight += assignmentGroup.Weight;
                    }
                }
            }

            double unweightedAverage = 0;
            if (unweightedMaxGroupPoints != 0)
            {
                unweightedAverage = (double)unweightedTotalGroupPoints / unweightedMaxGroupPoints;
            }

            if (totalWeight == 0)
            {
                return unweightedAverage;
            }

            double weightedAverage = weightedSum / totalWeight;

            return (weightedAverage + unweightedAverage) / 2;
        }

        public void AddAnnouncement(Announcement announcement)
        {
            Announcements.Add(announcement);
        }

        public void ReadAnnouncements()
        {
            Console.WriteLine("===========================");
            Console.WriteLine("Announcements:");
            foreach (var announcement in Announcements)
            {
                Console.WriteLine(announcement);
            }
        }

        public void UpdateAnnouncement(int id, string title, string content)
        {
            var announcement = Announcements?.Find(a => a?.ID == id);
            if (announcement != null)
            {
                announcement.Title = title;
                announcement.Content = content;
            }
        }

        public void DeleteAnnouncement(int id)
        {
            var announcement = Announcements?.Find(a => a?.ID == id);
            if (announcement != null)
            {
                Announcements?.Remove(announcement);
            }
        }

        public Announcement GetAnnouncementById(int id)
        {
            return Announcements?.FirstOrDefault(a => a?.ID == id);
        }

        public Assignment GetAssignmentById(int assignmentId)
        {
            foreach (var group in AssignmentGroups)
            {
                var assignment = group.Assignments.Find(a => a.AssignmentId == assignmentId);

                if (assignment != null)
                {
                    return assignment;
                }
            }

            return null;
        }

        public AssignmentGroup GetAssignmentGroupById(int groupId)
        {
            return AssignmentGroups.Find(g => g.GroupId == groupId);
        }

        public void DeleteAssignment(Assignment assignment)
        {
            foreach (var group in AssignmentGroups)
            {
                if (group.Assignments.Contains(assignment))
                {
                    group.Assignments.Remove(assignment);
                    break;
                }
            }
        }

        public void DeleteAssignmentGroup(AssignmentGroup group)
        {
            if (group.Name == "Unweighted")
            {
                throw new InvalidOperationException("Cannot delete the default 'Unweighted' group.");
            }

            // Move all assignments from the deleted group to the default unweighted group
            AssignmentGroup defaultGroup = AssignmentGroups.Find(g => g.Name == "Unweighted");

            foreach (var assignment in group.Assignments)
            {
                defaultGroup.Assignments.Add(assignment);
            }

            AssignmentGroups.Remove(group);
        }

        public AssignmentGroup GetGroupOfAssignment(Assignment assignment)
        {
            return AssignmentGroups.FirstOrDefault(group => group.Assignments.Contains(assignment));
        }

        public string GetSemester()
        {
            if (StartDate.Month >= 1 && StartDate.Month <= 4)
            {
                return "Spring";
            }
            else if (StartDate.Month >= 5 && StartDate.Month <= 7)
            {
                return "Summer";
            }
            else
            {
                return "Fall";
            }
        }

        public bool IsCourseInCurrentSemester()
        {
            DateTime now = DateTime.Now;
            DateTime springStart = new DateTime(now.Year, 1, 1);
            DateTime springEnd = new DateTime(now.Year, 4, 30);
            DateTime summerStart = new DateTime(now.Year, 5, 1);
            DateTime summerEnd = new DateTime(now.Year, 7, 31);
            DateTime fallStart = new DateTime(now.Year, 8, 1);
            DateTime fallEnd = new DateTime(now.Year, 12, 31);

            return (StartDate >= springStart && StartDate <= springEnd) ||
                   (StartDate >= summerStart && StartDate <= summerEnd) ||
                   (StartDate >= fallStart && StartDate <= fallEnd);
        }


    }

    public enum Semester
    {
        Spring,
        Summer,
        Fall
    }

}