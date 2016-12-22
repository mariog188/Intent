using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Intent.AnalisisDocumentos.BL
{
    public class LogFile
    {
        private string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
        const string kSucess = "Archivos Exitosos";
        const string kFail = "Archivos Fallidos";

        private string Sucess;
        private string fail;

        public LogFile()
        {
            FileStream fileStream = null;
            Sucess = string.Format("{0}\\{1}", path, kSucess);
            fail = string.Format("{0}\\{1}", path, kFail);
        }

        public void WriteLog(List<string> content, string extension )
        {
            File.WriteAllLines(string.Format("{0} {1}.txt", Sucess,extension) , content.ToArray());            
        }

        public void WriteError(List<string> content, string extension)
        {
            File.WriteAllLines(string.Format("{0} {1}.txt", fail, extension), content.ToArray());
        }

        public void CleanLog()
        {
            File.Delete(Sucess);
            File.Delete(fail);

        }
    }
}
