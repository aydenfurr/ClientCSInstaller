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
    /// Interaction logic for ChangelogPage.xaml
    /// </summary>
    public partial class ChangelogPage : Page
    {
        public ChangelogPage()
        {
            InitializeComponent();
            using(WebClient wc = new WebClient())
            {
                changelogText.Text = wc.DownloadString("https://raw.githubusercontent.com/aydenfurr/ClientUpdates/main/Changelogs.txt");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.hideDialog(this);
        }
    }
}
