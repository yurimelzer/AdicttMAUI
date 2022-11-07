using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdicttMAUI.Helpers
{
    public class FileAccessHelper
    {
        public static string GetLocalFilePath(string fileName)
        {
            return System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);
        }
    }
}
