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
        public MainWindow()
        {
            InitializeComponent();
            configFile = new ConfigFile(Prefijo.Text, Codigo.Text, Numero.Text, RutaBusqueda.Text);

            Config config = configFile.CovertToObject();
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
                ConfigFile configFile = new ConfigFile(Prefijo.Text, Codigo.Text, Numero.Text, RutaBusqueda.Text);
                configFile.CheckConfig();

                ProcessCSV.SplitCSV(dialog.FileName, RutaBusqueda.Text);

            }
        }

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    FolderBrowserDialog
        //    Microsoft.Win32.CommonDialog dialog = new Microsoft.Win32.CommonDialog();
        //    bool? result = dialog.ShowDialog();
        //    if (result.Value)
        //    {
        //        RutaBusqueda.Text = dialog.InitialDirectory;
        //    }
        //}
    }
}
