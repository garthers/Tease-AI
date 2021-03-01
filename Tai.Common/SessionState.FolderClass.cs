using System.IO;
namespace Tai.Common
{
    public partial class SessionState
    {
        public class FoldersClass
        {
            private SessionState _Parent;
            private string _AppPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            public FoldersClass(SessionState parent)
            {
                _Parent = parent;
            }



            /// <summary>
            /// 		''' Returns the Path for the selected personality. Ends with Backslash!
            /// 		''' </summary>
            /// 		''' <returns>The Path for the selected personality. Ends with Backslash!</returns>
            public string Personality
            {
                get
                {
                    return string.Format(@"{0}\Scripts\{1}\", _AppPath, _Parent.DomPersonality);
                }
            }

            public string PersonalitySystem
            {
                get
                {
                    return string.Format(@"{0}System\", Personality);
                }
            }
            /// <summary>
            /// 		''' Returns the Path for the selected personalities flags. Ends with Backslash!
            /// 		''' </summary>
            /// 		''' <returns>The Path for the selected personalities flags. Ends with Backslash!</returns>
            public string Flags
            {
                get
                {
                    return string.Format(@"{0}System\Flags\", Personality);
                }
            }
            /// <summary>
            /// 		''' Returns the Path for the selected personalities temporary flags. Ends with Backslash!
            /// 		''' </summary>
            /// 		''' <returns>The Path for the selected personalities temporary flags. Ends with Backslash!</returns>
            public string TempFlags
            {
                get
                {
                    return string.Format(@"{0}Temp\", Flags);
                }
            }
            /// <summary>
            /// 		''' Returns the Path for the selected personalities variables. Ends with Backslash!
            /// 		''' </summary>
            /// 		''' <returns>The Path for the selected personalities variables. Ends with Backslash!</returns>
            public string Variables
            {
                get
                {
                    return string.Format(@"{0}System\Variables\", Personality);
                }
            }

            public string StartScripts
            {
                get
                {
                    return string.Format(@"{0}Stroke\Start\", Personality);
                }
            }

            public string LinkScripts
            {
                get
                {
                    return string.Format(@"{0}Stroke\Link\", Personality);
                }
            }

            public string ModuleScripts
            {
                get
                {
                    return string.Format(@"{0}Modules\", Personality);
                }
            }

            public string EndScripts
            {
                get
                {
                    return string.Format(@"{0}Stroke\End\", Personality);
                }
            }
        }
    }
}