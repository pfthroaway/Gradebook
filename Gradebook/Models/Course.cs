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

        public Course()
        {
        }

        public Course(string number, string name, decimal creditHours)
        {
            Number = number;
            Name = name;
            CreditHours = creditHours;
        }
    }
}