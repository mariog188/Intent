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

        public List<string> Exitosos;
        public List<string> Fallidos;

        public SearchFile(string extension, string searchPath)
        {
            this.extension = extension;
            Exitosos = new List<string>();
            Fallidos = new List<string>();
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
                    Exitosos.Add(fileName);
                }
                catch (Exception)
                {
                    Fallidos.Add(fileName);
                }
            }
            else
                Fallidos.Add(fileName);
        }

        public void Log(List<string> exitosos, List<string> fallidos)
        {
            LogFile.WriteLog(exitosos);
            LogFile.WriteError(fallidos);

        }
    }
}
