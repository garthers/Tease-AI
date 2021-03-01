using System;
using System.Collections.Generic;
using System.Text;

namespace Tai.Common
{
    [Serializable]
    public class Mode
    {
        public string Keyword = "";
        public string Type = "";
        public string GotoLine = "";
        public string MessageText = "";
        public bool VideoMode = false;
        public bool GotoMode = false;
        public bool MessageMode = false;

        public void Clear()
        {
            Keyword = "";
            Type = "";
            GotoLine = "";
            MessageText = "";
            VideoMode = false;
            GotoMode = false;
            MessageMode = false;
        }
    }
}
