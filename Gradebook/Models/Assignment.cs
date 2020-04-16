using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gradebook.Models
{
    public class Assignment : BaseINPC
    {
        private string _name;
        private Dictionary<int, int> _grades = new Dictionary<int, int>();

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        public Dictionary<int, int> Grades
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