﻿using System;

namespace Gradebook.Models
{
    /// <summary>Represents a <see cref="Course "/> being taught at the <see cref="School"/>.</summary>
    public class Course : BaseINPC
    {
        private string _number, _name;
        private decimal _creditHours;

        /// <summary>Number of the <see cref="Course"/>, (e.g., "ENGL-1301").</summary>
        public string Number
        {
            get => _number;
            set
            {
                _number = value;
                NotifyPropertyChanged(nameof(Number));
            }
        }

        /// <summary>Name of the <see cref="Course"/>, (e.g., "English Composition I").</summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        /// <summary>Hours of credit the <see cref="Course"/> provides.</summary></summary>
        public decimal CreditHours
        {
            get => _creditHours;
            set
            {
                _creditHours = value;
                NotifyPropertyChanged(nameof(CreditHours));
            }
        }

        #region Override Operators

        public static bool Equals(Course left, Course right)
        {
            if (left is null && right is null) return true;
            if (left is null ^ right is null) return false;
            return string.Equals(left.Number, right.Number, StringComparison.OrdinalIgnoreCase) && string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && left.CreditHours == right.CreditHours;
        }

        public sealed override bool Equals(object obj) => Equals(this, obj as Course);

        public bool Equals(Course otherCourse) => Equals(this, otherCourse);

        public static bool operator ==(Course left, Course right) => Equals(left, right);

        public static bool operator !=(Course left, Course right) => !Equals(left, right);

        public sealed override int GetHashCode() => base.GetHashCode() ^ 17;

        /// <summary>Overrides the ToString() method to return only Name.</summary>
        public sealed override string ToString() => $"{Number} - {Name}";

        #endregion Override Operators

        #region Constructors

        /// <summary>Initializes an instance of <see cref="Course"/> by assigning values to Properties.</summary>
        /// <param name="number">Number of the <see cref="Course"/>, (e.g., "ENGL-1301").</param>
        /// <param name="name">Name of the <see cref="Course"/>, (e.g., "English Composition I")</param>
        /// <param name="creditHours">Hours of credit the <see cref="Course"/> provides</param>
        public Course(string number, string name, decimal creditHours)
        {
            Number = number;
            Name = name;
            CreditHours = creditHours;
        }

        #endregion Constructors
    }
}