namespace Gradebook.Models
{
    /// <summary>Represents a living <see cref="Person"/> at the <see cref="School"/>.</summary>
    public class Person : BaseINPC
    {
        private string _id, _firstName, _lastName;

        /// <summary>ID of the <see cref="Person"/>.</summary>
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                NotifyPropertyChanged(nameof(Id));
            }
        }

        /// <summary>The <see cref="Person"/>'s first name.</summary>
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                NotifyPropertyChanged(nameof(FirstName));
            }
        }

        /// <summary>The <see cref="Person"/>'s last name.</summary>
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                NotifyPropertyChanged(nameof(LastName));
            }
        }
    }
}