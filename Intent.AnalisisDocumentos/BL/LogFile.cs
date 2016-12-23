using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Intent.AnalisisDocumentos.BL
{
    public static class LogFile
    {
        private static string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
        const string kSucess = "Archivos Exitosos";
        const string kFail = "Archivos Fallidos";

        private static string Sucess = string.Format("{0}\\{1}", path, kSucess);
        private static string fail = string.Format("{0}\\{1}", path, kFail);

        public static void WriteLog(List<string> content)
        {
            try
            {
                File.WriteAllLines(string.Format("{0}.txt", Sucess), content.ToArray());
            }
            catch (Exception )
            {                
                throw;
            }
        }

        public static void WriteError(List<string> content)
        {
            try
            {
                File.WriteAllLines(string.Format("{0}.txt", fail), content.ToArray());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void CleanLog()
        {
            File.Delete(Sucess);
            File.Delete(fail);

        }
    }

    public class Log
    {
        public List<string> Exitosos;
        public List<string> Fallidos;

        public Log()
        {
            Exitosos = new List<string>();
            Fallidos = new List<string>();
        }
    }
}
