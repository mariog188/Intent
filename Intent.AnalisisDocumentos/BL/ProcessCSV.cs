using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Intent.AnalisisDocumentos.BL
{
    public class ProcessCSV
    {
        public static void SplitCSV(string file, string searchPath)
        {
            string[] lines = File.ReadAllLines(file);
            string id;
            string ext;
            foreach (string item in lines)
            {
                id = item.Split(',')[0];
                ext = item.Split(',')[1];
                //armar el nombre completo
                SearchFile.SearchAccessibleFiles(id, ext, searchPath);
            }
        }
    }
}
