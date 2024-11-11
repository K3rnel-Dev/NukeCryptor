using System;

namespace NukeBuilder.Algorithms.FormsHelper
{
    internal class MainElementor
    {
        public static string PrintDateInfo()
        {
            string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string userName = Environment.UserName;
            return $"{currentDateTime} - {userName}";
        }
    }
}
