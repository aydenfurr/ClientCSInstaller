using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Color = System.Windows.Media.Color;
using MessageBox = System.Windows.MessageBox;

namespace ClientCSInstaller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance;
        public MainWindow()
        {
            Instance = this;
            InitializeComponent();
            Utils.ThemeCurrentWindow(this);
            loadMixinVer();
            loadDefaultValues();
        }

        private void loadDefaultValues()
        {
            textBox1.Text = "C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\.minecraft";
            NewRB.IsChecked = true;
        }

        private bool gotVersion;
        private MainWindow.Root? versionList;
        public List<string> ver = new List<string>
        {
            ""
        };
        private void loadMixinVer()
        {
            if (!this.gotVersion)
            {
                string json = "";
                using (WebClient wc = new WebClient())
                {
                    json = wc.DownloadString("https://raw.githubusercontent.com/and1558/ClientUpdates/main/devverlist.json");
                }
                //MessageBox.Show("The server replied with: \n" + json, "Reply from server");
                this.versionList = JsonConvert.DeserializeObject<Root>(json);
                this.gotVersion = true;
            }
            this.ver.Clear();
            this.comboBox1.Items.Clear();
            foreach (MainWindow.clientmixinverlist clvrlist in versionList.clientmixinverlist)
            {
                this.ver.Add(clvrlist.clientver);
            }
            foreach (string version in this.ver)
            {
                this.comboBox1.Items.Add(version);
            }
        }
        private void loadLegacyVer()
        {
            this.ver.Clear();
            this.comboBox1.Items.Clear();
            foreach (MainWindow.clientverlist clvrlist in this.versionList.clientverlist)
            {
                this.ver.Add(clvrlist.clientver);
            }
            foreach (string version in this.ver)
            {
                this.comboBox1.Items.Add(version);
            }
        }
        public class Root
        {
            public List<MainWindow.clientmixinverlist> clientmixinverlist { get; set; }

            public List<MainWindow.clientverlist> clientverlist { get; set; }
        }

        public class clientmixinverlist
        {
            public string clientver { get; set; }
            public string mcver { get; set; }
        }

        public class clientverlist
        {
            public string clientver { get; set; }

            public string mcver { get; set; }
        }

        private void LegacyRB_Checked(object sender, RoutedEventArgs e)
        {
            loadLegacyVer();
        }

        private void NewRB_Checked(object sender, RoutedEventArgs e)
        {
            loadMixinVer();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = this.textBox1.Text;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (Directory.Exists(dialog.SelectedPath + "\\versions"))
                {
                    if (Directory.Exists(dialog.SelectedPath + "\\versions\\1.8.9"))
                    {
                        this.textBox1.Text = dialog.SelectedPath;
                        return;
                    }
                    MessageBox.Show("Your selected Minecraft folder does not have the version 1.8.9 [VANILLA] Installed!", "1.8.9VER_NOTFOUND", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                else
                {
                    MessageBox.Show("Your selected minecraft folder is INVALID!", "MCDIR_VERDIR_NOTFOUND_INVALID", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
            }
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogFrame.Visibility = Visibility.Visible;
            if (comboBox1.SelectedItem == null) mainFrame.Navigate(new ConfirmBox("Warning", "Please select a version to install!", 0));
            else
            { 
                ver1 = comboBox1.SelectedItem.ToString();
                isLegacy = (bool)LegacyRB.IsChecked;
                mcDir = textBox1.Text;
                mainFrame.Navigate(new ConfirmBox("Confirmation Dialog", "Are you sure you want to install this version?\n" + comboBox1.SelectedItem, 1)); 
            }
        }
        public string ver1 = "";
        public bool isLegacy = false;
        public string mcDir = "";

        bool firstLaunch = false;
        private void mainFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (!firstLaunch)
            {
                firstLaunch = true;
                return;
            }
            BlurEffect be = new BlurEffect();
            be.Radius = 20;
            contentGrid.Effect = be;
            SineEase easing = new SineEase();  // or whatever easing class you want
            easing.EasingMode = EasingMode.EaseOut;
            var ta = new ThicknessAnimation();
            ta.Duration = TimeSpan.FromSeconds(0.14);
            ta.EasingFunction = easing;
            ta.To = new Thickness(0, 0, 0, 0);
            if (e.NavigationMode == NavigationMode.New)
            {
                ta.From = new Thickness(0, 500, 0, 0);
            }
            else if (e.NavigationMode == NavigationMode.Back)
            {
                ta.From = new Thickness(0, 0, 500, 0);
            }
            try
            {
                (e.Content as Page).BeginAnimation(MarginProperty, ta);
            }
            catch (Exception ex)
            {

            }
                 
        }
        public void hideDialog(Page e)
        {
            SineEase easing = new SineEase();  // or whatever easing class you want
            easing.EasingMode = EasingMode.EaseOut;
            var ta = new ThicknessAnimation();
            ta.Duration = TimeSpan.FromSeconds(0.14);
            ta.EasingFunction = easing;
            ta.To = new Thickness(0, 500, 0, 0);
            ta.From = new Thickness(0, 0, 0, 0);
            ta.Completed += (s, e) =>
            {
                DialogFrame.Visibility = Visibility.Collapsed;
            };
            try
            {
                e.BeginAnimation(MarginProperty, ta);
                
            }
            catch (Exception ex)
            {

            }
        }
        private void DialogFrame_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!DialogFrame.IsVisible)
            {
                contentGrid.Effect = null;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DialogFrame.Visibility = Visibility.Visible;
            mainFrame.Navigate(new ChangelogPage());
        }
    }
}