using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;

namespace Tai.Common
{
#if false
    public partial class Engine
    {

        private readonly Timer ScriptTimer;
        private readonly Timer StrokeTimer;
        private readonly Timer StrokeTauntTimer;
        private readonly Timer EdgeTauntTimer;
        private readonly Timer EdgeCountTimer;
        private readonly Timer HoldEdgeTimer;
        private readonly Timer HoldEdgeTauntTimer;
        private readonly Timer CensorshipTimer;
        private readonly Timer RLGLTimer;
        private readonly Timer TnASlides;
        private readonly Timer AvoidTheEdge;
        private readonly Timer AvoidTheEdgeTaunts;
        private readonly Timer RLGLTauntTimer;
        private readonly Timer VideoTauntTimer;
        private readonly Timer TimeoutTimer;

        private bool IsStrokeLinkEnabled(string path)
        {
            return true;
        }

        private string DomPersonalityPath => Path.Combine(_rootPath, "Scripts", _domPersonalityName); // Application.StartupPath + @"\Scripts\" + dompersonalitycombobox.Text
        private string EndStrokePath => Path.Combine(DomPersonalityPath, "Stroke", "End");
        private string StrokeLinkPath => Path.Combine(DomPersonalityPath, "Stroke", "Link");
        private bool IsEndScriptEnabled(string path)
        {
            // return CLBEndList.GetItemChecked(x);

            // See:// Public Shared Sub saveCheckedListBox(ByVal target As CheckedListBox, ByVal filePath As String)
            _log.WriteError("Need to check saved options");
            return true;
        }


        private void GotoClear()
        {
            ssh.GotoFlag = false;
            ssh.FileGoto = "";
            ssh.SkipGotoLine = false;
        }

        private void StopEverything()
        {
            ScriptTimer.Stop();
            StrokeTimer.Stop();
            StrokeTauntTimer.Stop();
            CensorshipTimer.Stop();
            RLGLTimer.Stop();
            TnASlides.Stop();
            AvoidTheEdge.Stop();

            EdgeTauntTimer.Stop();
            HoldEdgeTimer.Stop();
            HoldEdgeTauntTimer.Stop();
            AvoidTheEdgeTaunts.Stop();
            RLGLTauntTimer.Stop();
            VideoTauntTimer.Stop();
            EdgeCountTimer.Stop();

            ssh.SubStroking = false;
            ssh.SubEdging = false;
            ssh.SubHoldingEdge = false;
            ssh.MultipleEdges = false;
            ssh.AskedToSpeedUp = false;
            ssh.AskedToSlowDown = false;

            ssh.WorshipMode = false;
            ssh.WorshipTarget = "";
            ssh.LongHold = false;
            ssh.ExtremeHold = false;
            ssh.HoldTaunts = false;
            ssh.LongTaunts = false;
            ssh.ExtremeTaunts = false;


            ssh.CBTBallsActive = false;
            ssh.CBTBallsFlag = false;
            ssh.CBTCockActive = false;
            ssh.CBTCockFlag = false;
            ssh.CBTBothActive = false;
            ssh.CBTBothFlag = false;
            ssh.CustomTaskActive = false;


            if (!ssh.giveUpReturn)
                ClearModes();

            OnStopEverything();
            // Unlock OrgasmChances

            ssh.StrokePace = 0;
        }

        private void OnStopEverything()
        {
            /*
             *             FrmSettings.LockOrgasmChances(false);

            if (FrmSettings.TBWebStop.Text != "")
            {
                try
                {
                    FrmSettings.WebToy.Navigate(FrmSettings.TBWebStop.Text);
                }
                catch
                {
                }
            }

             */
            throw new NotImplementedException();
        }

        private void handleCallReturn()
        {
            if (ssh.MultiTauntPictureHold)
                ssh.MultiTauntPictureHold = false;
            ssh.CallReturns.Pop().resumeState();
            if (ssh.ReturnSubState == "Stroking")
            {
                if (ssh.SubStroking == false)
                {
                    if (ssh.CallReturns.Count == 0)
                    {
                        if (_settings.Chastity == true)
                            // DomTask = "Now as I was saying @StartTaunts"
                            ssh.DomTask = "#Return_Chastity";
                        else
                            // DomTask = "Get back to stroking @StartStroking"
                            ssh.DomTask = "#Return_Stroking";
                        if (!ssh.ShowModule)
                        {
                            StrokeTimer.Start();
                            StrokeTauntTimer.Start();
                        }
                    }
                    else
                    {
                        ssh.DomTask = "@NullResponse";
                        ScriptTimer.Start();
                        ssh.ScriptTick = 2;
                    }
                    TypingDelayGeneric();
                }
                else if (ssh.CallReturns.Count == 0)
                {
                    if (!ssh.ShowModule)
                    {
                        StrokeTimer.Start();
                        StrokeTauntTimer.Start();
                    }
                }
                else
                {
                    ssh.DomTask = "@NullResponse";
                    ssh.ScriptTick = 2;
                    ScriptTimer.Start();
                }
            }
            else if (ssh.ReturnSubState == "Edging")
            {
                if (ssh.SubEdging == false)
                {
                    // DomTask = "Start getting yourself to the edge again @Edge"
                    ssh.DomTask = "#Return_Edging";
                    // SubStroking = True
                    TypingDelayGeneric();
                }
                else
                {
                    EdgeTauntTimer.Start();
                    EdgeCountTimer.Start();
                }
            }
            else if (ssh.ReturnSubState == "Holding The Edge")
            {
                if (ssh.SubEdging == false)
                {
                    // DomTask = "Start getting yourself to the edge again @EdgeHold"
                    ssh.DomTask = "#Return_Holding";
                    // SubStroking = True
                    TypingDelayGeneric();
                }
                else
                {
                    HoldEdgeTimer.Start();
                    HoldEdgeTauntTimer.Start();
                }
            }
            else if (ssh.ReturnSubState == "CBTBalls")
            {
                // DomTask = "Now let's get back to busting those #Balls @CBTBalls"
                ssh.DomTask = "#Return_CBTBalls";
                ssh.CBTBallsFirst = false;
                TypingDelayGeneric();
            }
            else if (ssh.ReturnSubState == "CBTCock")
            {
                // DomTask = "Now let's get back to abusing that #Cock @CBTCock"
                ssh.DomTask = "#Return_CBTCock";
                ssh.CBTCockFirst = false;
                TypingDelayGeneric();
            }
            else if (ssh.ReturnSubState == "Rest")
            {
                ssh.DomTypeCheck = true;
                ssh.ScriptTick = 2;
                ScriptTimer.Start();
                if (ssh.YesOrNo)
                    ssh.DomTask = "#SYS_ReturnAnswer";
                else
                    ssh.DomTask = "@NullResponse";
                TypingDelayGeneric();
            }
        }

        private void TypingDelayGeneric()
        {
            throw new NotImplementedException();
        }

        private void clearCallReturns()
        {
            while (ssh.CallReturns.Count > 1)
                ssh.CallReturns.Pop();
        }

