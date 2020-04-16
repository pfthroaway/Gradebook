using System.Collections.Generic;

namespace Gradebook.Models
{
    /// <summary><see cref="Assignment"/> assigned and graded in a <see cref="SchoolClass"/>.</summary>
    public class Assignment : BaseINPC
    {
        private string _name;
        private Dictionary<string, decimal> _grades = new Dictionary<string, decimal>();

        /// <summary>Name of the <see cref="Assignment"/>, (e.g., "Worksheet 1051A").</summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        /// <summary>Dictionary of the <see cref="Student"/>'s ID and the grade they received on the assignment.</summary>
        public Dictionary<string, decimal> Grades
        {
            get => _grades;
            set
            {
                _grades = value;
                NotifyPropertyChanged(nameof(Grades));
            }
        }
    }
}