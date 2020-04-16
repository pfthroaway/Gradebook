using System.Collections.Generic;

namespace Gradebook.Models
{
    /// <summary>Represents a class being taught in the <see cref="School"/>.</summary>
    public class SchoolClass : BaseINPC
    {
        private string _id;
        private Course _course;
        private string _teacher;
        private List<string> _students;
        private List<Assignment> _gradebook;

        /// <summary>ID of the <see cref="SchoolClass"/>  )e.g., "ENGL 1301-99").</summary>
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                NotifyPropertyChanged(nameof(Id));
            }
        }

        /// <summary>Course being taught in this <see cref="SchoolClass"/> (e.g., "ENGL 1301").</summary>
        public Course Course
        {
            get => _course;
            set
            {
                _course = value;
                NotifyPropertyChanged(nameof(Course));
            }
        }

        /// <summary>ID of the Teacher teaching this <see cref="SchoolClass"/>.</summary>
        public string Teacher
        {
            get => _teacher;
            set
            {
                _teacher = value;
                NotifyPropertyChanged(nameof(Course));
            }
        }

        /// <summary>List of <see cref="Assignment"/>s graded in this class.</summary>
        public List<Assignment> Gradebook
        {
            get => _gradebook;
            set
            {
                _gradebook = value;
                NotifyPropertyChanged(nameof(Gradebook));
            }
        }

        /// <summary>List of IDs of all <see cref="Student"/>s enrolled in this <see cref="SchoolClass"/>.</summary>
        public List<string> Students
        {
            get => _students;
            set
            {
                _students = value;
                NotifyPropertyChanged(nameof(Students));
            }
        }
    }
}