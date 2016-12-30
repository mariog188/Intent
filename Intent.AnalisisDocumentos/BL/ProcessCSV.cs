using Intent.AnalisisDocumentos.Entities;
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
            //foreach (string item in searchLines)
            //{
            //    id = ;
            //    searchFile.SearchAccessibleFiles(string.Format("{0}{1}{2}-{3}.{4}", config.Prefix, id, config.Code, config.Number, ext));
            //}
            //log.Exitosos = log.Exitosos.Concat(searchFile.Exitosos).ToList();
            //log.Fallidos = log.Fallidos.Concat(searchFile.Fallidos).ToList();

        }
    }



    public class ProcessCSV
    {
        private SearchFile searchFile = null;
        private Log log;
        private Config config;

        public ProcessCSV()
        {
            Log log = new Log();
        }
        public void SplitCSV(string file, string searchPath, Config config)
        {
            this.config = config;
            string[] lines = File.ReadAllLines(file);
            List<string> extensions = (from item in lines
                                       group item by item.Split(';')[1].ToLower() into groups
                                       select groups.Key).ToList();

            //Task[] tareass = new Task[extensions.Length];
            Parallel.ForEach(extensions, item =>
            {
                ThreadProcess tpr = new ThreadProcess(item, searchPath, lines, config, log);
                tpr.ProcessFiles();
            });

            //int contador = 0;
            //foreach (string ext in extensions)
            //{

            //    ThreadProcess tpr = new ThreadProcess(ext, searchPath, lines, config, log);

            //    tareass[contador] = Task.Factory.StartNew(() => tpr.ProcessFiles());
            //    contador++;
            //    //thread = new Thread(new ThreadStart(tpr.ProcessFiles));



            //    //ThreadPool.QueueUserWorkItem(tpr.ProcessFiles);
            //    //thread.Start();

            //}
            //Task.WaitAll(tareass);

            if (searchFile != null)
                searchFile.Log(log.Exitosos, log.Fallidos);
        }


    }
}
