using System;
using System.IO;
using System.Linq;

namespace Intent.AnalisisDocumentos.BL
{
    public class SearchFile
    {
        private String[] files;
        private string extension;
        private LogFile logfile = new LogFile();

        public SearchFile(string extension, string searchPath)
        {
            this.extension = extension;
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
                    logfile.WriteLog(fileName);
                }
                catch (Exception)
                {
                    logfile.WriteError(fileName);
                }
            }
            else
                logfile.WriteError(fileName);
        }
    }
}
