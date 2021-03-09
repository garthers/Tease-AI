using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Timers;
using Tai.Common.Images;

namespace Tai.Common
{

    public class RiskyState
    {
        public int RiskyPickNumber { get; internal set; }
        public string RiskyResponse { get; internal set; }
        public int RiskyPickCount { get; internal set; }
        public int LBLPick1 { get; internal set; }
        public int LBLPick2 { get; internal set; }
        public int LBLPick3 { get; internal set; }
        public int LBLPick4 { get; internal set; }
        public int LBLPick5 { get; internal set; }
        public int LBLPick6 { get; internal set; }
        public char RiskyEdgeOffer { get; internal set; }
        public char RiskyTokenOffer { get; internal set; }
        public char EdgesOwed { get; internal set; }
        public char TokensPaid { get; internal set; }
    }
    public partial class Engine
    {
        private Regex _reKeywords;
        private Regex _reKeywordsCI;
        private TraceSwitch _ts;

        Variables _vars;
        Flags _flags;
        private readonly string _rootPath;
        private readonly string _domPersonalityName;
        private readonly Chat _chat;
        private readonly SessionState ssh;
        private readonly ISettings _settings;

        private List<string> customVocabLines = new List<string>();
        private RiskyState _risky;
        private readonly ILog _log;
        private readonly ImageData _imageData;

        public Engine(string rootPath, string domPersonalityName, Chat chat, SessionState xssh, ISettings settings, ILog log, ImageSettings imageSettings)
        {
            InitPoundClean();
            throw new NotImplementedException("Fix variables path");
            _vars = new Variables(rootPath + "sdfdf");
            _log = log;
            _flags = new Flags("dasd","aa");
            _rootPath = rootPath;
            _domPersonalityName = domPersonalityName;
            _chat = chat;
            ssh = xssh;
            _settings = settings;
            //var imageSettings = new ImageSettings();
            _imageData = new ImageData(_settings, imageSettings, xssh.randomizer, log);
        }

        public void InitPoundClean()
        {
            // Create Regex-Pattern to find #Keywords and exclude custom imagetags.
            string[] ExcludeKeywords = new[] { "TagGarment", "TagUnderwear", "TagTattoo", "TagSexToy", "TagFurniture" };
            string Pattern = string.Format(@"##*(?!{0})[\w\d\+\-_]+", string.Join("|", ExcludeKeywords));

            // Append included non-Keywords to pattern.
            string[] NonKeywordInclude = new[] { @"@RT\(", @"@RandomText\(" };
            Pattern += NonKeywordInclude.Length == 0 ? "" : "|" + string.Join("|", NonKeywordInclude);

            _reKeywords = new Regex(Pattern);
            _reKeywordsCI = new Regex(Pattern, RegexOptions.IgnoreCase);
            _ts = new TraceSwitch("PoundClean", "");
        }

        private string VocabFolder => Path.Combine(_rootPath, "Scripts", _domPersonalityName, "Vocabulary"); 

        public string PoundClean(string stringClean, PoundOptions options = PoundOptions.None, int startRecurrence = 0)
        {
            List<string> AlreadyChecked = new List<string>();
            bool dotrace = true;
            TraceSwitch TS = _ts;
            DateTime StartTime = DateTime.Now;

            if (dotrace)
            {
                if (TS.TraceVerbose)
                {
                    Trace.WriteLine("============= PoundClean(String) =============");
                    Trace.Indent();
                    Trace.WriteLine(string.Format("StartValue: \"{0}\"", stringClean));
                }
                else if (TS.TraceInfo)
                {
                    Trace.WriteLine(string.Format("PoundClean(String) parsing: \"{0}\"", stringClean));
                    Trace.Indent();
                }

                Stopwatch Sw = new Stopwatch();
                Sw.Start();
            }
            string OrgString = stringClean;
            int ActRecurrence = startRecurrence;

            while (ActRecurrence < 6 && _reKeywords.IsMatch(stringClean))
            {
                ActRecurrence += 1;

                if (dotrace)
                {
                    if (TS.TraceVerbose)
                    {
                        Trace.WriteLine(string.Format("Starting scan run {0} on \"{1}\"", ActRecurrence, stringClean));
                        Trace.Indent();
                    }
                }

                stringClean = SysKeywordClean(stringClean);
                if (dotrace)
                {
                    if (TS.TraceVerbose)
                        Trace.WriteLine(string.Format("System keywords cleaned: \"{0}\"", stringClean));
                }

                MatchCollection Mc = _reKeywordsCI.Matches(stringClean);

                string ControlCustom = "";
                if (stringClean.Contains("@CustomMode("))
                    ControlCustom = Common.GetParentheses(stringClean, "@CustomMode(");

                foreach (Match Keyword in Mc)
                {
                    // Start next loop if we already checked this vocab.
                    if (AlreadyChecked.Contains(Keyword.Value, StringComparer.OrdinalIgnoreCase))
                        continue;

                    if (dotrace)
                    {
                        if (TS.TraceVerbose)
                            Trace.WriteLine(string.Format("Applying vocabulary: \"{0}\"", Keyword.Value));
                    }
                    AlreadyChecked.Add(Keyword.Value);
                    string Replacement = "";

                    try
                    {
                        string Filepath = Path.Combine(VocabFolder, Keyword.Value + ".txt");
                        List<string> VocabLines = default(List<string>);
                        if (Keyword.Value.Equals("#NULL", StringComparison.OrdinalIgnoreCase)) {
                            // Replace predefined value 
                            stringClean = stringClean.Replace(Keyword.Value, "");
                            continue;
                        }


                        // #################### Process vocab file #####################
                        try
                        {
                            VocabLines = Common.Txt2List(Filepath);
                        } catch (IOException ex)
                        {
                            stringClean = stringClean.Replace(Keyword.Value, _chat.ChatGetInlineError(Keyword.Value.Substring(1)));
                            string Lazytext = "Unable to locate vocabulary file: \"" + Keyword.Value + "\"";
                            _log.WriteError(Lazytext, new Exception(Lazytext), "PoundClean(String)");

                            continue;
                        }
                        VocabLines = FilterList(VocabLines);

                        if (ControlCustom.Contains(Keyword.ToString()))
                            customVocabLines = VocabLines;

                        if (VocabLines.Count <= 0)
                        {
                            // ----------------- No Lines available ----------------
                            Replacement = _chat.ChatGetInlineWarning(Keyword.Value.Substring(1));
                            _chat.ChatAddWarning("No available lines in vocabulary file: \"" + Keyword.Value + "");
                        }
                        else if (options.HasFlag(PoundOptions.CommaSepList))
                        {
                            // -------------- Get comma separated list --------------
                            List<string> CleanLines = new List<string>();

                            foreach (string Line in VocabLines)
                                CleanLines.Add(PoundClean(Line, options, ActRecurrence));

                            Replacement = string.Join(",", CleanLines);
                        }
                        else
                            // -------------- Pick a single random line -------------
                            Replacement = VocabLines[ssh.randomizer.Next(0, VocabLines.Count)];
                    }
                    catch (Exception ex)
                    {
                        _log.WriteError("Error Processing vocabulary file:  " + Keyword.Value, ex, "Tease AI did not return a valid line while parsing vocabulary file.");
                        Replacement = _chat.ChatGetInlineError(Keyword.Value.Substring(1));
                    }
                    finally
                    {
                        stringClean = stringClean.Replace(Keyword.Value, Replacement);
                    }
                }

                if (dotrace)
                    Trace.Unindent();
            }

            if (_reKeywords.IsMatch(stringClean))
            {
                if (dotrace)
                {
                    if (TS.TraceError)
                    {
                        Trace.WriteLine("PoundClean(String): Stopping scan, maximum allowed scan depth reached.");
                        Trace.Indent();
                        Trace.WriteLine(string.Format("StartValue: \"{0}\"", OrgString));
                        Trace.WriteLine(string.Format("EndValue:   \"{0}\"", stringClean));
                        Trace.Unindent();
                    }
                }
                _log.WriteError("Maximum allowed Vocabulary depth reached for line:" + OrgString + Environment.NewLine + "Aborted Cleaning at: " + stringClean, new StackOverflowException("PoundClean infinite loop protection"), "PoundClean(String)");
            }
            else if (dotrace)
            {
                if (TS.TraceVerbose)
                {
                    Trace.WriteLine(string.Format("EndValue: \"{0}\"", stringClean));
                    Trace.WriteLine(string.Format("Duration: {0}ms", (DateTime.Now - StartTime).TotalMilliseconds.ToString()));
                }
            }

            if (dotrace)
                Trace.Unindent();
            return stringClean;
        }

        public List<string> FilterList(List<string> ListClean)
        {
            /* TODO ERROR: Skipped IfDirectiveTrivia *//* TODO ERROR: Skipped DisabledTextTrivia *//* TODO ERROR: Skipped EndIfDirectiveTrivia */
            bool FilterPass;
            int ListIncrement = 1;
            if (ssh.StrokeFilter == true)
                ListIncrement = ssh.StrokeTauntCount;

            // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            // Check if Grouped-Lines-Files have the right amount of Lines
            // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            // No need to go further on an empty file.
            if (ListClean.Count <= 0)
            {
                Trace.WriteLine("FilterList started with empty List. Skipping filter.");
                return ListClean;
            }

            // To Avoid DivideByZeroException 
            if (ListIncrement <= 0)
            {
                string lazyText = "FilterList Started With LineGroupingValue \"" + ListIncrement + "\". ";
                _log.WriteError(lazyText, new ArgumentOutOfRangeException(lazyText), "FilterList Cancelled");
                return ListClean;
            }

            // Divide List.Count by StrokeTauntSize and get the Remainder.
            int InvalidLineCount = ListClean.Count % ListIncrement;

            // If there is a Remainder, the file has not the desired Line.Count.
            if (InvalidLineCount > 0)
                // So delete the Lines of the last and hopefully uncomplete Group. 
                ListClean.RemoveRange(ListClean.Count - InvalidLineCount, InvalidLineCount);
            // ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
            // Grouped-Lines-Check-END 
            // ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            List<string> ControlList = new List<string>();

            for (int i = 0; i <= ListClean.Count - 1; i++)
            {
                if (ListClean[i].Contains("@ControlFlag("))
                {
                    if (_flags.FlagExists(Common.GetParentheses(ListClean[i], "@ControlFlag(")) == true)
                        ControlList.Add(ListClean[i]);
                }
            }

            if (ControlList.Count > 0)
            {
                ListClean.Clear();
                ListClean = ControlList;
            }


            // ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
            // NEW FilterList TEST Begin
            // ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            for (int i = ListClean.Count - ListIncrement; i >= 0; i += -ListIncrement)
            {
                for (int x = ListIncrement - 1; x >= 0; x += -1)
                {
                    if (GetFilter(ListClean[i + x]) == false)
                    {
                        for (int n = ListIncrement - 1; n >= 0; n += -1)
                            ListClean.RemoveAt(i + n);
                        break;
                    }
                }
            }

            // ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
            // NEW FilterList TEST End
            // ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲


            // ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
            // Current FilterList Begin
            // ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            // For i As Integer = 0 To ListClean.Count - 1 Step ListIncrement

            // FilterPass = True

            // For x As Integer = 0 To ListIncrement - 1
            // If GetFilter(ListClean(i + x)) = False Then
            // FilterPass = False
            // Exit For
            // End If
            // Next

            // If FilterPass = False Then
            // For x As Integer = 0 To ListIncrement - 1
            // ListClean(i + x) = ListClean(i + x) & "###-INVALID-###"
            // Next
            // End If

            // Next

            // For i As Integer = ListClean.Count - 1 To 0 Step -1
            // If ListClean(i).Contains("###-INVALID-###") Then ListClean.RemoveAt(i)
            // Next

            // ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
            // Current FilterList End
            // ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            // Dim FilteredList As New List(Of String)

            // For i As Integer = 0 To ListClean.Count - 1
            // If Not ListClean(i).Contains("###-INVALID-###") Then FilteredList.Add(ListClean(i))
            // Next

            /* TODO ERROR: Skipped IfDirectiveTrivia *//* TODO ERROR: Skipped DisabledTextTrivia *//* TODO ERROR: Skipped EndIfDirectiveTrivia */     // If ListClean.Count = 0 Then ListClean.Add("test")
            return ListClean;
        }

