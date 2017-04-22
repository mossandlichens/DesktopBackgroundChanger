// -----------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DesktopBackgroundChangerUI
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class NativeMethods
    {
        /// <summary>
        ///     Get System Parameters Info
        /// </summary>
        /// <param name="action">System Parameters Info Action</param>
        /// <param name="param">System Parameters Info Param</param>
        /// <param name="lpvParam">System Parameters Info lpvParam</param>
        /// <param name="winIni">System Parameters Info winIni</param>
        /// <returns>System Parameters Info</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern int SystemParametersInfo(int action, int param, string lpvParam, int winIni);

        /// <summary>
        ///     Get the Internet Connection State
        /// </summary>
        /// <param name="description">Internet Connection State Description</param>
        /// <param name="reservedValue">Internet Connection State Reserved Value</param>
        /// <returns>true if the state is connected</returns>
        [DllImport("wininet.dll")]
        internal static extern bool InternetGetConnectedState(out int description, int reservedValue);
    }
}
