using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericFW
{
    public static class UploadUtils
    {
        static HashSet<string> kabulEdilenTurler = new HashSet<string>
        {
            ".jpeg", ".gif" ,".jpg", ".png"
        };

        public static bool IsImage(string extension)
        {
            return kabulEdilenTurler.Contains(extension);
        }
    }
}