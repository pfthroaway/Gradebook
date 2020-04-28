using Extensions;
using Extensions.ListViewHelp;
using Gradebook.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Gradebook.Views.StudentViews
{
    /// <summary>Interaction logic for StudentsView.xaml</summary>
    public partial class StudentsView : Page
    {
        private Student _selectedStudent;
        private List<Student> _allStudents;
        private ListViewSort _sort = new ListViewSort();

        /// <summary>Refreshes the ListView's ItemsSource.</summary>
        internal void RefreshItemsSource()
        {
            _allStudents = new List<Student>(School.AllStudents);
            LVStudents.ItemsSource = _allStudents;
            LVStudents.Items.Refresh();
        }

        #region Click

        private void BtnNewStudent_Click(object sender, RoutedEventArgs e) => School.Navigate(new NewStudentView());

        private void BtnDeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedStudent != null && School.YesNoNotification($"Are you sure that you want to delete this student? It will affect {School.AllClasses.Where(cls => cls.Students.Any(std => std == _selectedStudent.Id)).ToList().Count} classes. This action cannot be undone.", "Gradebook"))
            {
                School.DeleteStudent(_selectedStudent);
                RefreshItemsSource();
            }
        }

        private void BtnModifyStudent_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => School.GoBack();

        private void LVStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool selected = LVStudents.SelectedIndex >= 0;
            BtnDeleteStudent.IsEnabled = selected;
            BtnModifyStudent.IsEnabled = selected;
            if (selected)
                _selectedStudent = (Student)LVStudents.SelectedValue;
        }

        private void LVStudentsColumnHeader_Click(object sender, RoutedEventArgs e) => _sort = Functions.ListViewColumnHeaderClick(sender, _sort, LVStudents, "#CCCCCC");

        #endregion Click

        public StudentsView() => InitializeComponent();

        private void Page_Loaded(object sender, RoutedEventArgs e) => RefreshItemsSource();
    }
}