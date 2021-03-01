using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace Tai.Common
{
    public class Common
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

        public static string Color2Html(Color MyColor)
        {
            return "#" + MyColor.ToArgb().ToString("x").Substring(2).ToUpper();
        }
    }
}
