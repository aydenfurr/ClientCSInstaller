using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientCSInstaller
{
    /// <summary>
    /// Interaction logic for ProgressDialog.xaml
    /// </summary>
    public partial class ProgressDialog : Page
    {
        public ProgressDialog(MainWindow mainWindow)
        {
            InitializeComponent();
            new Thread(() =>
            {
                new backend(this).start(mainWindow.ver1, mainWindow.isLegacy, mainWindow.mcDir);
            }).Start();
        }
        public WebClient client = new WebClient();
    }
}
