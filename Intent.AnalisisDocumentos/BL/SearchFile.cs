using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Intent.AnalisisDocumentos.BL
{
    public class SearchFile
    {

        void SearchAccessibleFiles(string root, string searchTerm)
        {
            String mask = "*.txt";
            String source = @"c:\source\";
            String destination = @"c:\destination\";

            String[] files = Directory.GetFiles(source, mask, SearchOption.AllDirectories);
            foreach (String file in files)
            {
                File.Move(file, destination + new FileInfo(file).Name);
            }
        }
    }
}
