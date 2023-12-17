using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using static ClientCSInstaller.ParameterTypes;
using static ClientCSInstaller.PInvoke;
using Color = System.Windows.Media.Color;

namespace ClientCSInstaller
{
    internal class Utils
    {
        
        public static void ThemeCurrentWindow(Window window)
        {
            var dark = App.Current.Resources.MergedDictionaries.FirstOrDefault(t => t.Source.OriginalString.Contains("Themes/DarkMode.xaml"));
            var light = App.Current.Resources.MergedDictionaries.FirstOrDefault(t => t.Source.OriginalString.Contains("Themes/LightMode.xaml"));
            var win11 = App.Current.Resources.MergedDictionaries.FirstOrDefault(t => t.Source.OriginalString.Contains("Themes/Win11Style.xaml"));
            if (dark != null && win11 != null)
            {
                App.Current.Resources.MergedDictionaries.Remove(win11);
                App.Current.Resources.MergedDictionaries.Remove(dark);
            }
            App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/Win11Style;component/Themes/DarkMode.xaml") });
            App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/Win11Style;component/Themes/Win11Style.xaml") });
            // Initiate Dark Mode Title Bar
            var darkMode = true;
            IntPtr hWnd = new WindowInteropHelper(window).EnsureHandle();
            PInvoke.DwmSetWindowAttribute(hWnd, PInvoke.DWMWA_USE_IMMERSIVE_DARK_MODE, ref darkMode, System.Runtime.InteropServices.Marshal.SizeOf(true));
            RefreshFrame(window);
            PInvoke.SetWindowAttribute(
                hWnd,
                DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE,
                ParameterTypes.DWMSBT_MAINWINDOW);

        }
        public static void RefreshFrame(Window window)
        {
            IntPtr mainWindowPtr = new WindowInteropHelper(window).Handle;
            HwndSource mainWindowSrc = HwndSource.FromHwnd(mainWindowPtr);
            mainWindowSrc.CompositionTarget.BackgroundColor = Color.FromArgb(0, 0, 0, 0);

            MARGINS margins = new MARGINS();
            margins.cxLeftWidth = -1;
            margins.cxRightWidth = -1;
            margins.cyTopHeight = -1;
            margins.cyBottomHeight = -1;

            ExtendFrame(mainWindowSrc.Handle, margins);
        }
    }
}
