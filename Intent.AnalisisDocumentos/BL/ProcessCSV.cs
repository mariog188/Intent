using Intent.AnalisisDocumentos.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Intent.AnalisisDocumentos.BL
{
    public class ThreadProcess
    {
        private string ext;
        private string searchPath;
        private string[] lines;
        public SearchFile searchFile = null;
        private Config config;
        private Log log;

        public ThreadProcess(string ext, string searchPath, string[] lines, Config config, Log log)
        {
            this.ext = ext;
            this.searchPath = searchPath;
            this.lines = lines;
            this.config = config;
            this.log = log;
        }


        public void ProcessFiles()
        {

            searchFile = new SearchFile(string.Format("{0}{1}", ".", ext), searchPath);
            string[] searchLines = (from item in lines
                                    where item.Split(';')[1].ToLower().Equals(ext)
                                    select item).ToArray();
            Parallel.ForEach(searchLines, item =>
            {
                string id = item.Split(';')[0];
                searchFile.SearchAccessibleFiles(string.Format("{0}{1}{2}-{3}.{4}", config.Prefix, id, config.Code, config.Number, ext));
            });
            searchFile.files.Clear();

        }
    }



    public class ProcessCSV
    {
        private SearchFile searchFile = null;
        private Log log;
        private List<string> exitosos;
        private List<string> fallidos;
        private Config config;

        public ProcessCSV()
        {
            Log log = new Log();
        }
        public void SplitCSV(string file, string searchPath, Config config)
        {
            this.config = config;
            exitosos = new List<string>();
            fallidos = new List<string>();
            List<string> linesssss = File.ReadAllLines(file).ToList();

          
            //List<string> extensions = (from item in lines
            //                           group item by item.Split(';')[1].ToLower() into groups
            //                           select groups.Key).ToList();
            using (BlockingCollection<string> Exitosos = new BlockingCollection<string>())
            {
                using (BlockingCollection<string> Fallidos = new BlockingCollection<string>())
                {
                    foreach (var item in linesssss.Take(linesssss.Count/8))
                    {
                        Parallel.ForEach(linesssss, line =>
                                  {

                                      FileStream fileStream = null;
                                      string path = string.Format("{0}{1}{2}-{3}.{4}", config.Prefix, line.Split(';')[0], config.Code, config.Number, line.Split(';')[1]);
                                      List<string> files = Directory.GetFiles(searchPath, path, SearchOption.AllDirectories).ToList();
                                      if (files != null && files.Count > 0)
                                      {
                                          try
                                          {
                                              File.SetAttributes(files[0], FileAttributes.Normal);
                                              File.Delete(files[0]);
                                              fileStream = File.Create(files[0]);
                                              fileStream.Close();
                                              File.SetAttributes(files[0], FileAttributes.ReadOnly);
                                              Exitosos.Add(files[0]);
                                          }
                                          catch (Exception)
                                          {
                                              Fallidos.Add(path);
                                          }
                                      }
                                      else
                                          Fallidos.Add(path);
                                  });
                        linesssss.RemoveRange(0, linesssss.Count / 8);
                    }
                    Fallidos.CompleteAdding();
                    Exitosos.CompleteAdding();
                    exitosos = new List<string>(Exitosos);
                    fallidos = new List<string>(Fallidos);
                }
            }
            LogFile.WriteError(fallidos);
            LogFile.WriteLog(exitosos);


        }
    }
}
