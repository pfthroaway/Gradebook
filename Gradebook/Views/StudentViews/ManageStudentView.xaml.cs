using Extensions;
using Gradebook.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Gradebook.Views.StudentViews
{
    /// <summary>Interaction logic for ManageStudentView.xaml</summary>
    public partial class ManageStudentView : Page
    {
        /// <summary>Checks whether the Save buttons should be enabled.</summary>
        private void CheckButtons()
        {
            bool enabled = TxtID.Text.Trim().Length > 0 && TxtFirstName.Text.Trim().Length > 0 && TxtLastName.Text.Trim().Length > 0;
            BtnSave.IsEnabled = enabled;
            BtnReset.IsEnabled = enabled;
        }

        /// <summary>Resets all TextBoxes to the default data.</summary>
        private void Reset()
        {
            TxtID.Text = School.CurrentStudent.Id;
            TxtFirstName.Text = School.CurrentStudent.FirstName;
            TxtLastName.Text = School.CurrentStudent.LastName;
            TxtID.Focus();
        }

        #region Click

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (School.CurrentStudent.Id == TxtID.Text.Trim() || !School.AllStudents.Any(student => student.Id == TxtID.Text.Trim()))
            {
                School.ModifyStudent(School.CurrentStudent, new Student(School.CurrentStudent, TxtID.Text.Trim(), TxtFirstName.Text.Trim(), TxtLastName.Text.Trim()));
                School.GoBack();
            }
            else
                School.DisplayNotification("That ID has been taken. Please try a new ID.", "Gradebook");
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e) => Reset();

        private void BtnManageStudentClasses_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => School.GoBack();

        #endregion Click

        #region Page Manipulation

        public ManageStudentView()
        {
            InitializeComponent();
            Reset();
        }

        private void Txt_GotFocus(object sender, RoutedEventArgs e) => Functions.TextBoxGotFocus(sender);

        private void Txt_TextChanged(object sender, TextChangedEventArgs e) => CheckButtons();

        #endregion Page Manipulation
    }
}