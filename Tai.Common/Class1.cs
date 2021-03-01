using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Tai.Common
{
    public enum PoundOptions
    {
        None = 0,
        CommaSepList = 1
    }

    public interface IVars
    {
        string GetVariable(string varName);
    }
#if NOFLAG
    public class XSettings
    {
        public bool CBCockToClit { get; internal set; }
        public bool CBBallsToPussy { get; internal set; }
        public string DomName { get; internal set; }
        public string SubHonorific { get; internal set; }
        public string G1Honorific { get; internal set; }
        public string G2Honorific { get; internal set; }
        public string G3Honorific { get; internal set; }
        public int DomAge { get; internal set; }
        public int DomLevel { get; internal set; }
        public int NBEmpathy { get; internal set; }
        public int DomHairLength { get; internal set; }
        public string DomHairColor { get; internal set; }
        public string TBDomEyeColor { get; internal set; }
        public string DomCup { get; internal set; }
        public short NBDomMoodMin { get; internal set; }
        public short NBDomMoodMax { get; internal set; }
        public short NBAvgCockMin { get; internal set; }
        public short NBAvgCockMax { get; internal set; }
        public short NBSelfAgeMin { get; internal set; }
        public short NBSelfAgeMax { get; internal set; }
        public short NBSubAgeMin { get; internal set; }
        public short NBSubAgeMax { get; internal set; }
        public string AllowOrgasmRate { get; internal set; }
        public string RuinOrgasmRate { get; internal set; }
        public short SubAge { get; internal set; }
        public short NBBirthdayMonth { get; internal set; }
        public short NBBirthdayDay { get; internal set; }
        public short NBDomBirthdayMonth { get; internal set; }
        public short NBDomBirthdayDay { get; internal set; }
        public string TBSubHairColor { get; internal set; }
        public string TBSubEyeColor { get; internal set; }
        public short CockSize { get; internal set; }
        public int NBWritingTaskMin { get; internal set; }
        public int NBWritingTaskMax { get; internal set; }
        public string Glitter1 { get; internal set; }
        public string Glitter2 { get; internal set; }
        public string Glitter3 { get; internal set; }
        public bool OrgasmsLocked { get; internal set; }
        public DateTime OrgasmLockDate { get; internal set; }
        public string RandomHonorific { get; internal set; }
        public string GlitterSN { get; internal set; }
        public string GlitterNC1Color { get; internal set; }
        public string GlitterNC2Color { get; internal set; }
        public string GlitterNC3Color { get; internal set; }
        public string DomColor { get; internal set; }
        public string DomFont { get; internal set; }
        public int DomFontSize { get; internal set; }
        public int VVolume { get; internal set; }
        public int VRate { get; internal set; }
        public bool CBSlideshowSubDir { get; internal set; }
        public bool CBNewSlideshow { get; internal set; }
        public bool CBSlideshowRandom { get; internal set; }
        public int NextImageChance { get; internal set; }
        public string SubGreeting { get; internal set; }
        public string SubYes { get; internal set; }
        public string SubNo { get; internal set; }
        public string SubSorry { get; internal set; }
        public string DomImageDirRand { get; internal set; }
        public bool CBRandomDomme { get; internal set; }

        internal string GetDefaultFolder(string v)
        {
            throw new NotImplementedException();
        }

        internal string GetCurrentFolder(string v)
        {
            throw new NotImplementedException();
        }
    }
    public class Class1
    {
        private readonly IVars _vars;
        private readonly XSettings _settings;

        public Class1(IVars vars, XSettings settings)
        {
            _vars = vars;
            _settings = settings;
        }
        public string ConvertSeconds(int Seconds)
        {
            string RetVal;

            int SecondsDifference = Seconds;
            var HMS = TimeSpan.FromSeconds(SecondsDifference);
            var H = HMS.Hours.ToString();
            var M = HMS.Minutes.ToString();
            var S = HMS.Seconds.ToString();

            if (HMS.Hours == 1)
                H = "1 hour";
            else
                H = H + " hours";

            if (HMS.Minutes == 1)
                M = "1 minute";
            else
            {
                int t = HMS.Minutes;
                if (t >= 5)
                    t = 5 * (int)Math.Round(t / (double)5); // looks dubious ??
                M = t + " minutes";
            }

            if (HMS.Minutes > 4 | HMS.Hours > 0 | HMS.Seconds == 0)
                S = "";
            else if (HMS.Seconds == 1)
                S = "1 second";
            else
                S = S + " seconds";

            RetVal = "";

            if (HMS.Hours > 0)
            {
                RetVal = RetVal + H;
                if (HMS.Minutes > 0)
                    RetVal = RetVal + " and ";
            }

            if (HMS.Minutes > 0)
            {
                RetVal = RetVal + M;
                if (HMS.Seconds > 0 & HMS.Hours < 1 & HMS.Minutes < 4)
                    RetVal = RetVal + " and ";
            }

            RetVal = RetVal + S;

            return RetVal;
        }
        public int GetNthIndex(string searchString, char charToFind, int startIndex, int n)
        {
            var charIndexPair = searchString.Select((c, i) => new { Character = c, Index = i })
              .Where(x => x.Character == charToFind & x.Index > startIndex)
              .ElementAtOrDefault(n - 1);
            return charIndexPair != null ? charIndexPair.Index : -1;
        }

        public string GetParentheses(string ParenCheck, string CommandCheck, int Iterations = 1)
        {
            string ParenFlag = ParenCheck;
            int ParenStart = ParenFlag.IndexOf(CommandCheck) + CommandCheck.Length;
            // githib patch Dim ParenType As String

            char ParenType = '\0';

            // #### CHECK ALL GETPAREN!
            // If CommandCheck.Substring(CommandCheck.Length - 1, 1) = "(" Then ParenType = ")"
            // If CommandCheck.Substring(CommandCheck.Length - 1, 1) = "[" Then ParenType = "]"

            if (CommandCheck.Substring(CommandCheck.Length - 1, 1) == "(")
                ParenType = ')';
            if (CommandCheck.Substring(CommandCheck.Length - 1, 1) == "[")
                ParenType = ']';



            // ParenFlag = ParenFlag.Substring(ParenStart, ParenFlag.Length - ParenStart)

            // Dim ParenEnd As Integer = ParenFlag.IndexOf(ParenType, ParenStart)
            int ParenEnd = GetNthIndex(ParenFlag, ParenType, ParenStart, Iterations);

            Debug.Print("ParenEnd = " + ParenEnd);

            if (ParenEnd == -1)
                ParenEnd = ParenFlag.Length;
            ParenFlag = ParenFlag.Substring(ParenStart, ParenEnd - ParenStart);

            // ParenFlag = ParenFlag.Split(")")(0)
            // ParenFlag = ParenFlag.Split(ParenType)(0)
            // ParenFlag = ParenFlag.Replace(ParenType, "")
            // ParenFlag = ParenFlag.Substring(0, ParenFlag.Length - 1)
            Debug.Print("ParenFlag = " + ParenFlag);

            return ParenFlag;
        }

        public string GetVariable(string varName)
        {
            /*
             if (VariableExists(varName))
                 return TxtReadLine(Path.Combine(VariableFolder, varName));
             else
                 return 0;
            */
            return _vars.GetVariable(varName);
        }

        public string FixCommas(string CommaString)
        {
            CommaString = CommaString.Replace(", ", ",");
            CommaString = CommaString.Replace(" ,", ",");

            return CommaString;
        }


        public string SysKeywordClean(string StringClean)
        {
            if (StringClean.Contains("#Var["))
            {
                string[] VarArray = StringClean.Split(']');
                for (int i = 0; i < VarArray.Length; i++)
                {
                    if (VarArray[i].Contains("#Var["))
                        StringClean = StringClean.Replace("#Var[" + GetParentheses(VarArray[i] + "]", "#Var[") + "]", GetVariable(GetParentheses(VarArray[i] + "]", "#Var[")));
                }
            }

            if (StringClean.Contains("@RT(") | StringClean.Contains("@RandomText("))
            {
                string[] replace = new[] { "@RT(", "@RandomText(" };
                string[] RandArray = StringClean.Split('@');
                for (var a = 0; a < replace.Length; a++)
                {
                    for (int i = 0; i < RandArray.Length; i++)
                    {
                        RandArray[i] = "@" + RandArray[i];
                        if (RandArray[i].Contains(replace[a]))
                        {
                            var tempString = GetParentheses(RandArray[i], replace[a], RandArray[i].Split(')').Length - 1);
                            var startString = tempString;
                            tempString = tempString.Replace(",,", "###INSERT-COMMA###");
                            tempString = FixCommas(tempString);
                            string[] selectArray = tempString.Split(',');
                            for (int n = 0; n <= selectArray.Count() - 1; n++)
                                selectArray[n] = selectArray[n].Replace("###INSERT-COMMA###", ",");
                            tempString = selectArray[ssh.randomizer.Next(0, selectArray.Count())];
                            StringClean = StringClean.Replace(replace[a] + startString + ")", tempString);
                        }
                    }
                }
            }

            if (_settings.CBCockToClit)
            {
                StringClean = StringClean.Replace("#Cock", "#CockToClit");
                StringClean = StringClean.Replace("stroking", "#StrokingToRubbing");
            }

            if (_settings.CBBallsToPussy)
            {
                StringClean = StringClean.Replace("those #Balls", "that #Balls");
                StringClean = StringClean.Replace("#Balls", "#BallsToPussy");
            }

            StringClean = StringClean.Replace("#SubName", subName.Text);

            if (!ssh.SlideshowMain == null)
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

            StringClean = StringClean.Replace("#DomApathy", _settings.NBEmpathy.ToString());

            StringClean = StringClean.Replace("#DomHairLength", _settings.DomHairLength.ToString());

            StringClean = StringClean.Replace("#DomHair", _settings.DomHairColor);

            StringClean = StringClean.Replace("#DomEyes", _settings.TBDomEyeColor);

            StringClean = StringClean.Replace("#DomCup", _settings.DomCup);

            StringClean = StringClean.Replace("#DomMoodMin", _settings.NBDomMoodMin.ToString());

            StringClean = StringClean.Replace("#DomMoodMax", _settings.NBDomMoodMax.ToString());

            StringClean = StringClean.Replace("#DomMood", ssh.DommeMood);

            StringClean = StringClean.Replace("#DomAvgCockMin", _settings.NBAvgCockMin.ToString());

            StringClean = StringClean.Replace("#DomAvgCockMax", _settings.NBAvgCockMax.ToString());

            StringClean = StringClean.Replace("#DomSmallCockMax", (_settings.NBAvgCockMin - 1).ToString());

            StringClean = StringClean.Replace("#DomLargeCockMin", (_settings.NBAvgCockMax + 1).ToString());

            StringClean = StringClean.Replace("#DomSelfAgeMin", _settings.NBSelfAgeMin.ToString());

            StringClean = StringClean.Replace("#DomSelfAgeMax", _settings.NBSelfAgeMax.ToString());

            StringClean = StringClean.Replace("#DomSubAgeMin", _settings.NBSubAgeMin.ToString());

            StringClean = StringClean.Replace("#DomSubAgeMax", _settings.NBSubAgeMax.ToString());

            StringClean = StringClean.Replace("#DomOrgasmRate", _settings.AllowOrgasmRate);

            StringClean = StringClean.Replace("#DomRuinRate", _settings.RuinOrgasmRate);

            StringClean = StringClean.Replace("#SubAge", _settings.SubAge.ToString());

            StringClean = StringClean.Replace("#SubBirthdayMonth", _settings.NBBirthdayMonth.ToString());

            StringClean = StringClean.Replace("#SubBirthdayDay", _settings.NBBirthdayDay.ToString());

            StringClean = StringClean.Replace("#DomBirthdayMonth", _settings.NBDomBirthdayMonth.ToString());

            StringClean = StringClean.Replace("#DomBirthdayDay", _settings.NBDomBirthdayDay.ToString());

            StringClean = StringClean.Replace("#SubHair", _settings.TBSubHairColor);

            StringClean = StringClean.Replace("#SubEyes", _settings.TBSubEyeColor);

            StringClean = StringClean.Replace("#SubCockSize", _settings.CockSize.ToString());

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

            StringClean = StringClean.Replace("#CBTCockCount", ssh.CBTCockCount);
            StringClean = StringClean.Replace("#CBTBallsCount", ssh.CBTBallsCount);

            if (_settings.OrgasmsLocked)
                StringClean = StringClean.Replace("#OrgasmLockDate", _settings.OrgasmLockDate.Date.ToString());
            else
                StringClean = StringClean.Replace("#OrgasmLockDate", "later");

            if (StringClean.Contains("#RandomRound100("))
            {
                string RandomFlag = GetParentheses(StringClean, "#RandomRound100(");
                string OriginalFlag = RandomFlag;
                RandomFlag = FixCommas(RandomFlag);
                int RandInt;
                string[] FlagArray = RandomFlag.Split(',');

                RandInt = ssh.randomizer.Next(Conversion.Val(FlagArray[0]), Conversion.Val(FlagArray[1]) + 1);
                if (RandInt >= 100)
                    RandInt = 100 * Math.Round(RandInt / (double)100);
                StringClean = StringClean.Replace("#RandomRound100(" + OriginalFlag + ")", RandInt);
            }

            if (StringClean.Contains("#RandomRound10("))
            {
                string RandomFlag = GetParentheses(StringClean, "#RandomRound10(");
                string OriginalFlag = RandomFlag;
                RandomFlag = FixCommas(RandomFlag);
                int RandInt;
                string[] FlagArray = RandomFlag.Split(',');

                RandInt = ssh.randomizer.Next(Conversion.Val(FlagArray[0]), Conversion.Val(FlagArray[1]) + 1);
                if (RandInt >= 10)
                    RandInt = 10 * Math.Round(RandInt / (double)10);
                StringClean = StringClean.Replace("#RandomRound10(" + OriginalFlag + ")", RandInt);
            }


            if (StringClean.Contains("#RandomRound5("))
            {
                string RandomFlag = GetParentheses(StringClean, "#RandomRound5(");
                string OriginalFlag = RandomFlag;
                RandomFlag = FixCommas(RandomFlag);
                int RandInt;
                string[] FlagArray = RandomFlag.Split(',');

                RandInt = ssh.randomizer.Next(Conversion.Val(FlagArray[0]), Conversion.Val(FlagArray[1]) + 1);
                if (RandInt >= 5)
                    RandInt = 5 * Math.Round(RandInt / (double)5);
                StringClean = StringClean.Replace("#RandomRound5(" + OriginalFlag + ")", RandInt);
            }


            if (StringClean.Contains("#Random("))
            {
                string[] randomArray = StringClean.Split(')');

                for (int i = 0; i <= randomArray.Count() - 1; i++)
                {
                    if (randomArray[i].Contains("Random("))
                    {
                        randomArray[i] = randomArray[i] + ")";
                        string RandomFlag = GetParentheses(StringClean, "#Random(");
                        string OriginalFlag = RandomFlag;
                        RandomFlag = FixCommas(RandomFlag);
                        int RandInt;
                        string[] FlagArray = RandomFlag.Split(',');

                        RandInt = ssh.randomizer.Next(Conversion.Val(FlagArray[0]), Conversion.Val(FlagArray[1]) + 1);
                        StringClean = StringClean.Replace("#Random(" + OriginalFlag + ")", RandInt);
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
                        string DateFlag = GetParentheses(StringClean, "DateDifference(");
                        string OriginalFlag = DateFlag;
                        DateFlag = FixCommas(DateFlag);
                        string[] DateArray = DateFlag.Split(',');

                        int DDiff;

                        if (Strings.UCase(DateArray[1]).Contains("SECOND"))
                            DDiff = DateDiff(DateInterval.Second, GetDate(DateArray[0]), DateTime.Now);
                        if (Strings.UCase(DateArray[1]).Contains("MINUTE"))
                            DDiff = DateDiff(DateInterval.Minute, GetDate(DateArray[0]), DateTime.Now);
                        if (Strings.UCase(DateArray[1]).Contains("HOUR"))
                            DDiff = DateDiff(DateInterval.Hour, GetDate(DateArray[0]), DateTime.Now);
                        if (Strings.UCase(DateArray[1]).Contains("DAY"))
                            DDiff = DateDiff(DateInterval.Day, GetDate(DateArray[0]), DateTime.Now);
                        if (Strings.UCase(DateArray[1]).Contains("WEEK"))
                            DDiff = DateDiff(DateInterval.Day, GetDate(DateArray[0]), DateTime.Now) / (double)7;
                        if (Strings.UCase(DateArray[1]).Contains("MONTH"))
                            DDiff = DateDiff(DateInterval.Month, GetDate(DateArray[0]), DateTime.Now);
                        if (Strings.UCase(DateArray[1]).Contains("YEAR"))
                            DDiff = DateDiff(DateInterval.Year, GetDate(DateArray[0]), DateTime.Now);

                        StringClean = StringClean.Replace("#DateDifference(" + OriginalFlag + ")", DDiff);
                    }
                }
            }



            int PetNameVal = ssh.randomizer.Next(1, 5);

            if (PetNameVal == 1)
                ssh.PetName = _settings.petnameBox3.Text;
            if (PetNameVal == 2)
                ssh.PetName = _settings.petnameBox4.Text;
            if (PetNameVal == 3)
                ssh.PetName = _settings.petnameBox5.Text;
            if (PetNameVal == 4)
                ssh.PetName = _settings.petnameBox6.Text;

            if (ssh.DommeMood < _settings.NBDomMoodMin)
            {
                PetNameVal = ssh.randomizer.Next(1, 3);
                if (PetNameVal == 1)
                    ssh.PetName = _settings.petnameBox7.Text;
                if (PetNameVal == 2)
                    ssh.PetName = _settings.petnameBox8.Text;
            }


            if (ssh.DommeMood > _settings.NBDomMoodMax)
            {
                PetNameVal = ssh.randomizer.Next(1, 3);
                if (PetNameVal == 1)
                    ssh.PetName = _settings.petnameBox1.Text;
                if (PetNameVal == 2)
                    ssh.PetName = _settings.petnameBox2.Text;
            }


            StringClean = StringClean.Replace("#PetName", ssh.PetName);

            // If Hour(Date.Now) < 11 Then PreCleanString = PreCleanString.Replace("#GeneralTime", "this morning")
            if (DateTime.Hour(DateTime.Now) > 3 & DateTime.Hour(DateTime.Now) < 12)
                StringClean = StringClean.Replace("#GreetSub", "#GoodMorningSub");
            // If Hour(Date.Now) > 10 And Hour(Date.Now) < 18 Then PreCleanString = PreCleanString.Replace("#GeneralTime", "today")
            if (DateTime.Hour(DateTime.Now) > 11 & DateTime.Hour(DateTime.Now) < 18)
                StringClean = StringClean.Replace("#GreetSub", "#GoodAfternoonSub");
            // If Hour(Date.Now) > 17 Then PreCleanString = PreCleanString.Replace("#GeneralTime", "tonight")
            if (DateTime.Hour(DateTime.Now) > 17 | DateTime.Hour(DateTime.Now) < 4)
                StringClean = StringClean.Replace("#GreetSub", "#GoodEveningSub");


            if (DateTime.Hour(DateTime.Now) < 4)
                StringClean = StringClean.Replace("#GeneralTime", "tonight");
            if (DateTime.Hour(DateTime.Now) > 3 & DateTime.Hour(DateTime.Now) < 11)
                StringClean = StringClean.Replace("#GeneralTime", "this morning");
            if (DateTime.Hour(DateTime.Now) > 10 & DateTime.Hour(DateTime.Now) < 18)
                StringClean = StringClean.Replace("#GeneralTime", "today");
            if (DateTime.Hour(DateTime.Now) > 17)
                StringClean = StringClean.Replace("#GeneralTime", "tonight");

            if (ssh.AssImage == true)
                StringClean = StringClean.Replace("#TnAFastSlidesResult", "#BBnB_Ass");
            if (ssh.BoobImage == true)
                StringClean = StringClean.Replace("#TnAFastSlidesResult", "#BBnB_Boobs");

            if (StringClean.Contains("#RANDNumberLow"))
            {
                // ### Number between 3-5 , 5-25
                ssh.TempVal = ssh.randomizer.Next(1, 6) * _settings.domlevelNumBox.Value;
                if (ssh.TempVal > 10)
                    ssh.TempVal = 5 * Math.Round(ssh.TempVal / (double)5);
                if (ssh.TempVal < 3)
                    ssh.TempVal = 3;
                StringClean = StringClean.Replace("#RNDNumberLow", ssh.TempVal);
            }


            if (StringClean.Contains("#RANDNumberHigh"))
            {
                // ### Number between 5-25 , 25-100
                ssh.TempVal = ssh.randomizer.Next(5, 21) * _settings.domlevelNumBox.Value;
                if (ssh.TempVal > 10)
                    ssh.TempVal = 5 * Math.Round(ssh.TempVal / (double)5);
                StringClean = StringClean.Replace("#RNDNumberHigh", ssh.TempVal);
            }


            if (StringClean.Contains("#RANDNumber"))
            {
                // ### Number between 3-10 , 5-50
                ssh.TempVal = ssh.randomizer.Next(1, 11) * _settings.domlevelNumBox.Value;
                if (ssh.TempVal > 10)
                    ssh.TempVal = 5 * Math.Round(ssh.TempVal / (double)5);
                if (ssh.TempVal < 3)
                    ssh.TempVal = 3;
                StringClean = StringClean.Replace("#RNDNumber", ssh.TempVal);
            }



            StringClean = StringClean.Replace("#RP_ChosenCase", FrmCardList.RiskyPickNumber);
            StringClean = StringClean.Replace("#RP_RespondCase", FrmCardList.RiskyResponse);
            // StringClean = StringClean.Replace("#RP_CaseNumber", FrmCardList.RiskyCase)
            if (FrmCardList.RiskyPickCount == 0)
                StringClean = StringClean.Replace("#RP_CaseNumber", FrmCardList.LBLPick1.Text);
            if (FrmCardList.RiskyPickCount == 1)
                StringClean = StringClean.Replace("#RP_CaseNumber", FrmCardList.LBLPick2.Text);
            if (FrmCardList.RiskyPickCount == 2)
                StringClean = StringClean.Replace("#RP_CaseNumber", FrmCardList.LBLPick3.Text);
            if (FrmCardList.RiskyPickCount == 3)
                StringClean = StringClean.Replace("#RP_CaseNumber", FrmCardList.LBLPick4.Text);
            if (FrmCardList.RiskyPickCount == 4)
                StringClean = StringClean.Replace("#RP_CaseNumber", FrmCardList.LBLPick5.Text);
            if (FrmCardList.RiskyPickCount > 4)
                StringClean = StringClean.Replace("#RP_CaseNumber", FrmCardList.LBLPick6.Text);
            StringClean = StringClean.Replace("#RP_EdgeOffer", FrmCardList.RiskyEdgeOffer);
            StringClean = StringClean.Replace("#RP_TokenOffer", FrmCardList.RiskyTokenOffer);
            StringClean = StringClean.Replace("#RP_EdgesOwed", FrmCardList.EdgesOwed);
            StringClean = StringClean.Replace("#RP_TokensPaid", FrmCardList.TokensPaid);

            StringClean = StringClean.Replace("#BronzeTokens", ssh.BronzeTokens);
            StringClean = StringClean.Replace("#SilverTokens", ssh.SilverTokens);
            StringClean = StringClean.Replace("#GoldTokens", ssh.GoldTokens);

            StringClean = StringClean.Replace("#SessionEdges", ssh.SessionEdges);
            StringClean = StringClean.Replace("#SessionCBTCock", ssh.CBTCockCount);
            StringClean = StringClean.Replace("#SessionCBTBalls", ssh.CBTBallsCount);

            // StringClean = StringClean.Replace("#Sys_SubLeftEarly", _settings.Sys_SubLeftEarly)
            // StringClean = StringClean.Replace("#Sys_SubLeftEarlyTotal", _settings.Sys_SubLeftEarlyTotal)

            StringClean = StringClean.Replace("#SlideshowCount", ssh.CustomSlideshow.Count - 1);
            StringClean = StringClean.Replace("#SlideshowCurrent", ssh.CustomSlideshow.Index);
            StringClean = StringClean.Replace("#SlideshowRemaining", (ssh.CustomSlideshow.Count - 1) - ssh.CustomSlideshow.Index);

            StringClean = StringClean.Replace("#CurrentTime", Strings.Format(DateTime.Now, "h:mm"));
            StringClean = StringClean.Replace("#CurrentDay", Strings.Format(DateTime.Now, "dddd"));
            StringClean = StringClean.Replace("#CurrentMonth", Strings.Format(DateTime.Now, "MMMMM"));
            StringClean = StringClean.Replace("#CurrentYear", Strings.Format(DateTime.Now, "yyyy"));
            StringClean = StringClean.Replace("#CurrentDate", Strings.FormatDateTime(DateTime.Now, DateFormat.ShortDate));
            // StringClean = StringClean.Replace("#CurrentDate", Format(Now, "MM/dd/yyyy"))

            if (StringClean.Contains("#RandomSlideshowCategory"))
            {
                List<string> RanCat = new List<string>();

                if (_settings.CBIHardcore.Checked == true)
                    RanCat.Add("Hardcore");
                if (_settings.CBISoftcore.Checked == true)
                    RanCat.Add("Softcore");
                if (_settings.CBILesbian.Checked == true)
                    RanCat.Add("Lesbian");
                if (_settings.CBIBlowjob.Checked == true)
                    RanCat.Add("Blowjob");
                if (_settings.CBIFemdom.Checked == true)
                    RanCat.Add("Femdom");
                if (_settings.CBILezdom.Checked == true)
                    RanCat.Add("Lezdom");
                if (_settings.CBIHentai.Checked == true)
                    RanCat.Add("Hentai");
                if (_settings.CBIGay.Checked == true)
                    RanCat.Add("Gay");
                if (_settings.CBIMaledom.Checked == true)
                    RanCat.Add("Maledom");
                if (_settings.CBICaptions.Checked == true)
                    RanCat.Add("Captions");
                if (_settings.CBIGeneral.Checked == true)
                    RanCat.Add("General");

                if (RanCat.Count < 1)
                    ChatAddSystemMessage("ERROR: #RandomSlideshowCategory called but no local images have been set.");
                else
                    StringClean = StringClean.Replace("#RandomSlideshowCategory", RanCat[ssh.randomizer.Next(0, RanCat.Count)]);
            }


            // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            // ImageCount
            // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            if (StringClean.Contains("#LocalImageCount"))
            {
                Dictionary<ImageGenre, ImageDataContainer> temp = GetImageData();
                int counter = 0;

                foreach (ImageGenre genre in temp.Keys)
                    counter += temp[genre].CountImages(ImageSourceType.Local);

                StringClean = StringClean.Replace("#LocalImageCount", counter);
            }

            if (StringClean.Contains("#BlogImageCount"))
                StringClean = StringClean.Replace("#BlogImageCount", GetImageData(ImageGenre.Blog).CountImages());

            if (StringClean.Contains("#ButtImageCount"))
                StringClean = StringClean.Replace("#ButtImageCount", GetImageData(ImageGenre.Butt).CountImages());

            if (StringClean.Contains("#ButtsImageCount"))
                StringClean = StringClean.Replace("#ButtsImageCount", GetImageData(ImageGenre.Butt).CountImages());

            if (StringClean.Contains("#BoobImageCount"))
                StringClean = StringClean.Replace("#BoobImageCount", GetImageData(ImageGenre.Boobs).CountImages());

            if (StringClean.Contains("#BoobsImageCount"))
                StringClean = StringClean.Replace("#BoobsImageCount", GetImageData(ImageGenre.Boobs).CountImages());

            if (StringClean.Contains("#HardcoreImageCount"))
                StringClean = StringClean.Replace("#HardcoreImageCount", GetImageData(ImageGenre.Hardcore).CountImages());

            if (StringClean.Contains("#HardcoreImageCount"))
                StringClean = StringClean.Replace("#HardcoreImageCount", GetImageData(ImageGenre.Hardcore).CountImages());

            if (StringClean.Contains("#SoftcoreImageCount"))
                StringClean = StringClean.Replace("#SoftcoreImageCount", GetImageData(ImageGenre.Softcore).CountImages());

            if (StringClean.Contains("#LesbianImageCount"))
                StringClean = StringClean.Replace("#LesbianImageCount", GetImageData(ImageGenre.Lesbian).CountImages());

            if (StringClean.Contains("#BlowjobImageCount"))
                StringClean = StringClean.Replace("#BlowjobImageCount", GetImageData(ImageGenre.Blowjob).CountImages());

            if (StringClean.Contains("#FemdomImageCount"))
                StringClean = StringClean.Replace("#FemdomImageCount", GetImageData(ImageGenre.Femdom).CountImages());

            if (StringClean.Contains("#LezdomImageCount"))
                StringClean = StringClean.Replace("#LezdomImageCount", GetImageData(ImageGenre.Lezdom).CountImages());

            if (StringClean.Contains("#HentaiImageCount"))
                StringClean = StringClean.Replace("#HentaiImageCount", GetImageData(ImageGenre.Hentai).CountImages());

            if (StringClean.Contains("#GayImageCount"))
                StringClean = StringClean.Replace("#GayImageCount", GetImageData(ImageGenre.Gay).CountImages());

            if (StringClean.Contains("#MaledomImageCount"))
                StringClean = StringClean.Replace("#MaledomImageCount", GetImageData(ImageGenre.Maledom).CountImages());

            if (StringClean.Contains("#CaptionsImageCount"))
                StringClean = StringClean.Replace("#CaptionsImageCount", GetImageData(ImageGenre.Captions).CountImages());

            if (StringClean.Contains("#GeneralImageCount"))
                StringClean = StringClean.Replace("#GeneralImageCount", GetImageData(ImageGenre.General).CountImages());

            if (StringClean.Contains("#LikedImageCount"))
                StringClean = StringClean.Replace("#LikedImageCount", GetImageData(ImageGenre.Liked).CountImages());

            if (StringClean.Contains("#DislikedImageCount"))
                StringClean = StringClean.Replace("#DislikedImageCount", GetImageData(ImageGenre.Disliked).CountImages());
            // ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
            // ImageCount - End
            // ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
            if (StringClean.Contains("#EdgeHold"))
            {
                int i = _settings.NBHoldTheEdgeMin.Value;
                if (_settings.LBLMinHold.Text == "minutes")
                    i *= 60;

                int x = _settings.NBHoldTheEdgeMax.Value;
                if (_settings.LBLMaxHold.Text == "minutes")
                    x *= 60;

                int t = ssh.randomizer.Next(i, x + 1);
                if (t >= 5)
                    t = 5 * Math.Round(t / (double)5);
                string TConvert = ConvertSeconds(t);
                StringClean = StringClean.Replace("#EdgeHold", TConvert);
            }

            if (StringClean.Contains("#LongHold"))
            {
                int i = _settings.NBLongHoldMin.Value;
                int x = _settings.NBLongHoldMax.Value;
                int t = ssh.randomizer.Next(i, x + 1);
                t *= 60;
                if (t >= 5)
                    t = 5 * Math.Round(t / (double)5);
                string TConvert = ConvertSeconds(t);
                StringClean = StringClean.Replace("#LongHold", TConvert);
            }

            if (StringClean.Contains("#ExtremeHold"))
            {
                int i = _settings.NBExtremeHoldMin.Value;
                int x = _settings.NBExtremeHoldMax.Value;
                int t = ssh.randomizer.Next(i, x + 1);
                t *= 60;
                if (t >= 5)
                    t = 5 * Math.Round(t / (double)5);
                string TConvert = ConvertSeconds(t);
                StringClean = StringClean.Replace("#ExtremeHold", TConvert);
            }

            StringClean = StringClean.Replace("#CurrentImage", ssh.ImageLocation);

            int @int;

            if (StringClean.Contains("#TaskEdges"))
            {
                @int = ssh.randomizer.Next(_settings.NBTaskEdgesMin.Value, _settings.NBTaskEdgesMax.Value + 1);
                if (@int > 5)
                    @int = 5 * Math.Round(@int / (double)5);
                StringClean = StringClean.Replace("#TaskEdges", @int);
            }

            if (StringClean.Contains("#TaskStrokes"))
            {
                @int = ssh.randomizer.Next(_settings.NBTaskStrokesMin.Value, _settings.NBTaskStrokesMax.Value + 1);
                if (@int > 10)
                    @int = 10 * Math.Round(@int / (double)10);
                StringClean = StringClean.Replace("#TaskStrokes", @int);
            }

            if (StringClean.Contains("#TaskHours"))
            {
                @int = ssh.randomizer.Next(1, _settings.domlevelNumBox.Value + 1) + _settings.domlevelNumBox.Value;
                StringClean = StringClean.Replace("#TaskHours", @int);
            }

            if (StringClean.Contains("#TaskMinutes"))
            {
                @int = ssh.randomizer.Next(5, 13) * _settings.domlevelNumBox.Value;
                StringClean = StringClean.Replace("#TaskMinutes", @int);
            }

            if (StringClean.Contains("#TaskSeconds"))
            {
                @int = ssh.randomizer.Next(10, 30) * _settings.domlevelNumBox.Value * ssh.randomizer.Next(1, _settings.domlevelNumBox.Value + 1);
                StringClean = StringClean.Replace("#TaskSeconds", @int);
            }

            if (StringClean.Contains("#TaskAmountLarge"))
            {
                @int = (ssh.randomizer.Next(15, 26) * _settings.domlevelNumBox.Value) * 2;
                if (@int > 5)
                    @int = 5 * Math.Round(@int / (double)5);
                StringClean = StringClean.Replace("#TaskAmountLarge", @int);
            }

            if (StringClean.Contains("#TaskAmountSmall"))
            {
                @int = (ssh.randomizer.Next(5, 11) * _settings.domlevelNumBox.Value) / (double)2;
                if (@int > 5)
                    @int = 5 * Math.Round(@int / (double)5);
                StringClean = StringClean.Replace("#TaskAmountSmall", @int);
            }

            if (StringClean.Contains("#TaskAmount"))
            {
                @int = ssh.randomizer.Next(15, 26) * _settings.domlevelNumBox.Value;
                if (@int > 5)
                    @int = 5 * Math.Round(@int / (double)5);
                StringClean = StringClean.Replace("#TaskAmount", @int);
            }

            if (StringClean.Contains("#TaskStrokingTime"))
            {
                @int = ssh.randomizer.Next(_settings.NBTaskStrokingTimeMin.Value, _settings.NBTaskStrokingTimeMax.Value + 1);
                @int *= 60;
                string TConvert = ConvertSeconds(@int);
                StringClean = StringClean.Replace("#TaskStrokingTime", TConvert);
            }

            if (StringClean.Contains("#TaskHoldTheEdgeTime"))
            {
                @int = ssh.randomizer.Next(_settings.NBTaskEdgeHoldTimeMin.Value, _settings.NBTaskEdgeHoldTimeMax.Value + 1);
                @int *= 60;
                string TConvert = ConvertSeconds(@int);
                StringClean = StringClean.Replace("#TaskHoldTheEdgeTime", TConvert);
            }

            if (StringClean.Contains("#TaskCBTTime"))
            {
                @int = ssh.randomizer.Next(_settings.NBTaskCBTTimeMin.Value, _settings.NBTaskCBTTimeMax.Value + 1);
                @int *= 60;
                string TConvert = ConvertSeconds(@int);
                StringClean = StringClean.Replace("#TaskCBTTime", TConvert);
            }

            return StringClean;
        }



        public string PoundClean(string stringClean, PoundOptions options = PoundOptions.None, int startRecurrence = 0)
        {
            List<string> AlreadyChecked = new List<string>();
            bool dotrace = true;
            TraceSwitch TS = new TraceSwitch("PoundClean", "");
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

            // Create Regex-Pattern to find #Keywords and exclude custom imagetags.
            string[] ExcludeKeywords = new[] { "TagGarment", "TagUnderwear", "TagTattoo", "TagSexToy", "TagFurniture" };
            string Pattern = string.Format(@"##*(?!{0})[\w\d\+\-_]+", String.Join("|", ExcludeKeywords));

            // Append included non-Keywords to pattern.
            string[] NonKeywordInclude = new[] { @"@RT\(", @"@RandomText\(" };
            Pattern += NonKeywordInclude.Length == 0 ? "" : "|" + string.Join("|", NonKeywordInclude);

            Regex RegexKeyWords = new Regex(Pattern);

            while (ActRecurrence < 6 && RegexKeyWords.IsMatch(stringClean))
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

                // Find all remaining #Keywords.
                Regex Re = new Regex(Pattern, RegexOptions.IgnoreCase);
                MatchCollection Mc = Re.Matches(stringClean);

                string ControlCustom = "";
                if (stringClean.Contains("@CustomMode("))
                    ControlCustom = GetParentheses(stringClean, "@CustomMode(");

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
                        string Filepath = Application.StartupPath + @"\Scripts\" + dompersonalitycombobox.Text + @"\Vocabulary\" + Keyword.Value + ".txt";

                        // ################ Check if vocab file exists #################
                        if (!Directory.Exists(Path.GetDirectoryName(Filepath)) || !File.Exists(Filepath))
                        {
                            if (UCase(Keyword.Value) == "#NULL")
                                // Replace predefined value 
                                stringClean = stringClean.Replace(Keyword.Value, "");
                            else
                            {
                                // The vocab file is missing 

                                stringClean = stringClean.Replace(Keyword.Value, ChatGetInlineError(Keyword.Value.Substring(1)));
                                string Lazytext = "Unable to locate vocabulary file: \"" + Keyword.Value + "\"";
                                Log.WriteError(Lazytext, new Exception(Lazytext), "PoundClean(String)");
                            }

                            continue; // Start next loop 
                        }

                        // #################### Process vocab file #####################

                        List<string> VocabLines = Txt2List(Filepath);
                        VocabLines = FilterList(VocabLines);

                        if (ControlCustom.Contains(Keyword.ToString))
                            customVocabLines = VocabLines;

                        if (VocabLines.Count <= 0)
                        {
                            // ----------------- No Lines available ----------------
                            Replacement = ChatGetInlineWarning(Keyword.Value.Substring(1));
                            ChatAddWarning("No available lines in vocabulary file: \"" + Keyword.Value + "");
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
                        Log.WriteError("Error Processing vocabulary file:  " + Keyword.Value, ex, "Tease AI did not return a valid line while parsing vocabulary file.");
                        Replacement = ChatGetInlineError(Keyword.Value.Substring(1));
                    }
                    finally
                    {
                        stringClean = stringClean.Replace(Keyword.Value, Replacement);
                    }
                }

                if (dotrace)
                    Trace.Unindent();
            }

            if (RegexKeyWords.IsMatch(stringClean))
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
                Log.WriteError("Maximum allowed Vocabulary depth reached for line:" + OrgString + Constants.vbCrLf + "Aborted Cleaning at: " + stringClean, new StackOverflowException("PoundClean infinite loop protection"), "PoundClean(String)");
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

    }
#endif
}
