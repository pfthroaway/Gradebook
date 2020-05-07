using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Gradebook.Models.IO
{
    /// <summary>Represents all required JSON interactions to read/write data.</summary>
    public static class JSONInteraction
    {
        private static readonly string ClassesFolderLocation = Path.Combine(AppData.Location, "Classes");
        private static readonly string CoursesFolderLocation = Path.Combine(AppData.Location, "Courses");
        private static readonly string StudentsFolderLocation = Path.Combine(AppData.Location, "Students");
        private static readonly string TeachersFolderLocation = Path.Combine(AppData.Location, "Teachers");

        #region Class Manipulation

        /// <summary>Deletes a specified <see cref="SchoolClass"/> from disk.</summary>
        /// <param name="deleteClass"><see cref="SchoolClass"/> to be deleted</param>
        internal static void DeleteClass(SchoolClass deleteClass) => File.Delete(Path.Combine(ClassesFolderLocation, $"{deleteClass.Id}.json"));

        /// <summary>Loads all <see cref="SchoolClass"/>es from disk.</summary>
        /// <returns>List of <see cref="SchoolClass"/>es</returns>
        internal static List<SchoolClass> LoadClasses() => LoadJsonFromFolder<SchoolClass>(ClassesFolderLocation);

        /// <summary>Saves a <see cref="SchoolClass"/> to disk.</summary>
        /// <param name="newClass"><see cref="SchoolClass"/> to be saved to disk</param>
        public static void WriteClass(SchoolClass newClass)
        {
            if (!Directory.Exists(ClassesFolderLocation))
                Directory.CreateDirectory(ClassesFolderLocation);
            File.WriteAllText(Path.Combine(ClassesFolderLocation, $"{newClass.Id}.json"), JsonConvert.SerializeObject(newClass, Formatting.Indented));
        }

        #endregion Class Manipulation

        #region Course Manipulation

        /// <summary>Deletes a specified <see cref="Course"/> from disk.</summary>
        /// <param name="deleteCourse"><see cref="Course"/> to be deleted</param>
        internal static void DeleteCourse(Course deleteCourse) => File.Delete(Path.Combine(CoursesFolderLocation, $"{deleteCourse.Number}.json"));

        /// <summary>Loads all <see cref="Course"/>s from disk.</summary>
        /// <returns>List of <see cref="Course"/>s</returns>
        internal static List<Course> LoadCourses() => LoadJsonFromFolder<Course>(CoursesFolderLocation);

        /// <summary>Saves a <see cref="Course"/> to disk.</summary>
        /// <param name="newCourse"><see cref="Course"/> to be saved to disk</param>
        public static void WriteCourse(Course newCourse)
        {
            if (!Directory.Exists(CoursesFolderLocation))
                Directory.CreateDirectory(CoursesFolderLocation);
            File.WriteAllText(Path.Combine(CoursesFolderLocation, $"{newCourse.Number}.json"), JsonConvert.SerializeObject(newCourse, Formatting.Indented));
        }

        #endregion Course Manipulation

        #region Student Manipulation

        /// <summary>Deletes a specified <see cref="Student"/> from disk.</summary>
        /// <param name="deleteStudent"><see cref="Student"/> to be deleted</param>
        internal static void DeleteStudent(Student deleteStudent) => File.Delete(Path.Combine(StudentsFolderLocation, $"{deleteStudent.Id}.json"));

        /// <summary>Loads all <see cref="Student"/>s from disk.</summary>
        /// <returns>List of <see cref="Student"/>s</returns>
        internal static List<Student> LoadStudents() => LoadJsonFromFolder<Student>(StudentsFolderLocation);

        /// <summary>Saves a <see cref="Student"/> to disk.</summary>
        /// <param name="newStudent"><see cref="Student"/> to be saved to disk</param>
        public static void WriteStudent(Student newStudent)
        {
            if (!Directory.Exists(StudentsFolderLocation))
                Directory.CreateDirectory(StudentsFolderLocation);
            File.WriteAllText(Path.Combine(StudentsFolderLocation, $"{newStudent.Id}.json"), JsonConvert.SerializeObject(newStudent, Formatting.Indented));
        }

        #endregion Student Manipulation

        #region Teacher Manipulation

        /// <summary>Deletes a specified <see cref="Teacher"/> from disk.</summary>
        /// <param name="deleteTeacher"><see cref="Teacher"/> to be deleted</param>
        internal static void DeleteTeacher(Teacher deleteTeacher) => File.Delete(Path.Combine(TeachersFolderLocation, $"{deleteTeacher.Id}.json"));

        /// <summary>Loads all <see cref="Teacher"/>s from disk.</summary>
        /// <returns>List of <see cref="Teacher"/>s</returns>
        internal static List<Teacher> LoadTeachers() => LoadJsonFromFolder<Teacher>(TeachersFolderLocation);

        /// <summary>Saves a <see cref="Teacher"/> to disk.</summary>
        /// <param name="newTeacher"><see cref="Teacher"/> to be saved to disk</param>
        public static void NewTeacher(Teacher newTeacher)
        {
            if (!Directory.Exists(TeachersFolderLocation))
                Directory.CreateDirectory(TeachersFolderLocation);
            File.WriteAllText(Path.Combine(TeachersFolderLocation, $"{newTeacher.Id}.json"), JsonConvert.SerializeObject(newTeacher, Formatting.Indented));
        }

        #endregion Teacher Manipulation

        #region Load

        /// <summary>Loads JSON data from all files in a given folder.</summary>
        /// <param name="folderPath">Path to the folder where the files are to be loaded</param>
        /// <returns>JSON data from a folder</returns>
        private static List<T> LoadJsonFromFolder<T>(string folderPath)
        {
            List<T> list = new List<T>();
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            string[] files = Directory.GetFiles(folderPath);
            if (files.Length > 0)
            {
                foreach (string file in files)
                {
                    if (file.EndsWith(".json"))
                        list.Add(JsonConvert.DeserializeObject<T>(File.ReadAllText(file)));
                }
            }

            return list;
        }

        #endregion Load
    }
}