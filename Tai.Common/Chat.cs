using System;
using System.Collections.Generic;
using System.Text;

namespace Tai.Common
{
    // ===========================================================================================
    // 
    // Chat.vb
    // 
    // This file contains functions and methods to write and update the Chat.
    // 
    // ===========================================================================================

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
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;


    public interface IChatUI
    {
        void SendToChat(string bodyText);
        void SendToRiskyChat(object textToSet);
    }
    public class Chat
    {
        private readonly string _rootPath;
        private readonly SessionState _ssh;
        private readonly ISettings _settings;
        private readonly IChatUI _chatui;

        public Chat(string rootPath, ISettings settings, SessionState ssh, IChatUI chatui)
        {
            _rootPath = rootPath;
            _ssh = ssh;
            _settings = settings;
            _chatui = chatui;
        }
        private string SaveDir => Path.Combine(_rootPath, "Chatlogs");
        private void SaveChatLog(string documentText, bool IsAutosave)
        {
            if (documentText.Length > 300)
            {
                System.IO.Directory.CreateDirectory(SaveDir);

                if (IsAutosave &&  _settings.CBAutosaveChatlog)
                    File.WriteAllText(Path.Combine(SaveDir, "Autosave.html"), documentText);
                else if (!IsAutosave && _settings.CBExitSaveChatlog)
                    File.WriteAllText(Path.Combine(SaveDir, DateTime.Now.ToString("MM.dd.yyyy hhmm") + " chatlog.html"), documentText);
            }
        }

        private string ChatGetCssClassFromName(string name)
        {
            if (name == _settings.SubName)
                return "sub";
            else if (name == _settings.DomName)
                return "domme";
            else if (name == _settings.Glitter1)
                return "contact1";
            else if (name == _settings.Glitter2)
                return "contact2";
            else if (name == _settings.Glitter3)
                return "contact3";
            else
                return "random";
        }

