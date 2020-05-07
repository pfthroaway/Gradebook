using Extensions;
using Gradebook.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Gradebook.Views.StudentViews
{
    /// <summary>Interaction logic for NewStudentView.xaml</summary>
    public partial class NewStudentView : Page
    {
        /// <summary>Checks whether the Save buttons should be enabled.</summary>
        private void CheckButtons()
        {
            bool enabled = TxtID.Text.Trim().Length > 0 && TxtFirstName.Text.Trim().Length > 0 && TxtLastName.Text.Trim().Length > 0;
            BtnSaveAndDone.IsEnabled = enabled;
            BtnSaveAndNew.IsEnabled = enabled;
        }

        /// <summary>Clears all TextBoxes.</summary>
        private void Clear()
        {
            TxtID.Clear();
            TxtFirstName.Clear();
            TxtLastName.Clear();
            TxtID.Focus();
        }

        /// <summary>Saves a new <see cref="Student"/>.</summary>
        private void Save()
        {
            if (!School.AllStudents.Any(student => student.Id == TxtID.Text.Trim()))
                School.NewStudent(new Student(TxtID.Text.Trim(), TxtFirstName.Text.Trim(), TxtLastName.Text.Trim(), new List<string>()));
            else
                School.DisplayNotification("That ID has been taken. Please try a new ID.", "Gradebook");
        }

        #region Click

        private void BtnBack_Click(object sender, RoutedEventArgs e) => School.GoBack();

        private void BtnClear_Click(object sender, RoutedEventArgs e) => Clear();

        private void BtnSaveAndDone_Click(object sender, RoutedEventArgs e)
        {
            Save();
            School.GoBack();
        }

        private void BtnSaveAndNew_Click(object sender, RoutedEventArgs e)
        {
            Save();
            Clear();
        }

        #endregion Click

        #region Page-Manipulation Methods

        public NewStudentView()
        {
            InitializeComponent();
            TxtID.Focus();
        }

        private void Txt_GotFocus(object sender, RoutedEventArgs e) => Functions.TextBoxGotFocus(sender);

        private void Txt_TextChanged(object sender, TextChangedEventArgs e) => CheckButtons();

        #endregion Page-Manipulation Methods
    }
}