using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gradebook.Models
{
    /// <summary>Represents a <see cref="Person"/> who is enrolled in <see cref="SchoolClass"/>es.</summary>
    public class Student : Person
    {
        private List<string> _enrolledClasses;

        /// <summary>List of IDs for <see cref="SchoolClass"/>es in which the <see cref="Student"/> is currently enrolled.</summary>
        [JsonProperty(Order = 4)]
        public List<string> EnrolledClasses
        {
            get => _enrolledClasses;
            set
            {
                _enrolledClasses = value;
                NotifyPropertyChanged(nameof(EnrolledClasses));
            }
        }

        #region Override Operators

        public static bool Equals(Student left, Student right)
        {
            if (left is null && right is null) return true;
            if (left is null ^ right is null) return false;
            return string.Equals(left.Id, right.Id, StringComparison.OrdinalIgnoreCase) && string.Equals(left.FirstName, right.FirstName, StringComparison.OrdinalIgnoreCase) && string.Equals(left.LastName, right.LastName, StringComparison.OrdinalIgnoreCase) && !left.EnrolledClasses.Except(right.EnrolledClasses).Any() && !right.EnrolledClasses.Except(left.EnrolledClasses).Any();
        }

        public sealed override bool Equals(object obj) => Equals(this, obj as Student);

        public bool Equals(Student otherStudent) => Equals(this, otherStudent);

        public static bool operator ==(Student left, Student right) => Equals(left, right);

        public static bool operator !=(Student left, Student right) => !Equals(left, right);

        public sealed override int GetHashCode() => base.GetHashCode() ^ 17;

        /// <summary>Overrides the ToString() method to return only Name.</summary>
        public sealed override string ToString() => $"{Id} - {Name}";

        #endregion Override Operators

        #region Constructors

        /// <summary>Initializes an instance of <see cref="Course"/> by assigning values to Properties.</summary>
        /// <param name="id">ID of the <see cref="Student"/></param>
        /// <param name="firstName">The <see cref="Student"/>'s first name</param>
        /// <param name="lastName">The <see cref="Student"/>'s first name</param>
        /// <param name="enrolledClasses">List of IDs for <see cref="SchoolClass"/>es in which the <see cref="Student"/> is currently enrolled</param>
        public Student(string id, string firstName, string lastName, List<string> enrolledClasses)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            EnrolledClasses = enrolledClasses;
        }

        #endregion Constructors
    }
}