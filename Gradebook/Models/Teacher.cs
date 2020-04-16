using System.Collections.Generic;

namespace Gradebook.Models
{
    /// <summary>Represents a <see cref="Person"/> who teaches <see cref="SchoolClass"/>es.</summary>
    public class Teacher : Person
    {
        private List<string> _coursesTaught;

        /// <summary>List of IDs for <see cref="SchoolClass"/>es this <see cref="Teacher"/> teaches.</summary>
        public List<string> CoursesTaught
        {
            get => _coursesTaught;
            set
            {
                _coursesTaught = value;
                NotifyPropertyChanged(nameof(CoursesTaught));
            }
        }
    }
}