        public string SysKeywordClean(string StringClean)
        {
            if (StringClean.Contains("#Var["))
            {
                string[] VarArray = StringClean.Split(']');
                for (int i = 0; i <= VarArray.Count() - 1; i++)
                {
                    if (VarArray[i].Contains("#Var["))
                        StringClean = StringClean.Replace("#Var[" + Common.GetParentheses(VarArray[i] + "]", "#Var[") + "]", _vars.GetVariable(Common.GetParentheses(VarArray[i] + "]", "#Var[")));
                }
            }

            if (StringClean.Contains("@RT(") | StringClean.Contains("@RandomText("))
            {
                string[] replace = new[] { "@RT(", "@RandomText(" };
                string[] RandArray = StringClean.Split('@');
                for (var a = 0; a <= replace.Length - 1; a++)
                {
                    for (int i = 0; i <= RandArray.Count() - 1; i++)
                    {
                        RandArray[i] = "@" + RandArray[i];
                        if (RandArray[i].Contains(replace[a]))
                        {
                            var tempString =Common.GetParentheses(RandArray[i], replace[a], RandArray[i].Split(')').Length - 1);
                            var startString = tempString;
                            tempString = tempString.Replace(",,", "###INSERT-COMMA###");
                            tempString =Common.FixCommas(tempString);
                            string[] selectArray = tempString.Split(',');
                            for (int n = 0; n <= selectArray.Count() - 1; n++)
                                selectArray[n] = selectArray[n].Replace("###INSERT-COMMA###", ",");
                            tempString = selectArray[ssh.randomizer.Next(0, selectArray.Count())];
                            StringClean = StringClean.Replace(replace[a] + startString + ")", tempString);
                        }
                    }
                }
            }

            if (_settings.CockToClit)
            {
                StringClean = StringClean.Replace("#Cock", "#CockToClit");
                StringClean = StringClean.Replace("stroking", "#StrokingToRubbing");
            }

            if (_settings.BallsToPussy)
            {
                StringClean = StringClean.Replace("those #Balls", "that #Balls");
                StringClean = StringClean.Replace("#Balls", "#BallsToPussy");
            }

            StringClean = StringClean.Replace("#SubName", _settings.SubName);

            if (ssh.SlideshowMain != null)
                StringClean = StringClean.Replace("#DomName", ssh.SlideshowMain.TypeName);
            else
                StringClean = StringClean.Replace("#DomName", ssh.tempDomName);

            StringClean = StringClean.Replace("#MainDom", _settings.DomName);
            StringClean = StringClean.Replace("#MainHonorific", _settings.SubHonorific);

            if (StringClean.Contains("#Contact1Honorific"))
                StringClean = StringClean.Replace("#Contact1Honorific", _settings.G1Honorific);

            if (StringClean.Contains("#Contact2Honorific"))
                StringClean = StringClean.Replace("#Contact2Honorific", _settings.G2Honorific);

            if (StringClean.Contains("#Contact3Honorific"))
                StringClean = StringClean.Replace("#Contact3Honorific", _settings.G3Honorific);

            if (ssh.dommePresent == true)
                StringClean = StringClean.Replace("#DomHonorific", ssh.tempHonorific);
            else
                StringClean = StringClean.Replace("#DomHonorific", _settings.SubHonorific);

            StringClean = StringClean.Replace("#DomAge", _settings.DomAge.ToString());

            StringClean = StringClean.Replace("#DomLevel", _settings.DomLevel.ToString());

            StringClean = StringClean.Replace("#DomApathy", _settings.DomEmpathy.ToString());

            StringClean = StringClean.Replace("#DomHairLength", _settings.DomHairLength.ToLower());

            StringClean = StringClean.Replace("#DomHair", _settings.DomHair);

            StringClean = StringClean.Replace("#DomEyes", _settings.DomEyes);

            StringClean = StringClean.Replace("#DomCup", _settings.DomCup);

            StringClean = StringClean.Replace("#DomMoodMin", _settings.DomMoodMin.ToString());

            StringClean = StringClean.Replace("#DomMoodMax", _settings.DomMoodMax.ToString());

            StringClean = StringClean.Replace("#DomMood", ssh.DommeMood.ToString());

            StringClean = StringClean.Replace("#DomAvgCockMin", _settings.AvgCockMin.ToString());

            StringClean = StringClean.Replace("#DomAvgCockMax", _settings.AvgCockMax.ToString());

            StringClean = StringClean.Replace("#DomSmallCockMax", (_settings.AvgCockMin - 1).ToString());

            StringClean = StringClean.Replace("#DomLargeCockMin", (_settings.AvgCockMax + 1).ToString());

            StringClean = StringClean.Replace("#DomSelfAgeMin", _settings.SelfAgeMin.ToString());

            StringClean = StringClean.Replace("#DomSelfAgeMax", _settings.SelfAgeMax.ToString());

            StringClean = StringClean.Replace("#DomSubAgeMin", _settings.SubAgeMin.ToString());

            StringClean = StringClean.Replace("#DomSubAgeMax", _settings.SubAgeMax.ToString());

            StringClean = StringClean.Replace("#DomOrgasmRate", _settings.OrgasmAllow.ToLower().Replace("allows", "allow"));

            StringClean = StringClean.Replace("#DomRuinRate", _settings.OrgasmRuin.ToLower().Replace("ruins", "ruin"));

            StringClean = StringClean.Replace("#SubAge", _settings.SubAge.ToString());

            StringClean = StringClean.Replace("#SubBirthdayMonth", _settings.SubBirthMonth.ToString());

            StringClean = StringClean.Replace("#SubBirthdayDay", _settings.SubBirthDay.ToString());

            StringClean = StringClean.Replace("#DomBirthdayMonth", _settings.DomBirthMonth.ToString());

            StringClean = StringClean.Replace("#DomBirthdayDay", _settings.DomBirthDay.ToString());

            StringClean = StringClean.Replace("#SubHair", _settings.SubHair);

            StringClean = StringClean.Replace("#SubEyes", _settings.SubEyes);

            StringClean = StringClean.Replace("#SubCockSize", _settings.SubCockSize.ToString());

            StringClean = StringClean.Replace("#SubWritingTaskMin", _settings.NBWritingTaskMin.ToString());

            StringClean = StringClean.Replace("#SubWritingTaskMax", _settings.NBWritingTaskMax.ToString());

            // StringClean = StringClean.Replace("#SubWritingTaskRAND", randomizer.Next(NBWritingTaskMin.Value / 10, (NBWritingTaskMax.Value / 10) + 1)) * 10

            StringClean = StringClean.Replace("#ShortName", ssh.shortName);

            StringClean = StringClean.Replace("#GlitterContact1", _settings.Glitter1);
            StringClean = StringClean.Replace("#Contact1", _settings.Glitter1);
            StringClean = StringClean.Replace("#GlitterContact2", _settings.Glitter2);
            StringClean = StringClean.Replace("#Contact2", _settings.Glitter2);
            StringClean = StringClean.Replace("#GlitterContact3", _settings.Glitter3);
            StringClean = StringClean.Replace("#Contact3", _settings.Glitter3);

            StringClean = StringClean.Replace("#CBTCockCount", ssh.CBTCockCount.ToString());
            StringClean = StringClean.Replace("#CBTBallsCount", ssh.CBTBallsCount.ToString());

            if (_settings.OrgasmsLocked == true)
                StringClean = StringClean.Replace("#OrgasmLockDate", _settings.OrgasmLockDate.Date.ToString());
            else
                StringClean = StringClean.Replace("#OrgasmLockDate", "later");

            if (StringClean.Contains("#RandomRound100("))
            {
                string RandomFlag =Common.GetParentheses(StringClean, "#RandomRound100(");
                string OriginalFlag = RandomFlag;
                RandomFlag =Common.FixCommas(RandomFlag);
                int RandInt;
                string[] FlagArray = RandomFlag.Split(',');

                RandInt = ssh.randomizer.Next(int.Parse(FlagArray[0]), int.Parse(FlagArray[1]) + 1);
                if (RandInt >= 100)
                    RandInt = (int) (100.0 * Math.Round( RandInt / 100d));
                StringClean = StringClean.Replace("#RandomRound100(" + OriginalFlag + ")", RandInt.ToString());
            }

            if (StringClean.Contains("#RandomRound10("))
            {
                string RandomFlag =Common.GetParentheses(StringClean, "#RandomRound10(");
                string OriginalFlag = RandomFlag;
                RandomFlag =Common.FixCommas(RandomFlag);
                int RandInt;
                string[] FlagArray = RandomFlag.Split(',');

                RandInt = ssh.randomizer.Next(int.Parse(FlagArray[0]), int.Parse(FlagArray[1]) + 1);
                if (RandInt >= 10)
                    RandInt = (int) (10 * Math.Round(RandInt / (double)10));
                StringClean = StringClean.Replace("#RandomRound10(" + OriginalFlag + ")", RandInt.ToString());
            }


            if (StringClean.Contains("#RandomRound5("))
            {
                string RandomFlag =Common.GetParentheses(StringClean, "#RandomRound5(");
                string OriginalFlag = RandomFlag;
                RandomFlag =Common.FixCommas(RandomFlag);
                int RandInt;
                string[] FlagArray = RandomFlag.Split(',');

                RandInt = ssh.randomizer.Next(int.Parse(FlagArray[0]), int.Parse(FlagArray[1]) + 1);
                if (RandInt >= 5)
                    RandInt = (int)(5 * Math.Round(RandInt / (double)5));
                StringClean = StringClean.Replace("#RandomRound5(" + OriginalFlag + ")", RandInt.ToString());
            }


            if (StringClean.Contains("#Random("))
            {
                string[] randomArray = StringClean.Split(')');

                for (int i = 0; i <= randomArray.Count() - 1; i++)
                {
                    if (randomArray[i].Contains("Random("))
                    {
                        randomArray[i] = randomArray[i] + ")";
                        string RandomFlag =Common.GetParentheses(StringClean, "#Random(");
                        string OriginalFlag = RandomFlag;
                        RandomFlag =Common.FixCommas(RandomFlag);
                        int RandInt;
                        string[] FlagArray = RandomFlag.Split(',');

                        RandInt = ssh.randomizer.Next(int.Parse(FlagArray[0]), int.Parse(FlagArray[1]) + 1);
                        StringClean = StringClean.Replace("#Random(" + OriginalFlag + ")", RandInt.ToString());
                    }
                }
            }

            if (StringClean.Contains("#DateDifference("))
            {
                string[] myArray = StringClean.Split('#');

                for (int i = 0; i <= myArray.Count() - 1; i++)
                {
                    if (myArray[i].Contains("DateDifference"))
                    {
                        string DateFlag =Common.GetParentheses(StringClean, "DateDifference(");
                        string OriginalFlag = DateFlag;
                        DateFlag =Common.FixCommas(DateFlag);
                        string[] DateArray = DateFlag.Split(',');

                        long DDiff = Common.GetDateDifference(_vars.GetDate(DateArray[0]), DateArray[1]);

                        StringClean = StringClean.Replace("#DateDifference(" + OriginalFlag + ")", DDiff.ToString());
                    }
                }
            }



            int PetNameVal = ssh.randomizer.Next(1, 5);

            if (PetNameVal == 1)
                ssh.PetName = _settings.pnSetting3;
            if (PetNameVal == 2)
                ssh.PetName = _settings.pnSetting4;
            if (PetNameVal == 3)
                ssh.PetName = _settings.pnSetting5;
            if (PetNameVal == 4)
                ssh.PetName = _settings.pnSetting6;

            if (ssh.DommeMood < _settings.DomMoodMin)
            {
                PetNameVal = ssh.randomizer.Next(1, 3);
                if (PetNameVal == 1)
                    ssh.PetName = _settings.pnSetting7;
                if (PetNameVal == 2)
                    ssh.PetName = _settings.pnSetting8;
            }


            if (ssh.DommeMood > _settings.DomMoodMax)
            {
                PetNameVal = ssh.randomizer.Next(1, 3);
                if (PetNameVal == 1)
                    ssh.PetName = _settings.pnSetting1;
                if (PetNameVal == 2)
                    ssh.PetName = _settings.pnSetting2;
            }


            StringClean = StringClean.Replace("#PetName", ssh.PetName);

            // If Hour(Date.Now) < 11 Then PreCleanString = PreCleanString.Replace("#GeneralTime", "this morning")
            var now = DateTime.Now;
            var hour = now.Hour;
            if (hour > 3 && hour < 12)
                StringClean = StringClean.Replace("#GreetSub", "#GoodMorningSub");
            // If Hour(Date.Now) > 10 And Hour(Date.Now) < 18 Then PreCleanString = PreCleanString.Replace("#GeneralTime", "today")
            if (hour > 11 && hour < 18)
                StringClean = StringClean.Replace("#GreetSub", "#GoodAfternoonSub");
            // If Hour(Date.Now) > 17 Then PreCleanString = PreCleanString.Replace("#GeneralTime", "tonight")
            if (hour > 17 || hour < 4)
                StringClean = StringClean.Replace("#GreetSub", "#GoodEveningSub");


            if (hour < 4)
                StringClean = StringClean.Replace("#GeneralTime", "tonight");
            if (hour > 3 && hour < 11)
                StringClean = StringClean.Replace("#GeneralTime", "this morning");
            if (hour > 10 && hour < 18)
                StringClean = StringClean.Replace("#GeneralTime", "today");
            if (hour > 17)
                StringClean = StringClean.Replace("#GeneralTime", "tonight");

            if (ssh.AssImage)
                StringClean = StringClean.Replace("#TnAFastSlidesResult", "#BBnB_Ass");
            if (ssh.BoobImage)
                StringClean = StringClean.Replace("#TnAFastSlidesResult", "#BBnB_Boobs");

            if (StringClean.Contains("#RANDNumberLow"))
            {
                // ### Number between 3-5 , 5-25
                ssh.TempVal = ssh.randomizer.Next(1, 6) * _settings.DomLevel;
                if (ssh.TempVal > 10)
                    ssh.TempVal = (int)(5 * Math.Round(ssh.TempVal / (double)5));
                if (ssh.TempVal < 3)
                    ssh.TempVal = 3;
                StringClean = StringClean.Replace("#RNDNumberLow", ssh.TempVal.ToString());
            }


            if (StringClean.Contains("#RANDNumberHigh"))
            {
                // ### Number between 5-25 , 25-100
                ssh.TempVal = ssh.randomizer.Next(5, 21) * _settings.DomLevel;
                if (ssh.TempVal > 10)
                    ssh.TempVal = (int)(5 * Math.Round(ssh.TempVal / (double)5));
                StringClean = StringClean.Replace("#RNDNumberHigh", ssh.TempVal.ToString());
            }


            if (StringClean.Contains("#RANDNumber"))
            {
                // ### Number between 3-10 , 5-50
                ssh.TempVal = ssh.randomizer.Next(1, 11) * _settings.DomLevel;
                if (ssh.TempVal > 10)
                    ssh.TempVal = (int) (5 * Math.Round(ssh.TempVal / (double)5));
                if (ssh.TempVal < 3)
                    ssh.TempVal = 3;
                StringClean = StringClean.Replace("#RNDNumber", ssh.TempVal.ToString());
            }



            StringClean = StringClean.Replace("#RP_ChosenCase", _risky.RiskyPickNumber.ToString());
            StringClean = StringClean.Replace("#RP_RespondCase", _risky.RiskyResponse.ToString());
            // StringClean = StringClean.Replace("#RP_CaseNumber", FrmCardList.RiskyCase)
            if (_risky.RiskyPickCount == 0)
                StringClean = StringClean.Replace("#RP_CaseNumber", _risky.LBLPick1.ToString());
            if (_risky.RiskyPickCount == 1)
                StringClean = StringClean.Replace("#RP_CaseNumber", _risky.LBLPick2.ToString());
            if (_risky.RiskyPickCount == 2)
                StringClean = StringClean.Replace("#RP_CaseNumber", _risky.LBLPick3.ToString());
            if (_risky.RiskyPickCount == 3)
                StringClean = StringClean.Replace("#RP_CaseNumber", _risky.LBLPick4.ToString());
            if (_risky.RiskyPickCount == 4)
                StringClean = StringClean.Replace("#RP_CaseNumber", _risky.LBLPick5.ToString());
            if (_risky.RiskyPickCount > 4)
                StringClean = StringClean.Replace("#RP_CaseNumber", _risky.LBLPick6.ToString());
            StringClean = StringClean.Replace("#RP_EdgeOffer", _risky.RiskyEdgeOffer.ToString());
            StringClean = StringClean.Replace("#RP_TokenOffer", _risky.RiskyTokenOffer.ToString());
            StringClean = StringClean.Replace("#RP_EdgesOwed", _risky.EdgesOwed.ToString());
            StringClean = StringClean.Replace("#RP_TokensPaid", _risky.TokensPaid.ToString());

            StringClean = StringClean.Replace("#BronzeTokens", ssh.BronzeTokens.ToString());
            StringClean = StringClean.Replace("#SilverTokens", ssh.SilverTokens.ToString());
            StringClean = StringClean.Replace("#GoldTokens", ssh.GoldTokens.ToString());

            StringClean = StringClean.Replace("#SessionEdges", ssh.SessionEdges.ToString());
            StringClean = StringClean.Replace("#SessionCBTCock", ssh.CBTCockCount.ToString());
            StringClean = StringClean.Replace("#SessionCBTBalls", ssh.CBTBallsCount.ToString());

            // StringClean = StringClean.Replace("#Sys_SubLeftEarly", _settings.Sys_SubLeftEarly)
            // StringClean = StringClean.Replace("#Sys_SubLeftEarlyTotal", _settings.Sys_SubLeftEarlyTotal)

            StringClean = StringClean.Replace("#SlideshowCount", (ssh.CustomSlideshow.Count - 1).ToString());
            StringClean = StringClean.Replace("#SlideshowCurrent", ssh.CustomSlideshow.Index.ToString());
            StringClean = StringClean.Replace("#SlideshowRemaining", ((ssh.CustomSlideshow.Count - 1) - ssh.CustomSlideshow.Index).ToString());

            StringClean = StringClean.Replace("#CurrentTime", now.ToString("h:mm"));
            StringClean = StringClean.Replace("#CurrentDay", now.ToString("dddd"));
            StringClean = StringClean.Replace("#CurrentMonth", now.ToString("MMMMM"));
            StringClean = StringClean.Replace("#CurrentYear", now.ToString("yyyy"));
            StringClean = StringClean.Replace("#CurrentDate", now.ToShortDateString());
            // StringClean = StringClean.Replace("#CurrentDate", Format(Now, "MM/dd/yyyy"))

            if (StringClean.Contains("#RandomSlideshowCategory"))
            {
                List<string> RanCat = new List<string>();

                if (_settings.CBIHardcore)
                    RanCat.Add("Hardcore");
                if (_settings.CBISoftcore)
                    RanCat.Add("Softcore");
                if (_settings.CBILesbian)
                    RanCat.Add("Lesbian");
                if (_settings.CBIBlowjob)
                    RanCat.Add("Blowjob");
                if (_settings.CBIFemdom)
                    RanCat.Add("Femdom");
                if (_settings.CBILezdom)
                    RanCat.Add("Lezdom");
                if (_settings.CBIHentai)
                    RanCat.Add("Hentai");
                if (_settings.CBIGay)
                    RanCat.Add("Gay");
                if (_settings.CBIMaledom)
                    RanCat.Add("Maledom");
                if (_settings.CBICaptions)
                    RanCat.Add("Captions");
                if (_settings.CBIGeneral)
                    RanCat.Add("General");

                if (RanCat.Count < 1)
                    _chat.ChatAddSystemMessage("ERROR: #RandomSlideshowCategory called but no local images have been set.");
                else
                    StringClean = StringClean.Replace("#RandomSlideshowCategory", RanCat[ssh.randomizer.Next(0, RanCat.Count)]);
            }


            // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            // ImageCount
            // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            if (StringClean.Contains("#LocalImageCount"))
            {
                Dictionary<ImageGenre, ImageDataContainer> temp = _imageData.GetImageData();
                int counter = 0;

                foreach (ImageGenre genre in temp.Keys)
                    counter += temp[genre].CountImages(ImageSourceType.Local);

                StringClean = StringClean.Replace("#LocalImageCount", counter.ToString());
            }

            if (StringClean.Contains("#BlogImageCount"))
                StringClean = StringClean.Replace("#BlogImageCount", _imageData.GetImageData(ImageGenre.Blog).CountImages().ToString());

            if (StringClean.Contains("#ButtImageCount"))
                StringClean = StringClean.Replace("#ButtImageCount", _imageData.GetImageData(ImageGenre.Butt).CountImages().ToString());

            if (StringClean.Contains("#ButtsImageCount"))
                StringClean = StringClean.Replace("#ButtsImageCount", _imageData.GetImageData(ImageGenre.Butt).CountImages().ToString());

            if (StringClean.Contains("#BoobImageCount"))
                StringClean = StringClean.Replace("#BoobImageCount", _imageData.GetImageData(ImageGenre.Boobs).CountImages().ToString());

            if (StringClean.Contains("#BoobsImageCount"))
                StringClean = StringClean.Replace("#BoobsImageCount", _imageData.GetImageData(ImageGenre.Boobs).CountImages().ToString());

            if (StringClean.Contains("#HardcoreImageCount"))
                StringClean = StringClean.Replace("#HardcoreImageCount", _imageData.GetImageData(ImageGenre.Hardcore).CountImages().ToString());

            if (StringClean.Contains("#HardcoreImageCount"))
                StringClean = StringClean.Replace("#HardcoreImageCount", _imageData.GetImageData(ImageGenre.Hardcore).CountImages().ToString());

            if (StringClean.Contains("#SoftcoreImageCount"))
                StringClean = StringClean.Replace("#SoftcoreImageCount", _imageData.GetImageData(ImageGenre.Softcore).CountImages().ToString());

            if (StringClean.Contains("#LesbianImageCount"))
                StringClean = StringClean.Replace("#LesbianImageCount", _imageData.GetImageData(ImageGenre.Lesbian).CountImages().ToString());

            if (StringClean.Contains("#BlowjobImageCount"))
                StringClean = StringClean.Replace("#BlowjobImageCount", _imageData.GetImageData(ImageGenre.Blowjob).CountImages().ToString());

            if (StringClean.Contains("#FemdomImageCount"))
                StringClean = StringClean.Replace("#FemdomImageCount", _imageData.GetImageData(ImageGenre.Femdom).CountImages().ToString());

            if (StringClean.Contains("#LezdomImageCount"))
                StringClean = StringClean.Replace("#LezdomImageCount", _imageData.GetImageData(ImageGenre.Lezdom).CountImages().ToString());

            if (StringClean.Contains("#HentaiImageCount"))
                StringClean = StringClean.Replace("#HentaiImageCount", _imageData.GetImageData(ImageGenre.Hentai).CountImages().ToString());

            if (StringClean.Contains("#GayImageCount"))
                StringClean = StringClean.Replace("#GayImageCount", _imageData.GetImageData(ImageGenre.Gay).CountImages().ToString());

            if (StringClean.Contains("#MaledomImageCount"))
                StringClean = StringClean.Replace("#MaledomImageCount", _imageData.GetImageData(ImageGenre.Maledom).CountImages().ToString());

            if (StringClean.Contains("#CaptionsImageCount"))
                StringClean = StringClean.Replace("#CaptionsImageCount", _imageData.GetImageData(ImageGenre.Captions).CountImages().ToString());

            if (StringClean.Contains("#GeneralImageCount"))
                StringClean = StringClean.Replace("#GeneralImageCount", _imageData.GetImageData(ImageGenre.General).CountImages().ToString());

            if (StringClean.Contains("#LikedImageCount"))
                StringClean = StringClean.Replace("#LikedImageCount", _imageData.GetImageData(ImageGenre.Liked).CountImages().ToString());

            if (StringClean.Contains("#DislikedImageCount"))
                StringClean = StringClean.Replace("#DislikedImageCount", _imageData.GetImageData(ImageGenre.Disliked).CountImages().ToString());
            // ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
            // ImageCount - End
            // ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
            if (StringClean.Contains("#EdgeHold"))
            {
                int i = _settings.HoldTheEdgeMin;
                if (_settings.HoldTheEdgeMinAmount == "minutes")
                    i *= 60;

                int x = _settings.HoldTheEdgeMax;
                if (_settings.HoldTheEdgeMaxAmount == "minutes")
                    x *= 60;

                int t = ssh.randomizer.Next(i, x + 1);
                if (t >= 5)
                    t = (int)( 5 * Math.Round(t / (double)5));
                string TConvert = Common.ConvertSeconds(t);
                StringClean = StringClean.Replace("#EdgeHold", TConvert);
            }

            if (StringClean.Contains("#LongHold"))
            {
                int i = _settings.LongHoldMin;
                int x = _settings.LongHoldMax;
                int t = ssh.randomizer.Next(i, x + 1);
                t *= 60;
                if (t >= 5)
                    t = (int)(5 * Math.Round(t / (double)5));
                string TConvert = Common.ConvertSeconds(t);
                StringClean = StringClean.Replace("#LongHold", TConvert);
            }

            if (StringClean.Contains("#ExtremeHold"))
            {
                int i = _settings.ExtremeHoldMin;
                int x = _settings.ExtremeHoldMax;
                int t = ssh.randomizer.Next(i, x + 1);
                t *= 60;
                if (t >= 5)
                    t =(int)(5 * Math.Round(t / (double)5));
                string TConvert = Common.ConvertSeconds(t);
                StringClean = StringClean.Replace("#ExtremeHold", TConvert);
            }

            StringClean = StringClean.Replace("#CurrentImage", ssh.ImageLocation);

            int @int;

            if (StringClean.Contains("#TaskEdges"))
            {
                @int = ssh.randomizer.Next(_settings.TaskEdgesMin, _settings.TaskEdgesMax + 1);
                if (@int > 5)
                    @int = (int)(5 * Math.Round(@int / (double)5));
                StringClean = StringClean.Replace("#TaskEdges", @int.ToString());
            }

            if (StringClean.Contains("#TaskStrokes"))
            {
                @int = ssh.randomizer.Next(_settings.TaskStrokesMin, _settings.TaskStrokesMax + 1);
                if (@int > 10)
                    @int = (int)(10 * Math.Round(@int / (double)10));
                StringClean = StringClean.Replace("#TaskStrokes", @int.ToString());
            }

            if (StringClean.Contains("#TaskHours"))
            {
                @int = ssh.randomizer.Next(1, _settings.DomLevel + 1) + _settings.DomLevel;
                StringClean = StringClean.Replace("#TaskHours", @int.ToString());
            }

            if (StringClean.Contains("#TaskMinutes"))
            {
                @int = ssh.randomizer.Next(5, 13) * _settings.DomLevel;
                StringClean = StringClean.Replace("#TaskMinutes", @int.ToString());
            }

            if (StringClean.Contains("#TaskSeconds"))
            {
                @int = ssh.randomizer.Next(10, 30) * _settings.DomLevel * ssh.randomizer.Next(1, _settings.DomLevel + 1);
                StringClean = StringClean.Replace("#TaskSeconds", @int.ToString());
            }

            if (StringClean.Contains("#TaskAmountLarge"))
            {
                @int = (ssh.randomizer.Next(15, 26) * _settings.DomLevel) * 2;
                if (@int > 5)
                    @int = (int)(5 * Math.Round(@int / (double)5));
                StringClean = StringClean.Replace("#TaskAmountLarge", @int.ToString());
            }

            if (StringClean.Contains("#TaskAmountSmall"))
            {
                @int = ((ssh.randomizer.Next(5, 11) * _settings.DomLevel) / 2);
                if (@int > 5)
                    @int = (int)( 5 * Math.Round(@int / (double)5));
                StringClean = StringClean.Replace("#TaskAmountSmall", @int.ToString());
            }

            if (StringClean.Contains("#TaskAmount"))
            {
                @int = ssh.randomizer.Next(15, 26) * _settings.DomLevel;
                if (@int > 5)
                    @int = (int)( 5 * Math.Round(@int / (double)5));
                StringClean = StringClean.Replace("#TaskAmount", @int.ToString());
            }

            if (StringClean.Contains("#TaskStrokingTime"))
            {
                @int = ssh.randomizer.Next(_settings.TaskStrokingTimeMin, _settings.TaskStrokingTimeMax + 1);
                @int *= 60;
                string TConvert = Common.ConvertSeconds(@int);
                StringClean = StringClean.Replace("#TaskStrokingTime", TConvert);
            }

            if (StringClean.Contains("#TaskHoldTheEdgeTime"))
            {
                @int = ssh.randomizer.Next(_settings.TaskEdgeHoldTimeMin, _settings.TaskEdgeHoldTimeMax + 1);
                @int *= 60;
                string TConvert = Common.ConvertSeconds(@int);
                StringClean = StringClean.Replace("#TaskHoldTheEdgeTime", TConvert);
            }

            if (StringClean.Contains("#TaskCBTTime"))
            {
                @int = ssh.randomizer.Next(_settings.TaskCBTTimeMin, _settings.TaskCBTTimeMax + 1);
                @int *= 60;
                string TConvert = Common.ConvertSeconds(@int);
                StringClean = StringClean.Replace("#TaskCBTTime", TConvert);
            }



            return StringClean;
        }

