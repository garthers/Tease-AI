using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Tai.Common
{
    public partial class SessionState
    {
        public bool DommeTyping { get; internal set; }
        public bool C1Typing { get; internal set; }
        public bool C2Typing { get; internal set; }
        public bool C3Typing { get; internal set; }
        public bool RndTyping { get; internal set; }

        [Serializable]
        public class StackedCallReturn : ScriptPosition
        {

            // store if the user was in yesorno mode when callreturn was called
            private bool yesOrNostate;

            // store wheter we are currently running a module or we are in a stroking cycle
            private bool showingModule;

            // store the isLink status (useful for the "safenet" that will allow to start the stroke cycle even if the script doesn't contain a @StartStroking/Taunt
            // when it reaches the end of a link/beforeScript)
            private bool isALink;

            // store the rapidcode status so we can resume it when coming back if the script was in this mode
            private bool rapidText, rapidCode;

            // store all the modes variables so we can reset them on coming back
            private Mode edgeMode = new Mode(), ruinMode = new Mode(), cameMode = new Mode(), yesMode = new Mode(), noMode = new Mode();
            private Dictionary<string, Mode> customModes = new Dictionary<string, Mode>();

            public StackedCallReturn(SessionState session) : base(session, session.FileText, session.StrokeTauntVal, session.GotoFlag, session.FileGoto, session.ReturnSubState)
            {
                yesOrNostate = session.YesOrNo;
                edgeMode = session.edgeMode;
                cameMode = session.cameMode;
                ruinMode = session.ruinMode;
                yesMode = session.yesMode;
                noMode = session.noMode;
                customModes = session.Modes;
                showingModule = session.ShowModule;
                isALink = session.isLink;
                rapidCode = session.RapidCode;
                rapidText = session.RapidFire;
            }
            public void resumeState()
            {
                Session.StrokeTauntVal = Line;
                Session.FileText = FilePath;
                Session.ReturnSubState = ReturnState;
                Session.GotoFlag = GotoStatus;
                Session.FileGoto = LineGoTo;
                Session.YesOrNo = yesOrNostate;
                Session.edgeMode = edgeMode;
                Session.cameMode = cameMode;
                Session.ruinMode = ruinMode;
                Session.yesMode = yesMode;
                Session.noMode = noMode;
                Session.Modes = customModes;
                Session.ShowModule = showingModule;
                Session.isLink = isALink;
                Session.RapidCode = rapidCode;
                Session.RapidFire = rapidText;
            }
        }
    }
}