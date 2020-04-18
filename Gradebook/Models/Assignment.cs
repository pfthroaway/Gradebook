using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Gradebook.Models
{
    /// <summary><see cref="Assignment"/> assigned and graded in a <see cref="SchoolClass"/>.</summary>
    public class Assignment : BaseINPC
    {
        private string _name;
        private AssignmentType _assignmentType;
        private Dictionary<string, decimal> _grades = new Dictionary<string, decimal>(StringComparer.InvariantCultureIgnoreCase);

        /// <summary>Name of the <see cref="Assignment"/>, (e.g., "Worksheet 1051A").</summary>
        [JsonProperty(Order = 1)]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        /// <summary>This <see cref="Assignment"/>'s type (e.g., "Report").</summary>
        [JsonProperty(Order = 2)]
        public AssignmentType AssignmentType
        {
            get => _assignmentType;
            set
            {
                _assignmentType = value;
                NotifyPropertyChanged(nameof(AssignmentType));
            }
        }

        /// <summary>Dictionary of the <see cref="Student"/>'s ID and the grade they received on the assignment, (e.g., "S0001, 97").</summary>
        [JsonProperty(Order = 3)]
        public Dictionary<string, decimal> Grades
        {
            get => _grades;
            set
            {
                _grades = value;
                NotifyPropertyChanged(nameof(Grades));
            }
        }

        /// <summary>Gets a <see cref="Student"/>'s grade on the assignment</summary>
        public decimal GetStudentGrade(string studentID) => Grades.ContainsKey(studentID) ? Grades[studentID] : 0;

        /// <summary>Gets a <see cref="Student"/>'s grade on the assignment, with assignment name.</summary>
        public string GetStudentGradeText(string studentID) => Grades.ContainsKey(studentID) ? $"{Name}: {Grades[studentID]}" : "";

        public Assignment()
        {
        }

        public Assignment(string name, AssignmentType assignmentType, Dictionary<string, decimal> grades)
        {
            Name = name;
            AssignmentType = assignmentType;
            Grades = grades;
        }
    }
}