using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Tai.Common
{
    public partial class Engine
    {

        private void StrokeTimer_Tick(System.Object sender, System.EventArgs e)
        {
            if (ssh.InputFlag)
                return;
            var paused = false;
            OnPauseCheck(out paused);
            if (paused) return;

            if (ssh.DomTypeCheck == true & ssh.StrokeTick < 5)
                return;

            //if (chatBox.Text != "" & ssh.StrokeTick < 5)
            //    return;
            //if (ChatBox2.Text != "" & ssh.StrokeTick < 5)
            //     return;

            if (ssh.FollowUp != "" & ssh.StrokeTick < 5)
                return;


            ssh.StrokeTick -= 1;
            //FrmSettings.LBLCycleDebugCountdown.Text = ssh.StrokeTick;
            // FrmSettings.LBLDebugStrokeTime.Text = ssh.StrokeTick;

            // Debug.Print("StrokeTick = " & StrokeTick)

            if (ssh.StrokeTick < 4 & ssh.TempScriptCount > 0)
                ssh.StrokeTick += 1;

            if (ssh.StrokeTick < 1)
            {
                ssh.FirstRound = false;

                StrokeTimer.Stop();
                StrokeTauntTimer.Stop();

                if (ssh.RunningScript == true)
                {
                    ssh.ScriptTick = 3;
                    ScriptTimer.Start();
                }
                else
                    RunModuleScript(false);
            }
        }


        private void WaitTimer_Tick(System.Object sender, System.EventArgs e)
        {
            var paused = false;
            OnPauseCheck(out paused);
            if (paused) return;

            if (ssh.DomTypeCheck || ssh.YesOrNo)
                return;

            // Debug.Print("WaitTick = " & WaitTick)

            ssh.WaitTick -= 1;

            if (ssh.WaitTick < 1)
            {
                WaitTimer.Stop();
                ssh.ScriptTick = 1;
                if (ssh.RapidCode)
                    RunFileText();
            }
        }



        public void ScriptTimer_Tick(System.Object sender, System.EventArgs e)
        {
            // FrmSettings.LBLDebugScriptTime.Text = ssh.ScriptTick;
            OnSetDebugScriptTime(ssh.ScriptTick);
            // Debug.Print("ScriptTick = " & ScriptTick)

            if (ssh.DomTyping == true)
                return;
            if (ssh.YesOrNo == true)
                return;

            // If ChatText.IsBusy Then Return

            if (WaitTimer.Enabled == true || ssh.DomTypeCheck == true)
                return;

            // Debug.Print("ScriptTimer Substroking = " & SubStroking)
            // Debug.Print("ScriptTimer StrokePaceTimer = " & StrokePaceTimer.Enabled)

            var paused = false;
            OnPauseCheck(out paused);
            if (paused)
            {
                return;
            }

            //if (_settings.CBSettingsPause & FrmSettings.SettingsPanel.Visible == true)
            //    return;

            /* mciSend String something to do with speech ??
             * 
            if (playingStatus() == true)
            {
                if (ssh.ScriptTick < 4)
                    return;
            }
            */

            if (ssh.DomTypeCheck && ssh.ScriptTick < 4)
                return;


            // we have no idea what's in the chat box, we're a server? although we could add this later (signal user is typing/user is not typing)

            //if (chatBox.Text != "" && ssh.ScriptTick < 4)
            //    return;
            //if (ChatBox2.Text != "" & ssh.ScriptTick < 4)
            //    return;


            ssh.ScriptTick -= 1;
            // Debug.Print("ScriptTick = " & ScriptTick)
            if (ssh.ScriptTick < 1)
            {
                // no idea what a script tick is??
                ssh.ScriptTick = ssh.randomizer.Next(4, 7);

                RunFileText();
            }
        }

        private void OnSetDebugScriptTime(int scriptTick)
        {
            throw new NotImplementedException();
        }

        private void StrokeTauntTimer_Tick(System.Object sender, System.EventArgs e)
        {
            if (ssh.InputFlag == true)
                return;

            var paused = false;
            OnPauseCheck(out paused);
            if (paused) return;

            if (ssh.DomTyping == true)
                return;
            if (ssh.DomTypeCheck == true & ssh.StrokeTauntTick < 6)
                return;
            //if (chatBox.Text != "" & ssh.StrokeTauntTick < 6)
            //    return;
            //if (ChatBox2.Text != "" & ssh.StrokeTauntTick < 6)
            //     return;








            ssh.StrokeTauntTick -= 1;

            // FrmSettings.LBLDebugStrokeTauntTime.Text = ssh.StrokeTauntTick;

            if (ssh.StrokeTauntTick > 0)
                // #################### Time hasn't come #######################
                return;
            else if (ssh.TempScriptCount <= 0)
            {
                // ##################### Taunt from File #######################

                string tauntFile = "StrokeTaunts";
                if (_settings.Chastity)
                    tauntFile = "ChastityTaunts";
                if (ssh.GlitterTease)
                    tauntFile = "GlitterTaunts";

                List<TauntProcessingObject> TauntFiles = new List<TauntProcessingObject>();

                var path = Path.Combine(ssh.Folders.Personality, "Stroke");
                foreach (string str in Directory.EnumerateFiles(path, tauntFile + "_*.txt", SearchOption.TopDirectoryOnly))
                {
                    TauntProcessingObject Taunt = new TauntProcessingObject(str, ssh.randomizer);
                    if (Taunt.Avaialable)
                        TauntFiles.Add(Taunt);
                }


                if (TauntFiles.Count == 0)
                {
                    // No Taunt available
                    ssh.TauntText = "";
                    ssh.TauntLines = new List<string>();
                    ssh.TauntTextCount = 0;
                    ssh.TempScriptCount = 0;
                }
                else
                {
                    // Taunt available
                    TauntProcessingObject TauntToUse;

                    // Increase chance of one line taunt
                    int OneLineChance = ssh.randomizer.Next(0, 101);

                    if (OneLineChance < 60 && TauntFiles.Find(x => x.TauntSize == 1) != null)
                    {
                        // 1 line taunts available, force to use it.
                        List<TauntProcessingObject> OneLiners = TauntFiles.Where(x => x.TauntSize == 1).ToList();
                        TauntToUse = OneLiners[ssh.randomizer.Next(0, OneLiners.Count)];
                    }
                    else
                        // Pick a random taunt size.
                        TauntToUse = TauntFiles[ssh.randomizer.Next(0, TauntFiles.Count)];

                    ssh.TauntText = TauntToUse.FilePath;
                    ssh.TauntLines = TauntToUse.Lines;
                    ssh.TauntTextCount = TauntToUse.RandomTauntLine;
                    ssh.TempScriptCount = TauntToUse.TauntSize - 1;

                    ssh.MultiTauntPictureHold = false;
                }
            }
            else
            {
                // ##################### Next Taunt line #######################
                ssh.TauntTextCount += 1;

                if (ssh.TempScriptCount > 0)
                    ssh.MultiTauntPictureHold = true;

                ssh.TempScriptCount -= 1;
            }

            if (ssh.TauntLines.Count > 0)
            {
                try
                {
                    ssh.DomTask = ssh.TauntLines[ssh.TauntTextCount];
                }
                catch (Exception ex)
                {
                    _log.WriteError("Tease AI did not return a valid Taunt from file: " + ssh.TauntText, ex, "StrokeTauntTimer.Tick");
                    ssh.DomTask = "ERROR: Tease AI did not return a valid Taunt from file: " + ssh.TauntText;
                }
            }
            else
                try
                {
                    ssh.DomTask = ssh.TauntLines[ssh.TauntTextCount];
                }
                catch (Exception ex)
                {
                    _log.WriteError("Tease AI did not return a valid Taunt from any available file", ex, "StrokeTauntTimer.Tick");
                    ssh.DomTask = "ERROR: Tease AI did not return a valid Taunt from any available file";
                }



            if (ssh.DomTask.IndexOf("@CBT", StringComparison.OrdinalIgnoreCase) >= 0)
                CBTScript();
            else
                TypingDelayGeneric();


            if (ssh.TempScriptCount == 0)
            {
                if (_settings.TimerSTF == 1)
                    ssh.StrokeTauntTick = ssh.randomizer.Next(120, 241);
                if (_settings.TimerSTF == 2)
                    ssh.StrokeTauntTick = ssh.randomizer.Next(75, 121);
                if (_settings.TimerSTF == 3)
                    ssh.StrokeTauntTick = ssh.randomizer.Next(45, 76);
                if (_settings.TimerSTF == 4)
                    ssh.StrokeTauntTick = ssh.randomizer.Next(19, 46);
                if (_settings.TimerSTF == 5)
                    ssh.StrokeTauntTick = ssh.randomizer.Next(12, 25);
            }
            else
                ssh.StrokeTauntTick = ssh.randomizer.Next(5, 9);
        }

        private void EdgeTauntTimer_Tick(System.Object sender, System.EventArgs e)
        {
            if (MultipleEdgesTimer.Enabled)
                return;
            if (ssh.InputFlag)
                return;

            bool paused = false;
            OnPauseCheck(out paused);
            if (paused) return;


            if (ssh.DomTyping == true)
                return;
            if (ssh.DomTypeCheck == true & ssh.EdgeTauntInt < 6)
                return;
            //if (chatBox.Text != "" & ssh.EdgeTauntInt < 6)
            //    return;
            //if (ChatBox2.Text != "" & ssh.EdgeTauntInt < 6)
            //    return;

            FrmSettings.LBLDebugEdgeTauntTime.Text = ssh.EdgeTauntInt;

            // Debug.Print("EdgeTauntIn = " & EdgeTauntInt)

            ssh.EdgeTauntInt -= 1;

            if (ssh.EdgeTauntInt < 1)
            {
                string File2Read = "";

                if (ssh.GlitterTease == false)
                    File2Read = Path.Combine(DomPersonalityPath, "Stroke/Edge/Edge.txt");
                else
                    File2Read = Path.Combine(DomPersonalityPath, "Stroke/Edge/GroupEdge.txt");

                // Read all lines of the given file.
                List<string> ETLines = Common.Txt2List(File2Read);

                try
                {
                    ETLines = FilterList(ETLines);
                    ssh.DomTask = ETLines[ssh.randomizer.Next(0, ETLines.Count)];
                }
                catch (Exception ex)
                {
                    _log.WriteError("Tease AI did not return a valid Edge Taunt from file " + File2Read, ex, "EdgeTauntTimer.Tick");
                    ssh.DomTask = "ERROR: Tease AI did not return a valid Edge Taunt from file:  " + File2Read;
                }

                TypingDelayGeneric();

                ssh.EdgeTauntInt = ssh.randomizer.Next(6, 21);
            }
        }


        private void EdgeCountTimer_Tick(System.Object sender, System.EventArgs e)
        {
            bool paused = false;
            OnPauseCheck(out paused);
            if (paused) return;

            ssh.EdgeCountTick += 1; // in seconds

            if (_settings.CBEdgeUseAvg)
            {
                if (ssh.EdgeCountTick > ssh.EdgeTickCheck)
                    ssh.LongEdge = true;
            }
            else if (ssh.EdgeCountTick > _settings.LongEdge * 60)
                ssh.LongEdge = true;


            int m = TimeSpan.FromSeconds(ssh.EdgeCountTick).Minutes;
            int s = TimeSpan.FromSeconds(ssh.EdgeCountTick).Seconds;


            TimeSpan TST = TimeSpan.FromSeconds(ssh.EdgeCountTick);
        }

        private void StrokeTimeTotalTimer_Tick(System.Object sender, System.EventArgs e)
        {
            bool paused = false;
            OnPauseCheck(out paused);
            if (paused) return;

            if (ssh.SubStroking == false)
                return;

            ssh.StrokeTimeTotal += 1;
            // Debug.Print("StrokeTimeTotal = " & StrokeTimeTotal)

            _settings.StrokeTimeTotal = ssh.StrokeTimeTotal;

            TimeSpan STT = TimeSpan.FromSeconds(ssh.StrokeTimeTotal);

            // LBLStrokeTimeTotal.Text = String.Format("{0:000} D {1:00} H {2:00} M {3:00} S", STT.Days, STT.Hours, STT.Minutes, STT.Seconds)
            FrmSettings.LBLStrokeTimeTotal.Text = string.Format("{0:0000}:{1:00}:{2:00}:{3:00}", STT.Days, STT.Hours, STT.Minutes, STT.Seconds);
        }

        private void HoldEdgeTimer_Tick(System.Object sender, System.EventArgs e)
        {

            // Debug.Print("HoldEdgeTick = " & HoldEdgeTick)

            ssh.HoldEdgeTime += 1;
            ssh.HoldEdgeTimeTotal += 1;

            _settings.HoldEdgeTimeTotal = ssh.HoldEdgeTimeTotal;

            if (ssh.InputFlag == true)
                return;

            var paused = false;
            OnPauseCheck(out paused);
            if (paused) return;


            // If DomTyping = True Then Return
            if (ssh.DomTypeCheck == true & ssh.HoldEdgeTick < 4)
                return;
            //if (chatBox.Text != "" & ssh.HoldEdgeTick < 4)
            //    return;
            //if (ChatBox2.Text != "" & ssh.HoldEdgeTick < 4)
            //    return;
            if (ssh.FollowUp != "" & ssh.HoldEdgeTick < 4)
                return;

            ssh.HoldEdgeTick -= 1;

            FrmSettings.LBLDebugHoldEdgeTime.Text = ssh.HoldEdgeTick;
            // Debug.Print("HoldEdgeTick = " & HoldEdgeTick)

            if (ssh.HoldEdgeTick < 1)
            {

                // stop
                ssh.LongHold = false;
                ssh.ExtremeHold = false;
                ssh.HoldTaunts = false;
                ssh.LongTaunts = false;
                ssh.ExtremeTaunts = false;
                ssh.WorshipMode = false;
                ssh.WorshipTarget = "";

                // If OrgasmAllowed = True Then GoTo AllowedOrgasm
                // If EdgeToRuin = True Or OrgasmRuined = True Then GoTo RuinedOrgasm

                if (ssh.EdgeToRuin == true || ssh.OrgasmRuined == true)
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

                if (ssh.OrgasmDenied == true)
                {
                    ssh.LastOrgasmType = "DENIED";

                    if (!_settings.DomDenialEnd && ssh.TeaseTick < 1)
                    {
                        int RepeatChance = ssh.randomizer.Next(0, 101);

                        if (RepeatChance < 10 * _settings.DomLevel || (ssh.SecondSession && !_settings.DomDenialEnd))
                        {
                            ssh.SecondSession = false;
                            ssh.SubEdging = false;
                            ssh.SubStroking = false;
                            EdgeTauntTimer.Stop();

                            List<string> RepeatList = new List<string>();

                            var denialContinuePath = Path.Combine(DomPersonalityPath, "Interrupt/Denial Continue");
                            foreach (string foundFile in Directory.EnumerateFiles(denialContinuePath, "*.txt"))
                                RepeatList.Add(foundFile);

                            if (RepeatList.Count < 1)
                                goto NoRepeatFiles;

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
                HoldEdgeTimer.Stop();
                HoldEdgeTauntTimer.Stop();
                ssh.SubHoldingEdge = false;
                ssh.SubStroking = false;
                ssh.OrgasmYesNo = false;
                ssh.DomTask = "#StopStroking";
                if (ssh.Contact1Edge == true)
                {
                    ssh.DomTask = "@Contact1 #StopStroking";
                    ssh.Contact1Edge = false;
                }
                if (ssh.Contact2Edge == true)
                {
                    ssh.DomTask = "@Contact2 #StopStroking";
                    ssh.Contact2Edge = false;
                }
                if (ssh.Contact3Edge == true)
                {
                    ssh.DomTask = "@Contact3 #StopStroking";
                    ssh.Contact3Edge = false;
                }
                TypingDelayGeneric();
                return;

            RuinedOrgasm:
                ;
                _settings.LastRuined = DateTime.Now;
                FrmSettings.LBLLastRuined.Text = My.Settings.LastRuined;

                if (!_settings.DomOrgasmEnd && ssh.OrgasmRuined == true && ssh.TeaseTick < 1)
                {
                    int RepeatChance = ssh.randomizer.Next(0, 101);

                    if (RepeatChance < 8 * _settings.DomLevel || (ssh.SecondSession && !_settings.DomDenialEnd))
                    {
                        EdgeTauntTimer.Stop();
                        HoldEdgeTimer.Stop();
                        HoldEdgeTauntTimer.Stop();
                        ssh.SecondSession = false;
                        ssh.SubHoldingEdge = false;
                        ssh.SubStroking = false;
                        ssh.EdgeToRuin = false;
                        ssh.EdgeToRuinSecret = true;

                        List<string> RepeatList = new List<string>();

                        var ruinContinuePath = Path.Combine(DomPersonalityPath, "Interrupt/Ruin Continue");
                        foreach (string foundFile in Directory.EnumerateFiles(ruinContinuePath, "*.txt"))
                            RepeatList.Add(foundFile);

                        if (RepeatList.Count < 1)
                            goto NoRepeatRFiles;


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
                HoldEdgeTimer.Stop();
                HoldEdgeTauntTimer.Stop();
                ssh.SubHoldingEdge = false;
                ssh.SubStroking = false;
                ssh.EdgeToRuin = false;
                ssh.EdgeToRuinSecret = true;
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
                TypingDelay();
                return;

            AllowedOrgasm:
                ;
                if (_settings.OrgasmsLocked)
                {
                    if (_settings.OrgasmsRemaining < 1)
                    {
                        List<string> NoCumList = new List<string>();

                        var outOfOrgasmsPath = Path.Combine(DomPersonalityPath, "Interrupt/Out of Orgasms");
                        foreach (string foundFile in Directory.EnumerateFiles(outOfOrgasmsPath, "*.txt"))
                            NoCumList.Add(foundFile);

                        if (NoCumList.Count < 1)
                            goto NoNoCumFiles;


                        HoldEdgeTimer.Stop();
                        HoldEdgeTauntTimer.Stop();
                        ssh.SubHoldingEdge = false;
                        ssh.SubStroking = false;
                        ssh.OrgasmYesNo = false;
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
                _settings.LastOrgasm = DateTime.Now;
                FrmSettings.LBLLastOrgasm.Text = My.Settings.LastOrgasm;

                if (!_settings.DomOrgasmEnd && ssh.TeaseTick < 1)
                {
                    int RepeatChance = ssh.randomizer.Next(0, 101);

                    if (RepeatChance < 4 * _settings.DomLevel || (ssh.SecondSession && !_settings.DomDenialEnd))
                    {
                        HoldEdgeTimer.Stop();
                        HoldEdgeTauntTimer.Stop();
                        ssh.SecondSession = false;
                        ssh.SubHoldingEdge = false;
                        ssh.SubStroking = false;
                        ssh.EdgeToRuin = false;
                        ssh.EdgeToRuinSecret = true;
                        EdgeTauntTimer.Stop();

                        List<string> RepeatList = new List<string>();

                        var orgasmContinuePath = Path.Combine(DomPersonalityPath, "Interrupt/Orgasm Continue");

                        foreach (string foundFile in Directory.EnumerateFiles(orgasmContinuePath, "*.txt"))
                            RepeatList.Add(foundFile);

                        if (RepeatList.Count < 1)
                            goto NoRepeatOFiles;


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
                HoldEdgeTimer.Stop();
                HoldEdgeTauntTimer.Stop();
                ssh.SubHoldingEdge = false;
                ssh.SubStroking = false;
                ssh.OrgasmYesNo = false;
                // OrgasmAllowed = False
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
                TypingDelay();
                return;
            }
        }

        private void HoldEdgeTauntTimer_Tick(System.Object sender, System.EventArgs e)
        {
            if (ssh.InputFlag == true)
                return;
            var paused = false;

            OnPauseCheck(out paused);
            if (paused) return;

            if (ssh.DomTyping == true)
                return;
            if (ssh.DomTypeCheck == true & ssh.EdgeTauntInt < 6)
                return;
            //if (chatBox.Text != "" & ssh.EdgeTauntInt < 6)
            //    return;
            //if (ChatBox2.Text != "" & ssh.EdgeTauntInt < 6)
            //    return;

            ssh.EdgeTauntInt -= 1;

            if (ssh.EdgeTauntInt < 1)
            {
                string File2Read = "";

                if (!ssh.GlitterTease)
                    File2Read = Path.Combine(DomPersonalityPath, "Stroke/HoldTheEdge/HoldTheEdge.txt");
                else
                    File2Read = Path.Combine(DomPersonalityPath, "Stroke/HoldTheEdge/GroupHoldTheEdge.txt");

                // Read all lines of given file.
                List<string> ETLines = Common.Txt2List(File2Read);

                try
                {
                    ETLines = FilterList(ETLines);
                    ssh.DomTask = ETLines[ssh.randomizer.Next(0, ETLines.Count)];
                }
                catch (Exception ex)
                {
                    _log.WriteError("Tease AI did not return a valid Hold the Edge Taunt from file: " + File2Read, ex, "HoldEdgeTauntTimer.Tick");
                    ssh.DomTask = "ERROR: Tease AI did not return a valid Hold the Edge Taunt from file:  " + File2Read;
                }

                TypingDelayGeneric();

                ssh.EdgeTauntInt = ssh.randomizer.Next(13, 18);
            }
        }

        public void CensorshipTimer_Tick(System.Object sender, System.EventArgs e)
        {
            var bPaused = false;
            OnPauseCheck(out bPaused);
            if (bPaused) return;

            if (ssh.DomTyping == true)
                return;
            if (ssh.DomTypeCheck == true & ssh.CensorshipTick < 6)
                return;
            //if (chatBox.Text != "" & ssh.CensorshipTick < 6)
            //    return;
            //if (ChatBox2.Text != "" & ssh.CensorshipTick < 6)
           //     return;
            if (ssh.FollowUp != "" & ssh.CensorshipTick < 6)
                return;

            ssh.CensorshipTick -= 1;


            if (ssh.CensorshipTick < 1)
            {
                int CensorLineTemp = ssh.randomizer.Next(1, 101);


                string CensorVideo;

                if (ssh.CensorshipBarVisible || !_settings.CBCensorConstant)
                {
                    ssh.CensorshipBarVisible = false;
                    ssh.CensorshipTick = ssh.randomizer.Next(_settings.NBCensorHideMin, _settings.NBCensorHideMax+ 1);

                    if (CensorLineTemp > _settings.TimerVTF * 5)
                        return;

                    CensorVideo = Path.Combine(DomPersonalityPath, "Video/Censorship Sucks/CensorBarOff.txt");
                }
                else
                {
                    int CensorshipBarX;
                    int CensorshipBarY;
                    int CensorshipBarY2;

                    try
                    {
                        CensorshipBarY2 = ssh.randomizer.Next(200, DomWMP.Height / (double)2);
                    }
                    catch
                    {
                        CensorshipBarY2 = 100;
                    }

                    CensorshipBar.Height = CensorshipBarY2;
                    CensorshipBar.Width = CensorshipBarY2 * 2.6;

                    // QnD-BUGFIX: if CensorshipBar.Width > DomWMP.Width then ArgumentOutOfRangeException 
                    CensorshipBarX = ssh.randomizer.Next(5, CensorshipBar.Width > DomWMP.Width ? DomWMP.Width : DomWMP.Width - CensorshipBar.Width + 1);
                    CensorshipBarY = ssh.randomizer.Next(5, CensorshipBar.Height > DomWMP.Height ? DomWMP.Height : DomWMP.Height - CensorshipBar.Height + 1);
                    CensorshipBar.Location = new Point(CensorshipBarX, CensorshipBarY);



                    CensorshipBar.Visible = false;
                    CensorshipBar.Visible = true;
                    CensorshipBar.BringToFront();

                    ssh.CensorshipTick = ssh.randomizer.Next(_settings.NBCensorShowMin, _settings.NBCensorShowMax + 1);

                    if (CensorLineTemp > _settings.TimerVTF * 5)
                        return;

                    CensorVideo = Path.Combine(DomPersonalityPath, "Video/Censorship Sucks/CensorBarOn.txt");
                }

                // Read all lines of the given file.
                List<string> lines = Common.Txt2List(CensorVideo);

                int CensorLine;





                try
                {
                    lines = FilterList(lines);
                    if (lines.Count < 1)
                        return;
                    CensorLine = ssh.randomizer.Next(0, lines.Count);
                    ssh.DomTask = lines[CensorLine];
                }
                catch (Exception ex)
                {
                    _log.WriteError("Tease AI did not return a valid Censorship Sucks line from file: " + CensorVideo, ex, "CensorshipTimer.Tick");
                    ssh.DomTask = "ERROR: Tease AI did not return a valid Censorship Sucks line from file: " + CensorVideo;
                }

                TypingDelayGeneric();
            }
        }

        public void RLGLTimer_Tick(System.Object sender, System.EventArgs e)
        {
            // Check all Conditions before starting scripts.
            var paused = false;
            OnPauseCheck(out paused);
            if (paused) return;
            
            if (ssh.DomTyping == true)
                return;
            if (ssh.DomTypeCheck == true & ssh.RLGLTick < 6)
                return;
            //if (chatBox.Text != "" & ssh.RLGLTick < 6)
            //    return;
            //if (ChatBox2.Text != "" & ssh.RLGLTick < 6)
            //    return;
            if (ssh.FollowUp != "" & ssh.RLGLTick < 6)
                return;

            // Decrement TickCounter if Game is running.
            if (ssh.RLGLGame == true)
                ssh.RLGLTick -= 1;

            // Run scripts only if time is over.
            if (ssh.RLGLTick < 1)
            {
                // Swap the BooleanValue
                ssh.RedLight = !ssh.RedLight;
                // Turn off TauntTimer when State is red.
                if (ssh.RedLight)
                    RLGLTauntTimer.Stop();

                // Declare list to read
                List<string> tempList;
                string file2read;

                // Read File according to state and set the next timer-tick-duration.
                if (ssh.RedLight)
                {
                    // ################################## RED - Light ##################################
                    file2read = Path.Combine(DomPersonalityPath, "Video/Red Light Green Light/Red Light.txt");
                    tempList = Common.Txt2List(file2read);
                    ssh.RLGLTick = ssh.randomizer.Next(_settings.RedLightMin, _settings.RedLightMax + 1);
                }
                else
                {
                    // ################################## Green - Light ################################
                    file2read = Path.Combine(DomPersonalityPath, "Video/Red Light Green Light/Green Light.txt");
                    tempList = Common.Txt2List(file2read); 
                    ssh.RLGLTick = ssh.randomizer.Next(_settings.GreenLightMin, _settings.GreenLightMax + 1);
                }

                try
                {
                    tempList = FilterList(tempList);
                    ssh.DomTask = tempList[ssh.randomizer.Next(0, tempList.Count)];
                }
                catch (Exception ex)
                {
                    _log.WriteError("Tease AI did not return a valid RLGL line from file: " + file2read, ex, "RLGLTimer.Tick");
                    ssh.DomTask = "ERROR: Tease AI did not return a valid RLGL line from file: " + file2read;
                }

                TypingDelayGeneric();
            }
        }


        // 5000, 1000, 334 rates.
        private void TnAFastSlides_Tick(System.Object sender, System.EventArgs e)
        {
            Stopwatch tmpSw = new Stopwatch();

        RestartFunction:
            ;
            tmpSw.Restart();
            try
            {
                if (ssh.BoobList.Count < 1)
                    throw new Exception("No Boobs-images loaded.");
                if (ssh.AssList.Count < 1)
                    throw new Exception("No Butt-images loaded.");

                string tmpImageToShow = "";
                bool tmpLateSet;

                if (ssh.randomizer.Next(0, 101) < 51)
                {
                    tmpImageToShow = ssh.BoobList[ssh.randomizer.Next(0, ssh.BoobList.Count)];
                    tmpLateSet = true;
                }
                else
                {
                    tmpImageToShow = ssh.AssList[ssh.randomizer.Next(0, ssh.AssList.Count)];
                    tmpLateSet = false;
                }

                try
                {
                    ShowImage(tmpImageToShow, true);

                    if (tmpLateSet)
                    {
                        ssh.BoobImage = true;
                        ssh.AssImage = false;
                    }
                    else
                    {
                        ssh.BoobImage = false;
                        ssh.AssImage = true;
                    }

                    // If the elapsed time to load an image was longer as the Timer.Interval
                    // we restart the function instantly, to avoid an unnecessary delay.
                    // If it took way too long and the Timer was stopped during imagedownload, 
                    // we dont want the timer to restart.
                    if (tmpSw.ElapsedMilliseconds > TnASlides.Interval & TnASlides.Enabled)
                        goto RestartFunction;
                }
                catch (Exception ex)
                {
                    // @@@@@@@@@@@@@@@@ Exception while loading image @@@@@@@@@@@@@@@@@
                    // Remove the ImagePath and retry.
                    ssh.BoobList.RemoveAll(x => x.Contains(tmpImageToShow));
                    ssh.AssList.RemoveAll(x => x.Contains(tmpImageToShow));
                    goto RestartFunction;
                }
            }
            catch (Exception ex)
            {
                // ▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                // All Errors
                // ▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                TnASlides.Stop();
                _log.WriteError(ex.Message + Environment.NewLine + "TnA Slideshow will stop.", ex, "Exception in TnASlides.Tick occured");
            }
        }


        private void AvoidTheEdge_Tick(System.Object sender, System.EventArgs e)
        {
            var paused = false;
            OnPauseCheck(out paused);
            if (paused) return;

            if (ssh.DomTyping == true)
                return;
            if (ssh.DomTypeCheck == true & ssh.AvoidTheEdgeTick < 6)
                return;
            //if (chatBox.Text != "" & ssh.AvoidTheEdgeTick < 6)
            //    return;
            //if (ChatBox2.Text != "" & ssh.AvoidTheEdgeTick < 6)
            //    return;
            if (ssh.FollowUp != "" & ssh.AvoidTheEdgeTick < 6)
                return;

            ssh.AvoidTheEdgeTick -= 1;

            if (ssh.AvoidTheEdgeTick < 1)
            {
                
                string AvoidTheEdgeVideo = Path.Combine(DomPersonalityPath, "Video/AvoidTheEdge.txt");
                if (ssh.DommeVideo)
                    AvoidTheEdgeVideo = Path.Combine(DomPersonalityPath, @"Video/AvoidTheEdgeD.txt");

                int AvoidTheEdgeLineStart = 1;
                int AvoidTheEdgeLineEnd = 1;


                if (File.Exists(AvoidTheEdgeVideo))
                {
                }
                else
                {
                    if (ssh.DommeVideo)
                        _log.WriteError("AvoidTheEdgeD.txt is missing!", AvoidTheEdgeVideo);
                    else
                        _log.WriteError("AvoidTheEdge.txt is missing!", AvoidTheEdgeVideo);
                    return;
                }



                if (!ssh.AvoidTheEdgeStroking)
                {


                    // CensorshipTick = randomizer.Next(NBCensorHideMin.Value, NBCensorHideMax.Value + 1)

                    ssh.AvoidTheEdgeTick = 120 / _settings.TimerVTF;

                    // If AvoidTheEdgeLineTemp > TauntSlider.Value * 5 Then
                    // Return
                    // End If

                    using (StreamReader ioFileA = new StreamReader(AvoidTheEdgeVideo))
                    {
                        List<string> linesA = new List<string>();
                        int TempAvoidTheEdgeLine;

                        TempAvoidTheEdgeLine = -1;
                        while (ioFileA.Peek() != -1)
                        {
                            TempAvoidTheEdgeLine += 1;
                            linesA.Add(ioFileA.ReadLine());
                            if (ssh.VideoType == "Hardcore" & linesA[TempAvoidTheEdgeLine] == "[HardcoreStrokingOff]")
                                AvoidTheEdgeLineStart = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "Hardcore" & linesA[TempAvoidTheEdgeLine] == "[SoftcoreStrokingOn]")
                                AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "Softcore" & linesA[TempAvoidTheEdgeLine] == "[SoftcoreStrokingOff]")
                                AvoidTheEdgeLineStart = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "Softcore" & linesA[TempAvoidTheEdgeLine] == "[LesbianStrokingOn]")
                                AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "Lesbian" & linesA[TempAvoidTheEdgeLine] == "[LesbianStrokingOff]")
                                AvoidTheEdgeLineStart = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "Lesbian" & linesA[TempAvoidTheEdgeLine] == "[BlowjobStrokingOn]")
                                AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "Blowjob" & linesA[TempAvoidTheEdgeLine] == "[BlowjobStrokingOff]")
                                AvoidTheEdgeLineStart = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "Blowjob" & linesA[TempAvoidTheEdgeLine] == "[FemdomStrokingOn]")
                                AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "Femdom" & linesA[TempAvoidTheEdgeLine] == "[FemdomStrokingOff]")
                                AvoidTheEdgeLineStart = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "Femdom" & linesA[TempAvoidTheEdgeLine] == "[FemsubStrokingOn]")
                                AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "Femsub" & linesA[TempAvoidTheEdgeLine] == "[FemsubStrokingOff]")
                                AvoidTheEdgeLineStart = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "Femsub" & linesA[TempAvoidTheEdgeLine] == "[JOIStrokingOn]")
                                AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "JOI" & linesA[TempAvoidTheEdgeLine] == "[JOIStrokingOff]")
                                AvoidTheEdgeLineStart = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "JOI" & linesA[TempAvoidTheEdgeLine] == "[CHStrokingOn]")
                                AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "CH" & linesA[TempAvoidTheEdgeLine] == "[CHStrokingOff]")
                                AvoidTheEdgeLineStart = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "CH" & linesA[TempAvoidTheEdgeLine] == "[GeneralStrokingOn]")
                                AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "General" & linesA[TempAvoidTheEdgeLine] == "[GeneralStrokingOff]")
                                AvoidTheEdgeLineStart = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "General" & linesA[TempAvoidTheEdgeLine] == "[StrokingEnd]")
                                AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine;
                        }
                    }
                }
                else
                {
                    ssh.AvoidTheEdgeTick = 120 / _settings.TimerVTF;

                    using (StreamReader ioFileB = new StreamReader(AvoidTheEdgeVideo))
                    {
                        List<string> linesB = new List<string>();
                        int TempAvoidTheEdgeLine;

                        TempAvoidTheEdgeLine = -1;
                        while (ioFileB.Peek() != -1)
                        {
                            TempAvoidTheEdgeLine += 1;
                            linesB.Add(ioFileB.ReadLine());
                            if (ssh.VideoType == "Hardcore" & linesB[TempAvoidTheEdgeLine] == "[HardcoreStrokingOn]")
                                AvoidTheEdgeLineStart = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "Hardcore" & linesB[TempAvoidTheEdgeLine] == "[HardcoreStrokingOff]")
                                AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "Softcore" & linesB[TempAvoidTheEdgeLine] == "[SoftcoreStrokingOn]")
                                AvoidTheEdgeLineStart = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "Softcore" & linesB[TempAvoidTheEdgeLine] == "[SoftcoreStrokingOff]")
                                AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "Lesbian" & linesB[TempAvoidTheEdgeLine] == "[LesbianStrokingOn]")
                                AvoidTheEdgeLineStart = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "Lesbian" & linesB[TempAvoidTheEdgeLine] == "[LesbianStrokingOff]")
                                AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "Blowjob" & linesB[TempAvoidTheEdgeLine] == "[BlowjobStrokingOn]")
                                AvoidTheEdgeLineStart = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "Blowjob" & linesB[TempAvoidTheEdgeLine] == "[BlowjobStrokingOff]")
                                AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "Femdom" & linesB[TempAvoidTheEdgeLine] == "[FemdomStrokingOn]")
                                AvoidTheEdgeLineStart = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "Femdom" & linesB[TempAvoidTheEdgeLine] == "[FemdomStrokingOff]")
                                AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "Femsub" & linesB[TempAvoidTheEdgeLine] == "[FemsubStrokingOn]")
                                AvoidTheEdgeLineStart = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "Femsub" & linesB[TempAvoidTheEdgeLine] == "[FemsubStrokingOff]")
                                AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "JOI" & linesB[TempAvoidTheEdgeLine] == "[JOIStrokingOn]")
                                AvoidTheEdgeLineStart = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "JOI" & linesB[TempAvoidTheEdgeLine] == "[JOIStrokingOff]")
                                AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "CH" & linesB[TempAvoidTheEdgeLine] == "[CHStrokingOn]")
                                AvoidTheEdgeLineStart = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "CH" & linesB[TempAvoidTheEdgeLine] == "[CHStrokingOff]")
                                AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "General" & linesB[TempAvoidTheEdgeLine] == "[GeneralStrokingOn]")
                                AvoidTheEdgeLineStart = TempAvoidTheEdgeLine;
                            if (ssh.VideoType == "General" & linesB[TempAvoidTheEdgeLine] == "[GeneralStrokingOff]")
                                AvoidTheEdgeLineEnd = TempAvoidTheEdgeLine;
                        }
                    }
                }

                StreamReader ioFile = new StreamReader(AvoidTheEdgeVideo);
                List<string> lines = new List<string>();
                while (ioFile.Peek() != -1)
                    lines.Add(ioFile.ReadLine());

                int AvoidTheEdgeLine;

                // Debug.Print("AvoidTheEdgeLineStart = " & AvoidTheEdgeLineStart)
                // Debug.Print("AvoidTheEdgeLineEnd = " & AvoidTheEdgeLineEnd)

                AvoidTheEdgeLine = ssh.randomizer.Next(AvoidTheEdgeLineStart + 1, AvoidTheEdgeLineEnd);

                ssh.DomTask = lines[AvoidTheEdgeLine];

                TypingDelayGeneric();
            }
        }


        private void AvoidTheEdgeTaunts_Tick(System.Object sender, System.EventArgs e)
        {
            // TODO: Merge redundant code: VideoTauntTimer_Tick, RLGLTauntTimer_Tick, AvoidTheEdgeTaunts_Tick
            var paused = false;
            OnPauseCheck(out paused);
            if (paused) return;

            if (ssh.DomTyping == true)
                return;
            if (ssh.DomTypeCheck == true & ssh.AvoidTheEdgeTick < 6)
                return;
            //if (chatBox.Text != "" & ssh.AvoidTheEdgeTick < 6)
            //    return;
           // if (ChatBox2.Text != "" & ssh.AvoidTheEdgeTick < 6)
           //     return;



            ssh.AvoidTheEdgeTick -= 1;


            if (ssh.AvoidTheEdgeTick < 1)
            {
                int FrequencyTemp = ssh.randomizer.Next(1, 101);
                if (FrequencyTemp > _settings.TimerVTF * 8)
                {
                    ssh.AvoidTheEdgeTick = ssh.randomizer.Next(20, 31);
                    return;
                }

                string VTDir;

                VTDir = Path.Combine(DomPersonalityPath, "Video/Avoid The Edge/Taunts.txt");

                if (!File.Exists(VTDir))
                    return;

                // Read all lines of the given file.
                List<string> VTList = Common.Txt2List(VTDir);

                try
                {
                    VTList = FilterList(VTList);
                    if (VTList.Count < 1)
                        return;
                    ssh.DomTask = VTList[ssh.randomizer.Next(0, VTList.Count)];
                }
                catch (Exception ex)
                {
                    _log.WriteError("Tease AI did not return a valid Video Taunt from file: " + VTDir, ex, "AvoidTheEdgeTaunts.Tick");
                    ssh.DomTask = "ERROR: Tease AI did not return a valid Video Taunt from file:  " + VTDir;
                }
                TypingDelayGeneric();



                ssh.AvoidTheEdgeTick = ssh.randomizer.Next(20, 31);
            }
        }


        public void RLGLTauntTimer_Tick(System.Object sender, System.EventArgs e)
        {
            // TODO: Merge redundant code: VideoTauntTimer_Tick, RLGLTauntTimer_Tick, AvoidTheEdgeTaunts_Tick
            //if (FrmSettings.CBSettingsPause.Checked == true & FrmSettings.SettingsPanel.Visible == true)
            //    return;

            var paused = false;
            OnPauseCheck(out paused);
            if (paused) return;


            if (ssh.DomTyping == true)
                return;
            if (ssh.DomTypeCheck == true & ssh.RLGLTauntTick < 6)
                return;
            //if (chatBox.Text != "" & ssh.RLGLTauntTick < 6)
            //    return;
            //if (ChatBox2.Text != "" & ssh.RLGLTauntTick < 6)
            //    return;

            ssh.RLGLTauntTick -= 1;


            if (ssh.RLGLTauntTick < 1)
            {
                int FrequencyTemp = ssh.randomizer.Next(1, 101);
                if (FrequencyTemp > _settings.TimerVTF * 8)
                {
                    ssh.RLGLTauntTick = ssh.randomizer.Next(20, 31);
                    return;
                }

                string VTDir;

                VTDir = Path.Combine(DomPersonalityPath, @"Video/Red Light Green Light/Taunts.txt");

                if (!File.Exists(VTDir))
                    return;

                // Read all lines of the given file.
                List<string> VTList = Common.Txt2List(VTDir);

                try
                {
                    VTList = FilterList(VTList);
                    if (VTList.Count < 1)
                        return;
                    ssh.DomTask = VTList[ssh.randomizer.Next(0, VTList.Count)];
                }
                catch (Exception ex)
                {
                    _log.WriteError("Tease AI did not return a valid Video Taunt from file: " + VTDir, ex, "RLGLTauntTimer.Tick");
                    ssh.DomTask = "ERROR: Tease AI did not return a valid Video Taunt from file:  " + VTDir;
                }
                TypingDelayGeneric();



                ssh.RLGLTauntTick = ssh.randomizer.Next(20, 31);
            }
        }


        private void TeaseTimer_Tick(System.Object sender, System.EventArgs e)
        {
            FrmSettings.LBLDebugTeaseTime.Text = ssh.TeaseTick;
            // Debug.Print("TeaseTick = " & TeaseTick)

            //if (FrmSettings.CBSettingsPause.Checked == true & FrmSettings.SettingsPanel.Visible == true)
            //     return;
            var paused = false;
            OnPauseCheck(out paused);
            if (paused) return;

            ssh.TeaseTick -= 1;

            if (ssh.TeaseTick < 1)
                TeaseTimer.Stop();
        }

        private void VideoTauntTimer_Tick(System.Object sender, System.EventArgs e)
        {

            // TODO: Merge redundant code: VideoTauntTimer_Tick, RLGLTauntTimer_Tick, AvoidTheEdgeTaunts_Tick
            //if (FrmSettings.CBSettingsPause.Checked == true & FrmSettings.SettingsPanel.Visible == true)
            //   return;
            var paused = false;
            OnPauseCheck(out paused);
            if (paused) return;


            if (ssh.DomTyping == true)
                return;
            if (ssh.DomTypeCheck == true & ssh.VideoTauntTick < 6)
                return;
            //if (chatBox.Text != "" & ssh.VideoTauntTick < 6)
            //    return;
            //if (ChatBox2.Text != "" & ssh.VideoTauntTick < 6)
            //    return;

            ssh.VideoTauntTick -= 1;


            if (ssh.VideoTauntTick < 1)
            {
                int FrequencyTemp = ssh.randomizer.Next(1, 101);
                if (FrequencyTemp > _settings.TimerVTF * 8)
                {
                    ssh.VideoTauntTick = ssh.randomizer.Next(20, 31);
                    return;
                }

                string VTDir = string.Empty;

                if (ssh.RLGLGame)
                    VTDir = Path.Combine(DomPersonalityPath, "Video/Red Light Green Light/Taunts.txt");
                // TODO: Prevent File.Exits() with String.Empty
                if (!File.Exists(VTDir))
                    return;

                // Read all lines of the given file.
                List<string> VTList = Common.Txt2List(VTDir);

                try
                {
                    VTList = FilterList(VTList);
                    if (VTList.Count < 1)
                        return;
                    ssh.DomTask = VTList[ssh.randomizer.Next(0, VTList.Count)];
                }
                catch (Exception ex)
                {
                    _log.WriteError("Tease AI did not return a valid Video Taunt from file: " + VTDir, ex, "VideoTauntTimer.Tick");
                    ssh.DomTask = "ERROR: Tease AI did not return a valid Video Taunt from file: " + VTDir;
                }

                TypingDelayGeneric();



                ssh.VideoTauntTick = ssh.randomizer.Next(20, 31);
            }
        }

        private void TimeoutTimer_Tick(System.Object sender, System.EventArgs e)
        {
            //if (FrmSettings.CBSettingsPause.Checked == true & FrmSettings.SettingsPanel.Visible == true)
            //    return;

            Debug.Print("TimeoutTick = " + ssh.TimeoutTick);

            //if (chatBox.Text != "" & ssh.TimeoutTick < 3)
            //    return;
            //if (ChatBox2.Text != "" & ssh.TimeoutTick < 3)
            //    return;
            var paused = false;
            OnPauseCheck(out paused);
            if (paused) return;

            ssh.TimeoutTick -= 1;

            if (ssh.TimeoutTick < 1)
            {
                TimeoutTimer.Stop();
                ssh.YesOrNo = false;
                ssh.InputFlag = false;
                ssh.SkipGotoLine = true;
                GetGoto();

                RunFileText();
            }
        }


        private void VideoTimer_Tick(System.Object sender, System.EventArgs e)
        {
            //if (FrmSettings.CBSettingsPause.Checked == true & FrmSettings.SettingsPanel.Visible == true)
            //    return;
            var paused = false;
            OnPauseCheck(out paused);
            if (paused) return;


            if ((DomWMP.playState == WMPPlayState.wmppsPlaying))
                ssh.VideoTick -= 1;
            if (ssh.VideoTick < 1)
            {
                VideoTimer.Stop();
                DomWMP.Ctlcontrols.stop();
            }
        }

        private void MultipleEdgesTimer_Tick(System.Object sender, System.EventArgs e)
        {
            if (ssh.DomTypeCheck == true)
                return;
            //if (FrmSettings.CBSettingsPause.Checked == true & FrmSettings.SettingsPanel.Visible == true)
            //    return;

            var paused = false;
            OnPauseCheck(out paused);
            if (paused) return;

            ssh.MultipleEdgesTick -= 1;

            if (ssh.MultipleEdgesTick < 1)
            {
                ssh.EdgeTauntInt = ssh.randomizer.Next(5, 19);

                MultipleEdgesTimer.Stop();

                ssh.DomChat = "#SYS_MultipleEdgesStart";
                if (ssh.Contact1Edge == true)
                    ssh.DomChat = "@Contact1 #SYS_MultipleEdgesStart";
                if (ssh.Contact2Edge == true)
                    ssh.DomChat = "@Contact2 #SYS_MultipleEdgesStart";
                if (ssh.Contact3Edge == true)
                    ssh.DomChat = "@Contact3 #SYS_MultipleEdgesStart";
                TypingDelay();

                ssh.MultipleEdgesMetronome = "START";

                ssh.EdgeCountTick = 0;
                EdgeCountTimer.Start();
            }
        }

    }
}
