namespace DesktopBackgroundChangerUI
{
    using System;
    using System.IO;
    using System.Windows.Media;

    public class MainWindowViewModel : ViewModelBase
    {
        /// <summary>
        ///     Specific folder in Application Data Path
        /// </summary>
        private string localApplicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DesktopBackgroundChanger";

        private ImageSource desktopBackgroundImage;

        public ImageSource DesktopBackgroundImage
        {
            get { return this.desktopBackgroundImage; }
            set { this.desktopBackgroundImage = value; }
        }

        private string imageDescriptionHtmlString;

        public string ImageDescriptionHtmlString
        {
            get { return this.imageDescriptionHtmlString; }
            set { this.imageDescriptionHtmlString = value; }
        }

        public MainWindowViewModel()
        {
            StreamReader streamReader = File.OpenText(localApplicationDataPath + "\\ImageDescription.htm");
            this.ImageDescriptionHtmlString = streamReader.ReadToEnd();
            ImageSourceConverter imageSourceConverter = new ImageSourceConverter();
            this.DesktopBackgroundImage = (ImageSource)imageSourceConverter.ConvertFromString(localApplicationDataPath + "\\BackgroundImage.jpg");
        }
    }
}