        public void ChatAddMessage(string name, string message, bool delayOutput = false)
        {
            ChatAppend(@"<div class=""message"">
	<div class=""" + ChatGetCssClassFromName(name) + @""">
		<span class=""timestamp"">" + (DateTime.Now.ToString("hh:mm tt ")) + @"</span>
		<span class=""name"">" + name + @": </span>
		<span class=""text"">" + message + @"</span>
	</div>
</div>" + Environment.NewLine, delayOutput);
        }

        public void ChatAddEmoteMessage(string name, string message, bool delayOutput = false)
        {
            ChatAppend(@"<div class=""emoteMessage"">
    <div class=""" + ChatGetCssClassFromName(name) + @""">
		<span class=""timestamp"">" + (DateTime.Now.ToString("hh:mm tt ")) + @"</span>
		<span class=""name"">" + name + @": </span>
		<span class=""text"">" + message + @"</span>
	</div>
</div>" + Environment.NewLine, delayOutput);
        }

        /// <summary>Appends a system message to chat and prints it if desired. </summary>
        /// 	''' <param name="messageText">Messagetext to append to chat.</param>
        /// 	''' <param name="delayOutput">If true the chatwindow-content won't change until despired.</param>
        public void ChatAddSystemMessage(string messageText, bool delayOutput = false)
        {
            ChatAppend(@"<div class=""systemMessage"">
	<span>
		" + messageText + @"
	</span>
</div>", delayOutput, true);
        }

        public void ChatAddWritingTaskInfo(string message, bool delayOutput = false)
        {
            ChatAppend(@"<div class=""writingTaskInfo"">
	<span>
		" + message + @"
	</span>
</div>", delayOutput, false);
        }

        public void ChatAddScriptPosInfo(string descr, bool delayOutput = false)
        {
            ChatAppend(@"<div class=""scriptPosInfo"">
	<span>::: " + descr == "@" ? "TYPO" : descr.Replace("@", "") + @" ::: <br>
		::: FileText = " + _ssh.FileText + " ::: LineVal = " + _ssh.StrokeTauntVal + @"<br>
		::: TauntText = " + _ssh.TauntText + " ::: LineVal = " + _ssh.TauntTextCount + @"<br>
		::: ResponseFile = " + _ssh.ResponseFile + " ::: LineVal = " + _ssh.ResponseLine + @"
	</span>
</div>", delayOutput, true);
        }

        /// <summary>Marks the given text as inline warning.</summary>
        /// 	''' <param name="textToMark">The text to mark.</param>
        /// 	''' <returns>Returns the given text and the necessary HTML-Tags.</returns>
        public string ChatGetInlineWarning(string textToMark)
        {
            return "<span class=\"inlineWarning\">" + textToMark + "</span>";
        }

        public void ChatAddWarning(string msg)
        {
            ChatAppend(@"<div class=""warning"">
	<span class=""msg"">WARNING: " + msg + @"</span>
</div>", false, true);
        }

        /// <summary>Marks the given text as inline error.</summary>
        /// 	''' <param name="textToMark">The text to mark.</param>
        /// 	''' <returns>Returns the given text and the necessary HTML-Tags.</returns>
        public string ChatGetInlineError(string textToMark)
        {
            return "<span class=\"inlineError\">" + textToMark + "</span>";
        }

        public void ChatAddException(string msg, Exception ex)
        {
            ChatAppend(@"<div class=""exception"">
	<span class=""msg"">ERROR: " + msg + @"</span>
	<span class=""exMessage"">::: Exception: " + ex.Message + @"</span>
</div>", false, true);
        }

        public void ChatAppend(string elementText, bool delayOutput = false, bool linkify = false)
        {
            if (linkify == true)
            {
                Regex re = new Regex(@"[a-zA-Z]:\\(((?![<>:""/\\|?*]).)+((?<![ .])\\)?)*");
                string ReplacePattern = "<a href=\"file://$&\">$&</a>";

                elementText = re.Replace(elementText, ReplacePattern);
            }

            _ssh.Chat += elementText + "" + Environment.NewLine;
            if (delayOutput == false)
                this.ChatUpdate();
        }

        public void ChatClear()
        {
            _ssh.Chat = "";
            ChatUpdate();
        }

        private string CssPath => Path.Combine(_rootPath, "System", "CSS", "ChatWindow.css");

        internal void ChatUpdate()
        {
            /*
            if (this.InvokeRequired)
            {
                this.Invoke(() => ChatUpdate());
                return;
            }*/

            bool DommeTyping = false;
            var C1Typing = false;
            var C2Typing = false;
            var C3Typing = false;
            var RndTyping = false;

            if (_ssh.IsTyping)
            {
                if (_ssh.tempDomName == _settings.DomName)
                    DommeTyping = true;
                else if (_ssh.tempDomName == _settings.Glitter1)
                    C1Typing = true;
                else if (_ssh.tempDomName == _settings.Glitter2)
                    C2Typing = true;
                else if (_ssh.tempDomName == _settings.Glitter3)
                    C3Typing = true;
                else
                    RndTyping = true;
            }

            // ===============================================================================
            // Generate stylesheet
            // ===============================================================================
            string Style = CssTryGetFile(CssPath, "fix-me-if-necessary"); // My.Resources.ChatFallbackStyle);
            Style = CssReplaceSettings(Style) + Environment.NewLine;

            Style += @"	/*				--- Visibility Section ---					*/
/* This section is generated by code and added automatically*/

.exception {" + (_settings.CBOutputErrors ? "visibility:visible; display:initial;" : "visibility:hidden; display:none") + @"}
.warning {" + (_settings.CbChatDisplayWarnings ? "visibility:visible; display:initial;" : "visibility:hidden; display:none") + @"}
.writingTaskInfo {" + (_settings.WritingProgress ? "visibility:visible; display:initial;" : "visibility:hidden; display:none") + @"}

.timestamp { " + (_settings.CBTimeStamps ? "visibility: visible; display:initial;" : "visibility:hidden; display:none") + @"}
.name {" + (_settings.CBShowNames ? "visibility:visible; display:initial;" : "visibility:hidden; display:none") + @"}
#DommeIsTyping {" + (DommeTyping ? "visibility:visible; display:initial;" : "visibility:hidden; display:none") + @"}
#Contact1IsTyping {" + (C1Typing ? "visibility:visible; display:initial;" : "visibility:hidden; display:none") + @"}
#Contact2IsTyping {" + (C2Typing ? "visibility:visible; display:initial;" : "visibility:hidden; display:none") + @"}
#Contact3IsTyping {" + (C3Typing ? "visibility:visible; display:initial;" : "visibility:hidden; display:none") + @"}
#RandomIsTyping {" + (RndTyping ? "visibility:visible; display:initial;" : "visibility:hidden; display:none") + "}";

            // ===============================================================================
            // Generate body Text
            // ===============================================================================
            string BodyText = "<div id=\"Chat\">" + Environment.NewLine + _ssh.Chat + Environment.NewLine + "</div>" + "<div id=\"DommeIsTyping\">" + _settings.DomName + @" is typing...</div>
<div id=""Contact1IsTyping"">" + _settings.Glitter1 + @" is typing...</div>
<div id=""Contact2IsTyping"">" + _settings.Glitter2 + @" is typing...</div>
<div id=""Contact3IsTyping"">" + _settings.Glitter3 + @" is typing...</div>
<div id=""RandomIsTyping"">Unknown user is typing...</div>";

            // ===============================================================================
            // Page Output
            // ===============================================================================
            try
            {
                /*
                var GetLastMessage = WebBrowser x =>
                {
                    if (x.Document == null)
                        return "";

                    if (x.Document.GetElementById("Chat") != null)
                    {
                        {
                            var withBlock = x.Document.GetElementById("Chat");
                            if (withBlock.CanHaveChildren && withBlock.Children.Count > 0)
                                return withBlock.Children(withBlock.Children.Count - 1).OuterHtml;
                        }
                    }
                    return "";
                };
                */

                _chatui.SendToChat(BodyText);
                /*
                string TextToSet = HtmlBuildPage("Chat", Style, BodyText);

                try
                {
                    if (_settings.CBWebtease)
                    {
                        // ################### Webtease Mode ###########################
                        WebBrowser Helper = new WebBrowser();
                        Helper.DocumentText = TextToSet;
                        while (!Helper.ReadyState == WebBrowserReadyState.Complete)
                            Application.DoEvents();

                        string LP = GetLastMessage(Helper);

                        TextToSet = TextToSet.Replace(_ssh.Chat, LP);
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError("Unable to determine last chat message to create webtease output.", ex, "Output WebTease Chat failed");
                }

                ChatText.DocumentText = TextToSet;
                ChatText2.DocumentText = TextToSet;
                */
                // ChatReadyState();
                //SaveChatLog(true);

                if (_ssh.RiskyDeal)
                {
                    _chatui.SendToRiskyChat(BodyText);
                    //FrmCardList.WBRiskyChat.DocumentText = TextToSet.Replace(_ssh.Chat, GetLastMessage(ChatText));
                }
            }
            catch (COMException ex) when (ex.ErrorCode == -2147024726)
            {
                // ▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                // Unable to access Webbrowser
                // ▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                //MessageBox.Show("Unable to access the Webbrowser.", "Update Chat failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        /*
        public void ScrollChatDown()
        {
            try
            {
                ChatText.Document.Window.ScrollTo(Int16.MaxValue, Int16.MaxValue);
            }
            catch
            {
            }

            try
            {
                ChatText2.Document.Window.ScrollTo(Int16.MaxValue, Int16.MaxValue);
            }
            catch
            {
            }
        }
        */

        private string CssTryGetFile(string filePath, string fallback)
        {
            if (System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(filePath)) && System.IO.File.Exists(filePath))
                return System.IO.File.ReadAllText(filePath);
            else
                return fallback;
        }

        private string CssReplaceSettings(string input)
        {
            string HtmlSizeToEm(int x) 
            {
                if (x <= 1)
                    return "0.63em";
                else if (x <= 2)
                    return "0.82em";
                else if (x <= 3)
                    return "1.00em";
                else if (x <= 4)
                    return "1.13em";
                else if (x <= 5)
                    return "1.5em";
                else if (x <= 6)
                    return "2em";
                else if (x <= 7)
                    return "3em";
                else
                    return "4em";
            };

            input = input.Replace("/*ChatWindowColor*/", Common.Color2Html(_settings.ChatWindowColor));
            input = input.Replace("/*ChatTextColor*/", Common.Color2Html(_settings.ChatTextColor));
            input = input.Replace("/*SubNameColor*/", _settings.SubColor);
            input = input.Replace("/*DommeNameColor*/", _settings.DomColor);
            input = input.Replace("/*Contact1NameColor*/", Common.Color2Html(_settings.GlitterNC1Color));
            input = input.Replace("/*Contact2NameColor*/", Common.Color2Html(_settings.GlitterNC2Color));
            input = input.Replace("/*Contact3NameColor*/", Common.Color2Html(_settings.GlitterNC3Color));

            input = input.Replace("/*DommeFontSize*/", HtmlSizeToEm(_settings.DomFontSize));
            input = input.Replace("/*SubFontSize*/", HtmlSizeToEm(_settings.SubFontSize));

            input = input.Replace("/*DommeFontName*/", _settings.DomFont);
            input = input.Replace("/*SubFontName*/", _settings.SubFont);

            return input;
        }

        /*
        private void ChatText_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            ScrollChatDown();
        }
        */
        /*
        public void ChatReadyState()
        {
            while (ChatText.ReadyState != WebBrowserReadyState.Complete | ChatText2.ReadyState != WebBrowserReadyState.Complete)
                Application.DoEvents();
            ScrollChatDown();
        }
        */
        /*
        private void ChatText_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (e.Url.AbsolutePath != "blank")
            {
                if (e.Url.IsFile)
                    ShellExecute(e.Url.LocalPath);
                else
                    ShellExecute(e.Url.AbsolutePath);
                e.Cancel = true;
            }
        }
        */
    }
}
