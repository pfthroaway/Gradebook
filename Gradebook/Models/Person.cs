using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gradebook.Models
{
    public class Person : BaseINPC
    {
        private string _id, _firstName, _lastName;

        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                NotifyPropertyChanged(nameof(Id));
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                NotifyPropertyChanged(nameof(FirstName));
            }
        }

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