using Extensions;
using Extensions.DataTypeHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gradebook.Models
{
    /// <summary>Represents a class being taught in the <see cref="School"/>.</summary>
    public class SchoolClass : BaseINPC
    {
        #region Fields

        private string _id, _teacher;
        private List<string> _students;
        private Course _course;
        private List<decimal> _gradingScale = new List<decimal>();
        private List<Assignment> _gradebook;
        private List<DayOfWeek> _days;
        private DateTime _startTime, endTime;

        #endregion Fields

        #region Modifying Properties

        /// <summary>ID of the <see cref="SchoolClass"/> (e.g., "ENGL 1301-99").</summary>
        [JsonProperty(Order = 1)]
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                NotifyPropertyChanged(nameof(Id));
            }
        }

        /// <summary>ID of the Teacher teaching this <see cref="SchoolClass"/>.</summary>
        [JsonProperty(Order = 2)]
        public string Teacher
        {
            get => _teacher;
            set
            {
                _teacher = value;
                NotifyPropertyChanged(nameof(Teacher));
            }
        }

        /// <summary>List of IDs of all <see cref="Student"/>s enrolled in this <see cref="SchoolClass"/>.</summary>
        [JsonProperty(Order = 3)]
        public List<string> Students
        {
            get => _students;
            set
            {
                _students = value;
                NotifyPropertyChanged(nameof(Students));
            }
        }

        /// <summary>Course being taught in this <see cref="SchoolClass"/> (e.g., "ENGL 1301").</summary>
        [JsonProperty(Order = 4)]
        public Course Course
        {
            get => _course;
            set
            {
                _course = value;
                NotifyPropertyChanged(nameof(Course));
            }
        }

        /// <summary>Grading scale for assignment types. 10% = "0.1m"<br/>
        ///  Use this order:<br/>
        ///  Daily, Homework, Project, Quiz, Report, Test.<br/>
        /// (e.g., "0.1m, 0.1m, 0.15m, 0.2m, 0.15m, 0.3m")</summary>
        [JsonProperty(Order = 5)]
        public List<decimal> GradingScale
        {
            get => _gradingScale;
            set
            {
                _gradingScale = value;
                NotifyPropertyChanged(nameof(GradingScale));
            }
        }

        /// <summary>List of <see cref="Assignment"/>s graded in this class.</summary>
        [JsonProperty(Order = 6)]
        public List<Assignment> Gradebook
        {
            get => _gradebook;
            set
            {
                _gradebook = value;
                NotifyPropertyChanged(nameof(Gradebook));
            }
        }

        /// <summary>Days of the week the <see cref="SchooLClass"/> meets.</summary>
        [JsonProperty(Order = 7)]
        public List<DayOfWeek> Days
        {
            get => _days;
            set
            {
                _days = value;
                NotifyPropertyChanged(nameof(Days));
            }
        }

        /// <summary>Time the <see cref="SchoolClass"/> starts.</summary>
        [JsonProperty(Order = 8)]
        public DateTime StartTime
        {
            get => _startTime;
            set { _startTime = value; NotifyPropertyChanged(nameof(StartTime), nameof(StartTimeToString), nameof(Time)); }
        }

        /// <summary>Time the <see cref="SchoolClass"/> ends.</summary>
        [JsonProperty(Order = 9)]
        public DateTime EndTime
        {
            get => endTime;
            set { endTime = value; NotifyPropertyChanged(nameof(EndTime), nameof(EndTimeToString), nameof(Time)); }
        }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>Days of the week the <see cref="SchooLClass"/> meets, formatted.</summary>
        [JsonIgnore]
        public string DaysToString => string.Join(", ", Days);

        /// <summary>Time the <see cref="SchoolClass"/> starts, formatted.</summary>
        [JsonIgnore]
        public string StartTimeToString => StartTime.ToString("hh:MM tt");

        /// <summary>Time the <see cref="SchoolClass"/> ends, formatted.</summary>
        [JsonIgnore]
        public string EndTimeToString => EndTime.ToString("hh:MM tt");

        /// <summary>Formatted string regarding the time the class occurs.</summary>
        [JsonIgnore]
        public string Time => $"{StartTimeToString}-{EndTimeToString}";

        /// <summary>Grading scale for assignment types, formatted.</summary>
        [JsonIgnore]
        public string GradingScaleToString
        {
            get
            {
                string format = "";
                int index = 0;
                foreach (AssignmentType type in Enum.GetValues(typeof(AssignmentType)))
                {
                    format += $"{type}: {GradingScale[index] * 100}%\n";
                    index++;
                }
                return format;
            }
        }

        #endregion Helper Properties

        /// <summary>Displays a <see cref="Student"/>'s grades for this <see cref="SchoolClass"/>.</summary>
        /// <param name="studentID">ID of the <see cref="Student"/> whose grades are being requested</param>
        /// <returns><see cref="Student"/>'s grades</returns>
        public string GetStudentGrades(string studentID)
        {
            if (!string.IsNullOrWhiteSpace(studentID) && Students.Any(std => string.Equals(std, studentID, StringComparison.OrdinalIgnoreCase)))
            {
                string grades = "";
                List<decimal> types = new List<decimal>(Enum.GetNames(typeof(AssignmentType)).Length);
                for (int i = 0; i < GradingScale.Count; i++)
                    types.Add(0);

                foreach (Assignment assignment in Gradebook)
                {
                    string thisGrade = assignment.GetStudentGradeText(studentID);
                    if (!string.IsNullOrWhiteSpace(thisGrade))
                    {
                        grades += $"{thisGrade}\n";
                        int ind = Int32Helper.Parse(assignment.AssignmentType);
                        types[ind] += assignment.GetStudentGrade(studentID);
                    }
                }
                //TODO Figure out a way to have an assignment count for a given type so I can math it up properly.
                string gradesByType = "";
                int index = 0;
                foreach (AssignmentType type in Enum.GetValues(typeof(AssignmentType)))
                {
                    gradesByType += $"{type}: {types[index] * 100}%\n";
                    index++;
                }
                return $"{grades}\n\n{gradesByType}";
            }
            return "";
        }

        public SchoolClass()
        {
        }

        public SchoolClass(string id, string teacherID, List<string> studentIDs, Course course, List<decimal> gradingScale, List<Assignment> gradebook, List<DayOfWeek> days, DateTime startTime, DateTime endTime)
        {
            Id = id;
            Teacher = teacherID;
            Students = studentIDs;
            Course = course;
            GradingScale = gradingScale;
            Gradebook = gradebook;
            Days = days;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}