        public void RunFileText()
        {
            Debug.Print("SaidHello = " + ssh.SaidHello);
            if (ssh.SaidHello == false)
                return;

            // Debug.Print("CBTCockFlag = " & CBTCockFlag)
            // Debug.Print("CBTBallsFlag = " & CBTBallsFlag)
            if (ssh.CBTCockFlag == true | ssh.CBTBallsFlag == true | ssh.CBTBothFlag == true | ssh.CustomTask == true)
                return;

            // Debug.Print("WritingTaskFlag = " & WritingTaskFlag)
            if (ssh.WritingTaskFlag == true)
                return;

            // Debug.Print("TeaseVideo = " & TeaseVideo)
            if (ssh.TeaseVideo == true)
                return;



            if (ssh.RiskyDelay == true)
                return;

            if (ssh.InputFlag == true)
                return;

            if (ssh.CensorshipGame == true | ssh.RLGLGame == true | ssh.AvoidTheEdgeStroking == true | ssh.SubEdging == true | ssh.SubHoldingEdge == true)
                return;

            if (ssh.MultipleEdges == true)
                return;

            // Debug.Print("RunFileText " & StrokeTauntVal)

            List<string> lines = new List<string>();

            ssh.StrokeTauntVal += 1;
            lines = Common.Txt2List(ssh.FileText);
            try
            {
                if (ssh.StrokeTauntVal < lines.Count - 1)
                {
                    if (lines[ssh.StrokeTauntVal].Substring(0, 1) == "(")
                    {
                        do
                            ssh.StrokeTauntVal += 1;
                        while (lines[ssh.StrokeTauntVal].Substring(0, 1) == "(");
                    }
                }
            }
            catch (Exception ex)
            {
                _log.WriteError($"RunFileText error {ex}");
            }

            try
            {
                if (ssh.RunningScript == false & ssh.AvoidTheEdgeGame == false & ssh.CallReturns.Count == 0)
                {
                    Debug.Print("End Check StrokeTauntVal = " + ssh.StrokeTauntVal);

                    // end the scripts anyway if it reaches the last line, even if the user forgot to use the @End command
                    if (ssh.StrokeTauntVal >= lines.Count | lines[ssh.StrokeTauntVal] == "@End")
                    {
                        if (ssh.ShowModule == true)
                            ssh.ModuleEnd = true;
                        // if we reached the end of the script and we are in a link or in the beforeTease, and we forget to use the @StartStroking/Taunt
                        // it will automatically do it to avoid session errors
                        if ((ssh.isLink | ssh.BeforeTease) & ssh.SubStroking == false)
                        {
                            ssh.isLink = false;
                            if (_settings.Chastity)
                                ssh.DomChat = "@NullResponse @StartTaunts";
                            else
                                ssh.DomChat = "@NullResponse @StartStroking";
                            TypingDelay();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.WriteError($"RunFileText (2) error {ex}");
            }

            HandleScripts();
        }

        private void TypingDelay()
        {
            throw new NotImplementedException();
        }

        private bool ModuleEnd()
        {
            if (ssh.ModuleEnd && !ssh.AvoidTheEdgeGame)
            {
                Debug.Print("Module End Called?");
                ScriptTimer.Stop();
                ssh.ModuleEnd = false;
                ssh.ShowModule = false;

                // DelayFlag = True
                // DelayTick = randomizer.Next(3, 6)

                // DelayTimer.Start()

                // Do
                // Application.DoEvents()
                // Loop Until DelayFlag = False

                // LastScriptCountdown -= 1
                // Debug.Print("LastScriptCountdown = " & LastScriptCountdown)

                if (ssh.Playlist == true)
                {
                    Debug.Print("Playlist True - StrokeTimer");
                    if (ssh.PlaylistCurrent == ssh.PlaylistFile.Count - 1)
                        RunLastScript();
                    else
                        RunLinkScript();
                }
                else if (ssh.TeaseTick < 1 & ssh.BookmarkModule == false)
                    RunLastScript();
                else
                    RunLinkScript();
                return true;
            }
            return false;
        }
        public void HandleScripts()
        {
            Debug.Print("Handlescripts Called");

            if (ModuleEnd()) return;

            if (StrokeTimer.Enabled == true)
                return;

            Debug.Print("CHeck");
            Debug.Print(ssh.FileText);

            string CheckText;


            CheckText = ssh.FileText;

            // If File.Exists(HandleScriptText) Then
            if (!File.Exists(CheckText))
            {
                _log.WriteError($"File does not exist {CheckText}");
                throw new Exception($"Missing script file {CheckText}");
            }

            // Debug.Print(StrokeTauntVal)
            // Dim ioFile As New StreamReader(HandleScriptText)
            List<string> lines = Common.Txt2List(CheckText);
            int line;


            line = ssh.StrokeTauntVal;
            var notEnd = false;
            if (line == lines.Count)
            {
                if (ssh.ShowModule == true)
                {
                    ssh.ModuleEnd = true;
                    if (ModuleEnd()) return; // ! ssh.AvoidTheEdgeGame
                }
                else
                    notEnd = true;
            }
            else
            {
                Debug.Print("CHeck");

                if (GetFilter(lines[line], true) == false)
                {
                    RunFileText();
                    return;
                }
            }
            if (lines[line] == "@End" || ssh.StrokeTauntVal > lines.Count || notEnd)
            {
                if (ssh.CallReturns.Count > 0)
                {
                    handleCallReturn();
                    if (ssh.ShowModule)
                        return;
                }

                if (ssh.RiskyEdges == true)
                    ssh.RiskyEdges = false;
                if (ssh.LastScript == true)
                {
                    ssh.LastScript = false;
                    OnLastScriptDone();
                    return;
                }
                if (ssh.HypnoGen == true)
                {
                    /*
                    if (ssh.Induction == true)
                    {
                        ssh.Induction = false;
                        ssh.StrokeTauntVal = -1;
                        ssh.FileText = ssh.TempHypno;
                        ssh.ScriptTick = 1;
                        ScriptTimer.Start();
                        return;
                    }
                    ssh.HypnoGen = false;
                    ssh.AFK = false;
                    DomWMP.Ctlcontrols.stop();
                    BTNHypnoGenStart.Text = "Guide Me!";
                    */
                    throw new NotImplementedException("No hypnogen");
                }

                ScriptTimer.Stop();
                return;
            }

            // If lines(line).Substring(0, 1) = "(" Then
            // Do
            // line += 1
            // If MiniScript = True Then
            // MiniTauntVal += 1
            // Else
            // StrokeTauntVal += 1
            // End If

            // Loop Until lines(line).Substring(0, 1) <> "("
            // End If


            if (line < lines.Count - 1)
            {
                if (lines[line + 1].Substring(0, 1) == "[")
                {
                    ssh.YesOrNo = true;
                    ScriptTimer.Stop();
                }
            }

            Debug.Print("CHeck");


            ssh.DomTask = (lines[line].Trim());


            ssh.StringLength = 1;

            if (ssh.DomTask.Contains("@Goto("))
                GetGoto();

            Debug.Print("TempVal = " + ssh.TempVal);


            //if (ssh.DomTask.Contains("@ShowTaggedImage"))
            //    ssh.JustShowedBlogImage = true;

           // if (ssh.DomTask.Contains("@NullResponse"))
           //     ssh.NullResponse = true;

            if (ssh.HypnoGen == true)
            {
                throw new Exception("HypnoGen not implemented");
                /*
                if (CBHypnoGenSlideshow.Checked == true)
                {
                    if (LBHypnoGenSlideshow.SelectedItem == "Boobs")
                        ssh.DomTask = ssh.DomTask + " @ShowBoobsImage";
                    if (LBHypnoGenSlideshow.SelectedItem == "Butts")
                        ssh.DomTask = ssh.DomTask + " @ShowButtImage";
                    if (LBHypnoGenSlideshow.SelectedItem == "Hardcore")
                        ssh.DomTask = ssh.DomTask + " @ShowHardcoreImage";
                    if (LBHypnoGenSlideshow.SelectedItem == "Softcore")
                        ssh.DomTask = ssh.DomTask + " @ShowSoftcoreImage";
                    if (LBHypnoGenSlideshow.SelectedItem == "Lesbian")
                        ssh.DomTask = ssh.DomTask + " @ShowLesbianImage";
                    if (LBHypnoGenSlideshow.SelectedItem == "Blowjob")
                        ssh.DomTask = ssh.DomTask + " @ShowBlowjobImage";
                    if (LBHypnoGenSlideshow.SelectedItem == "Femdom")
                        ssh.DomTask = ssh.DomTask + " @ShowFemdomImage";
                    if (LBHypnoGenSlideshow.SelectedItem == "Lezdom")
                        ssh.DomTask = ssh.DomTask + " @ShowLezdomImage";
                    if (LBHypnoGenSlideshow.SelectedItem == "Hentai")
                        ssh.DomTask = ssh.DomTask + " @ShowHentaiImage";
                    if (LBHypnoGenSlideshow.SelectedItem == "Gay")
                        ssh.DomTask = ssh.DomTask + " @ShowGayImage";
                    if (LBHypnoGenSlideshow.SelectedItem == "Maledom")
                        ssh.DomTask = ssh.DomTask + " @ShowMaledomImage";
                    if (LBHypnoGenSlideshow.SelectedItem == "Captions")
                        ssh.DomTask = ssh.DomTask + " @ShowCaptionsImage";
                    if (LBHypnoGenSlideshow.SelectedItem == "General")
                        ssh.DomTask = ssh.DomTask + " @ShowGeneralImage";
                    if (LBHypnoGenSlideshow.SelectedItem == "Tagged")
                        ssh.DomTask = ssh.DomTask + " @ShowTaggedImage @Tag" + TBHypnoGenImageTag.Text;
                }
                */
            }


            if (ssh.DomTask != "")
                TypingDelayGeneric();
            else
                RunFileText();

        }

        private void OnLastScriptDone()
        {
            throw new NotImplementedException();
            /*
             * 
             *                     SaveChatLog(false);
                    ssh.Reset();
                    FrmSettings.LockOrgasmChances(false);
                    mainPictureBox.Image = null;

             */
        }

        public void RunLastScript()
        {
            ssh.StrokeTauntVal = -1;
            ClearModes();
            ssh.ShowModule = true;
            ssh.isLink = false;
            ssh.FirstRound = false;


            _vars.SetVariable("SYS_SubLeftEarly", 0);

            _vars.SetVariable("SYS_EndTotal", int.Parse(_vars.GetVariable("SYS_EndTotal")) + 1);


            if (ssh.Playlist == false || ssh.PlaylistFile[ssh.PlaylistCurrent].Contains("Random End") ||
                ssh.PlaylistFile.Count == 0)
            {
                List<string> EndList = new List<string>();
                EndList.Clear();

                string ChastityEndCheck;
                if (_settings.Chastity)
                    ChastityEndCheck = "*_CHASTITY.txt";
                else
                    ChastityEndCheck = "*.txt";

                foreach (string foundFile in Directory.EnumerateFiles(EndStrokePath, ChastityEndCheck))
                {
                    string TempEnd = foundFile;
                    TempEnd = TempEnd.Replace(".txt", "");
                    while (!!TempEnd.Contains(@"\"))
                        TempEnd = TempEnd.Remove(0, 1);
                    if (!IsEndScriptEnabled(foundFile)) continue; ;
                    {
                        if (_settings.Chastity)
                        {
                            if (!TempEnd.Contains("_BEG") && !TempEnd.Contains("_RESTRICTED"))
                                EndList.Add(foundFile);
                        }
                        else if (ssh.OrgasmRestricted)
                        {
                            if (TempEnd.Contains("_RESTRICTED"))
                                EndList.Add(foundFile);
                        }
                        else if (!TempEnd.Contains("_BEG") && !TempEnd.Contains("_CHASTITY") && !TempEnd.Contains("_RESTRICTED"))
                            EndList.Add(foundFile);
                    }
                }


                if (EndList.Count < 1)
                {
                    if (_settings.Chastity)
                        ssh.FileText = Path.Combine(DomPersonalityPath, @"System/Scripts/End_CHASTITY.txt");
                    else if (ssh.OrgasmRestricted)
                        ssh.FileText = Path.Combine(DomPersonalityPath, @"System/Scripts/End_RESTRICTED.txt");
                    else
                        ssh.FileText = Path.Combine(DomPersonalityPath, @"System/Scripts/End.txt");
                }
                else
                    ssh.FileText = EndList[ssh.randomizer.Next(0, EndList.Count)];
            }
            else if (ssh.PlaylistFile[ssh.PlaylistCurrent].Contains("Regular-TeaseAI-Script"))
            {
                ssh.FileText = Path.Combine(DomPersonalityPath, @"Stroke/End", ssh.PlaylistFile[ssh.PlaylistCurrent]);
                ssh.FileText = ssh.FileText.Replace(" Regular-TeaseAI-Script", "");
                ssh.FileText = ssh.FileText + ".txt";
            }
            else
                ssh.FileText = Path.Combine(DomPersonalityPath,@"Playlist/End", ssh.PlaylistFile[ssh.PlaylistCurrent] + ".txt");

            if (!ssh.WorshipMode)
            {
                if (ssh.SlideshowLoaded)
                {
                    OnSlideshowEnable();
                }
                ssh.LockImage = false;
            }


            ssh.StrokeTauntVal = -1;

            ssh.LastScript = true;


            ssh.ScriptTick = 3;
            ScriptTimer.Start();
        }
        private void OnSlideshowEnable()
        {
            throw new NotImplementedException();
            /*
            nextButton.Enabled = true;
            previousButton.Enabled = true;
            PicStripTSMIdommeSlideshow.Enabled = true;
            */
        }

        private void ClearModes()
        {
            ssh.edgeMode.Clear();
            ssh.cameMode.Clear();
            ssh.ruinMode.Clear();
            ssh.yesMode.Clear();
            ssh.noMode.Clear();
            ssh.Modes.Clear();
        }

        public void RunLinkScript()
        {
            ssh.StrokeTauntVal = -1;
            if (ssh.MultiTauntPictureHold)
                ssh.MultiTauntPictureHold = false;
            ssh.isLink = true;
            ssh.ShowModule = true;
            Debug.Print("RunLinkScript() Called");
            ssh.FirstRound = false;
            ClearModes();

            if (!ssh.Playlist || ssh.PlaylistFile[ssh.PlaylistCurrent].Contains("Random Link") || ssh.PlaylistFile.Count == 0)
            {
                Debug.Print("SetLink = " + ssh.SetLink);


                if (ssh.SetLink != "")
                {
                    Debug.Print("SetLink Called");
                    ssh.FileText = ssh.SetLink;
                }
                else
                {
                    List<string> LinkList = new List<string>();
                    LinkList.Clear();


                    string ChastityLinkCheck;
                    if (_settings.Chastity == true)
                        ChastityLinkCheck = "*_CHASTITY.txt";
                    else
                        ChastityLinkCheck = "*.txt";

                    foreach (string foundFile in Directory.EnumerateFiles(StrokeLinkPath, ChastityLinkCheck))
                    {
                        string TempLink = foundFile;
                        TempLink = TempLink.Replace(".txt", "");
                        while (!!TempLink.Contains(@"\"))
                            TempLink = TempLink.Remove(0, 1);
                        if (!IsStrokeLinkEnabled(foundFile)) continue;


                        if (_settings.Chastity)
                        {
                            LinkList.Add(foundFile);
                        }
                        else if (!TempLink.Contains("_CHASTITY"))
                            LinkList.Add(foundFile);
                        
                    }

                    if (LinkList.Count < 1)
                    {
                        if (_settings.Chastity)
                            ssh.FileText = Path.Combine(DomPersonalityPath, @"System/Scripts/Link_CHASTITY.txt");
                        else
                            ssh.FileText = Path.Combine(DomPersonalityPath, @"System/Scripts/Link.txt");
                    }
                    else
                        ssh.FileText = LinkList[ssh.randomizer.Next(0, LinkList.Count)];
                }
            }
            else
            {
                Debug.Print("Playlist Link Called");
                if (ssh.PlaylistFile[ssh.PlaylistCurrent].Contains("Regular-TeaseAI-Script"))
                {
                    ssh.FileText = Path.Combine(DomPersonalityPath, @"Stroke/Link", ssh.PlaylistFile[ssh.PlaylistCurrent]);
                    ssh.FileText = ssh.FileText.Replace(" Regular-TeaseAI-Script", "");
                    ssh.FileText = ssh.FileText + ".txt";
                }
                else
                    ssh.FileText = Path.Combine(DomPersonalityPath, @"Playlist/Link", ssh.PlaylistFile[ssh.PlaylistCurrent] + ".txt");
            }

            ssh.SetLink = "";
            Debug.Print("SetLink = " + ssh.SetLink);


            if (ssh.WorshipMode == false)
            {
                ssh.LockImage = false;
                if (ssh.SlideshowLoaded == true)
                {
                    OnSlideshowEnable();
                }
            }


            if (ssh.SetLinkGoto != "")
            {
                ssh.FileGoto = ssh.SetLinkGoto;
                ssh.SkipGotoLine = true;
                GetGoto();
                ssh.SetLinkGoto = "";
            }
            else
                ssh.StrokeTauntVal = -1;


            if (ssh.Playlist == true)
                ssh.PlaylistCurrent += 1;
            if (ssh.Playlist == true)
                ssh.BookmarkLink = false;

            if (ssh.BookmarkLink == true)
            {
                ssh.BookmarkLink = false;
                ssh.FileText = ssh.BookmarkLinkFile;
                ssh.StrokeTauntVal = ssh.BookmarkLinkLine;
            }

            Debug.Print("Link FileText Called");


            ssh.ScriptTick = 3;
            ScriptTimer.Start();
        }


        public void GetGoto()
        {
            string ReplaceGoto = "";
            int ReplaceX = 0;

            ssh.GotoFlag = true;

            string StripGoto;

            if (ssh.GotoDommeLevel == true | ssh.SkipGotoLine == true)
            {
                StripGoto = ssh.FileGoto;
                goto SkipGotoSearch;
            }

            string TempGoto = ssh.DomTask + " some garbage";
            int GotoIndex = TempGoto.IndexOf("@Goto(") + 6;
            TempGoto = TempGoto.Substring(GotoIndex, TempGoto.Length - GotoIndex);
            TempGoto = TempGoto.Split(')')[0];
            ssh.FileGoto = TempGoto;

            StripGoto = ssh.FileGoto;

            if (TempGoto.Contains(","))
            {
                TempGoto = TempGoto.Replace(", ", ",");
                string[] GotoSplit = TempGoto.Split(',');
                int GotoTemp = ssh.randomizer.Next(0, GotoSplit.Length);
                ssh.FileGoto = GotoSplit[GotoTemp];
            }



        SkipGotoSearch:
            ;
            string GotoText;

            GotoText = ssh.FileText;
            List<string> gotolines = Common.Txt2List(GotoText);
            int CountGotoLines = gotolines.Count;

        ResumeGotoSearch:
            ;
            try
            {
                // TODO: Add Errorhandling.
                if (File.Exists(GotoText))
                {
                    // Read all lines of the given file.
                    if (StripGoto.Substring(0, 1) != "(")
                        StripGoto = "(" + StripGoto + ")";
                    if (ssh.FileGoto.Substring(0, 1) != "(")
                        ssh.FileGoto = "(" + ssh.FileGoto + ")";

                    ssh.DomTask = ssh.DomTask.Replace("@Goto" + StripGoto, "");

                    int gotoline = -1;

                    do
                    {
                        gotoline += 1;
                        if (ssh.GotoDommeLevel == true & gotoline == CountGotoLines)
                        {
                            ssh.FileGoto = "(DommeLevel)";
                            goto SkipGotoSearch;
                        }
                    }
                    while (!gotolines[gotoline].StartsWith(ssh.FileGoto));

                    ssh.StrokeTauntVal = gotoline;
                }
            }
            catch (Exception ex)
            {
                // ▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                // All Errors
                // ▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                _log.WriteError("Exception occured finding GotoLabel \"" + ssh.FileGoto + "\" in file \"" + GotoText + "\"", ex, "Exception occured in GetGoto()");

                // Dim GotoLikeList As New List(Of String)
                // GotoLikeList = Txt2List(GotoText)

                // BreakPoint 
                do
                {
                    for (int i = 0; i <= gotolines.Count - 1; i++)
                    {
                        if (gotolines[i].Substring(0, 1) == "(")
                        {
                            int t = GetLikeValue(gotolines[i], ssh.FileGoto);
                            if (GetLikeValue(gotolines[i], ssh.FileGoto) == ReplaceX)
                            {
                                ReplaceX = 1885;
                                ReplaceGoto = gotolines[i];
                                break;
                            }
                        }
                    }

                    ReplaceX += 1;
                    // Application.DoEvents();
                }
                while (ReplaceX <= 5);
            }

            if (ReplaceGoto != "")
            {
                ReplaceX = 0;
                ssh.FileGoto = ReplaceGoto;
                ReplaceGoto = "";
                goto ResumeGotoSearch;
            }

            ssh.GotoDommeLevel = false;
            ssh.SkipGotoLine = false;

            if (ReplaceX != 0)
            {
                if (ssh.CallReturns.Count > 0)
                {
                    _chat.ChatAddWarning("Error: @Goto() could not find a valid Goto Label. Sending you to the previous callreturn, to avoid blocking the session");
                    handleCallReturn();
                    if (ssh.ShowModule)
                        return;
                }
                else
                {
                    StopEverything();
                    ssh.CallReturns.Clear();
                    _chat.ChatAddWarning("Error: @Goto() could not find a valid Goto Label. Sending you to a link, to avoid blocking the session");
                    if (!ssh.LastScript)
                    {
                        if (ssh.BeforeTease)
                        {
                            ssh.BeforeTease = false;
                            if (_settings.CBTeaseLengthDD)
                            {
                                if (_settings.DomLevel == 1)
                                    ssh.TeaseTick = ssh.randomizer.Next(10, 16) * 60;
                                if (_settings.DomLevel == 2)
                                    ssh.TeaseTick = ssh.randomizer.Next(15, 21) * 60;
                                if (_settings.DomLevel == 3)
                                    ssh.TeaseTick = ssh.randomizer.Next(20, 31) * 60;
                                if (_settings.DomLevel == 4)
                                    ssh.TeaseTick = ssh.randomizer.Next(30, 46) * 60;
                                if (_settings.DomLevel == 5)
                                    ssh.TeaseTick = ssh.randomizer.Next(45, 61) * 60;
                            }
                            else
                                ssh.TeaseTick = ssh.randomizer.Next(_settings.TeaseLengthMin * 60, _settings.TeaseLengthMax * 60);
                        }
                        RunLinkScript();
                    }
                    else
                    {
                        _chat.ChatAddWarning("Error: @Goto() could not find a valid Goto Label. Since this is the final cycle, the session will now end.");

                        _vars.SetVariable("SYS_SubLeftEarly", 0);
                        /*
                        SaveChatLog(false);
                        ssh.Reset();
                        FrmSettings.LockOrgasmChances(false);
                        */
                        OnLastScriptDone();
                    }
                }
            }
        }


        public int GetLikeValue(string s, string t)
        {
            s = s.ToLower();
            t = t.ToLower();

            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1 + 1, m + 1 + 1];

            if (n == 0)
                return m;
            if (m == 0)
                return n;

            int i;
            int j;

            for (i = 0; i <= n; i++)
                d[i, 0] = i;
            for (j = 0; j <= m; j++)
                d[0, j] = j;
            for (i = 1; i <= n; i++)
            {
                for (j = 1; j <= m; j++)
                {
                    int cost;
                    if (t[j - 1] == s[i - 1])
                        cost = 0;
                    else
                        cost = 1;
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            }
            return d[n, m];
        }


        private void OnTextAccepted()
        {
            //
            //chatBox.Text = "";
            //ChatBox2.Text = "";
        }

        private bool WordExists(string searchString, string findString)
        {
            bool returnValue = false;
            if (System.Text.RegularExpressions.Regex.Matches(searchString, @"\b" + findString + @"\b", System.Text.RegularExpressions.RegexOptions.IgnoreCase).Count > 0)
                returnValue = true;
            return returnValue;
        }

        // return -1 if word to check was not present, 0 if something in the honorific is wrong, 1 if all is ok
        // we don't reduce errors number when doing the checks for what the sub writes in chat (so we reduce them only when asked a direct question)
        // otherwise it will be nearly impossible to be punished since each phrase not using it would reduce the count
        private int checkSubAnswer(string caseToCheck = "", bool reduceErrors = true)
        {
            ssh.DomChat = "";
            if (ssh.contactToUse != null)
            {
                if (ssh.contactToUse.Equals(ssh.SlideshowContact1))
                    ssh.DomChat = "@Contact1 ";
                else if (ssh.contactToUse.Equals(ssh.SlideshowContact2))
                    ssh.DomChat = "@Contact2 ";
                else if (ssh.contactToUse.Equals(ssh.SlideshowContact3))
                    ssh.DomChat = "@Contact3 ";
            }
            List<string> checkfor = new List<string>();
            bool checkForHonorific = true;
            string[] splitChat;
            splitChat = Common.ObtainSplitParts(ssh.ChatString, true);


            // checkanswers is a new class which stores all the saved settings for hi,yes,no,sorry words that the used has chosen
            // if we don't have a specific word to check for, we check for them all (this is used anytime the sub write anything)
            if (caseToCheck == "")
                checkfor = ssh.checkAnswers.returnAll();
            else if (caseToCheck == "yes" || caseToCheck == "no" || caseToCheck == "hi" || caseToCheck == "sorry" || caseToCheck == "please" || caseToCheck == "thanks")
                checkfor.Add(ssh.checkAnswers.returnWords(caseToCheck));
            else
            {
                // otherwise we check only to see if he wrote exactly what he was supposed to do
                checkfor.Add(caseToCheck);
                checkForHonorific = false;
                // so we don't split the response but check the whole phrase (we "split" for an unused char so it is basically impossible the user will use it)
                splitChat = new[] { ssh.ChatString };  // - ??? ssh.ChatString.Split(new char[] { '§' });
            }

            for (var i = 0; i <= checkfor.Count - 1; i++)
            {
                string[] SplitParts = Common.ObtainSplitParts(checkfor[i], false);

                for (int n = 0; n <= SplitParts.Length - 1; n++)
                {
                    for (int m = 0; m <= splitChat.Length - 1; m++)
                    {
                        bool condition;
                        if (checkForHonorific)
                            // this will make so that the #Sys_MissingHonorific will respond using the word that triggered it (i.e. yes what? sure what? etc)
                            condition = splitChat[m].Equals(SplitParts[n], StringComparison.OrdinalIgnoreCase);
                        else
                            // but if we are not checking for honorific (i.e. an answer to a specific question) we just check it the needed words are present
                            // in the sub answer
                            condition = ssh.ChatString.IndexOf(SplitParts[n], StringComparison.OrdinalIgnoreCase) >= 0;

                        if (condition)
                        {
                            if (ssh.ChatString.IndexOf(SplitParts[n], StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                if (checkForHonorific)
                                {
                                    if (_settings.CBUseHonor)
                                    {
                                        if (!WordExists(ssh.ChatString, ssh.tempHonorific) || (_settings.CBUseName && !WordExists(ssh.ChatString, ssh.tempDomName)))
                                        {
                                            ssh.DomChat += SplitParts[n] + " #SYS_MissingHonorific";
                                            if (!_settings.DomLowercase)
                                            {
                                                string DomU = ssh.DomChat.Substring(0, 1).ToUpper();
                                                ssh.DomChat = ssh.DomChat.Remove(0, 1);
                                                ssh.DomChat = DomU + ssh.DomChat;
                                                ssh.nameErrors += 1;
                                                ssh.wrongAttempt = true;
                                            }
                                            ssh.JustShowedBlogImage = true;
                                            TypingDelay();
                                            return 0;
                                        }

                                        /* NOT PRIORITY
                                        if (_settings.CBCapHonor)
                                        {
                                            if (!WordExists(ssh.ChatString, ssh.tempHonorific))
                                            {
                                                // Not a priority.
                                                ssh.DomChat += "#SYS_CapitalizeHonorific";
                                                ssh.nameErrors += 1;
                                                ssh.wrongAttempt = true;
                                                ssh.JustShowedBlogImage = true;
                                                TypingDelay();
                                                return 0;
                                            }
                                            // we only reduce the errors if we were responding a question, not everytime we write...the sub need to remember to use the honorific :D
                                            if (reduceErrors)
                                            {
                                                if (ssh.wrongAttempt)
                                                    ssh.wrongAttempt = false;
                                                else if ((ssh.nameErrors > 0))
                                                    ssh.nameErrors -= 1;
                                            }
                                        }
                                        */
                                    }
                                }
                                return 1;
                            }
                        }
                    }
                }
            }
            return -1;
        }


        private void sendButton_Click(string chatString)
        {
            // Check for empty or whitespaced input.
            if (string.IsNullOrWhiteSpace(chatString))
                return;

            if (TimeoutTimer.Enabled)
                TimeoutTimer.Stop();

            //if (dompersonalitycombobox.Items.Count < 1)
            //{
            //    MessageBox.Show(this, "No domme Personalities were found! Please install at least one Personality directory in the Scripts folder before using this part of the program.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            //    return;
            //}

            ssh.ChatString = chatString.Trim();

            OnTextAccepted();

            if (_settings.Shortcuts)
            {
                if (string.Equals(ssh.ChatString, _settings.ShortYes, StringComparison.OrdinalIgnoreCase))
                    ssh.ChatString = "Yes " + ssh.tempHonorific;
                if (string.Equals(ssh.ChatString, _settings.ShortNo, StringComparison.OrdinalIgnoreCase))
                    ssh.ChatString = "No " + ssh.tempHonorific;
                if (string.Equals(ssh.ChatString, _settings.ShortEdge, StringComparison.OrdinalIgnoreCase))
                    ssh.ChatString = "On the edge";
                if (string.Equals(ssh.ChatString, _settings.ShortSpeedUp, StringComparison.OrdinalIgnoreCase))
                    ssh.ChatString = "Let me speed up";
                if (string.Equals(ssh.ChatString, _settings.ShortSlowDown, StringComparison.OrdinalIgnoreCase))
                    ssh.ChatString = "Let me slow down";
                if (string.Equals(ssh.ChatString, _settings.ShortStop, StringComparison.OrdinalIgnoreCase))
                    ssh.ChatString = "Let me stop";
                if (string.Equals(ssh.ChatString, _settings.ShortStroke, StringComparison.OrdinalIgnoreCase))
                    ssh.ChatString = "May I start stroking?";
                if (string.Equals(ssh.ChatString, _settings.ShortCum, StringComparison.OrdinalIgnoreCase))
                    ssh.ChatString = "Please let me cum " + ssh.tempHonorific;
                if (string.Equals(ssh.ChatString, _settings.ShortGreet, StringComparison.OrdinalIgnoreCase))
                    ssh.ChatString = "Hello " + ssh.tempHonorific;
                if (string.Equals(ssh.ChatString, _settings.ShortSafeword, StringComparison.OrdinalIgnoreCase))
                    ssh.ChatString = _settings.Safeword;
            }




            if (ssh.ChatString.Substring(0, 1) == "@")
            {
                _chat.ChatAddScriptPosInfo(ssh.ChatString);

                return;
            }

            var bPauseCheck = false;
            if (OnPauseCheck(out bPauseCheck))
            {
                return;
            }



            if (ssh.WritingTaskFlag == true)
                goto WritingTaskLine;

            _chat.ChatAddMessage(_settings.SubName, ssh.ChatString);





            ssh.SubWroteLast = true;




            // If frmApps.CBDebugAwareness.Checked = True Then GoTo DebugAwareness



            if (!ssh.SaidHello)
            {
                if (ssh.ChatString.IndexOf("TASK", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    List<string> TaskList = new List<string>();
                    var startTasksPath = Path.Combine(DomPersonalityPath, "Interrupt/Start Tasks");
                    foreach (string TaskFile in Directory.EnumerateFiles(Path.Combine(startTasksPath,"*.txt")))
                        TaskList.Add(TaskFile);

                    if (TaskList.Count > 0)
                    {
                        if (Directory.Exists(_settings.DomImageDir) & ssh.SlideshowLoaded == false)
                        {
                            LoadDommeImageFolder();
                            InitDommeImageFolder();
                        }
                        ssh.BeforeTease = true;
                        ssh.SaidHello = true;
                        ssh.SubEdging = false;
                        ssh.SubHoldingEdge = false;
                        ssh.FileText = TaskList[ssh.randomizer.Next(0, TaskList.Count)];
                        ssh.LockImage = false;
                        if (ssh.SlideshowLoaded == true)
                        {
                            OnSlideshowEnable();
                            /*
                            nextButton.Enabled = true;
                            previousButton.Enabled = true;
                            PicStripTSMIdommeSlideshow.Enabled = true;
                            */
                        }
                        ssh.StrokeTauntVal = -1;
                        ssh.ScriptTick = 3;
                        ScriptTimer.Start();
                        ssh.ShowModule = false;
                    }
                    else
                    {
                        _log.WriteError("no start task files found in ", startTasksPath);
                        //MessageBox.Show(this, "No files were found in " + Application.StartupPath + @"\Scripts\" + dompersonalitycombobox.Text + @"\Interrupt\Start Tasks!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        throw new Exception($"No task files in {startTasksPath}");
                    }
                    return;
                }

                Debug.Print("CHeck");

                if (checkSubAnswer("hi") == 1)
                {
                    Debug.Print("CHeck");
                    // ?? RefreshRandomize(); TODO => allow fixed seed
                    ssh.justStarted = true;
                    ssh.SaidHello = true;
                    ssh.BeforeTease = true;



                    if (_settings.CBTeaseLengthDD)
                    {
                        if (_settings.DomLevel == 1)
                            ssh.TeaseTick = ssh.randomizer.Next(10, 16) * 60;
                        if (_settings.DomLevel == 2)
                            ssh.TeaseTick = ssh.randomizer.Next(15, 21) * 60;
                        if (_settings.DomLevel == 3)
                            ssh.TeaseTick = ssh.randomizer.Next(20, 31) * 60;
                        if (_settings.DomLevel == 4)
                            ssh.TeaseTick = ssh.randomizer.Next(30, 46) * 60;
                        if (_settings.DomLevel == 5)
                            ssh.TeaseTick = ssh.randomizer.Next(45, 61) * 60;
                    }
                    else
                        ssh.TeaseTick = ssh.randomizer.Next(_settings.TeaseLengthMin * 60, _settings.TeaseLengthMax * 60);


                    TeaseTimer.Start();

                    // Lock Orgasm Chances if setting is activated. 
                    if (_settings.LockOrgasmChances)
                        FrmSettings.LockOrgasmChances(true);

                    if (ssh.PlaylistFile.Count == 0)
                        goto NoPlaylistStartFile;

                    if (ssh.Playlist == false | ssh.PlaylistFile(0).Contains("Random Start"))
                    {
                    NoPlaylistStartFile:
                        ;
                        List<string> StartList = new List<string>();
                        StartList.Clear();

                        string ChastityStartCheck;
                        if (_settings.Chastity == true)
                            ChastityStartCheck = "*_CHASTITY.txt";
                        else
                            ChastityStartCheck = "*.txt";

                        foreach (string foundFile in My.Computer.FileSystem.GetFiles(Application.StartupPath + @"\Scripts\" + dompersonalitycombobox.Text + @"\Stroke\Start\", Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, ChastityStartCheck))
                        {
                            string TempStart = foundFile;
                            TempStart = TempStart.Replace(".txt", "");
                            while (!!TempStart.Contains(@"\"))
                                TempStart = TempStart.Remove(0, 1);
                            for (int x = 0; x <= FrmSettings.CLBStartList.Items.Count - 1; x++)
                            {
                                if (_settings.Chastity == true)
                                {
                                    if (FrmSettings.CLBStartList.Items(x) == TempStart & FrmSettings.CLBStartList.GetItemChecked(x) == true)
                                        StartList.Add(foundFile);
                                }
                                else if (FrmSettings.CLBStartList.Items(x) == TempStart & FrmSettings.CLBStartList.GetItemChecked(x) == true & !TempStart.Contains("_CHASTITY"))
                                    StartList.Add(foundFile);
                            }
                        }

                        if (StartList.Count < 1)
                        {
                            if (_settings.Chastity == true)
                                ssh.FileText = Application.StartupPath + @"\Scripts\" + dompersonalitycombobox.Text + @"\System\Scripts\Start_CHASTITY.txt";
                            else
                                ssh.FileText = Application.StartupPath + @"\Scripts\" + dompersonalitycombobox.Text + @"\System\Scripts\Start.txt";
                        }
                        else
                            ssh.FileText = StartList[ssh.randomizer.Next(0, StartList.Count)];
                    }
                    else
                    {
                        Debug.Print("Start situation found");
                        if (ssh.PlaylistFile(0).Contains("Regular-TeaseAI-Script"))
                        {
                            ssh.FileText = Application.StartupPath + @"\Scripts\" + dompersonalitycombobox.Text + @"\Stroke\Start\" + ssh.PlaylistFile(0);
                            ssh.FileText = ssh.FileText.Replace(" Regular-TeaseAI-Script", "");
                            ssh.FileText = ssh.FileText + ".txt";
                        }
                        else
                            ssh.FileText = Application.StartupPath + @"\Scripts\" + dompersonalitycombobox.Text + @"\Playlist\Start\" + ssh.PlaylistFile(0) + ".txt";
                    }

                    if (ssh.Playlist == true)
                        ssh.PlaylistCurrent += 1;
                    ssh.LastScriptCountdown = ssh.randomizer.Next(3, 5 * FrmSettings.domlevelNumBox.Value);

                    if (Directory.Exists(_settings.DomImageDir) & ssh.SlideshowLoaded == false)
                    {
                        if (FrmSettings.CBRandomDomme.Checked == true)
                            ssh.glitterDommeNumber = 4;
                        LoadDommeImageFolder();
                        InitDommeImageFolder();
                    }


                    ssh.StrokeTauntVal = -1;
                    ssh.ScriptTick = 3;
                    ScriptTimer.Start();
                }
            }




            if (ssh.SaidHello == false)
                return;

            if (UCase(ssh.ChatString) == UCase(FrmSettings.TBSafeword.Text))
            {
                List<string> SafeList = new List<string>();

                foreach (string SafeFile in My.Computer.FileSystem.GetFiles(Application.StartupPath + @"\Scripts\" + dompersonalitycombobox.Text + @"\Interrupt\Safeword\", Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, "*.txt"))
                    SafeList.Add(SafeFile);

                if (SafeList.Count > 0)
                {
                    StopEverything();
                    ssh.FileText = SafeList[ssh.randomizer.Next(0, SafeList.Count)];
                    ssh.ShowModule = true;
                    ssh.StrokeTauntVal = -1;
                    ssh.ScriptTick = 2;
                    ScriptTimer.Start();
                }
                else
                    MessageBox.Show(this, "No files were found in " + Application.StartupPath + @"\Scripts\" + dompersonalitycombobox.Text + @"\Interrupt\Safeword!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            if (UCase(ssh.ChatString).Contains(Strings.UCase("stop")))
                TnASlides.Stop();

            // If UCase(ChatString).Contains(UCase("stop")) Then
            // If CustomSlideshow = True Then CustomSlideshowTimer.Stop()
            // End If


            WritingTaskLine:
            ;
            if (ssh.WritingTaskFlag == true)
            {
                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                // Writing Task
                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼

                // ##################### Evaluate Input ########################
                bool InputWrong, TaskTimeout, TaskFailed, TaskSuccess;

                if (ssh.ChatString == LBLWritingTaskText.Text)
                {
                    ssh.WritingTaskLinesWritten += 1;
                    ssh.WritingTaskLinesRemaining -= 1;
                    LBLLinesWritten.Text = ssh.WritingTaskLinesWritten;
                    LBLLinesRemaining.Text = ssh.WritingTaskLinesRemaining;

                    if (ssh.WritingTaskLinesRemaining <= 0)
                        TaskSuccess = true;
                }
                else
                {
                    InputWrong = true;

                    ssh.WritingTaskMistakesMade += 1;
                    LBLMistakesMade.Text = ssh.WritingTaskMistakesMade;

                    if (ssh.WritingTaskMistakesAllowed - ssh.WritingTaskMistakesMade <= 0)
                        TaskFailed = true;
                }

                if (ssh.WritingTaskCurrentTime < 1 & _settings.TimedWriting == true & ssh.WritingTaskFlag == true)
                    TaskTimeout = true;

                // ################# Generate output text ######################
                string ProgrText = "";
                string OutColor = "";
                string OutHtml = "<span style=\"color:{0}\">{1}</span>";

                if (TaskTimeout | TaskFailed | InputWrong)
                {
                    if (TaskTimeout)
                        ProgrText = "Time Expired";
                    else if (TaskFailed)
                        ProgrText = "Task Failed";
                    else if (InputWrong)
                    {
                        ProgrText = string.Format("Wrong: {0} mistakes remaining", ssh.WritingTaskMistakesAllowed - ssh.WritingTaskMistakesMade);
                        if (ssh.WritingTaskMistakesAllowed - ssh.WritingTaskMistakesMade == 1)
                            ProgrText = ProgrText.Replace("mistakes", "mistake");
                    }

                    OutColor = "red";
                }
                else
                {
                    if (TaskSuccess)
                        ProgrText = "Task completed successfully";
                    else
                    {
                        ProgrText = string.Format("Correct: {0} lines remaining", ssh.WritingTaskLinesRemaining);
                        if (ssh.WritingTaskLinesRemaining == 1)
                            ProgrText = ProgrText.Replace("lines", "line");
                    }

                    OutColor = "green";
                }

                // ####################### Print output ########################
                ChatAddMessage(_settings.SubName, string.Format(OutHtml, OutColor, ssh.ChatString), true);
                ChatAddWritingTaskInfo(string.Format(OutHtml, OutColor, ProgrText));

                // ##################### Continue script? ######################
                if (TaskTimeout | TaskFailed)
                {
                    ClearWriteTask();
                    ssh.SkipGotoLine = true;
                    ssh.FileGoto = "Failed Writing Task";
                    GetGoto();
                    ssh.ScriptTick = 4;
                    ScriptTimer.Start();
                }
                else if (TaskSuccess)
                {
                    ClearWriteTask();
                    ssh.ScriptTick = 3;
                    ScriptTimer.Start();
                }


                if (ssh.randomWriteTask)
                    setWriteTask();
            }

            if (ssh.AFK & !ssh.YesOrNo)
                return;

            // Previous Commas





            List<string> EdgeList = new List<string>();
            EdgeList = Txt2List(Application.StartupPath + @"\Scripts\" + dompersonalitycombobox.Text + @"\Vocabulary\Responses\System\EdgeKEY.txt");



            string EdgeCheck = ssh.ChatString;

            string EdgeString;

            Debug.Print("EdgeFOund 1 = " + ssh.EdgeFound);

            for (int i = 0; i <= EdgeList.Count - 1; i++)
            {
                EdgeString = EdgeList[i];
                EdgeString = EdgeString.Replace("'", "");
                EdgeString = EdgeString.Replace(".", "");
                EdgeString = EdgeString.Replace(",", "");
                EdgeString = EdgeString.Replace("!", "");
                EdgeCheck = EdgeCheck.Replace("'", "");
                EdgeCheck = EdgeCheck.Replace(".", "");
                EdgeCheck = EdgeCheck.Replace(",", "");
                EdgeCheck = EdgeCheck.Replace("!", "");
                if (Strings.UCase(EdgeCheck).Contains("DONT") | Strings.UCase(EdgeCheck).Contains("NEVER") | Strings.UCase(EdgeCheck).Contains("NOT"))
                {
                    if (Strings.UCase(EdgeCheck).Contains(Strings.UCase(EdgeString)))
                    {
                        ssh.EdgeNOT = true;
                        break;
                    }
                }
                if (Strings.UCase(EdgeString) == Strings.UCase(EdgeCheck))
                {
                    ssh.EdgeFound = true;
                    break;
                }
            }

            Debug.Print("EdgeFOund 2 = " + ssh.EdgeFound);

            if (ssh.EdgeFound == true & _settings.Chastity == false)
            {
                Debug.Print("EdgeFOund = True Called");

                ssh.EdgeFound = false;


                if (ssh.SubHoldingEdge == true)
                {
                    Debug.Print("EdgeFOund = SubHoldingedge");
                    ssh.DomChat = " #YoureAlreadySupposedToBeClose";
                    TypingDelay();
                    return;
                }

                SetVariable("SYS_EdgeTotal", Val(GetVariable("SYS_EdgeTotal") + 1));

                if (ssh.TauntEdging == true & ssh.SubEdging == false & ssh.ShowModule == false)
                {
                    ssh.DomChat = "#SYS_TauntEdgingAsked";
                    TypingNoDelay();

                    // Recalculate TantEdging-Chance
                    if (ssh.randomizer.Next(1, 101) <= FrmSettings.NBTauntEdging.Value)
                        ssh.TauntEdging = false;

                    return;
                }


                if (ssh.edgeMode.VideoMode == true)
                {
                    ssh.SessionEdges += 1;
                    ssh.edgeMode.VideoMode = false;
                    ssh.TeaseVideo = false;
                    VideoTimer.Stop();
                    DomWMP.Visible = false;
                    DomWMP.Ctlcontrols.stop();
                    mainPictureBox.Visible = true;
                    ssh.FileGoto = ssh.edgeMode.GotoLine;
                    ssh.SkipGotoLine = true;
                    GetGoto();
                    return;
                }

                if (ssh.edgeMode.GotoMode == true)
                {
                    ssh.SessionEdges += 1;
                    ssh.edgeMode.GotoMode = false;
                    ssh.FileGoto = ssh.edgeMode.GotoLine;
                    ssh.SkipGotoLine = true;
                    GetGoto();
                    return;
                }

                if (ssh.edgeMode.MessageMode == true)
                {
                    ssh.SessionEdges += 1;
                    ssh.edgeMode.MessageMode = false;
                    ssh.ChatString = ssh.edgeMode.MessageText;
                    goto DebugAwareness;
                }

                // EdgeMessageYesNo = EdgeArray(1)

                if (ssh.RLGLGame == true)
                {
                    Debug.Print("EdgeFOund = RLGL");
                    ssh.DomChat = "#TryToHoldIt";
                    TypingDelay();
                    return;
                }


                if (ssh.AvoidTheEdgeStroking == true)
                {
                    Debug.Print("EdgeFOund = ATE");

                    AvoidTheEdgeTaunts.Stop();

                    ssh.SessionEdges += 1;
                    ssh.AvoidTheEdgeStroking = false;
                    ssh.VideoTease = false;

                    List<string> ATEList = new List<string>();

                    foreach (string foundFile in My.Computer.FileSystem.GetFiles(Application.StartupPath + @"\Scripts\" + dompersonalitycombobox.Text + @"\Video\Avoid The Edge\Scripts\", Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, "*.txt"))
                        ATEList.Add(foundFile);

                    if (ATEList.Count < 1)
                    {
                        MessageBox.Show(this, "No Avoid The Edge scripts were found!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        return;
                    }

                    DomWMP.Ctlcontrols.pause();

                    ssh.StrokeTauntVal = -1;
                    ssh.FileText = ATEList[ssh.randomizer.Next(0, ATEList.Count)];

                    ssh.ScriptTick = 1;
                    ScriptTimer.Start();
                    return;
                }


                if (ssh.SubEdging == true)
                {
                    Debug.Print("EdgeFOund = SubEdging");

                    EdgeCountTimer.Stop();

                    if (ssh.MultipleEdges == true)
                    {
                        ssh.MultipleEdgesAmount -= 1;
                        ssh.SessionEdges += 1;

                        if (ssh.MultipleEdgesAmount < 1)
                            ssh.MultipleEdges = false;
                        else
                        {
                            EdgeCountTimer.Stop();
                            ssh.DomChat = "#SYS_MultipleEdgesStop";
                            if (ssh.Contact1Edge == true)
                                ssh.DomChat = "@Contact1 #SYS_MultipleEdgesStop";
                            if (ssh.Contact2Edge == true)
                                ssh.DomChat = "@Contact2 #SYS_MultipleEdgesStop";
                            if (ssh.Contact3Edge == true)
                                ssh.DomChat = "@Contact3 #SYS_MultipleEdgesStop";
                            TypingNoDelay();
                            ssh.MultipleEdgesTick = ssh.MultipleEdgesInterval;
                            MultipleEdgesTimer.Start();
                            ssh.MultipleEdgesMetronome = "STOP";
                            return;
                        }
                    }

                    if (ssh.AlreadyStrokingEdge == true)
                    {
                        ssh.AvgEdgeCount += 1;
                        if (ssh.AvgEdgeStroking == 0)
                            ssh.AvgEdgeStroking = ssh.EdgeCountTick;
                        else
                            ssh.AvgEdgeStroking = ssh.AvgEdgeStroking + ssh.EdgeCountTick;
                        _settings.AvgEdgeStroking = ssh.AvgEdgeStroking;
                        _settings.AvgEdgeCount = ssh.AvgEdgeCount;
                    }
                    else
                    {
                        ssh.AvgEdgeCountRest += 1;
                        if (ssh.AvgEdgeNoTouch == 0)
                            ssh.AvgEdgeNoTouch = ssh.EdgeCountTick;
                        else
                            ssh.AvgEdgeNoTouch = ssh.AvgEdgeNoTouch + ssh.EdgeCountTick;
                        _settings.AvgEdgeNoTouch = ssh.AvgEdgeNoTouch;
                        _settings.AvgEdgeCountRest = ssh.AvgEdgeCountRest;
                    }


                    if (_settings.AvgEdgeCount > 4)
                    {
                        ssh.AvgEdgeStroking = _settings.AvgEdgeStroking;
                        TimeSpan TS1 = TimeSpan.FromSeconds(ssh.AvgEdgeStroking / (double)ssh.AvgEdgeCount);
                        FrmSettings.LBLAvgEdgeStroking.Text = string.Format("{0:00}:{1:00}", TS1.Minutes, TS1.Seconds);
                    }
                    else
                        FrmSettings.LBLAvgEdgeStroking.Text = "WAIT";

                    if (_settings.AvgEdgeCountRest > 4)
                    {
                        ssh.AvgEdgeNoTouch = _settings.AvgEdgeNoTouch;
                        TimeSpan TS2 = TimeSpan.FromSeconds(ssh.AvgEdgeNoTouch / (double)ssh.AvgEdgeCountRest);
                        FrmSettings.LBLAvgEdgeNoTouch.Text = string.Format("{0:00}:{1:00}", TS2.Minutes, TS2.Seconds);
                    }
                    else
                        FrmSettings.LBLAvgEdgeNoTouch.Text = "WAIT";

                    if (FrmSettings.domlevelNumBox.Value == 1)
                        ssh.HoldEdgeChance = 20;
                    if (FrmSettings.domlevelNumBox.Value == 2)
                        ssh.HoldEdgeChance = 25;
                    if (FrmSettings.domlevelNumBox.Value == 3)
                        ssh.HoldEdgeChance = 30;
                    if (FrmSettings.domlevelNumBox.Value == 4)
                        ssh.HoldEdgeChance = 40;
                    if (FrmSettings.domlevelNumBox.Value == 5)
                        ssh.HoldEdgeChance = 50;

                    int HoldEdgeInt = ssh.randomizer.Next(1, 101);

                    if (ssh.EdgeHold == true)
                        HoldEdgeInt = 0;
                    if (ssh.EdgeNoHold == true)
                        HoldEdgeInt = 1000;


                    Debug.Print("HoldEdgeInt = " + HoldEdgeInt);

                    ssh.EdgeHold = false;
                    ssh.EdgeNoHold = false;



                    if (HoldEdgeInt < ssh.HoldEdgeChance)
                    {
                        Debug.Print("EdgeFOund = HOldtheedge");

                        ssh.DomTypeCheck = true;
                        ssh.SubEdging = false;
                        ssh.SubStroking = false;
                        ssh.SubHoldingEdge = true;
                        EdgeTauntTimer.Stop();
                        ssh.DomChat = "#HoldTheEdge";
                        if (ssh.Contact1Edge == true)
                            ssh.DomChat = "@Contact1 #HoldTheEdge";
                        if (ssh.Contact2Edge == true)
                            ssh.DomChat = "@Contact2 #HoldTheEdge";
                        if (ssh.Contact3Edge == true)
                            ssh.DomChat = "@Contact3 #HoldTheEdge";
                        TypingNoDelay();


                        if (ssh.EdgeHoldFlag == false)
                        {
                            ssh.HoldEdgeTick = ssh.HoldEdgeChance;

                            int HoldEdgeMin;
                            int HoldEdgeMax;

                            HoldEdgeMin = FrmSettings.NBHoldTheEdgeMin.Value;
                            if (FrmSettings.LBLMinHold.Text == "minutes")
                                HoldEdgeMin *= 60;

                            HoldEdgeMax = FrmSettings.NBHoldTheEdgeMax.Value;
                            if (FrmSettings.LBLMaxHold.Text == "minutes")
                                HoldEdgeMax *= 60;


                            if (ssh.ExtremeHold == true)
                            {
                                HoldEdgeMin = FrmSettings.NBExtremeHoldMin.Value * 60;
                                HoldEdgeMax = FrmSettings.NBExtremeHoldMax.Value * 60;
                            }

                            if (ssh.LongHold == true)
                            {
                                HoldEdgeMin = FrmSettings.NBLongHoldMin.Value * 60;
                                HoldEdgeMax = FrmSettings.NBLongHoldMax.Value * 60;
                            }

                            if (HoldEdgeMax < HoldEdgeMin)
                                HoldEdgeMax = HoldEdgeMin + 1;

                            ssh.HoldEdgeTick = ssh.randomizer.Next(HoldEdgeMin, HoldEdgeMax + 1);

                            if (ssh.HoldEdgeTick < 10)
                                ssh.HoldEdgeTick = 10;
                        }
                        else
                        {
                            ssh.HoldEdgeTick = ssh.EdgeHoldSeconds;
                            ssh.EdgeHoldFlag = false;
                        }

                        ssh.HoldEdgeTime = 0;

                        HoldEdgeTimer.Start();
                        HoldEdgeTauntTimer.Start();
                        return;
                    }
                    else
                    {
                        if (ssh.EdgeToRuin == true | ssh.OrgasmRuined == true)
                        {
                            ssh.LastOrgasmType = "RUINED";
                            ssh.OrgasmRuined = false;
                            goto RuinedOrgasm;
                        }

                        if (ssh.OrgasmAllowed == true)
                        {
                            ssh.LastOrgasmType = "ALLOWED";
                            ssh.OrgasmAllowed = false;
                            goto AllowedOrgasm;
                        }


                        Debug.Print("Ruined Orgasm skipped");

                        if (ssh.OrgasmDenied == true)
                        {
                            ssh.LastOrgasmType = "DENIED";

                            if (FrmSettings.CBDomDenialEnds.Checked == false & ssh.TeaseTick < 1)
                            {
                                int RepeatChance = ssh.randomizer.Next(0, 101);

                                if (RepeatChance < 10 * FrmSettings.domlevelNumBox.Value | (ssh.SecondSession & FrmSettings.CBDomDenialEnds.Checked == false))
                                {
                                    ssh.SecondSession = false;
                                    ssh.SubEdging = false;
                                    ssh.SubStroking = false;
                                    EdgeTauntTimer.Stop();

                                    List<string> RepeatList = new List<string>();

                                    foreach (string foundFile in My.Computer.FileSystem.GetFiles(Application.StartupPath + @"\Scripts\" + dompersonalitycombobox.Text + @"\Interrupt\Denial Continue\", Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, "*.txt"))
                                        RepeatList.Add(foundFile);

                                    if (RepeatList.Count < 1)
                                        goto NoRepeatFiles;


                                    if (FrmSettings.CBTeaseLengthDD.Checked == true)
                                    {
                                        if (FrmSettings.domlevelNumBox.Value == 1)
                                            ssh.TeaseTick = ssh.randomizer.Next(10, 16) * 60;
                                        if (FrmSettings.domlevelNumBox.Value == 2)
                                            ssh.TeaseTick = ssh.randomizer.Next(15, 21) * 60;
                                        if (FrmSettings.domlevelNumBox.Value == 3)
                                            ssh.TeaseTick = ssh.randomizer.Next(20, 31) * 60;
                                        if (FrmSettings.domlevelNumBox.Value == 4)
                                            ssh.TeaseTick = ssh.randomizer.Next(30, 46) * 60;
                                        if (FrmSettings.domlevelNumBox.Value == 5)
                                            ssh.TeaseTick = ssh.randomizer.Next(45, 61) * 60;
                                    }
                                    else
                                        ssh.TeaseTick = ssh.randomizer.Next(FrmSettings.NBTeaseLengthMin.Value * 60, FrmSettings.NBTeaseLengthMax.Value * 60);
                                    TeaseTimer.Start();

                                    ssh.OrgasmYesNo = false;

                                    // Github Patch
                                    ssh.YesOrNo = false;

                                    // ShowModule = True
                                    ssh.StrokeTauntVal = -1;
                                    ssh.FileText = RepeatList[ssh.randomizer.Next(0, RepeatList.Count)];
                                    ssh.ScriptTick = 2;
                                    ScriptTimer.Start();
                                    ssh.OrgasmDenied = false;
                                    ssh.OrgasmYesNo = false;
                                    ssh.EndTease = false;
                                    return;
                                }
                                else if (ssh.LastScript == true)
                                    ssh.EndTease = true;
                            }
                        }

                    NoRepeatFiles:
                        ;
                        ssh.DomTypeCheck = true;
                        ssh.OrgasmYesNo = false;
                        ssh.SubEdging = false;
                        ssh.SubStroking = false;
                        EdgeTauntTimer.Stop();
                        ssh.DomChat = "#StopStrokingEdge";
                        if (ssh.Contact1Edge == true)
                        {
                            ssh.DomChat = "@Contact1 #StopStrokingEdge";
                            ssh.Contact1Edge = false;
                        }
                        if (ssh.Contact2Edge == true)
                        {
                            ssh.DomChat = "@Contact2 #StopStrokingEdge";
                            ssh.Contact2Edge = false;
                        }
                        if (ssh.Contact3Edge == true)
                        {
                            ssh.DomChat = "@Contact3 #StopStrokingEdge";
                            ssh.Contact3Edge = false;
                        }
                        TypingNoDelay();
                        return;
                    }

                RuinedOrgasm:
                    ;
                    _settings.LastRuined = Strings.FormatDateTime(DateTime.Now, DateFormat.ShortDate);
                    FrmSettings.LBLLastRuined.Text = _settings.LastRuined;

                    if (FrmSettings.CBDomOrgasmEnds.Checked == false & ssh.OrgasmRuined == true & ssh.TeaseTick < 1)
                    {
                        int RepeatChance = ssh.randomizer.Next(0, 101);

                        if (RepeatChance < 8 * FrmSettings.domlevelNumBox.Value | (ssh.SecondSession & FrmSettings.CBDomDenialEnds.Checked == false))
                        {
                            ssh.SecondSession = false;
                            ssh.SubEdging = false;
                            ssh.SubStroking = false;
                            ssh.EdgeToRuin = false;
                            ssh.EdgeToRuinSecret = true;
                            EdgeTauntTimer.Stop();

                            List<string> RepeatList = new List<string>();

                            foreach (string foundFile in My.Computer.FileSystem.GetFiles(Application.StartupPath + @"\Scripts\" + dompersonalitycombobox.Text + @"\Interrupt\Ruin Continue\", Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, "*.txt"))
                                RepeatList.Add(foundFile);

                            if (RepeatList.Count < 1)
                                goto NoRepeatRFiles;


                            if (FrmSettings.CBTeaseLengthDD.Checked == true)
                            {
                                if (FrmSettings.domlevelNumBox.Value == 1)
                                    ssh.TeaseTick = ssh.randomizer.Next(10, 16) * 60;
                                if (FrmSettings.domlevelNumBox.Value == 2)
                                    ssh.TeaseTick = ssh.randomizer.Next(15, 21) * 60;
                                if (FrmSettings.domlevelNumBox.Value == 3)
                                    ssh.TeaseTick = ssh.randomizer.Next(20, 31) * 60;
                                if (FrmSettings.domlevelNumBox.Value == 4)
                                    ssh.TeaseTick = ssh.randomizer.Next(30, 46) * 60;
                                if (FrmSettings.domlevelNumBox.Value == 5)
                                    ssh.TeaseTick = ssh.randomizer.Next(45, 61) * 60;
                            }
                            else
                                ssh.TeaseTick = ssh.randomizer.Next(FrmSettings.NBTeaseLengthMin.Value * 60, FrmSettings.NBTeaseLengthMax.Value * 60);
                            TeaseTimer.Start();

                            ssh.OrgasmYesNo = false;

                            // Github Patch
                            ssh.YesOrNo = false;

                            // ShowModule = True
                            ssh.StrokeTauntVal = -1;
                            ssh.FileText = RepeatList[ssh.randomizer.Next(0, RepeatList.Count)];
                            ssh.ScriptTick = 2;
                            ScriptTimer.Start();
                            ssh.OrgasmRuined = false;
                            ssh.OrgasmYesNo = false;
                            ssh.EndTease = false;
                            return;
                        }
                        else if (ssh.LastScript == true)
                            ssh.EndTease = true;
                    }



                NoRepeatRFiles:
                    ;
                    ssh.DomTypeCheck = true;
                    ssh.SubEdging = false;
                    ssh.SubStroking = false;
                    ssh.EdgeToRuin = false;
                    ssh.EdgeToRuinSecret = true;
                    EdgeTauntTimer.Stop();
                    ssh.OrgasmYesNo = false;
                    ssh.DomChat = "#RuinYourOrgasm";
                    if (ssh.Contact1Edge == true)
                    {
                        ssh.DomChat = "@Contact1 #RuinYourOrgasm";
                        ssh.Contact1Edge = false;
                    }
                    if (ssh.Contact2Edge == true)
                    {
                        ssh.DomChat = "@Contact2 #RuinYourOrgasm";
                        ssh.Contact2Edge = false;
                    }
                    if (ssh.Contact3Edge == true)
                    {
                        ssh.DomChat = "@Contact3 #RuinYourOrgasm";
                        ssh.Contact3Edge = false;
                    }
                    TypingNoDelay();
                    if (ssh.LastScript == true)
                        ssh.EndTease = true;
                    return;

                AllowedOrgasm:
                    ;
                    if (_settings.OrgasmsLocked == true)
                    {
                        if (_settings.OrgasmsRemaining < 1)
                        {
                            List<string> NoCumList = new List<string>();

                            foreach (string foundFile in My.Computer.FileSystem.GetFiles(Application.StartupPath + @"\Scripts\" + dompersonalitycombobox.Text + @"\Interrupt\Out of Orgasms\", Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, "*.txt"))
                                NoCumList.Add(foundFile);

                            if (NoCumList.Count < 1)
                                goto NoNoCumFiles;


                            ssh.SubEdging = false;
                            ssh.SubStroking = false;
                            EdgeTauntTimer.Stop();
                            ssh.OrgasmYesNo = false;

                            // Github Patch
                            ssh.YesOrNo = false;

                            // ShowModule = True
                            ssh.StrokeTauntVal = -1;
                            ssh.FileText = NoCumList[ssh.randomizer.Next(0, NoCumList.Count)];
                            ssh.ScriptTick = 2;
                            ScriptTimer.Start();
                            return;
                        }


                        _settings.OrgasmsRemaining -= 1;
                    }

                NoNoCumFiles:
                    ;
                    _settings.LastOrgasm = Strings.FormatDateTime(DateTime.Now, DateFormat.ShortDate);
                    FrmSettings.LBLLastOrgasm.Text = _settings.LastOrgasm;

                    if (FrmSettings.CBDomOrgasmEnds.Checked == false & ssh.TeaseTick < 1)
                    {
                        int RepeatChance = ssh.randomizer.Next(0, 101);

                        if (RepeatChance < 4 * FrmSettings.domlevelNumBox.Value | (ssh.SecondSession & FrmSettings.CBDomDenialEnds.Checked == false))
                        {
                            ssh.SecondSession = false;
                            ssh.SubEdging = false;
                            ssh.SubStroking = false;
                            EdgeTauntTimer.Stop();

                            List<string> RepeatList = new List<string>();

                            foreach (string foundFile in My.Computer.FileSystem.GetFiles(Application.StartupPath + @"\Scripts\" + dompersonalitycombobox.Text + @"\Interrupt\Orgasm Continue\", Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, "*.txt"))
                                RepeatList.Add(foundFile);

                            if (RepeatList.Count < 1)
                                goto NoRepeatOFiles;


                            if (FrmSettings.CBTeaseLengthDD.Checked == true)
                            {
                                if (FrmSettings.domlevelNumBox.Value == 1)
                                    ssh.TeaseTick = ssh.randomizer.Next(10, 16) * 60;
                                if (FrmSettings.domlevelNumBox.Value == 2)
                                    ssh.TeaseTick = ssh.randomizer.Next(15, 21) * 60;
                                if (FrmSettings.domlevelNumBox.Value == 3)
                                    ssh.TeaseTick = ssh.randomizer.Next(20, 31) * 60;
                                if (FrmSettings.domlevelNumBox.Value == 4)
                                    ssh.TeaseTick = ssh.randomizer.Next(30, 46) * 60;
                                if (FrmSettings.domlevelNumBox.Value == 5)
                                    ssh.TeaseTick = ssh.randomizer.Next(45, 61) * 60;
                            }
                            else
                                ssh.TeaseTick = ssh.randomizer.Next(FrmSettings.NBTeaseLengthMin.Value * 60, FrmSettings.NBTeaseLengthMax.Value * 60);
                            TeaseTimer.Start();

                            ssh.OrgasmYesNo = false;

                            // Github Patch
                            ssh.YesOrNo = false;

                            // ShowModule = True
                            ssh.StrokeTauntVal = -1;
                            ssh.FileText = RepeatList[ssh.randomizer.Next(0, RepeatList.Count)];
                            ssh.ScriptTick = 2;
                            ScriptTimer.Start();
                            ssh.OrgasmAllowed = false;
                            ssh.OrgasmYesNo = false;
                            ssh.EndTease = false;
                            return;
                        }
                        else if (ssh.LastScript == true)
                            ssh.EndTease = true;
                    }



                NoRepeatOFiles:
                    ;
                    ssh.DomTypeCheck = true;
                    ssh.SubEdging = false;
                    ssh.SubStroking = false;
                    // OrgasmAllowed = False
                    EdgeTauntTimer.Stop();
                    ssh.OrgasmYesNo = false;
                    ssh.DomChat = "#CumForMe";
                    if (ssh.Contact1Edge == true)
                    {
                        ssh.DomChat = "@Contact1 #CumForMe";
                        ssh.Contact1Edge = false;
                    }
                    if (ssh.Contact2Edge == true)
                    {
                        ssh.DomChat = "@Contact2 #CumForMe";
                        ssh.Contact2Edge = false;
                    }
                    if (ssh.Contact3Edge == true)
                    {
                        ssh.DomChat = "@Contact3 #CumForMe";
                        ssh.Contact3Edge = false;
                    }
                    TypingNoDelay();
                    if (ssh.LastScript == true)
                        ssh.EndTease = true;
                    return;
                }



                if (ssh.SubStroking == true)
                {
                    int TauntStop = ssh.randomizer.Next(1, 101);

                    if (TauntStop <= FrmSettings.NBTauntEdging.Value)
                    {
                        ssh.FirstRound = false;
                        // ShowModule = True
                        StrokeTauntTimer.Stop();
                        StrokeTimer.Stop();


                        if (ssh.BookmarkModule == true)
                        {
                            ssh.DomTypeCheck = true;
                            ssh.SubEdging = false;
                            ssh.SubStroking = false;
                            ssh.DomChat = "#StopStrokingEdge";
                            if (ssh.Contact1Edge == true)
                            {
                                ssh.DomChat = "@Contact1 #StopStrokingEdge";
                                ssh.Contact1Edge = false;
                            }
                            if (ssh.Contact2Edge == true)
                            {
                                ssh.DomChat = "@Contact2 #StopStrokingEdge";
                                ssh.Contact2Edge = false;
                            }
                            if (ssh.Contact3Edge == true)
                            {
                                ssh.DomChat = "@Contact3 #StopStrokingEdge";
                                ssh.Contact3Edge = false;
                            }
                            TypingNoDelay();

                            do
                                Application.DoEvents();
                            while (!ssh.DomTypeCheck == false);

                            ssh.BookmarkModule = false;
                            ssh.FileText = ssh.BookmarkModuleFile;
                            ssh.StrokeTauntVal = ssh.BookmarkModuleLine;
                            RunFileText();
                            return;
                        }

                        RunModuleScript(true);
                    }
                    else
                    {
                        ssh.TauntEdging = true;
                        ssh.DomChat = "#SYS_TauntEdging";
                        TypingDelay();
                    }
                }


                return;
            }


            if (ssh.EdgeFound == true & _settings.Chastity == true)
            {
                ssh.EdgeFound = false;
                ssh.EdgeNOT = true;
            }






        DebugAwareness:
            ;
            if (ssh.InputFlag == true & ssh.DomTypeCheck == false)
                SetVariable(ssh.InputString, ssh.ChatString);

            // Remove commas and apostrophes from user's entered text
            ssh.ChatString = ssh.ChatString.Replace(",", "");
            ssh.ChatString = ssh.ChatString.Replace("'", "");
            ssh.ChatString = ssh.ChatString.Replace(".", "");


            if (UCase(ssh.ChatString) == Strings.UCase("CAME") | UCase(ssh.ChatString) == Strings.UCase("I CAME") | UCase(ssh.ChatString) == Strings.UCase("JUST CAME") | UCase(ssh.ChatString) == Strings.UCase("I JUST CAME"))
            {
                if (ssh.cameMode.MessageMode == true)
                {
                    ssh.cameMode.MessageMode = false;
                    ssh.ChatString = ssh.cameMode.MessageText;
                }
            }

            if (UCase(ssh.ChatString) == Strings.UCase("RUINED") | UCase(ssh.ChatString) == Strings.UCase("I RUINED") | UCase(ssh.ChatString) == Strings.UCase("RUINED IT") | UCase(ssh.ChatString) == Strings.UCase("I RUINED IT"))
            {
                if (ssh.ruinMode.MessageMode == true)
                {
                    ssh.ruinMode.MessageMode = false;
                    ssh.ChatString = ssh.ruinMode.MessageText;
                }
            }


            // If the domme is waiting for a response, go straight to this sub-routine instead
            if (ssh.YesOrNo == true & ssh.SubEdging == true)
                goto EdgeSkip;
            if (ssh.YesOrNo == true & ssh.SubHoldingEdge == true)
                goto EdgeSkip;

            if (ssh.YesOrNo == true & ssh.OrgasmYesNo == false & ssh.DomTypeCheck == false)
            {
                YesOrNoQuestions();
                return;
            }



        EdgeSkip:
            ;
            Debug.Print("Before Dom Response, YesOrNo = " + ssh.YesOrNo);

            DomResponse();
        }

        private bool OnPauseCheck(out bool bPauseCheck)
        {
            /*
            if (FrmSettings.CBSettingsPause.Checked == true & FrmSettings.SettingsPanel.Visible == true)
            {
                Interaction.MsgBox("Please close the settings menu or disable \"Pause Program When Settings Menu is Visible\" option first!", Title: "Warning!");
                return;
            }
            */
        }


        private ContactData CreateContactData(ContactType type)
        {
            // we got an Rng too ???
            return new ContactData(type, _settings, ssh.randomizer);
        }

        public void LoadDommeImageFolder(bool newSet = true)
        {
            // check which domme should be loaded
            if (!ssh.newSlideshow)
            {
                if (ssh.glitterDommeNumber == 0)
                {
                    if (!_settings.CBRandomDomme)
                    {
                        if (ssh.dommePresent && ssh.SlideshowMasterDomme != null)
                            ssh.SlideshowMain = ssh.SlideshowMasterDomme;
                        else
                        {
                            newSet = true;
                            ssh.SlideshowMain = CreateContactData(ContactType.Domme);
                        }

                        ssh.tempHonorific = PoundClean(_settings.SubHonorific);
                    }
                    else
                    {
                        ssh.SlideshowMain = CreateContactData(ContactType.Random);
                        ssh.tempHonorific = PoundClean(_settings.RandomHonorific);
                    }
                }
                else if (ssh.glitterDommeNumber == 1)
                {
                    if (!ssh.contact1Present)
                        ssh.SlideshowMain = CreateContactData(ContactType.Contact1);
                    else
                        ssh.SlideshowMain = ssh.SlideshowContact1;
                    ssh.tempHonorific = _settings.G1Honorific;
                }
                else if (ssh.glitterDommeNumber == 2)
                {
                    if (!ssh.contact2Present)
                        ssh.SlideshowMain = CreateContactData(ContactType.Contact2);
                    else
                        ssh.SlideshowMain = ssh.SlideshowContact2;
                    ssh.tempHonorific = _settings.G2Honorific;
                }
                else if (ssh.glitterDommeNumber == 3)
                {
                    if (!ssh.contact3Present)
                        ssh.SlideshowMain = CreateContactData(ContactType.Contact3);
                    else
                        ssh.SlideshowMain = ssh.SlideshowContact3;
                    ssh.tempHonorific = _settings.G3Honorific;
                }
                else if (ssh.glitterDommeNumber == 4)
                {
                    if (newSet)
                    {
                        ssh.SlideshowMain = CreateContactData(ContactType.Random);
                        ssh.tempHonorific = PoundClean(_settings.RandomHonorific);
                        ssh.SlideshowContactRandom = ssh.SlideshowMain;
                    }
                    else
                        ssh.SlideshowMain = ssh.SlideshowContactRandom;
                    ssh.contactToUse = ssh.SlideshowMain;
                }
            }
            if (newSet || ssh.SlideshowMain.TypeName == "")
                ssh.SlideshowMain.LoadNew(ssh.newSlideshow);
            if (ssh.glitterDommeNumber == 0)
                ssh.SlideshowMasterDomme = ssh.SlideshowMain;
            ssh.tempDomName = ssh.SlideshowMain.TypeName;
            ssh.tempDomHonorific = ssh.tempHonorific;
            if (ssh.tempDomName != _settings.DomName)
            {
                string avatarImage = checkForImage("/avatar.*", ssh.SlideshowMain.getCurrentBaseFolder(ssh.SlideshowMain.Contact));
                if (avatarImage == "")
                    avatarImage = ssh.SlideshowMain.ImageList(ssh.randomizer.Next(0, ssh.SlideshowMain.ImageList().Count - 1));
                domAvatar.Image = Image.FromFile(avatarImage);
                ssh.domAvatarImage = avatarImage;
            }
            ssh.shortName = ssh.SlideshowMain.ShortName;
            this.domName.Text = ssh.tempDomName;
            FrmSettings.LBLCurrentDomme.Text = ssh.tempDomName;
            ssh.newSlideshow = false;

            ShowImage(ssh.SlideshowMain.CurrentImage, false);
            ssh.SlideshowLoaded = true;
            ssh.JustShowedBlogImage = false;

            nextButton.Enabled = true;
            previousButton.Enabled = true;
            PicStripTSMIdommeSlideshow.Enabled = true;

            if (ssh.RiskyDeal == true)
                FrmCardList.PBRiskyPic.Image = Image.FromFile(ssh.SlideshowMain.CurrentImage);

            if (FrmSettings.timedRadio.Checked == true)
            {
                ssh.SlideshowTimerTick = FrmSettings.slideshowNumBox.Value;
                SlideshowTimer.Start();
            }
        }


        public void InitDommeImageFolder()
        {
            if (_settings.CBAutoDomPP || _settings.CBRandomDomme)
            {
                string avatarImage = checkForImage("/avatar*.*", ssh.SlideshowMain.getCurrentBaseFolder(ssh.SlideshowMain.Contact));
                if (string.IsNullOrEmpty(avatarImage))
                    avatarImage = ssh.SlideshowMain.ImageList[ssh.randomizer.Next(0, ssh.SlideshowMain.ImageList.Count - 1)];
                OnSetAvatarImage(avatarImage);

                ssh.domAvatarImage = avatarImage;
                string glitterImage2 = checkForImage("/glitter*.*", ssh.SlideshowMain.getCurrentBaseFolder(ssh.SlideshowMain.Contact));
                if (string.IsNullOrEmpty(glitterImage2))
                    glitterImage2 = avatarImage;
                if (!string.IsNullOrEmpty(glitterImage2))
                {
                    FrmSettings.GlitterAV.Image = Image.FromFile(glitterImage2);
                    if (!FrmSettings.CBRandomDomme.Checked)
                        _settings.GlitterAV = glitterImage2;
                }
            }
            CheckRandomOpportunities();
            if (FrmSettings.CBRandomGlitter.Checked && FrmSettings.CBRandomGlitter.Enabled)
            {
                string Contact1dirspecial = _settings.RandomImageDir + @"\#Contact1";
                string Contact2dirspecial = _settings.RandomImageDir + @"\#Contact2";
                string Contact3dirspecial = _settings.RandomImageDir + @"\#Contact3";
                string Contact1path = "";
                string Contact2path = "";
                string Contact3path = "";
                if (Directory.Exists(Contact1dirspecial))
                    Contact1path = LoadRandomFolder(Contact1dirspecial);
                else
                    Contact1path = LoadRandomFolder(_settings.RandomImageDir);

                if (Directory.Exists(Contact2dirspecial))
                    Contact2path = LoadRandomFolder(Contact2dirspecial);
                else
                    Contact2path = LoadRandomFolder(_settings.RandomImageDir);

                if (Directory.Exists(Contact3dirspecial))
                    Contact3path = LoadRandomFolder(Contact3dirspecial);
                else
                    Contact3path = LoadRandomFolder(_settings.RandomImageDir);


                DirectoryInfo Contact1dir = Directory.GetParent(Contact1path);
                while (Contact1dir.Name == ssh.tempDomName)
                {
                    Contact1path = LoadRandomFolder(_settings.RandomImageDir);
                    Contact1dir = Directory.GetParent(Contact1path);
                }
                DirectoryInfo Contact2dir = Directory.GetParent(Contact2path);
                while (Contact2dir.Name == ssh.tempDomName || Contact2dir.Name == Contact1dir.Name)
                {
                    Contact2path = LoadRandomFolder(_settings.RandomImageDir);
                    Contact2dir = Directory.GetParent(Contact2path);
                }

                DirectoryInfo Contact3dir = Directory.GetParent(Contact3path);
                while (Contact3dir.Name == ssh.tempDomName || Contact3dir.Name == Contact2dir.Name || Contact3dir.Name == Contact1dir.Name)
                {
                    Contact3path = LoadRandomFolder(_settings.RandomImageDir);
                    Contact3dir = Directory.GetParent(Contact3path);
                }


                _settings.Contact1ImageDir = Contact1dir.FullName;
                _settings.Contact2ImageDir = Contact2dir.FullName;
                _settings.Contact3ImageDir = Contact3dir.FullName;
                _settings.Glitter1 = Contact1dir.Name;
                _settings.Glitter2 = Contact2dir.Name;
                _settings.Glitter3 = Contact3dir.Name;
                ssh.SlideshowContact1 = CreateContactData(ContactType.Contact1);
                ssh.SlideshowContact1.LoadNew(ssh.newSlideshow);
                string glitterImage = checkForImage("/glitter*.*", Contact1path);
                if (glitterImage == "")
                    glitterImage = checkForImage("/*", Contact1path);
                if (glitterImage != "")
                {
                    _settings.GlitterAV1 = glitterImage;
                    FrmSettings.GlitterAV1.Image = Image.FromFile(glitterImage);
                }
                ssh.SlideshowContact2 = CreateContactData(ContactType.Contact2);
                ssh.SlideshowContact2.LoadNew(ssh.newSlideshow);
                glitterImage = checkForImage("/glitter*.*", Contact2path);
                if (glitterImage == "")
                    glitterImage = checkForImage("/*", Contact2path);
                if (glitterImage != "")
                {
                    _settings.GlitterAV2 = glitterImage;
                    FrmSettings.GlitterAV2.Image = Image.FromFile(glitterImage);
                }

                ssh.SlideshowContact3 = CreateContactData(ContactType.Contact3);
                ssh.SlideshowContact3.LoadNew(ssh.newSlideshow);
                glitterImage = checkForImage("/glitter*.*", Contact3path);
                if (glitterImage == "")
                    glitterImage = checkForImage("/*", Contact3path);
                if (glitterImage != "")
                {
                    _settings.GlitterAV3 = glitterImage;
                    FrmSettings.GlitterAV3.Image = Image.FromFile(glitterImage);
                }
            }
            string RandImgDirectoryD = @"\#RandomPics";
            string RandImgPathDirectoryD = ssh.SlideshowMain.getCurrentBaseFolder(ssh.SlideshowMain.Contact) + RandImgDirectoryD;
            string ClipDirectoryD = @"\#Clips";
            string ClipPathDirectoryD = ssh.SlideshowMain.getCurrentBaseFolder(ssh.SlideshowMain.Contact) + ClipDirectoryD;
            if ((Directory.Exists(RandImgDirectoryD)))

                // Conversions.ToString(_settings.PropertyValues["DomImageDirRand"].Property.DefaultValue)
                _settings.DomImageDirRand = RandImgPathDirectoryD;
            if ((Directory.Exists(ClipPathDirectoryD)))
            {
                string def = _settings.PropertyValues("VideoSoftcoreD").Property.DefaultValue;
                _settings.VideoSoftcoreD = ClipPathDirectoryD + @"\Softcore";
                if (FrmSettings.VideoSoftcoreD_Count(false) != 0)
                    _settings.CBSoftcoreD = true;
                else
                {
                    _settings.CBSoftcoreD = false;
                    _settings.VideoSoftcoreD = def;
                }
                def = _settings.PropertyValues("VideoHardcoreD").Property.DefaultValue;
                _settings.VideoHardcoreD = ClipPathDirectoryD + @"\Hardcore";
                if (FrmSettings.VideoHardcoreD_Count(false) != 0)
                    _settings.CBHardcoreD = true;
                else
                {
                    _settings.CBHardcoreD = false;
                    _settings.VideoHardcoreD = def;
                }
                def = _settings.PropertyValues("VideoLesbianD").Property.DefaultValue;
                _settings.VideoLesbianD = ClipPathDirectoryD + @"\Lesbian";
                if (FrmSettings.VideoLesbianD_Count(false) != 0)
                    _settings.CBLesbianD = true;
                else
                {
                    _settings.CBLesbianD = false;
                    _settings.VideoLesbianD = def;
                }
                def = _settings.PropertyValues("VideoBlowjobD").Property.DefaultValue;
                _settings.VideoBlowjobD = ClipPathDirectoryD + @"\Blowjob";
                if (FrmSettings.VideoBlowjobD_Count(false) != 0)
                    _settings.CBBlowjobD = true;
                else
                {
                    _settings.CBBlowjobD = false;
                    _settings.VideoBlowjobD = def;
                }
                def = _settings.PropertyValues("VideoFemsubD").Property.DefaultValue;
                _settings.VideoFemsubD = ClipPathDirectoryD + @"\Femsub";
                if (FrmSettings.VideoFemsubD_Count(false) != 0)
                    _settings.CBFemsubD = true;
                else
                {
                    _settings.CBFemsubD = false;
                    _settings.VideoFemsubD = def;
                }
                def = _settings.PropertyValues("VideoJOID").Property.DefaultValue;
                _settings.VideoJOID = ClipPathDirectoryD + @"\JOI";
                if (FrmSettings.VideoJOID_Count(false) != 0)
                    _settings.CBJOID = true;
                else
                {
                    _settings.CBJOID = false;
                    _settings.VideoJOID = def;
                }
                def = _settings.PropertyValues("VideoCHD").Property.DefaultValue;
                _settings.VideoCHD = ClipPathDirectoryD + @"\CH";
                if (FrmSettings.VideoCHD_Count(false) != 0)
                    _settings.CBCHD = true;
                else
                {
                    _settings.CBCHD = false;
                    _settings.VideoCHD = def;
                }
                def = _settings.PropertyValues("VideoGeneralD").Property.DefaultValue;
                _settings.VideoGeneralD = ClipPathDirectoryD + @"\General";
                if (FrmSettings.VideoGeneralD_Count(false) != 0)
                    _settings.CBGeneralD = true;
                else
                {
                    _settings.CBGeneralD = false;
                    _settings.VideoGeneralD = def;
                }
            }
            if (ssh.currentlyPresentContacts.Count == 0)
                ssh.currentlyPresentContacts.Add(ssh.SlideshowMain.TypeName);
        }

        private void OnSetAvatarImage(string avatarImage)
        {
            //                 domAvatar.Image = Image.FromFile(avatarImage);
            throw new NotImplementedException();
        }

        public string LoadRandomFolder(string baseDirectory)
        {
            if (!Directory.Exists(baseDirectory))
                throw new DirectoryNotFoundException($"The given slideshow base directory {baseDirectory}  was not found.");
            string[] baseDirs = DirectoryExt.GetDirectories(baseDirectory);
            var selectedBaseDir = baseDirs[ssh.randomizer.Next(0, baseDirs.Length)];
            var subDirs = DirectoryExt.GetDirectories(selectedBaseDir).ToList();
            string rndFolder = null;
            do
            {
                rndFolder = subDirs[ssh.randomizer.Next(0, subDirs.Count)];
                if (DirectoryExt.GetFilesImages(rndFolder, System.IO.SearchOption.TopDirectoryOnly).Count > 0)
                    break;
                subDirs.Remove(rndFolder);
            }
            while (true);
            return rndFolder;
        }


        private string checkForImage(string ImageToShow, string imageDir = "")
        {
            string tmpImgLoc = "";
            bool throwException = false;
            if (imageDir == "" & !(ImageToShow.Contains(@":\") || ImageToShow.Contains(":/")))
            {
                imageDir = Path.Combine(_rootPath, @"Images");
                throwException = true;
            }
            try
            {
                if (Common.IsUrl(ImageToShow))
                {
                    // ########################## ImageURL was given #########################
                    tmpImgLoc = ImageToShow;
                    return tmpImgLoc;
                }

                // Change evtl. wrong given Slashes
                ImageToShow = ImageToShow.Replace("/", @"\");

                ImageToShow = imageDir + ImageToShow;
                ImageToShow = ImageToShow.Replace(@"\\", @"\");

                if (ImageToShow.Contains("*"))
                {
                    // ######################### Directory was given #########################
                    string tmpFilter = Path.GetFileName(ImageToShow);
                    string tmpDir = Path.GetDirectoryName(ImageToShow);
                    List<string> ImageList;

                    if (!Directory.Exists(tmpDir))
                    {
                        if (throwException)
                            throw new Exception("The given directory \"" + tmpDir + "\" does not exist." + Environment.NewLine + Environment.NewLine + "Please make sure the directory exists and it is spelled correctly in the script.");
                    }

                    if (tmpFilter == "*" || tmpFilter == "*.*")
                        ImageList = DirectoryExt.GetFilesImages(tmpDir);
                    else
                        ImageList = Directory.GetFiles(tmpDir, tmpFilter, SearchOption.TopDirectoryOnly).ToList();

                    if (ImageList.Count == 0)
                    {
                        if (throwException)
                            throw new FileNotFoundException("No images matching the filter \"" + tmpFilter + "\" were found in \"" + tmpDir + "\"!" + Environment.NewLine + Environment.NewLine + "Please make sure that valid files exist and the wildcards are applied correctly in the script.");
                        tmpImgLoc = "";
                    }
                    else
                        tmpImgLoc = ImageList[ssh.randomizer.Next(0, ImageList.Count)];
                }
                else
                    // ############################# Single Image ############################
                    if (File.Exists(ImageToShow))
                    tmpImgLoc = ImageToShow;
                else if (throwException)
                    throw new Exception("\"" + Path.GetFileName(ImageToShow) + "\" was not found in \"" + Path.GetDirectoryName(ImageToShow) + "\"!" + Environment.NewLine + Environment.NewLine + "Please make sure the file exists and it is spelled correctly in the script.");
                
            }
            catch (Exception ex)
            {
                // ▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                // All Errors
                // ▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                //if (throwException)
                _log.WriteError("Command @ShowImage[] was unable to display the image.", ex, "Error at @ShowImage[]");
            }
            return tmpImgLoc;
        }


    }

#endif
}
