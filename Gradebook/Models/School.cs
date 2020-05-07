using Extensions;
using Extensions.Enums;
using Gradebook.Models.IO;
using Gradebook.Views.StudentViews;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Gradebook.Models
{
    /// <summary>Static class representing the <see cref="School"/> and all its necessary components, and also includes application navigation.</summary>
    public static class School
    {
        public static List<SchoolClass> AllClasses = new List<SchoolClass>();
        public static List<Teacher> AllTeachers = new List<Teacher>();
        public static List<Student> AllStudents = new List<Student>();
        public static List<Course> AllCourses = new List<Course>();
        public static SchoolClass CurrentClass;
        public static Student CurrentStudent;

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

        #region Class Manipulation

        /// <summary>Deletes a <see cref="SchoolClass"/>.</summary>
        /// <param name="deleteClass"><see cref="SchoolClass"/> to be deleted</param>
        public static void DeleteClass(SchoolClass deleteClass)
        {
            JSONInteraction.DeleteClass(deleteClass);
            foreach (Teacher teacher in AllTeachers)
                teacher.ClassesTaught.RemoveAll(clsTaught => clsTaught.Equals(deleteClass.Id, StringComparison.OrdinalIgnoreCase));
            foreach (Student student in AllStudents)
                student.EnrolledClasses.RemoveAll(clsTaught => clsTaught.Equals(deleteClass.Id, StringComparison.OrdinalIgnoreCase));
            AllClasses.Remove(deleteClass);
        }

        /// <summary>Saves a <see cref="SchoolClass"/> to disk.</summary>
        /// <param name="newClass"><see cref="SchoolClass"/> to be saved to disk</param>
        public static void NewClass(SchoolClass newClass)
        {
            JSONInteraction.WriteClass(newClass);
            AllClasses.Add(newClass);
            AllClasses = AllClasses.OrderBy(cls => cls.Id).ToList();
        }

        #endregion Class Manipulation

        #region Course Manipulation

        /// <summary>Deletes a <see cref="Course"/>.</summary>
        /// <param name="deleteCourse"><see cref="Course"/> to be deleted</param>
        public static void DeleteCourse(Course deleteCourse)
        {
            // if a course is deleted, all classes that teach it need to be deleted
            // all teachers who teach a class with that course need to have that class deleted
            // all students enrolled in a class with that course need to have that class deleted
            JSONInteraction.DeleteCourse(deleteCourse);
            foreach (SchoolClass cls in AllClasses)
            {
                if (cls.Course == deleteCourse)
                    DeleteClass(cls);
            }
            AllCourses.Remove(deleteCourse);
        }

        /// <summary>Saves a <see cref="Course"/> to disk.</summary>
        /// <param name="newCourse"><see cref="Course"/> to be saved to disk</param>
        public static void NewCourse(Course newCourse)
        {
            JSONInteraction.WriteCourse(newCourse);
            AllCourses.Add(newCourse);
            AllCourses = AllCourses.OrderBy(course => course.Number).ToList();
        }

        #endregion Course Manipulation

        #region Student Manipulation

        /// <summary>Deletes a <see cref="Student"/>.</summary>
        /// <param name="deleteStudent"><see cref="Student"/> to be deleted</param>
        public static void DeleteStudent(Student deleteStudent)
        {
            JSONInteraction.DeleteStudent(deleteStudent);
            foreach (SchoolClass cls in AllClasses)
            {
                if (cls.Students.Any(std => std.Equals(deleteStudent.Id, StringComparison.OrdinalIgnoreCase)))
                {
                    cls.Students.RemoveAll(std => std.Equals(deleteStudent.Id, StringComparison.OrdinalIgnoreCase));
                    foreach (Assignment assignment in cls.Gradebook)
                        assignment.Grades.Remove(deleteStudent.Id);
                    JSONInteraction.WriteClass(cls);
                }
            }
            AllStudents.Remove(deleteStudent);
        }

        /// <summary>Modifies a passed <see cref="Student"/> on disk.</summary>
        /// <param name="oldStudent">Old <see cref="Student"/></param>
        /// <param name="newStudent">New <see cref="Student"/></param>
        public static void ModifyStudent(Student oldStudent, Student newStudent)
        {
            if (oldStudent.Id != newStudent.Id)
                JSONInteraction.DeleteStudent(oldStudent);
            JSONInteraction.WriteStudent(newStudent);
            AllStudents.Replace<Student>(oldStudent, newStudent);
        }

        /// <summary>Saves a <see cref="Student"/> to disk.</summary>
        /// <param name="newStudent"><see cref="Student"/> to be saved to disk</param>
        public static void NewStudent(Student newStudent)
        {
            JSONInteraction.WriteStudent(newStudent);
            AllStudents.Add(newStudent);
            AllStudents = AllStudents.OrderBy(student => student.Id).ToList();
        }

        #endregion Student Manipulation

        #region Teacher Manipulation

        /// <summary>Deletes a <see cref="Teacher"/>.</summary>
        /// <param name="deleteTeacher"><see cref="Teacher"/> to be deleted</param>
        public static void DeleteTeacher(Teacher deleteTeacher)
        {
            JSONInteraction.DeleteTeacher(deleteTeacher);
            foreach (SchoolClass cls in AllClasses)
            {
                if (cls.Teacher.Equals(deleteTeacher.Id, StringComparison.OrdinalIgnoreCase))
                {
                    cls.Teacher = "";
                    JSONInteraction.WriteClass(cls);
                }
            }
            AllTeachers.Remove(deleteTeacher);
        }

        /// <summary>Saves a <see cref="Teacher"/> to disk.</summary>
        /// <param name="newTeacher"><see cref="Teacher"/> to be saved to disk</param>
        public static void NewTeacher(Teacher newTeacher)
        {
            JSONInteraction.NewTeacher(newTeacher);
            AllTeachers.Add(newTeacher);
            AllTeachers = AllTeachers.OrderBy(course => course.Id).ToList();
        }

        #endregion Teacher Manipulation

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