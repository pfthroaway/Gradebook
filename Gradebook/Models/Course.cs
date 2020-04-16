namespace Gradebook.Models
{
    /// <summary>Represents a <see cref="Course "/> being taught at the <see cref="School"/>.</summary>
    public class Course : BaseINPC
    {
        private string _name;

        /// <summary>Name of the <see cref="Course"/>, (e.g., "ENGL-1301").</summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }
    }
}