using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for ConfirmBox.xaml
    /// </summary>
    public partial class ConfirmBox : Page
    {
        public ConfirmBox()
        {
            InitializeComponent();
        }
        public ConfirmBox(String title, String content, int eventType)
        {
            InitializeComponent();
            titleLabel.Content = title;
            contentLabel.Content = content;
            if(eventType == 0)
            {
                primaryButton.Content = "OK";
                primaryButton.Click += (s, e) =>
                {
                    MainWindow.Instance.hideDialog(this);
                };
                cancelButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                primaryButton.Click += (s, e) =>
                {
                    MainWindow.Instance.mainFrame.Navigate(new ProgressDialog(MainWindow.Instance));
                };
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.hideDialog(this);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
