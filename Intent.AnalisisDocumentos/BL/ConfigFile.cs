using Intent.AnalisisDocumentos.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Intent.AnalisisDocumentos.BL
{
    //Clase encargada de manejar el archivo de configuracion
    public class ConfigFile
    {
        #region Constants
        const string fileName = "config";
        private static string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName); 
        #endregion

        public static void CheckConfig(string prefix, string code, string number, string path)
        {
            string file = string.Format("{0}{1}", fileName, path);
            if (File.Exists(file))
            {
            }
            else
            {
                StringWriter stringWriter = new StringWriter();
                XmlTextWriter xmlWriter = null;

                Config config = new Config();
                config.Prefix = prefix;
                config.Code = code;
                config.Number = number;
                config.Path = path;

                try
                {
                    System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(config.GetType());
                    xmlWriter = new XmlTextWriter(stringWriter);
                    xmlSerializer.Serialize(xmlWriter, config);
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
        }

        private static void ConvertToXml()
        {
        }
    }
}
