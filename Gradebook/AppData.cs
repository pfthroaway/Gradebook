using System;
using System.IO;

namespace Gradebook
{
    public static class AppData
    {
        internal static string Location = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PF Software", "Gradebook");
    }
}