using Newtonsoft.Json;

namespace Gradebook.Models
{
    /// <summary>Represents a living <see cref="Person"/> at the <see cref="School"/>.</summary>
    public class Person : BaseINPC
    {
        private string _id, _firstName, _lastName;

        /// <summary>ID of the <see cref="Person"/>.</summary>
        [JsonProperty(Order = 1)]
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
        [JsonProperty(Order = 2)]
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
        [JsonProperty(Order = 3)]
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                NotifyPropertyChanged(nameof(LastName));
            }
        }

        /// <summary>Displays a <see cref="Person"/>'s first and last names.</summary>
        [JsonIgnore]
        public string Name => $"{FirstName} {LastName}";
    }
}