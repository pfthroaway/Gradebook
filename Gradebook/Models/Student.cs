using System.Collections.Generic;

namespace Gradebook.Models
{
    /// <summary>Represents a <see cref="Person"/> who is enrolled in <see cref="SchoolClass"/>es.</summary>
    public class Student : Person
    {
        private List<string> _enrolledClasses;

        /// <summary>List of IDs for <see cref="SchoolClass"/>es in which the <see cref="Student"/> is currently enrolled.</summary>
        public List<string> EnrolledClasses
        {
            get => _enrolledClasses;
            set
            {
                _enrolledClasses = value;
                NotifyPropertyChanged(nameof(EnrolledClasses));
            }
        }

        public Student()
        {
        }

        public Student(string id, string firstName, string lastName, List<string> enrolledClasses)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            EnrolledClasses = enrolledClasses;
        }
    }
}