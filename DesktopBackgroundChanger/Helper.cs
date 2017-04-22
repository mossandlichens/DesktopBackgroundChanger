// <copyright file="Helper.cs" company="Moss and Lichens">
//     Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace DesktopBackgroundChanger
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Runtime.InteropServices;
    using Microsoft.Win32;

    /// <summary>
    ///     Helper class for desktop background changing
    /// </summary>
    public class Helper
    {
        /// <summary>
        ///     Application Data Path
        /// </summary>
        private static string applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        /// <summary>
        ///     Specific folder in Application Data Path
        /// </summary>
        private static string localApplicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DesktopBackgroundChanger";

        /// <summary>
        ///     Gets the Application Data Path
        /// </summary>
        public static string ApplicationDataPath
        {
            get { return Helper.applicationDataPath; }
        }

        /// <summary>
        ///     Gets the Specific folder in Application Data Path
        /// </summary>
        public static string LocalApplicationDataPath
        {
            get { return Helper.localApplicationDataPath; }
        }

        /// <summary>
        ///     Check if connected to Internet
        /// </summary>
        /// <returns>true if connected</returns>
        public static bool IsConnectedToInternet()
        {
            int desc;
            return InternetGetConnectedState(out desc, 0);
        }

        /// <summary>
        ///     Get the value of the key
        /// </summary>
        /// <param name="key">Key variable</param>
        /// <returns>Key value in config</returns>
        public static string GetAppSettingsValue(string key)
        {
            return new System.Configuration.AppSettingsReader().GetValue(key, typeof(string)).ToString();
        }

        /// <summary>
        ///     Download the page source to the application data
        /// </summary>
        /// <param name="pageUrl">Page Url to download</param>
        public static void DownloadPage(string pageUrl)
        {
            WebClient webClient = new WebClient();
            try
            {
                webClient.DownloadFile(new Uri(pageUrl), localApplicationDataPath + "\\PageSource.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine("DesktopBackgroundChanger:DownloadPage failed - " + e.Message);
            }
        }

        /// <summary>
        ///     Set the Internet Explorer Wallpaper
        /// </summary>
        /// <param name="wallpaperLocation">location of the wallpaper</param>
        /// <param name="wallpaperStyle">style of the wallpaper</param>
        /// <param name="tileWallpaper">title of the wallpaper</param>
        public static void SetWallpaper(string wallpaperLocation, int wallpaperStyle, int tileWallpaper)
        {
            // Sets the actual wallpaper
            SystemParametersInfo(20, 0, wallpaperLocation, 0x01 | 0x02);

            // Set the wallpaper style to streched (can be changed to tile, center, maintain aspect ratio, etc.
            RegistryKey wallPaper = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", true);

            // Sets the wallpaper style
            // rkWallPaper.SetValue("WallpaperStyle", WallpaperStyle);
            // Whether or not this wallpaper will be displayed as a tile
            // rkWallPaper.SetValue("TileWallpaper", TileWallpaper);
            wallPaper.Close();
        }

        /// <summary>
        ///     Convert the JPEG file to BMP
        /// </summary>
        public static void ConvertJPEGToBMP()
        {
            using (Image img = Image.FromFile(localApplicationDataPath + "\\BackgroundImage.jpg"))
            {
                img.Save(localApplicationDataPath + "\\Internet Explorer Wallpaper.bmp", ImageFormat.Bmp);
            }
        }

        /// <summary>
        ///     Download the background image to the Application Data
        /// </summary>
        /// <param name="backgroundImageLink">link to background image</param>
        public static void DownloadBackgroundImage(string backgroundImageLink)
        {
            WebClient webClient = new WebClient();
            try
            {
                webClient.DownloadFile(new Uri(backgroundImageLink), localApplicationDataPath + "\\BackgroundImage.jpg");
            }
            catch (Exception e)
            {
                Console.WriteLine("DesktopBackgroundChanger:DownloadBackgroundImage failed - " + e.Message);
            }
        }

        /// <summary>
        ///     Delete the input file
        /// </summary>
        /// <param name="file">file to delete</param>
        public static void DeleteFile(string file)
        {
            File.Delete(file);
        }

        /// <summary>
        ///     Copy the source to target
        /// </summary>
        /// <param name="source">source file</param>
        /// <param name="target">target file</param>
        public static void CopyFile(string source, string target)
        {
            File.Copy(source, target);
        }

        /// <summary>
        ///     Check if Network is available
        /// </summary>
        /// <returns>true if Network is available</returns>
        public static bool IsNetworkAvailable()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }

        /// <summary>
        ///     Create Image Description as html file
        /// </summary>
        /// <param name="imageDescription">contents of the html file</param>
        public static void CreateImageDescription(string imageDescription)
        {
            StreamWriter sw = File.CreateText(localApplicationDataPath + "\\ImageDescription.htm");
            sw.Write(imageDescription);
            sw.Flush();
            sw.Close();            
        }

        /// <summary>
        ///     Get System Parameters Info
        /// </summary>
        /// <param name="action">System Parameters Info Action</param>
        /// <param name="param">System Parameters Info Param</param>
        /// <param name="lpvParam">System Parameters Info lpvParam</param>
        /// <param name="winIni">System Parameters Info winIni</param>
        /// <returns>System Parameters Info</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int action, int param, string lpvParam, int winIni);

        /// <summary>
        ///     Get the Internet Connection State
        /// </summary>
        /// <param name="description">Internet Connection State Description</param>
        /// <param name="reservedValue">Internet Connection State Reserved Value</param>
        /// <returns>true if the state is connected</returns>
        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int description, int reservedValue);                
    }
}
