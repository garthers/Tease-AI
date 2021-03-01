using System;
using System.Collections.Generic;
using System.Text;

namespace Tai.Common
{
    internal class My
    {
        public static Settings Settings => Settings.Default;
    }

    internal class Form1
    {
        public static SessionState ssh { get; set; }
    }

    internal static class FrmSettings {
        public static class TbxDomImageDir
        {
            public static string Text { get; set; }
        }
    }
}
