using Intent.AnalisisDocumentos.Entities;
using System.IO;
using System.Linq;

namespace Intent.AnalisisDocumentos.BL
{
    public class ProcessCSV
    {
        public static void SplitCSV(string file, string searchPath, Config config)
        {
            string[] lines = File.ReadAllLines(file);
            string[] extensions = (from item in lines
                                   group item by item.Split(';')[1].ToLower() into groups
                                   select groups.Key).ToArray();
            string id;
            foreach (string ext in extensions)
            {
                SearchFile searchFile = new SearchFile(string.Format("{0}{1}", ".", ext), searchPath);
                string[] searchLines = (from item in lines
                                        where item.Split(';')[1].Equals(ext)
                                        select item).ToArray();
                foreach (string item in searchLines)
                {
                    id = item.Split(';')[0];
                    searchFile.SearchAccessibleFiles(string.Format("{0}{1}{2}-{3}.{4}", config.Prefix, id, config.Code, config.Number, ext));
                }
                searchFile.Log(ext);
            }
        }
    }
}
