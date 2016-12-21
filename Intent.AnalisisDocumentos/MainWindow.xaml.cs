using Intent.AnalisisDocumentos.BL;
using Intent.AnalisisDocumentos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
    }
}
