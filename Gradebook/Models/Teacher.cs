using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gradebook.Models
{
    /// <summary>Represents a <see cref="Person"/> who teaches <see cref="SchoolClass"/>es.</summary>
    public class Teacher : Person
    {
        private List<string> _coursesTaught;

        /// <summary>List of IDs for <see cref="SchoolClass"/>es this <see cref="Teacher"/> teaches.</summary>
        [JsonProperty(Order = 4)]
        public List<string> CoursesTaught
        {
            get => _coursesTaught;
            set
            {
                _coursesTaught = value;
                NotifyPropertyChanged(nameof(CoursesTaught));
            }
        }

        #region Override Operators

        public static bool Equals(Teacher left, Teacher right)
        {
            if (left is null && right is null) return true;
            if (left is null ^ right is null) return false;
            return string.Equals(left.Id, right.Id, StringComparison.OrdinalIgnoreCase) && string.Equals(left.FirstName, right.FirstName, StringComparison.OrdinalIgnoreCase) && string.Equals(left.LastName, right.LastName, StringComparison.OrdinalIgnoreCase) && !left.CoursesTaught.Except(right.CoursesTaught).Any() && !right.CoursesTaught.Except(left.CoursesTaught).Any();
        }

        public sealed override bool Equals(object obj) => Equals(this, obj as Teacher);

        public bool Equals(Teacher otherTeacher) => Equals(this, otherTeacher);

        public static bool operator ==(Teacher left, Teacher right) => Equals(left, right);

        public static bool operator !=(Teacher left, Teacher right) => !Equals(left, right);

        public sealed override int GetHashCode() => base.GetHashCode() ^ 17;

        /// <summary>Overrides the ToString() method to return only Name.</summary>
        public sealed override string ToString() => $"{Id} - {Name}";

        #endregion Override Operators

        /// <summary>Initializes an instance of <see cref="Course"/> by assigning values to Properties.</summary>
        /// <param name="id">ID of the <see cref="Teacher"/></param>
        /// <param name="firstName">The <see cref="Teacher"/>'s first name</param>
        /// <param name="lastName">The <see cref="Teacher"/>'s first name</param>
        /// <param name="coursesTaught">List of IDs for <see cref="SchoolClass"/>es this <see cref="Teacher"/> teaches</param>
        public Teacher(string id, string firstName, string lastName, List<string> coursesTaught)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            CoursesTaught = coursesTaught;
        }
    }
}