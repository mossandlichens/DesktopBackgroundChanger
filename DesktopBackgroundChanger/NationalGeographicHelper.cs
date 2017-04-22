// <copyright file="NationalGeographicHelper.cs" company="Moss and Lichens">
//     Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace DesktopBackgroundChanger
{
    /// <summary>
    ///     Helper with specific string parsing for National Geographic
    /// </summary>
    public class NationalGeographicHelper
    {
        /// <summary>
        ///     Get the page url for the category
        /// </summary>
        /// <param name="categoryUrl">Category Url</param>
        /// <returns>Page Url of the Category</returns>
        public static string GetPageUrl(string categoryUrl)
        {
            string result = string.Empty;

            string pageSource = System.IO.File.ReadAllText(Helper.LocalApplicationDataPath + "\\PageSource.txt");

            int posSearchResults = pageSource.IndexOf("search_results");

            int posPageUrlStart = pageSource.IndexOf("href=", posSearchResults);
            int posPageUrlEnd = pageSource.IndexOf(">", posPageUrlStart);

            result = "http://photography.nationalgeographic.com" + pageSource.Substring(posPageUrlStart + 6, posPageUrlEnd - posPageUrlStart - 7);

            return result;
        }

        /// <summary>
        ///     Get the link to the background image
        /// </summary>
        /// <returns>background image link</returns>
        public static string ExtractBackgroundImageLink()
        {
            string result = string.Empty;

            string pageSource = System.IO.File.ReadAllText(Helper.LocalApplicationDataPath + "\\PageSource.txt");

            int posPrimaryPhoto = pageSource.IndexOf("primary_photo");

            int posImageStart = pageSource.IndexOf("src=", posPrimaryPhoto);
            int posImageEnd = pageSource.IndexOf(".jpg", posPrimaryPhoto);

            result = pageSource.Substring(posImageStart + 5, posImageEnd + 4 - posImageStart - 5);

            return result;
        }

        /// <summary>
        ///     Get the Category Page Url
        /// </summary>
        /// <param name="category">type of category</param>
        /// <returns>Category Url</returns>
        public static string GetCategoryUrl(string category)
        {
            string result = string.Empty;

            switch (category)
            {
                case "1":
                    result = Helper.GetAppSettingsValue("NG_Animals");
                    break;
                case "2":
                    result = Helper.GetAppSettingsValue("NG_Adventure");
                    break;
                case "3":
                    result = Helper.GetAppSettingsValue("NG_History");
                    break;
                case "4":
                    result = Helper.GetAppSettingsValue("NG_BlackAndWhite");
                    break;
                case "5":
                    result = Helper.GetAppSettingsValue("NG_Landscapes");
                    break;
                case "6":
                    result = Helper.GetAppSettingsValue("NG_Nature");
                    break;
                case "7":
                    result = Helper.GetAppSettingsValue("NG_People");
                    break;
                case "8":
                    result = Helper.GetAppSettingsValue("NG_Science");
                    break;
                case "9":
                    result = Helper.GetAppSettingsValue("NG_Travel");
                    break;
                case "10":
                    result = Helper.GetAppSettingsValue("NG_Underwater");
                    break;
            }

            return result;
        }

        /// <summary>
        ///     Get the div tag which contains the image description
        /// </summary>
        /// <returns>image description</returns>
        public static string ExtractImageDescription()
        {
            string result = string.Empty;

            string pageSource = System.IO.File.ReadAllText(Helper.LocalApplicationDataPath + "\\PageSource.txt");

            int posImageDescriptionTagStart = pageSource.IndexOf("<div id=\"caption\">");
            int posImageDescriptionTagEnd = pageSource.IndexOf("</div>", posImageDescriptionTagStart);
            
            result = pageSource.Substring(posImageDescriptionTagStart, posImageDescriptionTagEnd + 6 - posImageDescriptionTagStart);

            return result;
        }
    }
}
