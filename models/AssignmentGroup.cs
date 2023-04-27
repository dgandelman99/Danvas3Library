using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Danvas3.models
{
    public class AssignmentGroup
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public List<Assignment> Assignments { get; set; }
        public int GroupId { get; set; }

        public AssignmentGroup(string name, double weight)
        {
            Name = name;
            Weight = weight;
            Assignments = new List<Assignment>();
            GroupId = Course.GetNextGroupId(); // assign the next available id for particular course
        }

        public override string ToString()
        {
            return $"---Group [ID: {GroupId}]: {Name} (Weight: {Weight})----";
        }

        public void AddAssignment(Assignment assignment)
        {
            Assignments.Add(assignment);
        }

        public void RemoveAssignment(Assignment assignment)
        {
            Assignments.Remove(assignment);
        }

        public void UpdateAssignment(Assignment assignmentToUpdate, Assignment newAssignment)
        {
            var index = Assignments.IndexOf(assignmentToUpdate);

            if (index != -1)
            {
                Assignments[index] = newAssignment;
            }
        }

        public int GetTotalPoints(Person student)
        {
            int totalPoints = 0;

            foreach (var assignment in Assignments)
            {
                totalPoints += assignment.Grades[student];
            }

            return totalPoints;
        }
    }
}