        private bool FilterCheck(string input, int value)
        {
            return FilterCheck(input, value.ToString());
        }

        private bool FilterCheck(string Input, string condition)
        {
            var inputSections = Input.Split(',').Select(x => x.Trim()).ToArray();
            var containsNot = inputSections.Where(x => x.Equals("not", StringComparison.OrdinalIgnoreCase)).ToList();

            if (containsNot.Any())
            {
                foreach (var section in inputSections)
                {
                    // if (section.Equals("not", StringComparison.OrdinalIgnoreCase)) continue;
                    if (section.Equals(condition, StringComparison.OrdinalIgnoreCase))
                        return false;

                }
                return true;
            } else
            {
                foreach (var section in inputSections)
                {
                    if (section.Equals(condition, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
                return false;
            }
        }

        public bool FilterCheckOrgasm(string input, string setting)
        {
            var TextCondition = string.Empty;
            if (setting.Equals("ALWAYS ALLOWS",StringComparison.OrdinalIgnoreCase))
                TextCondition = "ALWAYS";
            if (setting.Equals("OFTEN ALLOWS", StringComparison.OrdinalIgnoreCase))
                TextCondition = "OFTEN";
            if (setting.Equals("SOMETIMES ALLOWS", StringComparison.OrdinalIgnoreCase))
                TextCondition = "SOMETIMES";
            if (setting.Equals("RARELY ALLOWS", StringComparison.OrdinalIgnoreCase))
                TextCondition = "RARELY";
            if (setting.Equals("NEVER ALLOWS", StringComparison.OrdinalIgnoreCase))
                TextCondition = "NEVER";
            return FilterCheck(input, setting);
        }

        public bool FilterCheckRuin(string input, string setting)
        {
            var TextCondition = string.Empty;
            if (setting.Equals("ALWAYS RUINS", StringComparison.OrdinalIgnoreCase))
                TextCondition = "ALWAYS";
            if (setting.Equals("OFTEN RUINS", StringComparison.OrdinalIgnoreCase))
                TextCondition = "OFTEN";
            if (setting.Equals("SOMETIMES RUINS", StringComparison.OrdinalIgnoreCase))
                TextCondition = "SOMETIMES";
            if (setting.Equals("RARELY RUINS", StringComparison.OrdinalIgnoreCase))
                TextCondition = "RARELY";
            if (setting.Equals("NEVER RUINS", StringComparison.OrdinalIgnoreCase))
                TextCondition = "NEVER";
            return FilterCheck(input, setting);
        }

        public bool GetMatch(string Line, string Command, string Match)
        {
            Debug.Print("Line = " + Line);
            Debug.Print("Command = " + Command);
            Debug.Print("Match = " + Match);

            string CommandFlag = Common.GetParentheses(Line, Command);

            if (CommandFlag.Contains(","))
            {
                CommandFlag = Common.FixCommas(CommandFlag);

                string[] CommandArray = CommandFlag.Split(',');

                bool NotFlag = false;

                for (int i = 0; i <= CommandArray.Count() - 1; i++)
                {
                    if (CommandArray[i].Equals("NOT", StringComparison.OrdinalIgnoreCase))
                        NotFlag = true;
                }

                if (NotFlag == true)
                {
                    for (int i = 0; i <= CommandArray.Count() - 1; i++)
                    {
                        if (CommandArray[i] == Match)
                            return false;
                    }

                    return true;
                }
                else
                    for (int i = 0; i <= CommandArray.Count() - 1; i++)
                    {
                        if (CommandArray[i] == Match)
                            return true;
                    }
            }
            else if (CommandFlag == Match)
                return true;

            return false;
        }


        public bool GetFilter(string FilterString, bool Linear = false)
        {
            var now = DateTime.Now;
            string OrgFilterString = FilterString;
            var mainPictureBoxVisible = true; // (DomWMP.playState == WMPPlayState.wmppsPlaying) || (DomWMP.playState == WMPPlayState.wmppsPaused)
            var videoPlayingOrPaused = false; // TODO (DomWMP.playState == WMPPlayState.wmppsPlaying) || (DomWMP.playState == WMPPlayState.wmppsPaused)
            try
            {
                if (Linear == false)
                {
                    if (FilterString.IndexOf("@DommeTag(", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        if (ssh.LockImage || !mainPictureBoxVisible || ssh.CustomSlideEnabled)
                            return false;
                        else if (_imageData.GetLocal(Common.GetParentheses(FilterString, "@DommeTag("), true) == string.Empty)
                            return false;
                    }

                    if (FilterString.IndexOf("@DommeTagOr(", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        if (ssh.LockImage || !mainPictureBoxVisible || ssh.CustomSlideEnabled)
                            return false;
                        else if (_imageData.GetLocal(Common.GetParentheses(FilterString, "@DommeTagOr("), true, "Or") == string.Empty)
                            return false;
                    }

                    if (FilterString.IndexOf("@DommeTagAny(", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        if (ssh.LockImage || !mainPictureBoxVisible || ssh.CustomSlideEnabled)
                            return false;
                        else if (_imageData.GetLocal(Common.GetParentheses(FilterString, "@DommeTagAny("), true, "Any") == string.Empty)
                            return false;
                    }

                    if (FilterString.IndexOf("@DomTag(", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        if (ssh.LockImage || !mainPictureBoxVisible || ssh.CustomSlideEnabled)
                            return false;
                        else if (_imageData.GetLocal(Common.GetParentheses(FilterString, "@DomTag("), true) == string.Empty)
                            return false;
                    }

                    if (FilterString.IndexOf("@DomTagOr(", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        if (ssh.LockImage || !mainPictureBoxVisible || ssh.CustomSlideEnabled)
                            return false;
                        else if (_imageData.GetLocal(Common.GetParentheses(FilterString, "@DomTagOr("), true, "Or") == string.Empty)
                            return false;
                    }

                    if (FilterString.IndexOf("@DomTagAny(", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        if (ssh.LockImage || !mainPictureBoxVisible || ssh.CustomSlideEnabled)
                            return false;
                        else if (_imageData.GetLocal(Common.GetParentheses(FilterString, "@DomTagAny("), true, "Any") == string.Empty)
                            return false;
                    }
                    if (FilterString.IndexOf("@DomTagFirst(", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        if (ssh.LockImage || !mainPictureBoxVisible || ssh.CustomSlideEnabled)
                            return false;
                        else if (_imageData.GetLocal(Common.GetParentheses(FilterString, "@DomTagFirst("), true, "First") == string.Empty)
                            return false;
                    }
                    if (FilterString.Contains("@ImageTag("))
                    {
                        if (ssh.LockImage || !mainPictureBoxVisible || ssh.CustomSlideEnabled)
                            return false;
                        else if (_imageData.GetLocal(Common.GetParentheses(FilterString, "@ImageTag(")) == string.Empty)
                            return false;
                    }

                    if (FilterString.Contains("@ImageTagOr("))
                    {
                        if (ssh.LockImage || !mainPictureBoxVisible || ssh.CustomSlideEnabled)
                            return false;
                        else if (_imageData.GetLocal(Common.GetParentheses(FilterString, "@ImageTagOr("), false, "Or") == string.Empty)
                            return false;
                    }

                    if (FilterString.Contains("@ImageTagAny("))
                    {
                        if (ssh.LockImage || !mainPictureBoxVisible || ssh.CustomSlideEnabled)
                            return false;
                        else if (_imageData.GetLocal(Common.GetParentheses(FilterString, "@ImageTagAny("), false, "Any") == string.Empty)
                            return false;
                    }
                    if (FilterString.Contains("@ShowDomRandomImage") && (ssh.LockImage || !mainPictureBoxVisible || ssh.CustomSlideEnabled))
                        return false;
                    if (FilterString.Contains("@ShowImage") && (ssh.LockImage || !mainPictureBoxVisible || ssh.CustomSlideEnabled))
                        return false;
                    // ################## @Show-Category-Image #####################
                    if (FilterString.Contains("@ShowBlogImage") | FilterString.Contains("@NewBlogImage"))
                    {
                        if (!_imageData.GetImageData(ImageGenre.Blog).IsAvailable() | ssh.LockImage == true | ssh.CustomSlideEnabled == true | mainPictureBoxVisible == false)
                            return false;
                    }
                    if (FilterString.Contains("@ShowBlowjobImage"))
                    {
                        if (!_imageData.GetImageData(ImageGenre.Blowjob).IsAvailable() | ssh.LockImage == true | ssh.CustomSlideEnabled == true | mainPictureBoxVisible == false)
                            return false;
                    }
                    if (FilterString.Contains("@ShowBoobsImage") | FilterString.Contains("@ShowBoobImage"))
                    {
                        if (!_imageData.GetImageData(ImageGenre.Boobs).IsAvailable() | ssh.LockImage == true | ssh.CustomSlideEnabled == true | mainPictureBoxVisible == false)
                            return false;
                    }
                    if (FilterString.Contains("@ShowButtImage") | FilterString.Contains("@ShowButtsImage"))
                    {
                        if (!_imageData.GetImageData(ImageGenre.Butt).IsAvailable() | ssh.LockImage == true | ssh.CustomSlideEnabled == true | mainPictureBoxVisible == false)
                            return false;
                    }
                    if (FilterString.Contains("@ShowCaptionsImage"))
                    {
                        if (!_imageData.GetImageData(ImageGenre.Captions).IsAvailable() | ssh.LockImage == true | ssh.CustomSlideEnabled == true | mainPictureBoxVisible == false)
                            return false;
                    }
                    if (FilterString.Contains("@ShowDislikedImage"))
                    {
                        if (!_imageData.GetImageData(ImageGenre.Disliked).IsAvailable() | ssh.LockImage == true | ssh.CustomSlideEnabled == true | mainPictureBoxVisible == false)
                            return false;
                    }
                    if (FilterString.Contains("@ShowFemdomImage"))
                    {
                        if (!_imageData.GetImageData(ImageGenre.Femdom).IsAvailable() | ssh.LockImage == true | ssh.CustomSlideEnabled == true | mainPictureBoxVisible == false)
                            return false;
                    }
                    if (FilterString.Contains("@ShowGayImage"))
                    {
                        if (!_imageData.GetImageData(ImageGenre.Gay).IsAvailable() | ssh.LockImage == true | ssh.CustomSlideEnabled == true | mainPictureBoxVisible == false)
                            return false;
                    }
                    if (FilterString.Contains("@ShowGeneralImage"))
                    {
                        if (!_imageData.GetImageData(ImageGenre.General).IsAvailable() | ssh.LockImage == true | ssh.CustomSlideEnabled == true | mainPictureBoxVisible == false)
                            return false;
                    }
                    if (FilterString.Contains("@ShowHardcoreImage"))
                    {
                        if (!_imageData.GetImageData(ImageGenre.Hardcore).IsAvailable() | ssh.LockImage == true | ssh.CustomSlideEnabled == true | mainPictureBoxVisible == false)
                            return false;
                    }
                    if (FilterString.Contains("@ShowHentaiImage"))
                    {
                        if (!_imageData.GetImageData(ImageGenre.Hentai).IsAvailable() | ssh.LockImage == true | ssh.CustomSlideEnabled == true | mainPictureBoxVisible == false)
                            return false;
                    }
                    if (FilterString.Contains("@ShowLesbianImage"))
                    {
                        if (!_imageData.GetImageData(ImageGenre.Lesbian).IsAvailable() | ssh.LockImage == true | ssh.CustomSlideEnabled == true | mainPictureBoxVisible == false)
                            return false;
                    }
                    if (FilterString.Contains("@ShowLezdomImage"))
                    {
                        if (!_imageData.GetImageData(ImageGenre.Lezdom).IsAvailable() | ssh.LockImage == true | ssh.CustomSlideEnabled == true | mainPictureBoxVisible == false)
                            return false;
                    }
                    if (FilterString.Contains("@ShowLikedImage"))
                    {
                        if (!_imageData.GetImageData(ImageGenre.Liked).IsAvailable() | ssh.LockImage == true | ssh.CustomSlideEnabled == true | mainPictureBoxVisible == false)
                            return false;
                    }
                    if (FilterString.Contains("@ShowLocalImage"))
                    {
                        if (_flags.FlagExists("SYS_NoPornAllowed") == true | ssh.LockImage == true | mainPictureBoxVisible == false)
                            return false;
                    }
                    if (FilterString.Contains("@ShowLocalImage") | FilterString.Contains("@ShowButtImage") | FilterString.Contains("@ShowBoobsImage") | FilterString.Contains("@ShowButtsImage") | FilterString.Contains("@ShowBoobsImage"))
                    {
                        if (ssh.CustomSlideEnabled == true | ssh.LockImage == true)
                            return false;
                    }
                    // TODO: Add ImageDataContainerUsage to filter @ShowLocalImage correct.
                    if (FilterString.Contains("@ShowLocalImage") & _settings.CBIHardcore == false & _settings.CBISoftcore == false & _settings.CBILesbian == false & _settings.CBIBlowjob == false & _settings.CBIFemdom == false & _settings.CBILezdom == false & _settings.CBIHentai == false & _settings.CBIGay == false & _settings.CBIMaledom == false & _settings.CBICaptions == false & _settings.CBIGeneral == false)
                        return false;

                    if (FilterString.Contains("@ShowTaggedImage") || FilterString.Contains("@Tag"))
                    {
                        List<string> Tags = FilterString.Split()
        .Select(s => s.Trim())
        .Where(w => System.Convert.ToString(w).StartsWith("@Tag")).ToList();

                        if (_imageData.GetLocal(Tags, null) == string.Empty | mainPictureBoxVisible == false)
                            return false;
                    }

                    if (FilterString.Contains("@ShowMaledomImage"))
                    {
                        if (!_imageData.GetImageData(ImageGenre.Maledom).IsAvailable() | ssh.LockImage == true | ssh.CustomSlideEnabled == true | mainPictureBoxVisible == false)
                            return false;
                    }
                    if (FilterString.Contains("@ShowSoftcoreImage"))
                    {
                        if (!_imageData.GetImageData(ImageGenre.Softcore).IsAvailable() | ssh.LockImage == true | ssh.CustomSlideEnabled == true | mainPictureBoxVisible == false)
                            return false;
                    }
                }
                if (FilterString.Contains("@Force"))
                {
                    if (!ssh.LockImage)
                        return false;
                }
                else if (ssh.LockImage || ssh.CustomSlideEnabled)
                {
                    if (!FilterString.Contains("@PlayVideoNoWait") && FilterString.Contains("@PlayVideo"))
                        return false;
                    if (FilterString.Contains("@PlayRedLightGreenLight") || FilterString.Contains("@PlayAvoidTheEdge"))
                        return false;
                }
                if (!FilterString.Contains("@PlayVideoNoWait") && (FilterString.Contains("@PlayVideo") || FilterString.Contains("@PlayRedLightGreenLight") || FilterString.Contains("@PlayAvoidTheEdge")) && !FilterString.Contains("@StopVideo") && (videoPlayingOrPaused))
                    return false;
                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                // Commands to sort out
                // This Section contains @Commands, which are able to disqualify vocabulary lines.
                // 
                // Example-line: "Whatever Text to display @DommeTag(Glaring)"
                // 
                // This line has to be sorted out, if there are no corresponding images tagged 
                // with "glaring".
                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼

                if (FilterString.Contains("@Flag(") | FilterString.Contains("@ControlFlag("))
                {
                    string writeFlag;
                    string[] splitFlag;
                    if (FilterString.Contains("@Flag("))
                        writeFlag = Common.GetParentheses(FilterString, "@Flag(");
                    else
                        writeFlag = Common.GetParentheses(FilterString, "@ControlFlag(");
                    writeFlag = Common.FixCommas(writeFlag);
                    splitFlag = writeFlag.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var s in splitFlag)
                    {
                        if (!_flags.FlagExists(s))
                            return false;
                    }
                }

                if (FilterString.Contains("@NotFlag("))
                {
                    string writeFlag;
                    string[] splitFlag;
                    writeFlag = Common.GetParentheses(FilterString, "@NotFlag(");
                    writeFlag = Common.FixCommas(writeFlag);
                    splitFlag = writeFlag.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var s in splitFlag)
                    {
                        if (_flags.FlagExists(s))
                            return false;
                    }
                }

                if (FilterString.Contains("@FlagOr("))
                {
                    string writeFlag;
                    string[] splitFlag;
                    writeFlag = Common.GetParentheses(FilterString, "@FlagOr(");
                    writeFlag = Common.FixCommas(writeFlag);
                    splitFlag = writeFlag.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    bool result = false;
                    foreach (var s in splitFlag)
                    {
                        if (_flags.FlagExists(s))
                        {
                            result = true;
                            break;
                        }
                    }
                    if (!result)
                        return false;
                }

                if (FilterString.Contains("@Mood("))
                {
                    string moodFlag = Common.GetParentheses(FilterString, "@Mood(");
                    if (Int32.TryParse(moodFlag, out var iMood))
                    {
                        if (ssh.DommeMood != iMood)
                            return false;
                    }
                    else if (moodFlag == "worst")
                    {
                        if (ssh.DommeMood != 1)
                            return false;
                    }
                    else if (moodFlag == "bad")
                    {
                        if (ssh.DommeMood >= _settings.DomMoodMin)
                            return false;
                    }
                    else if (moodFlag == "neutral")
                    {
                        if (ssh.DommeMood > _settings.DomMoodMax | ssh.DommeMood < _settings.DomMoodMin)
                            return false;
                    }
                    else if (moodFlag == "good")
                    {
                        if (ssh.DommeMood <= _settings.DomMoodMax)
                            return false;
                    }
                    else if (moodFlag == "best")
                    {
                        if (ssh.DommeMood != 10)
                            return false;
                    }
                }

                if (FilterString.Contains("@GoodMood") & ssh.DommeMood <= _settings.DomMoodMax)
                    return false;
                if (FilterString.Contains("@BadMood") & ssh.DommeMood >= _settings.DomMoodMin)
                    return false;
                if (FilterString.Contains("@NeutralMood"))
                {
                    if (ssh.DommeMood > _settings.DomMoodMax | ssh.DommeMood < _settings.DomMoodMin)
                        return false;
                }
                if (FilterString.Contains("@Variable["))
                {
                    if (_vars.CheckVariable(FilterString) == false)
                        return false;
                }

                if (FilterString.Contains("@Group("))
                {
                    string GroupCheck = Common.GetParentheses(FilterString, "@Group(");
                    string[] grouparray = GroupCheck.Split(',');
                    bool b = false;
                    for (int i = 0; i <= grouparray.Length - 1; i++)
                    {
                        b = true;
                        foreach (char c in grouparray[i])
                        {
                            if (!ssh.Group.Contains(c))
                            {
                                b = false;
                                break;
                            }
                        }
                    }
                    if (!b)
                        return false;
                }

                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                // Possible space Filters
                // This Section Contains @CommandFilters which allow space chars (0x20).
                // 
                // Example: "@Cup(A, B) Whatever Text To display"
                // Mostly all perametrized command filters allow space chars in parameters.
                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼

                if (FilterString.Contains("@AllowsOrgasm("))
                {
                    if (FilterCheckOrgasm(Common.GetParentheses(FilterString, "@AllowsOrgasm("), _settings.OrgasmAllow) == false)
                        return false;
                }
                if (FilterString.Contains("@ApathyLevel("))
                {
                    if (FilterCheck(Common.GetParentheses(FilterString, "@ApathyLevel("), _settings.DomEmpathy) == false)
                        return false;
                }
                if (FilterString.Contains("@DommeLevel("))
                {
                    if (FilterCheck(Common.GetParentheses(FilterString, "@DommeLevel("), _settings.DomLevel) == false)
                        return false;
                }
                if (FilterString.Contains("@Cup("))
                {
                    if (FilterCheck(Common.GetParentheses(FilterString, "@Cup("), _settings.DomCup) == false)
                        return false;
                }
                if (FilterString.Contains("@RuinsOrgasm("))
                {
                    if (FilterCheckRuin(Common.GetParentheses(FilterString, "@RuinsOrgasm("), _settings.OrgasmRuin) == false)
                        return false;
                }

                if (FilterString.Contains("@CheckDate(") && !Linear)
                {
                    if (!_vars.CheckDateList(FilterString))
                        return false;
                }

                if (FilterString.Contains("@Month("))
                {
                    if (GetMatch(FilterString, "@Month(", now.Month.ToString()) == false)
                        return false;
                }

                if (FilterString.Contains("@Day("))
                {
                    if (GetMatch(FilterString, "@Day(", now.Day.ToString()) == false)
                        return false;
                }

                if (FilterString.Contains("@DayOfWeek("))
                {
                    string Para = Common.GetParentheses(FilterString, "@DayOfWeek(");

                    if (int.TryParse(Para, out var _))
                    {
                        if (GetMatch(FilterString, "@DayOfWeek(", Common.Weekday(now, DayOfWeek.Monday).ToString()) == false)
                            return false;
                    }
                    else
                    {
                        // Monday... etc
                        if (GetMatch(FilterString, "@DayOfWeek(", now.ToString("dddd")) == false)
                            return false;
                    }
                }
                if (FilterString.Contains("@SetModule("))
                {
                    if (ssh.SetModule != "" | ssh.BookmarkModule == true)
                        return false;
                }
                // ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
                // Possible space Filters - End
                // ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                // Single word filters
                // This section contains single word command filters. 
                // Since there are some legacy commands, which are filters and also instructions, 
                // this section will ignore all @Statements after @NullResponse or the first 
                // word not starting with "@" (0x40)
                // 
                // Beware: destroys the original FilterString-Value!
                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                string[] FilterList;

                FilterList = FilterString.Split(' ');
                FilterString = "";

                for (int f = 0; f <= FilterList.Count() - 1; f++)
                {
                    if (!FilterList[f].StartsWith("@") | FilterList[f].Contains("@NullResponse"))
                        break;
                    FilterString = FilterString + FilterList[f] + " ";
                }

                if (FilterString == "")
                    return true;

                if (FilterString.ToLower().Contains("@crazy") && !_settings.DomCrazy)
                    return false;
                if (FilterString.ToLower().Contains("@vulgar") && !_settings.DomVulgar)
                    return false;
                if (FilterString.ToLower().Contains("@supremacist") && !_settings.DomSupremacist)
                    return false;
                if (FilterString.ToLower().Contains("@sadistic") && !_settings.DomSadistic)
                    return false;
                if (FilterString.ToLower().Contains("@degrading") && !_settings.DomDegrading)
                    return false;
                if (FilterString.ToLower().Contains("@cfnm") && !_settings.DomCFNM)
                    return false;

                if (FilterString.ToLower().Contains("@dommelevel1") && _settings.DomLevel != 1)
                    return false;
                if (FilterString.ToLower().Contains("@dommelevel2") && _settings.DomLevel != 2)
                    return false;
                if (FilterString.ToLower().Contains("@dommelevel3") && _settings.DomLevel != 3)
                    return false;
                if (FilterString.ToLower().Contains("@dommelevel4") && _settings.DomLevel != 4)
                    return false;
                if (FilterString.ToLower().Contains("@dommelevel5") && _settings.DomLevel != 5)
                    return false;

                if (FilterString.ToLower().Contains("@selfyoung") && _settings.DomAge > _settings.SelfAgeMin - 1)
                    return false;
                if (FilterString.ToLower().Contains("@selfold") && _settings.DomAge < _settings.SelfAgeMax + 1)
                    return false;
                if (FilterString.ToLower().Contains("@selfyoung") || FilterString.ToLower().Contains("@selfold"))
                {
                    if (ssh.VideoTease == true || ssh.TeaseVideo == true)
                        return false;
                }
                if (FilterString.ToLower().Contains("@subyoung") & _settings.SubAge > _settings.SubAgeMin - 1)
                    return false;
                if (FilterString.ToLower().Contains("@subold") & _settings.SubAge < _settings.SubAgeMax + 1)
                    return false;

                if (FilterString.ToLower().Contains("@acup"))
                {
                    if (_settings.DomCup != "A" || ssh.JustShowedBlogImage)
                        return false;
                }
                if (FilterString.ToLower().Contains("@bcup"))
                {
                    if (_settings.DomCup != "B" || ssh.JustShowedBlogImage)
                        return false;
                }
                if (FilterString.ToLower().Contains("@ccup"))
                {
                    if (_settings.DomCup != "C" || ssh.JustShowedBlogImage)
                        return false;
                }
                if (FilterString.ToLower().Contains("@dcup"))
                {
                    if (_settings.DomCup != "D" || ssh.JustShowedBlogImage)
                        return false;
                }
                if (FilterString.ToLower().Contains("@ddcup"))
                {
                    if (_settings.DomCup != "DD" || ssh.JustShowedBlogImage)
                        return false;
                }
                if (FilterString.ToLower().Contains("@ddd+cup"))
                {
                    if (_settings.DomCup != "DDD+" || ssh.JustShowedBlogImage)
                        return false;
                }

                if (FilterString.ToLower().Contains("@cocksmall") & _settings.SubCockSize >= _settings.AvgCockMin)
                    return false;
                if (FilterString.ToLower().Contains("@cockaverage"))
                {
                    if (_settings.SubCockSize < _settings.AvgCockMin || _settings.SubCockSize > _settings.AvgCockMax)
                        return false;
                }

                if (FilterString.ToLower().Contains("@cocklarge") && _settings.SubCockSize <= _settings.AvgCockMax)
                    return false;

                if (FilterString.ToLower().Contains("@dombirthday"))
                {
                    if (_settings.DomBirthMonth != now.Month || _settings.DomBirthDay != now.Day)
                        return false;
                }

                if (FilterString.ToLower().Contains("@subbirthday"))
                {
                    if (_settings.SubBirthMonth!= now.Month || _settings.SubBirthDay != now.Day)
                        return false;
                }

                if (FilterString.ToLower().Contains("@valentinesday") && now.Day != 14 || now.Month != 2)
                    return false;
                if (FilterString.ToLower().Contains("@christmaseve") && now.Day != 24 || now.Month != 12)
                    return false;
                if (FilterString.ToLower().Contains("@christmasday") &&  now.Day != 25 || now.Month != 12)
                    return false;
                if (FilterString.ToLower().Contains("@newyearseve") &&  now.Day != 31 || now.Month != 12)
                    return false;
                if (FilterString.ToLower().Contains("@newyearsday") && now.Day != 1 || now.Month != 1)
                    return false;

                if (FilterString.ToLower().Contains("@firstround") && !ssh.FirstRound)
                    return false;
                if (FilterString.ToLower().Contains("@notfirstround") && ssh.FirstRound)
                    return false;

                if (FilterString.ToLower().Contains("@strokespeedmax") && ssh.StrokePace < _settings.MaxPace)
                    return false;
                if (FilterString.ToLower().Contains("@strokespeedmin") && ssh.StrokePace > _settings.MinPace)
                    return false;
                if (FilterString.ToLower().Contains("@strokefaster") || FilterString.ToLower().Contains("@strokefastest"))
                {
                    if (ssh.StrokePace >= _settings.MaxPace || ssh.WorshipMode)
                        return false;
                }
                if (FilterString.ToLower().Contains("@strokeslower") || FilterString.ToLower().Contains("@strokeslowest"))
                {
                    if (ssh.StrokePace <= _settings.MinPace || ssh.WorshipMode == true)
                        return false;
                }

                if (FilterString.ToLower().Contains("@alwaysallowsorgasm") & _settings.OrgasmAllow != "Always Allows")
                    return false;
                if (FilterString.ToLower().Contains("@oftenallowsorgasm") & _settings.OrgasmAllow != "Often Allows")
                    return false;
                if (FilterString.ToLower().Contains("@sometimesallowsorgasm") & _settings.OrgasmAllow != "Sometimes Allows")
                    return false;
                if (FilterString.ToLower().Contains("@rarelyallowsorgasm") & _settings.OrgasmAllow != "Rarely Allows")
                    return false;
                if (FilterString.ToLower().Contains("@neverallowsorgasm") & _settings.OrgasmAllow != "Never Allows")
                    return false;

                if (FilterString.ToLower().Contains("@alwaysruinsorgasm") & _settings.OrgasmRuin != "Always Ruins")
                    return false;
                if (FilterString.ToLower().Contains("@oftenruinsorgasm") & _settings.OrgasmRuin != "Often Ruins")
                    return false;
                if (FilterString.ToLower().Contains("@sometimesruinsorgasm") & _settings.OrgasmRuin != "Sometimes Ruins")
                    return false;
                if (FilterString.ToLower().Contains("@rarelyruinsorgasm") & _settings.OrgasmRuin != "Rarely Ruins")
                    return false;
                if (FilterString.ToLower().Contains("@neverruinsorgasm") & _settings.OrgasmRuin != "Never Ruins")
                    return false;

                if (FilterString.ToLower().Contains("@notalwaysallowsorgasm") & _settings.OrgasmAllow == "Always Allows")
                    return false;
                if (FilterString.ToLower().Contains("@notneverallowsorgasm") & _settings.OrgasmAllow == "Never Allows")
                    return false;
                if (FilterString.ToLower().Contains("@notalwaysruinsorgasm") & _settings.OrgasmRuin == "Always Ruins")
                    return false;
                if (FilterString.ToLower().Contains("@notneverruinsorgasm") & _settings.OrgasmRuin == "Never Ruins")
                    return false;

                if (FilterString.Contains("@LongEdge"))
                {
                    if (!ssh.LongEdge || !_settings.CBLongEdgeTaunts)
                        return false;
                }
                if (FilterString.Contains("@InterruptLongEdge"))
                {
                    if (!ssh.LongEdge || !_settings.CBLongEdgeInterrupts || ssh.TeaseTick < 1 || ssh.RiskyEdges)
                        return false;
                }

                if (FilterString.Contains("@1MinuteHold"))
                {
                    if (!ssh.SubHoldingEdge || ssh.HoldEdgeTime < 60 || ssh.HoldEdgeTime > 119)
                        return false;
                }
                if (FilterString.Contains("@2MinuteHold"))
                {
                    if (!ssh.SubHoldingEdge || ssh.HoldEdgeTime < 120 || ssh.HoldEdgeTime > 179)
                        return false;
                }
                if (FilterString.Contains("@3MinuteHold"))
                {
                    if (!ssh.SubHoldingEdge || ssh.HoldEdgeTime < 180 || ssh.HoldEdgeTime > 239)
                        return false;
                }
                if (FilterString.Contains("@4MinuteHold"))
                {
                    if (!ssh.SubHoldingEdge || ssh.HoldEdgeTime < 240 || ssh.HoldEdgeTime > 299)
                        return false;
                }
                if (FilterString.Contains("@5MinuteHold"))
                {
                    if (!ssh.SubHoldingEdge || ssh.HoldEdgeTime < 300 || ssh.HoldEdgeTime > 599)
                        return false;
                }
                if (FilterString.Contains("@10MinuteHold"))
                {
                    if (!ssh.SubHoldingEdge || ssh.HoldEdgeTime < 600 || ssh.HoldEdgeTime > 899)
                        return false;
                }
                if (FilterString.Contains("@15MinuteHold"))
                {
                    if (!ssh.SubHoldingEdge || ssh.HoldEdgeTime < 900 || ssh.HoldEdgeTime > 1799)
                        return false;
                }
                if (FilterString.Contains("@30MinuteHold"))
                {
                    if (!ssh.SubHoldingEdge || ssh.HoldEdgeTime < 1800 || ssh.HoldEdgeTime > 2699)
                        return false;
                }
                if (FilterString.Contains("@45MinuteHold"))
                {
                    if (!ssh.SubHoldingEdge || ssh.HoldEdgeTime < 2700 || ssh.HoldEdgeTime > 3599)
                        return false;
                }
                if (FilterString.Contains("@60MinuteHold"))
                {
                    if (!ssh.SubHoldingEdge || ssh.HoldEdgeTime < 3600)
                        return false;
                }

                if (FilterString.Contains("@CBTLevel1") && _settings.CBTSlider != 1)
                    return false;
                if (FilterString.Contains("@CBTLevel2") && _settings.CBTSlider != 2)
                    return false;
                if (FilterString.Contains("@CBTLevel3") && _settings.CBTSlider != 3)
                    return false;
                if (FilterString.Contains("@CBTLevel4") && _settings.CBTSlider != 4)
                    return false;
                if (FilterString.Contains("@CBTLevel5") && _settings.CBTSlider != 5)
                    return false;
                if (FilterString.Contains("@SubCircumcised") && !_settings.SubCircumcised)
                    return false;
                if (FilterString.Contains("@SubNotCircumcised") && _settings.SubCircumcised)
                    return false;
                if (FilterString.Contains("@SubPierced") && !_settings.SubPierced)
                    return false;
                if (FilterString.Contains("@SubNotPierced") && _settings.SubPierced)
                    return false;
                if (FilterString.Contains("@BeforeTease") && !ssh.BeforeTease)
                    return false;
                if (FilterString.Contains("@OrgasmDenied") && !ssh.OrgasmDenied)
                    return false;
                if (FilterString.Contains("@OrgasmAllowed") && !ssh.OrgasmAllowed)
                    return false;
                if (FilterString.Contains("@OrgasmRuined") && !ssh.OrgasmRuined)
                    return false;

                if (FilterString.Contains("@ApathyLevel1") && _settings.DomEmpathy != 1)
                    return false;
                if (FilterString.Contains("@ApathyLevel2") && _settings.DomEmpathy != 2)
                    return false;
                if (FilterString.Contains("@ApathyLevel3") && _settings.DomEmpathy != 3)
                    return false;
                if (FilterString.Contains("@ApathyLevel4") && _settings.DomEmpathy != 4)
                    return false;
                if (FilterString.Contains("@ApathyLevel5") && _settings.DomEmpathy != 5)
                    return false;
                if (FilterString.Contains("@InChastity") && !_settings.Chastity)
                    return false;
                if (FilterString.Contains("@NotInChastity") && _settings.Chastity)
                    return false;
                if (FilterString.Contains("@HasChastity") && !_settings.CBOwnChastity)
                    return false;
                if (FilterString.Contains("@DoesNotHaveChastity") && !_settings.CBOwnChastity)
                    return false;
                if (FilterString.Contains("@ChastityPA") && !_settings.ChastityPA)
                    return false;
                if (FilterString.Contains("@ChastitySpikes") && !_settings.ChastitySpikes)
                    return false;
                if (FilterString.Contains("@VitalSub") && !_settings.VitalSub)
                    return false;
                if (FilterString.Contains("@VitalSubAssignment"))
                {
                    if (!_settings.VitalSub || !_settings.VitalSubAssignments)
                        return false;
                }

                if (FilterString.Contains("@RuinTaunt"))
                {
                    if (!ssh.EdgeToRuin || ssh.EdgeToRuinSecret)
                        return false;
                }

                if (FilterString.Contains("@VideoHardcore"))
                {
                    if (!ssh.VideoTease || ssh.VideoType != "Hardcore")
                        return false;
                }
                if (FilterString.Contains("@VideoSoftcore"))
                {
                    if (!ssh.VideoTease || ssh.VideoType != "Softcore")
                        return false;
                }
                if (FilterString.Contains("@VideoLesbian"))
                {
                    if (!ssh.VideoTease || ssh.VideoType != "Lesbian")
                        return false;
                }
                if (FilterString.Contains("@VideoBlowjob"))
                {
                    if (!ssh.VideoTease || ssh.VideoType != "Blowjob")
                        return false;
                }
                if (FilterString.Contains("@VideoFemdom"))
                {
                    if (!ssh.VideoTease || ssh.VideoType != "Femdom")
                        return false;
                }
                if (FilterString.Contains("@VideoFemsub"))
                {
                    if (!ssh.VideoTease || ssh.VideoType != "Femsub")
                        return false;
                }
                if (FilterString.Contains("@VideoGeneral"))
                {
                    if (!ssh.VideoTease || ssh.VideoType != "General")
                        return false;
                }

                if (FilterString.Contains("@VideoHardcoreDomme"))
                {
                    if (!ssh.VideoTease || ssh.VideoType != "HardcoreD")
                        return false;
                }
                if (FilterString.Contains("@VideoSoftcoreDomme"))
                {
                    if (!ssh.VideoTease || ssh.VideoType != "SoftcoreD")
                        return false;
                }
                if (FilterString.Contains("@VideoLesbianDomme"))
                {
                    if (!ssh.VideoTease || ssh.VideoType != "LesbianD")
                        return false;
                }
                if (FilterString.Contains("@VideoBlowjobDomme"))
                {
                    if (!ssh.VideoTease || ssh.VideoType != "BlowjobD")
                        return false;
                }
                if (FilterString.Contains("@VideoFemdomDomme"))
                {
                    if (!ssh.VideoTease || ssh.VideoType != "FemdomD")
                        return false;
                }
                if (FilterString.Contains("@VideoFemsubDomme"))
                {
                    if (!ssh.VideoTease || ssh.VideoType != "FemsubD")
                        return false;
                }
                if (FilterString.Contains("@VideoGeneralDomme"))
                {
                    if (!ssh.VideoTease || ssh.VideoType != "GeneralD")
                        return false;
                }

                if (FilterString.Contains("@CockTorture") && !_settings.CBTCock)
                    return false;
                if (FilterString.Contains("@BallTorture") && !_settings.CBTBalls)
                    return false;
                if (FilterString.Contains("@BallTorture0") && ssh.CBTBallsCount != 0)
                    return false;
                if (FilterString.Contains("@BallTorture1") && ssh.CBTBallsCount != 1)
                    return false;
                if (FilterString.Contains("@BallTorture2") && ssh.CBTBallsCount != 2)
                    return false;
                if (FilterString.Contains("@BallTorture3") && ssh.CBTBallsCount != 3)
                    return false;
                if (FilterString.Contains("@BallTorture4+") && ssh.CBTBallsCount < 4)
                    return false;
                if (FilterString.Contains("@CockTorture0") && ssh.CBTCockCount != 0)
                    return false;
                if (FilterString.Contains("@CockTorture1") && ssh.CBTCockCount != 1)
                    return false;
                if (FilterString.Contains("@CockTorture2") && ssh.CBTCockCount != 2)
                    return false;
                if (FilterString.Contains("@CockTorture3") && ssh.CBTCockCount != 3)
                    return false;
                if (FilterString.Contains("@CockTorture4+") && ssh.CBTCockCount < 4)
                    return false;

                if (FilterString.Contains("@Stroking") || FilterString.Contains("@SubStroking"))
                {
                    if (!ssh.SubStroking && !_settings.Chastity)
                        return false;
                }

                if (FilterString.Contains("@NotStroking") || FilterString.Contains("@SubNotStroking"))
                {
                    if (ssh.SubStroking || _settings.Chastity)
                        return false;
                }

                if (FilterString.Contains("@Edging") || FilterString.Contains("@SubEdging"))
                {
                    if (!ssh.SubEdging)
                        return false;
                }

                if (FilterString.Contains("@NotEdging") || FilterString.Contains("@SubNotEdging"))
                {
                    if (ssh.SubEdging)
                        return false;
                }

                if (FilterString.Contains("@HoldingTheEdge") || FilterString.Contains("@SubHoldingTheEdge"))
                {
                    if (!ssh.SubHoldingEdge)
                        return false;
                }

                if (FilterString.Contains("@NotHoldingTheEdge") || FilterString.Contains("@SubNotHoldingTheEdge"))
                {
                    if (ssh.SubHoldingEdge)
                        return false;
                }

                if (FilterString.Contains("@Morning") && ssh.GeneralTime != "Morning")
                    return false;
                if (FilterString.Contains("@Afternoon") && ssh.GeneralTime != "Afternoon")
                    return false;
                if (FilterString.Contains("@Night") && ssh.GeneralTime != "Night")
                    return false;

                if (FilterString.Contains("@OrgasmRestricted") && !ssh.OrgasmRestricted)
                    return false;
                if (FilterString.Contains("@OrgasmNotRestricted") && ssh.OrgasmRestricted)
                    return false;
                if (FilterString.Contains("@SubWorshipping") && !ssh.WorshipMode)
                    return false;
                if (FilterString.Contains("@SubNotWorshipping") && ssh.WorshipMode)
                    return false;
                if (FilterString.Contains("@LongHold"))
                {
                    if (!ssh.LongHold || !ssh.SubHoldingEdge)
                        return false;
                }

                if (FilterString.Contains("@ExtremeHold"))
                {
                    if (!ssh.ExtremeHold || !ssh.SubHoldingEdge)
                        return false;
                }

                if (FilterString.Contains("@HoldTaunt"))
                {
                    if (!ssh.HoldTaunts)
                        return false;
                }

                if (FilterString.Contains("@LongTaunt"))
                {
                    if (!ssh.LongTaunts)
                        return false;
                }

                if (FilterString.Contains("@ExtremeTaunt"))
                {
                    if (!ssh.ExtremeTaunts)
                        return false;
                }

                if (FilterString.Contains("@AssWorship"))
                {
                    if (ssh.WorshipTarget != "Ass" || !ssh.WorshipMode)
                        return false;
                }

                if (FilterString.Contains("@BoobWorship"))
                {
                    if (ssh.WorshipTarget != "Boobs" || !ssh.WorshipMode)
                        return false;
                }

                if (FilterString.Contains("@PussyWorship"))
                {
                    if (ssh.WorshipTarget != "Pussy" || !ssh.WorshipMode)
                        return false;
                }

                if (FilterString.Contains("@Contact1"))
                {
                    if (!ssh.GlitterTease || !ssh.Group.Contains("1"))
                        return false;
                }

                if (FilterString.Contains("@Contact2"))
                {
                    if (!ssh.GlitterTease || !ssh.Group.Contains("2"))
                        return false;
                }

                if (FilterString.Contains("@Contact3"))
                {
                    if (!ssh.GlitterTease || !ssh.Group.Contains("3"))
                        return false;
                }
                if (FilterString.Contains("@Contact("))
                {
                    if (!ssh.GlitterTease)
                        return false;

                    string getContacts = Common.GetParentheses(FilterString, "@Contact(");
                    getContacts = Common.FixCommas(getContacts);
                    string[] array5 = getContacts.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string contact in array5)
                    {
                        if (!ssh.Group.Contains(contact))
                            return false;
                    }
                }
                if (FilterString.Contains("@Info"))
                    return false;
                // ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
                // Single word filters - End
                // ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
                return true;
            }
            catch (Exception ex)
            {
                // ▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                // All Errors
                // ▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                _log.WriteError(string.Format("Exception occured while checking line \"{0}\".", OrgFilterString), ex, "GetFilter(String, Boolean)");
                return false;
            }
        }
    }
}
