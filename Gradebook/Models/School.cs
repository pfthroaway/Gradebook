using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gradebook.Models
{
    /// <summary>Static class representing the <see cref="School"/> and all its necessary components.</summary>
    public static class School
    {
        public static List<SchoolClass> AllClasses = new List<SchoolClass>();
        public static List<Teacher> AllTeachers = new List<Teacher>();
        public static List<Student> AllStudents = new List<Student>();
        public static List<Course> AllCourses = new List<Course>();

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
    }
}