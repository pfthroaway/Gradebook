using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gradebook.Models
{
    /// <summary>Represents a <see cref="Person"/> who teaches <see cref="SchoolClass"/>es.</summary>
    public class Teacher : Person
    {
        private List<string> _classesTaught;

        /// <summary>List of IDs for <see cref="SchoolClass"/>es this <see cref="Teacher"/> teaches.</summary>
        [JsonProperty(Order = 4)]
        public List<string> ClassesTaught
        {
            get => _classesTaught;
            set
            {
                _classesTaught = value;
                NotifyPropertyChanged(nameof(ClassesTaught));
            }
        }

        #region Override Operators

        public static bool Equals(Teacher left, Teacher right)
        {
            if (left is null && right is null) return true;
            if (left is null ^ right is null) return false;
            return string.Equals(left.Id, right.Id, StringComparison.OrdinalIgnoreCase) && string.Equals(left.FirstName, right.FirstName, StringComparison.OrdinalIgnoreCase) && string.Equals(left.LastName, right.LastName, StringComparison.OrdinalIgnoreCase) && !left.ClassesTaught.Except(right.ClassesTaught).Any() && !right.ClassesTaught.Except(left.ClassesTaught).Any();
        }

        public sealed override bool Equals(object obj) => Equals(this, obj as Teacher);

        public bool Equals(Teacher otherTeacher) => Equals(this, otherTeacher);

        public static bool operator ==(Teacher left, Teacher right) => Equals(left, right);

        public static bool operator !=(Teacher left, Teacher right) => !Equals(left, right);

        public sealed override int GetHashCode() => base.GetHashCode() ^ 17;

        /// <summary>Overrides the ToString() method to return only Name.</summary>
        public sealed override string ToString() => $"{Id} - {Name}";

        #endregion Override Operators

        /// <summary>Initializes an instance of <see cref="Teacher"/> by assigning values to Properties.</summary>
        /// <param name="id">ID of the <see cref="Teacher"/></param>
        /// <param name="firstName">The <see cref="Teacher"/>'s first name</param>
        /// <param name="lastName">The <see cref="Teacher"/>'s first name</param>
        /// <param name="classesTaught">List of IDs for <see cref="SchoolClass"/>es this <see cref="Teacher"/> teaches</param>
        public Teacher(string id, string firstName, string lastName, List<string> classesTaught)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            ClassesTaught = classesTaught;
        }
    }
}