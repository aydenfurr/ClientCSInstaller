using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ClientCSInstaller
{
    public class FileHelper
    {
        public FileHelper(ProgressDialog mainWindow)
        {
            this.mWindow = mainWindow;
            this.mWindow.client.DownloadProgressChanged += (s, e) =>
            {
                this.mWindow.Dispatcher.Invoke(() =>
                {
                    this.mWindow.downloadProgressLabel.Content = "Downloading: " + e.ProgressPercentage + "% | " + e.BytesReceived / 1024L + " KB / " + e.TotalBytesToReceive / 1024L + " KB";
                    this.mWindow.downloadProgress.Value = (double)e.ProgressPercentage + 0.0;
                });
            };
            this.mWindow.client.DownloadFileCompleted += (s, e) =>
            {
                this.mWindow.Dispatcher.Invoke(() =>
                {
                    this.mWindow.downloadProgressLabel.Content = "Finished!";
                });
            };
        }

        public static void deleteDirectory(DirectoryInfo baseDir)
        {
            if (!baseDir.Exists)
            {
                return;
            }
            foreach (DirectoryInfo baseDir2 in baseDir.EnumerateDirectories())
            {
                FileHelper.deleteDirectory(baseDir2);
            }
            baseDir.Delete(true);
        }

        public void downloadFile(string url, string fileDir)
        {
            this.mWindow.client.DownloadFileAsync(new Uri(url), fileDir);
            while (this.mWindow.client.IsBusy)
            {
            }
        }

        private ProgressDialog mWindow;
    }
}
