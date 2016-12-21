using Intent.AnalisisDocumentos.BL;
using Intent.AnalisisDocumentos.Entities;
using System.Windows;

namespace Intent.AnalisisDocumentos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ConfigFile configFile;
        private Config config;
        public MainWindow()
        {
            InitializeComponent();
            //carga configuracion 
            configFile = new ConfigFile(Prefijo.Text, Codigo.Text, Numero.Text, RutaBusqueda.Text);
            config = configFile.CovertToObject();
            if (config != null)
            {
                Prefijo.Text = config.Prefix;
                Codigo.Text = config.Code;
                Numero.Text = config.Number;
                RutaBusqueda.Text = config.Path;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            bool? result = dialog.ShowDialog();
            if (result.Value)
            {
                try
                {
                    //guarda nueva configuracion 
                    ConfigFile configFile = new ConfigFile(Prefijo.Text, Codigo.Text, Numero.Text, RutaBusqueda.Text, true);
                    //Divide csv y busca el archivo
                    ProcessCSV.SplitCSV(dialog.FileName, RutaBusqueda.Text, configFile.Config);
                    MessageBox.Show("Documentos Procesados de manera exitosa");
                }
                catch (System.Exception)
                {
                    MessageBox.Show("Error en el proceso ");
                }
            }
        }

    }
}
