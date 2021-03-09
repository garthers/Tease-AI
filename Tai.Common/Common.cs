using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Tai.Common
{
    public static class Common
    {
        public static bool IsUrl(string path)
        {
            return path.Contains("://");
        }
        private static Dictionary<string, List<string>> _txtcache = new Dictionary<string, List<string>>(StringComparer.InvariantCultureIgnoreCase);

        private static Dictionary<string, List<string>> TxtCache => _txtcache;


        /// <summary>

        /// 	''' Synclocked Object!

        /// 	''' <para> Do not Access directly. Use the Property instead.</para>

        /// 	''' </summary>
        private static Dictionary<string, FileSystemWatcher> _FswTxtCache = new Dictionary<string, FileSystemWatcher>();
        /// <summary>
        /// 	''' Synclock-Storage for TxtCache.
        /// 	''' </summary>
        private static object _synclockFswTxtCache = new object();
        /// <summary>
        /// 	''' A Dictionary containing all filesystemwatchers for directories 
        /// 	''' containing files of the TxtCache.
        /// 	''' </summary>
        public static Dictionary<string, FileSystemWatcher> FswChache
        {
            get
            {
                lock (_synclockFswTxtCache)
                    return _FswTxtCache;
            }
            set
            {
                lock (_synclockFswTxtCache)
                    _FswTxtCache = value;
            }
        }


        private static void createFileSystemWatcher(string Filepath)
        {
            try
            {
                string tmpString = Path.GetDirectoryName(Filepath);

                if (tmpString == AppDomain.CurrentDomain.BaseDirectory)
                    return;
                if (FswChache.ContainsKey(tmpString))
                    return;

                FileSystemWatcher tmpFsw = new FileSystemWatcher()
                {
                    IncludeSubdirectories = true,
                    Path = tmpString
                };

                tmpFsw.Changed += UpdateTextfileCache;
                tmpFsw.Renamed += UpdateTextfileCache;

                tmpFsw.EnableRaisingEvents = true;

                FswChache.Add(tmpString, tmpFsw);
            }
            catch (Exception ex)
            {
                // Log.WriteError("TxtCache: Unable to Create FileSystemWatcher for:" + Filepath, ex, "TxtCache Create FileSystemWatcher");
                throw; // todo
            }
        }

        private static void UpdateTextfileCache(object sender, FileSystemEventArgs e)
        {
            if (TxtCache.ContainsKey(e.FullPath))

                /* TODO ERROR: Skipped IfDirectiveTrivia *//* TODO ERROR: Skipped DisabledTextTrivia *//* TODO ERROR: Skipped EndIfDirectiveTrivia */
                TxtCache.Remove(e.FullPath);
        }
        /// =========================================================================================================

        public static int TxtRemoveLine(string Filepath, string Searchpattern)
        {
            int rtnInt = 0;
            try
            {
                if (Searchpattern == null)
                    throw new ArgumentException("Searchpattern was empty.");
                if (Searchpattern.Count() < 5)
                    throw new ArgumentException("Searchpattern has to be longer than 5 Chars.");
                // Read the TextFile
                List<string> tmplist = Txt2List(Filepath);

                // Check if File has lines.
                if (tmplist.Count > 0)
                {
                    // Remove all lines containing the searchpattern
                    rtnInt = tmplist.RemoveAll(x => x.ToLower().Contains(Searchpattern.ToLower()));

                    // If there were Lines deleted, 
                    if (rtnInt > 0)
                        File.WriteAllLines(Filepath, tmplist);
                }

                // Return the Deleted line count 
                return rtnInt;
            }
            catch (Exception ex)
            {
                // ▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                // All Errors
                // ▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                Debug.WriteLine("Error removing TextLine: \"" + Searchpattern + "\" from file \"" + Filepath + "\": " + ex.ToString());

                return 0;
            }
        }
        public static int Weekday(DateTime dt, DayOfWeek startOfWeek)
        {
            return (dt.DayOfWeek - startOfWeek + 7) % 7;
        }
        public static string ConvertSeconds(int Seconds)
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


        /// 	''' <summary>
        /// 	''' Reads a TextFile into a generic List(of String). EmptyLines are removed from the list.
        /// 	''' </summary>
        /// 	''' <param name="filepath">The Filepath to read.</param>
        /// 	''' <returns>A List(of String) containing all Lines of the given File. Returns 
        /// 	''' an empty List if the specified file doesn't exists, or an exception occurs.</returns>
        /// 	''' <remarks>This Method will create the given DirectoryStructure for the given
        /// 	''' Filepath if it doesn't exist.</remarks>
        internal static List<string> Txt2List(string filepath)
        {
            /* TODO ERROR: Skipped IfDirectiveTrivia *//* TODO ERROR: Skipped DisabledTextTrivia *//* TODO ERROR: Skipped EndIfDirectiveTrivia */
            try
            {
                if (filepath == null)
                    throw new ArgumentNullException("The given filepath was NULL.");

                if (filepath == null | filepath == "")
                    throw new ArgumentException("The given filepath was empty \"" + filepath + "\"");

                List<string> TextList = new List<string>();

                if (TxtCache.TryGetValue(filepath, out var val))
                {
                    /* TODO ERROR: Skipped IfDirectiveTrivia *//* TODO ERROR: Skipped DisabledTextTrivia *//* TODO ERROR: Skipped EndIfDirectiveTrivia */


                    return val;
                }




                // Check if the given Directory exists. MyDirectory.Exists will 
                // try to create the directory, if it's an App-sub-dir.
                if (DirectoryExt.Exists(Path.GetDirectoryName(Path.GetFullPath(filepath))))
                {
                    /* TODO ERROR: Skipped IfDirectiveTrivia *//* TODO ERROR: Skipped DisabledTextTrivia *//* TODO ERROR: Skipped EndIfDirectiveTrivia */
                    if (File.Exists(filepath))
                    {
                        using (StreamReader TextReader = new StreamReader(filepath))
                        {
                            while (TextReader.Peek() != -1)
                                TextList.Add(TextReader.ReadLine());

                            // Remove all empty Lines from list.
                            TextList.RemoveAll(x => x == "");

                            TxtCache.Add(filepath.ToLower(), TextList);

                            createFileSystemWatcher(filepath);
                            return TextList;
                        }
                    }
                    else
                        throw new FileNotFoundException("Can't locate the file: \"" + filepath + "\"");
                }
                else
                    throw new DirectoryNotFoundException("Can't locate the directory \"" + Path.GetDirectoryName(filepath) + "\"");
            }
            catch (Exception ex)
            {
                // ▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                // All Errors
                // ▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                // Log.WriteError(ex.Message, ex, "Error loading TextFile: \"" + filepath + "\"");
                throw new NotImplementedException("To do");
            }
            //return new List<string>();
        }
        public static string TxtReadLine(string filePath)
        {
            return Txt2List(filePath)[0];
        }
        public static string Color2Html(Color MyColor)
        {
            return "#" + MyColor.ToArgb().ToString("x").Substring(2).ToUpper();
        }

        public static int GetNthIndex(string searchString, char charToFind, int startIndex, int n)
        {
            var charIndexPair = searchString.Select((c, i) => new { Character = c, Index = i })
              .Where(x => x.Character == charToFind & x.Index > startIndex)
              .ElementAtOrDefault(n - 1);
            return charIndexPair != null ? charIndexPair.Index : -1;
        }

        public static string FixCommas(string CommaString)
        {
            CommaString = CommaString.Replace(", ", ",");
            CommaString = CommaString.Replace(" ,", ",");

            return CommaString;
        }
        public static double Fix(double Number)
        {
            if (Number >= 0.0)
            {
                return Math.Floor(Number);
            }
            return 0.0 - Math.Floor(0.0 - Number);
        }

        public static long GetDateDifference(DateTime date1, string DateString)
        {
            long DDiff = 0;
            //date1 > date2 -> 
            var date2 = DateTime.Now;
            var timespan = (date2 - date1);


            if (DateString.IndexOf("SECOND", StringComparison.CurrentCultureIgnoreCase) != -1)
            {
                DDiff = (long)Math.Round(Fix(timespan.TotalSeconds));
            }
            else if (DateString.IndexOf("MINUTE", StringComparison.CurrentCultureIgnoreCase) != -1)
            {
                DDiff = (long)Math.Round(Fix(timespan.TotalMinutes));
            }
            else if (DateString.IndexOf("HOUR", StringComparison.CurrentCultureIgnoreCase) != -1)
                DDiff = (long)Math.Round(Fix(timespan.TotalHours));
            else if (DateString.IndexOf("DAY", StringComparison.CurrentCultureIgnoreCase) != -1)
                DDiff = (long)Math.Round(Fix(timespan.TotalDays));
            else if (DateString.IndexOf("WEEK", StringComparison.CurrentCultureIgnoreCase) != -1)
                DDiff = (long)Math.Round(Fix(timespan.TotalDays)) / 7;
            else if (DateString.IndexOf("MONTH", StringComparison.CurrentCultureIgnoreCase) != -1)
            {
                Calendar currentCalendar = Thread.CurrentThread.CurrentCulture.Calendar; ;
                DDiff = (currentCalendar.GetYear(date2) - currentCalendar.GetYear(date1)) * 12 + currentCalendar.GetMonth(date2) - currentCalendar.GetMonth(date1);

            }
            else if (DateString.IndexOf("YEAR", StringComparison.CurrentCultureIgnoreCase) != -1)
            {
                Calendar currentCalendar = Thread.CurrentThread.CurrentCulture.Calendar;
                DDiff = currentCalendar.GetYear(date2) - currentCalendar.GetYear(date1);

            }
            return DDiff;
        }

        public static long GetDateDifferenceOdd(DateTime date1, string DateString)
        {
            long DDiff = 0;
            //date1 > date2 -> 
            var date2 = DateTime.Now;
            var timespan = (date2 - date1);


            if (DateString.IndexOf("SECOND", StringComparison.CurrentCultureIgnoreCase) != -1)
            {
                DDiff = (long)Math.Round(Fix(timespan.TotalSeconds));
            }
            else if (DateString.IndexOf("MINUTE", StringComparison.CurrentCultureIgnoreCase) != -1)
            {
                DDiff = (long)Math.Round(Fix(timespan.TotalMinutes)) * 60;
            }
            else if (DateString.IndexOf("HOUR", StringComparison.CurrentCultureIgnoreCase) != -1)
                DDiff = (long)Math.Round(Fix(timespan.TotalHours)) * 3600;
            else if (DateString.IndexOf("DAY", StringComparison.CurrentCultureIgnoreCase) != -1)
                DDiff = (long)Math.Round(Fix(timespan.TotalDays)) * 86400;
            else if (DateString.IndexOf("WEEK", StringComparison.CurrentCultureIgnoreCase) != -1)
                DDiff = (long)Math.Round(Fix(timespan.TotalDays)) / 7 * 604800;
            else if (DateString.IndexOf("MONTH", StringComparison.CurrentCultureIgnoreCase) != -1)
            {
                Calendar currentCalendar = Thread.CurrentThread.CurrentCulture.Calendar; ;
                DDiff = (currentCalendar.GetYear(date2) - currentCalendar.GetYear(date1)) * 12 + currentCalendar.GetMonth(date2) - currentCalendar.GetMonth(date1);
                DDiff *= 2629746;
            }
            else if (DateString.IndexOf("YEAR", StringComparison.CurrentCultureIgnoreCase) != -1)
            {
                Calendar currentCalendar = Thread.CurrentThread.CurrentCulture.Calendar;
                DDiff = currentCalendar.GetYear(date2) - currentCalendar.GetYear(date1);
                DDiff *= 31536000;
            }

            return DDiff;
        }

        public static string GetParentheses(string ParenCheck, string CommandCheck, int Iterations = 1)
        {
            string ParenFlag = ParenCheck;
            int ParenStart = ParenFlag.IndexOf(CommandCheck) + CommandCheck.Length;
            // githib patch Dim ParenType As String

            char ParenType = default;

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

        public static string[] ObtainSplitParts(string splitMe, bool isChat)
        {
            splitMe = "[" + splitMe + "] Null";
            string[] Splits = splitMe.Split(new char[] { ']' });
            Splits[0] = Splits[0].Replace("[", "");
            do
            {
                Splits[0] = Splits[0].Replace("  ", " ");
                Splits[0] = Splits[0].Replace(" ,", ",");
                Splits[0] = Splits[0].Replace(", ", ",");
                Splits[0] = Splits[0].Replace("'", "");
            }
            while (!!Splits[0].Contains("  ") & !Splits[0].Contains(", ") & !Splits[0].Contains(" ,") & !Splits[0].Contains("'"));
            if (isChat)
                // che(32) is the code for empty space - ' '
                return Splits[0].Split(new char[] { ' ', ',' });
            else
                return Splits[0].Split(new char[] { ',' });
        }
    }
}
