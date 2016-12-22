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
            Sucess = string.Format("{0}\\{1}.txt", path, kSucess);
            fail = string.Format("{0}\\{1}.txt", path, kFail);
            if (!File.Exists(Sucess))
            { 
                fileStream = File.Create(Sucess);
                fileStream.Close();
            }
            if (!File.Exists(fail))
            {
                fileStream = File.Create(fail);
                fileStream.Close();
            }
        }

        public void WriteLog(string content)
        {
            File.AppendAllText(Sucess,Environment.NewLine);
            File.AppendAllText(Sucess, content);
        }

        public void WriteError(string content)
        {
            File.AppendAllText(fail,Environment.NewLine);
            File.AppendAllText(fail, content);
        }

        public void CleanLog()
        {
            File.Delete(Sucess);
            File.Delete(fail);

        }
    }
}
