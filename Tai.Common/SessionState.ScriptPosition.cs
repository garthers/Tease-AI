using System;
using System.IO;
namespace Tai.Common
{
    public partial class SessionState
    {
        [Serializable]
        public abstract class ScriptPosition
        {
            public SessionState Session { get; set; } = null;

            private string _FilePath = null;
            /// <summary>Get or sets the Filepath. </summary>
            /// 		''' <returns>Returns the absolute filepaths.</returns>
            /// 		''' <remarks>If the filepath is within the persomality path, it is stored internal as relative path. 
            /// 		''' This is done to support moving of application-folder between serializing and deserializing.</remarks>
            public string FilePath
            {
                get
                {
                    if (Path.IsPathRooted(_FilePath))
                        return _FilePath;
                    else
                        return Session.Folders.Personality + _FilePath;
                }
                set
                {
                    if (Path.IsPathRooted(value) && value.StartsWith(Session.Folders.Personality, StringComparison.OrdinalIgnoreCase))
                        _FilePath = value.Replace(Session.Folders.Personality, "");
                    else
                        _FilePath = value;
                }
            }

            public int Line { get; set; } = -1;

            public bool GotoStatus { get; set; } = false;

            public string LineGoTo { get; set; } = "";

            public string ReturnState { get; set; } = "";

            /// <summary>Creates a new instance with given parameters.</summary>

            public ScriptPosition(SessionState session, string filepath, int line, bool goToStatus, string lineGoTo, string returnState)
            {
                this.Session = session;

                this.FilePath = filepath;

                this.Line = line;

                this.GotoStatus = goToStatus;

                this.LineGoTo = lineGoTo;

                this.ReturnState = returnState;
            }
        }
    }

}