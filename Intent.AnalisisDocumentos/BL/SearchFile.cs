using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Intent.AnalisisDocumentos.BL
{
    public class SearchFile
    {
        public static void SearchAccessibleFiles(string fileName, string extension, string searchPath)
        {
            String[] files = Directory.GetFiles(searchPath, string.Format("{0}{1}", "*", extension), SearchOption.AllDirectories);
            string file = files.FirstOrDefault(item => item.Contains(string.Format("{0}{1}", fileName, extension)));
            if (!string.IsNullOrEmpty(file))
            {
                File.Delete(file);
                File.Create(file);

            }

        }
    }
}
