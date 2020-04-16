using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gradebook.Models
{
    public class SchoolClass : BaseINPC
    {
        private int _id;
        private Course _course;
        private Teacher _teacher;
        private List<int> _students;
        private List<Assignment> _gradebook;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                NotifyPropertyChanged(nameof(Id));
            }
        }

        public Course Course
        {
            get => _course;
            set
            {
                _course = value;
                NotifyPropertyChanged(nameof(Course));
            }
        }

        public Teacher Teacher
        {
            get => _teacher;
            set
            {
                _teacher = value;
                NotifyPropertyChanged(nameof(Course));
            }
        }

        public List<Assignment> Gradebook
        {
            get => _gradebook;
            set
            {
                _gradebook = value;
                NotifyPropertyChanged(nameof(Gradebook));
            }
        }

        public string Name => $"{Id} - {Course.Name}";

        public List<int> Students
        {
            get => _students;
            set
            {
                _students = value;
                NotifyPropertyChanged(nameof(Students));
            }
        }
    }
}