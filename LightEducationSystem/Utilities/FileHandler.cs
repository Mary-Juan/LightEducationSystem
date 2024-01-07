using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightEducationSystem.Utilities
{
    public static class FileHandler
    {
        public static void SaveToFile(string data, string filePath)
        {
            File.AppendAllText(filePath, data + Environment.NewLine);
        }

        public static string[] ReadFile(string filePath)
        {
            return File.ReadAllLines(filePath);
        }
    }
}
