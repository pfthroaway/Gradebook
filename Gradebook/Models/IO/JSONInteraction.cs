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

        /// <summary>Deletes a specified <see cref="Course"/> from disk.</summary>
        /// <param name="deleteCourse"><see cref="Course"/> to be deleted</param>
        internal static void DeleteCourse(Course deleteCourse) => File.Delete(Path.Combine(CoursesFolderLocation, $"{deleteCourse.Number}.json"));

        #region Load

        /// <summary>Loads all <see cref="SchoolClass"/>es from disk.</summary>
        /// <returns>List of <see cref="SchoolClass"/>es</returns>
        internal static List<SchoolClass> LoadClasses() => LoadJsonFromFolder<SchoolClass>(ClassesFolderLocation);

        /// <summary>Loads all <see cref="Course"/>s from disk.</summary>
        /// <returns>List of <see cref="Course"/>s</returns>
        internal static List<Course> LoadCourses() => LoadJsonFromFolder<Course>(CoursesFolderLocation);

        /// <summary>Loads all <see cref="Student"/>s from disk.</summary>
        /// <returns>List of <see cref="Student"/>s</returns>
        internal static List<Student> LoadStudents() => LoadJsonFromFolder<Student>(StudentsFolderLocation);

        /// <summary>Loads all <see cref="Teacher"/>s from disk.</summary>
        /// <returns>List of <see cref="Teacher"/>s</returns>
        internal static List<Teacher> LoadTeachers() => LoadJsonFromFolder<Teacher>(TeachersFolderLocation);

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

        #region Save

        /// <summary>Saves a <see cref="SchoolClass"/> to disk.</summary>
        /// <param name="schoolClass"><see cref="SchoolClass"/> to be saved to disk</param>
        public static void SaveClass(SchoolClass schoolClass)
        {
            if (!Directory.Exists(ClassesFolderLocation))
                Directory.CreateDirectory(ClassesFolderLocation);
            File.WriteAllText(Path.Combine(ClassesFolderLocation, $"{schoolClass.Id}.json"), JsonConvert.SerializeObject(schoolClass, Formatting.Indented));
        }

        /// <summary>Saves a <see cref="Course"/> to disk.</summary>
        /// <param name="saveCourse"><see cref="Course"/> to be saved to disk</param>
        public static void SaveCourse(Course saveCourse)
        {
            if (!Directory.Exists(CoursesFolderLocation))
                Directory.CreateDirectory(CoursesFolderLocation);
            File.WriteAllText(Path.Combine(CoursesFolderLocation, $"{saveCourse.Number}.json"), JsonConvert.SerializeObject(saveCourse, Formatting.Indented));
        }

        /// <summary>Saves a <see cref="Student"/> to disk.</summary>
        /// <param name="student"><see cref="Student"/> to be saved to disk</param>
        public static void SaveStudent(Student student)
        {
            if (!Directory.Exists(StudentsFolderLocation))
                Directory.CreateDirectory(StudentsFolderLocation);
            File.WriteAllText(Path.Combine(StudentsFolderLocation, $"{student.Id}.json"), JsonConvert.SerializeObject(student, Formatting.Indented));
        }

        /// <summary>Saves a <see cref="Teacher"/> to disk.</summary>
        /// <param name="teacher"><see cref="Teacher"/> to be saved to disk</param>
        public static void SaveTeacher(Teacher teacher)
        {
            if (!Directory.Exists(TeachersFolderLocation))
                Directory.CreateDirectory(TeachersFolderLocation);
            File.WriteAllText(Path.Combine(TeachersFolderLocation, $"{teacher.Id}.json"), JsonConvert.SerializeObject(teacher, Formatting.Indented));
        }

        #endregion Save
    }
}