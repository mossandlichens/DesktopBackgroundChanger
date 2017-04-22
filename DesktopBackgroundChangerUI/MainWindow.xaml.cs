namespace DesktopBackgroundChangerUI
{
    using System.Windows;
    using DesktopBackgroundChangerUI.Properties;
    using SimpleBrowser;
    using System.Diagnostics;
    using System.Windows.Media;
    using System.Windows.Documents;
    using System.Collections.Generic;
    using System.Xml.Linq;
    using System;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool shouldClose;

        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void MenuItemAbout_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Settings.Default.ApplicationWebsite);
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            shouldClose = true;
            Application.Current.Shutdown();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (shouldClose)
            {
                base.OnClosing(e);
            }
            else
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void MenuItemUpdatePhotoOfTheDay_Click(object sender, RoutedEventArgs e)
        {
            if (Helper.IsNetworkAvailable() == false
                    || Helper.IsConnectedToInternet() == false)
            {
                MessageBox.Show("Internet not available. Please try later.");
                return;
            }

            var browser = new Browser();
            browser.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US) AppleWebKit/534.10 (KHTML, like Gecko) Chrome/8.0.552.224 Safari/534.10";
            browser.Navigate(Settings.Default.NG_Default);

            var primary_photo = browser.Find("div", FindBy.Class, "primary_photo");
            var caption = browser.Find("div", FindBy.Id, "caption");
            var pTags = caption.Select("p");
            string wallpaperCaption = string.Empty;
            for (int i = 0; i < pTags.TotalElementsFound; i++)
            {
                if (i == 3)
                {
                    wallpaperCaption = pTags.Value;
                }
                pTags.Next();
            }
                                                            
            string backgroundImageLink = primary_photo.Select("img").GetAttribute("src");
            Helper.DownloadBackgroundImage(backgroundImageLink);
            string internetExplorerWallpaper = Helper.ApplicationDataPath + "\\Microsoft\\Internet Explorer\\Internet Explorer Wallpaper.bmp";
            Helper.DeleteFile(internetExplorerWallpaper);
            string newWallpaper = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DesktopBackgroundChanger" + "\\BackgroundImage.jpg";

            this.imageDesktopBackground.Source = Helper.GetBitmapImage(newWallpaper);
            this.textBlockCaption.Text = wallpaperCaption;

            Helper.CopyFile(newWallpaper, internetExplorerWallpaper);
            Helper.SetWallpaper(internetExplorerWallpaper, 2, 0);
        }

    }
}
