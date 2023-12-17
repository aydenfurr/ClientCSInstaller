using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace ClientCSInstaller
{
    public class backend
    {
        public backend(ProgressDialog mainWindow)
        {
            this.mWindow = mainWindow;
            this.fileHelper = new FileHelper(this.mWindow);
        }

        public void start(string version, bool legacy, string mcDir)
        {
            if (!legacy)
            {
                this.setProgress(0, "Main Thread - Starting Installer Thread");
                DirectoryInfo baseDir = new DirectoryInfo(mcDir + "\\versions\\and1558\\");
                string verDirStr = mcDir + "\\versions\\and1558";
                this.setProgress(5, " ThreadInstaller - Deleting old version");
                FileHelper.deleteDirectory(baseDir);
                this.setProgress(11, "Installer Thread - Checking if " + verDirStr + " exists");
                if (!Directory.Exists(verDirStr))
                {
                    this.setProgress(21, "Installer Thread - Creating folder");
                    Directory.CreateDirectory(verDirStr);
                }
                string mainLibDirStr = mcDir + "\\libraries\\uk\\and1558\\AND1558\\LOCAL";
                FileHelper.deleteDirectory(new DirectoryInfo(mcDir + "\\libraries\\uk\\and1558\\"));
                string ofLibDirStr = mcDir + "\\libraries\\cc\\hyperium\\OptiFine\\LOCAL";
                FileHelper.deleteDirectory(new DirectoryInfo(mcDir + "\\libraries\\cc\\hyperium\\"));
                string lwrapperLibDirStr = mcDir + "\\libraries\\net\\minecraft\\launchwrapper\\1.11";
                FileHelper.deleteDirectory(new DirectoryInfo(mcDir + "\\libraries\\net\\minecraft\\"));
                this.setProgress(26, "Installer Thread - Checking if " + mainLibDirStr + " exists");
                if (!Directory.Exists(mainLibDirStr))
                {
                    Directory.CreateDirectory(mainLibDirStr);
                }
                this.setProgress(27, "Installer Thread - Checking if " + ofLibDirStr + " exists");
                if (!Directory.Exists(ofLibDirStr))
                {
                    Directory.CreateDirectory(ofLibDirStr);
                }
                this.setProgress(28, "Installer Thread - Checking if " + lwrapperLibDirStr + " exists");
                if (!Directory.Exists(lwrapperLibDirStr))
                {
                    Directory.CreateDirectory(lwrapperLibDirStr);
                }
                this.setProgress(45, "Installer Thread - Downloading Vanilla 1.8.9 Jar File");
                this.fileHelper.downloadFile(Constant.getVanillaJarUrl(), verDirStr + "\\and1558.jar");
                this.setProgress(47, "Installer Thread - Downloading Modified JSON File");
                this.fileHelper.downloadFile(Constant.getJsonUrl(version), verDirStr + "\\and1558.json");
                this.setProgress(65, "Installer Thread - Downloading Client Jar");
                this.fileHelper.downloadFile(Constant.getNewJarUrl(version), mainLibDirStr + "\\AND1558-LOCAL.jar");
                this.setProgress(78, "Installer Thread - Downloading Optifine Jar");
                this.fileHelper.downloadFile(Constant.getOptifineUrl(), ofLibDirStr + "\\Optifine-LOCAL.jar");
                this.setProgress(89, "Installer Thread - Downloading Launchwrapper");
                this.fileHelper.downloadFile(Constant.getLaunchwrapperUrl(), lwrapperLibDirStr + "\\launchwrapper-1.11.jar");
                this.setProgress(100, "Installer Thread - Done!");
                return;
            }
            if (legacy && MessageBox.Show("The version: " + version + "\nis a legacy version and is NO LONGER SUPPORTED!\nAre you sure you want to continue?", "Continue?", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                this.setProgress(0, "[Legacy] Main Thread - Starting Installer Thread");
                DirectoryInfo baseDir2 = new DirectoryInfo(mcDir + "\\versions\\and1558\\");
                string verDirStr2 = mcDir + "\\versions\\and1558";
                this.setProgress(12, "[Legacy] ThreadInstaller - Deleting old version");
                FileHelper.deleteDirectory(baseDir2);
                this.setProgress(21, "[Legacy] Installer Thread - Checking if " + verDirStr2 + " exists");
                if (!Directory.Exists(verDirStr2))
                {
                    this.setProgress(25, "[Legacy] Installer Thread - Creating folder");
                    Directory.CreateDirectory(verDirStr2);
                }
                this.setProgress(35, "[Legacy] Installer Thread - Downloading Client Jar, Saving to " + verDirStr2 + "\\and1558.jar");
                Thread.Sleep(100);
                this.fileHelper.downloadFile(Constant.getJarUrl(version), verDirStr2 + "\\and1558.jar");
                this.setProgress(76, "[Legacy] Installer Thread - Downloading Client Json, Saving to " + verDirStr2 + "and1558.json");
                Thread.Sleep(100);
                this.fileHelper.downloadFile(Constant.getJsonUrl(version), verDirStr2 + "\\and1558.json");
                this.setProgress(100, "[Legacy] Main Thread - Done");
            }
        }

        // Token: 0x06000011 RID: 17 RVA: 0x00002558 File Offset: 0x00000758
        public void setProgress(int progress, string message)
        {
            this.mWindow.Dispatcher.Invoke(() =>
            {
                this.mWindow.allProgress.Value = (double)progress;
                this.mWindow.allProgressLabel.Content = message;
                if(progress == 100)
                {
                    this.mWindow.title.Content = "The Installer has finished installing your Client!";
                }
            });
        }

        // Token: 0x04000003 RID: 3
        public int progress;

        // Token: 0x04000004 RID: 4
        public string message = "";

        // Token: 0x04000005 RID: 5
        public FileHelper fileHelper;

        // Token: 0x04000006 RID: 6
        public ProgressDialog mWindow;
    }
}
