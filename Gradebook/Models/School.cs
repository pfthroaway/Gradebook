using Extensions;
using Extensions.Enums;
using Gradebook.Models.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Gradebook.Models
{
    /// <summary>Static class representing the <see cref="School"/> and all its necessary components.</summary>
    public static class School
    {
        public static List<SchoolClass> AllClasses = new List<SchoolClass>();
        public static List<Teacher> AllTeachers = new List<Teacher>();
        public static List<Student> AllStudents = new List<Student>();
        public static List<Course> AllCourses = new List<Course>();

        #region Navigation

        /// <summary>Instance of MainWindow currently loaded</summary>
        internal static MainWindow MainWindow { get; set; }

        /// <summary>Navigates to selected Page.</summary>
        /// <param name="newPage">Page to navigate to.</param>
        internal static void Navigate(Page newPage) => MainWindow.MainFrame.Navigate(newPage);

        /// <summary>Navigates to the previous Page.</summary>
        internal static void GoBack()
        {
            if (MainWindow.MainFrame.CanGoBack)
                MainWindow.MainFrame.GoBack();
        }

        #endregion Navigation

        /// <summary>Ensures proper directory structure and file layout for the application.</summary>
        internal static void FileManagement()
        {
            if (!Directory.Exists(AppData.Location))
                Directory.CreateDirectory(AppData.Location);
        }

        /// <summary>Loads all necessary items from disk.</summary>
        public static void LoadAll()
        {
            FileManagement();
            AllClasses = JSONInteraction.LoadClasses();
            AllCourses = JSONInteraction.LoadCourses();
            AllStudents = JSONInteraction.LoadStudents();
            AllTeachers = JSONInteraction.LoadTeachers();
        }

        /// <summary>Gets a specific <see cref="Student"/>'s grades from all their <see cref="SchoolClass"/>es.</summary>
        /// <param name="studentID"><see cref="Student"/>'s ID whose grades are being requested</param>
        /// <returns>Specific <see cref="Student"/>'s grades from all their <see cref="SchoolClass"/>es</returns>
        public static string GetStudentGrades(string studentID)
        {
            string grades = "";
            if (!string.IsNullOrWhiteSpace(studentID))
            {
                Student student = AllStudents.Find(std => std.Id == studentID);
                if (student != null && student.EnrolledClasses.Count > 0)
                {
                    List<SchoolClass> enrolledClasses = new List<SchoolClass>();
                    foreach (string stdCls in student.EnrolledClasses)
                        enrolledClasses.Add(AllClasses.Find(cls => string.Equals(stdCls, cls.Id, StringComparison.OrdinalIgnoreCase)));
                    if (enrolledClasses.Count > 0)
                    {
                        foreach (SchoolClass cls in enrolledClasses)
                        {
                            grades += $"{cls.Id} - {cls.Course.Number} - {cls.Course.Name}\n";
                            string thisClassGrades = cls.GetStudentGrades(studentID);
                            if (!string.IsNullOrWhiteSpace(thisClassGrades))
                                grades += $"{thisClassGrades}\n";
                        }
                    }
                }
            }
            return grades;
        }

        #region Notification Management

        /// <summary>Displays a new Notification in a thread-safe way.</summary>
        /// <param name="message">Message to be displayed</param>
        /// <param name="title">Title of the Notification window</param>
        internal static void DisplayNotification(string message, string title) => Application.Current.Dispatcher.Invoke(() => new Notification(message, title, NotificationButton.OK, MainWindow).ShowDialog());

        /// <summary>Displays a new Notification in a thread-safe way and retrieves a boolean result upon its closing.</summary>
        /// <param name="message">Message to be displayed</param>
        /// <param name="title">Title of the Notification window</param>
        /// <returns>Returns value of clicked button on Notification.</returns>
        internal static bool YesNoNotification(string message, string title) => Application.Current.Dispatcher.Invoke(() => new Notification(message, title, NotificationButton.YesNo, MainWindow).ShowDialog() == true);

        #endregion Notification Management
    }
}