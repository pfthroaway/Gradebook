using Extensions;
using Extensions.ListViewHelp;
using Gradebook.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Gradebook.Views.TeacherViews
{
    /// <summary>Interaction logic for TeachersView.xaml</summary>
    public partial class TeachersView : Page
    {
        private Teacher _selectedTeacher;
        private List<Teacher> _allTeachers;
        private ListViewSort _sort = new ListViewSort();

        /// <summary>Refreshes the ListView's ItemsSource.</summary>
        internal void RefreshItemsSource()
        {
            _allTeachers = new List<Teacher>(School.AllTeachers);
            LVTeachers.ItemsSource = _allTeachers;
            LVTeachers.Items.Refresh();
        }

        #region Click

        private void BtnNewTeacher_Click(object sender, RoutedEventArgs e) => School.Navigate(new NewTeacherView());

        private void BtnDeleteTeacher_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedTeacher != null && School.YesNoNotification($"Are you sure that you want to delete this teacher? It will affect {School.AllClasses.Where(cls => cls.Teacher == _selectedTeacher.Id).ToList().Count} classes. This action cannot be undone.", "Gradebook"))
            {
                School.DeleteTeacher(_selectedTeacher);
                RefreshItemsSource();
            }
        }

        private void BtnModifyTeacher_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => School.GoBack();

        private void LVTeachers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool selected = LVTeachers.SelectedIndex >= 0;
            BtnDeleteTeacher.IsEnabled = selected;
            BtnModifyTeacher.IsEnabled = selected;
            if (selected)
                _selectedTeacher = (Teacher)LVTeachers.SelectedValue;
        }

        private void LVTeachersColumnHeader_Click(object sender, RoutedEventArgs e) => _sort = Functions.ListViewColumnHeaderClick(sender, _sort, LVTeachers, "#CCCCCC");

        #endregion Click

        public TeachersView() => InitializeComponent();

        private void Page_Loaded(object sender, RoutedEventArgs e) => RefreshItemsSource();
    }
}