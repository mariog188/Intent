using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Intent.AnalisisDocumentos.BL
{
    public class SearchFile
    {
        private String[] files;
        private string extension;
        private List<string> exitosos;
        private List<string> fallidos;

        public SearchFile(string extension, string searchPath)
        {
            this.extension = extension;
            exitosos = new List<string>();
            fallidos = new List<string>();
            files = Directory.GetFiles(searchPath, string.Format("{0}{1}", "*", extension), SearchOption.AllDirectories);
        }

        public void SearchAccessibleFiles(string fileName)
        {
            string file = files.FirstOrDefault(item => item.Contains(fileName));
            FileStream fileStream = null;
            if (!string.IsNullOrEmpty(file))
            {
                try
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                    fileStream = File.Create(file);
                    fileStream.Close();
                    File.SetAttributes(file, FileAttributes.ReadOnly);
                    exitosos.Add(fileName);
                }
                catch (Exception)
                {
                    fallidos.Add(fileName);
                }
            }
            else
                fallidos.Add(fileName);
        }

        public void Log()
        {
            LogFile.WriteLog(exitosos);
            LogFile.WriteError(fallidos);

        }
    }
}
