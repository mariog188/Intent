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

        public static void WriteLog(List<string> content )
        {
            File.WriteAllLines(string.Format("{0}.txt", Sucess) , content.ToArray());            
        }

        public static void WriteError(List<string> content)
        {
            File.WriteAllLines(string.Format("{0}.txt", fail), content.ToArray());
        }

        public static void CleanLog()
        {
            File.Delete(Sucess);
            File.Delete(fail);

        }
    }
}
