// <copyright file="Program.cs" company="Moss and Lichens">
//     Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace DesktopBackgroundChanger
{
    using System;
    using System.Diagnostics;

    /// <summary>
    ///     Console class for Desktop Background Changer
    /// </summary>
    public class Program
    {
        /// <summary>
        ///     Main entry point to the program
        /// </summary>
        /// <param name="args">Arguments to select the category of photo</param>
        public static void Main(string[] args)
        {
            try
            {
                // Uninstall code
                if (args.Length == 2 && args[0] == "-uninstall")
                {
                    string productCode = args[1];
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.System);
                    ProcessStartInfo uninstallProcess = new ProcessStartInfo(path + "\\msiexec.exe ", "/x " + productCode);
                    Process.Start(uninstallProcess);
                    return;
                }

                Console.WriteLine("National Geographic | Desktop Background Changer");
                if (Helper.IsNetworkAvailable() == false
                    || Helper.IsConnectedToInternet() == false)
                {
                    Console.WriteLine("Internet not available");
                    Console.Read();
                    return;
                }

                // Photo of the Day Categories
                // 1. Animals
                // 2. Adventure and Exploration
                // 3. Black and White
                // 4. History
                // 5. Landscapes
                // 6. Nature and Weather
                // 7. People and Culture
                // 8. Science and Space
                // 9. Travel
                // 10. Underwater
                string pageUrl = string.Empty;

                if (args.Length > 0)
                {
                    string category = args[0];
                    string categoryUrl = NationalGeographicHelper.GetCategoryUrl(category);
                    Console.WriteLine("Downloading category page...");
                    Helper.DownloadPage(categoryUrl);
                    Console.WriteLine("Getting page...");
                    pageUrl = NationalGeographicHelper.GetPageUrl(categoryUrl);
                }

                Console.WriteLine("Downloading page...");
                if (pageUrl == string.Empty)
                {
                    pageUrl = Helper.GetAppSettingsValue("NG_Default");
                }

                // Image
                Helper.DownloadPage(pageUrl);
                string backgroundImageLink = NationalGeographicHelper.ExtractBackgroundImageLink();
                Console.WriteLine("Downloading image...");
                Helper.DownloadBackgroundImage(backgroundImageLink);
                Console.WriteLine("Converting image...");
                Helper.ConvertJPEGToBMP();

                // Image Description
                string imageDescription = NationalGeographicHelper.ExtractImageDescription();
                Console.WriteLine("Creating Image Description...");
                Helper.CreateImageDescription(imageDescription);
                
                // Wallpaper
                Console.WriteLine("Changing Wallpaper...");
                string internetExplorerWallpaper = Helper.ApplicationDataPath + "\\Microsoft\\Internet Explorer\\Internet Explorer Wallpaper.bmp";
                Helper.DeleteFile(internetExplorerWallpaper);
                Helper.CopyFile(Helper.LocalApplicationDataPath + "\\Internet Explorer Wallpaper.bmp", internetExplorerWallpaper);
                Helper.SetWallpaper(internetExplorerWallpaper, 2, 0);
                Console.WriteLine("Wallpaper changed successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine("DesktopBackgroundChanger:Main failed - " + e.Message);
                Console.Read();
            }
        }        
    }
}
