using System;
using System.Reflection;

namespace ITechArt.Common.Utils
{
    public static class AppVersionUtil
    {
        public static string GetAppVersion()
        {
            return Assembly.GetEntryAssembly()?.GetName().Version?.ToString();
        }
    }
}
