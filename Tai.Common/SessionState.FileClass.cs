using System;
using System.Collections.Generic;
using System.Text;

namespace Tai.Common
{
    public partial class SessionState
    {
        public class FileClass
        {
            private SessionState _Session;

            public FileClass(SessionState session)
            {
                _Session = session;
            }

            public string StartChecklist
            {
                get
                {
                    return _Session.Folders.PersonalitySystem + "StartCheckList.cld";
                }
            }

            public string ModuleChecklist
            {
                get
                {
                    return _Session.Folders.PersonalitySystem + "ModuleCheckList.cld";
                }
            }

            public string LinkChecklist
            {
                get
                {
                    return _Session.Folders.PersonalitySystem + "LinkCheckList.cld";
                }
            }

            public string EndChecklist
            {
                get
                {
                    return _Session.Folders.PersonalitySystem + "EndCheckList.cld";
                }
            }
        }
    }

}
