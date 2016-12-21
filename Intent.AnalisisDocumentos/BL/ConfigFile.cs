using Intent.AnalisisDocumentos.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Intent.AnalisisDocumentos.BL
{
    //Clase encargada de manejar el archivo de configuracion
    public class ConfigFile
    {
        #region Constant
        const string fileName = "config";
        #endregion

        #region fields
        private string configPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
        private string prefix;
        private string code;
        private string number;
        private string searchPath;
        #endregion

        #region Properties
        public string file;
        #endregion

        public ConfigFile(string prefix, string code, string number, string path)
        {
            this.prefix = prefix;
            this.code = code;
            this.number = number;
            searchPath = path.EndsWith(@"\") ? path : path + @"\"; ;
            file = string.Format("{0}\\{1}{2}", configPath, fileName, ".xml");
        }




        public void CheckConfig()
        {
            if (!File.Exists(file))
                ConvertToXml(file);
        }

        private void ConvertToXml(string file)
        {
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlWriter = null;

            Config config = new Config();
            config.Prefix = prefix;
            config.Code = code;
            config.Number = number;
            config.Path = searchPath;
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(config.GetType());
                xmlWriter = new XmlTextWriter(stringWriter);
                xmlSerializer.Serialize(xmlWriter, config);
                File.WriteAllText(file, stringWriter.ToString());
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                stringWriter.Close();
                if (xmlWriter != null)
                    xmlWriter.Close();
            }
        }

        public Config CovertToObject()
        {
            Config config = null;
            if (File.Exists(file))
            {
                StreamReader stringReader = null;
                XmlSerializer serializer = null;
                XmlTextReader xmlReader = null;
                try
                {
                    stringReader = new StreamReader(file);
                    serializer = new XmlSerializer(typeof(Config));
                    xmlReader = new XmlTextReader(stringReader);
                    config = (Config)serializer.Deserialize(xmlReader);
                }
                catch (Exception exc)
                {

                    throw;
                }
                finally
                {
                    if (xmlReader != null)
                        xmlReader.Close();
                    if (stringReader != null)
                        stringReader.Close();
                }
            }
            return config;
        }
    }
}
