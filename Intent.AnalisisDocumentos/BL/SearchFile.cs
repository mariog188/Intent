using System;
using System.IO;
using System.Linq;

namespace Intent.AnalisisDocumentos.BL
{
    public class SearchFile
    {
        private String[] files;
        private string extension;

        public SearchFile(string extension, string searchPath)
        {            
            this.extension = extension;
            files = Directory.GetFiles(searchPath, string.Format("{0}{1}", "*", extension), SearchOption.AllDirectories);
        }

        public void SearchAccessibleFiles(string fileName)
        {
            string file = files.FirstOrDefault(item => item.Contains(fileName));
            if (!string.IsNullOrEmpty(file))
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
                File.Create(file);
                File.SetAttributes(file, FileAttributes.ReadOnly);
            }

        }
    }
}
