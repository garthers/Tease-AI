/// <summary>
/// ''' This Class is an Extension for the Class IO.Directory based on the needs of Tease-AI.
/// ''' Comments based on Microsoft: https://msdn.microsoft.com
/// ''' </summary>
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
    public sealed class DirectoryExt
    {

        /// <summary>
        /// 	''' Determines whether the given path refers to an existing directory on disk.
        /// 	''' This Function creates the Root-directory if it doesn't extists and is a subdirectory of the 
        /// 	''' Applications StartupPath. 
        /// 	''' </summary>
        /// 	''' <param name="path">The path to test. </param>
        /// 	''' <returns>true if path refers to an existing directory; false if the directory does not exist or 
        /// 	''' an error occurs when trying to determine if the specified directory exists.</returns>
        /// 	''' <remarks>BaseFunction to wrap around.</remarks>
        private static bool DirectoryCheck(string path)
        {
            if (path.ToUpper() == "No path selected".ToUpper()) // WTF!!
                return false;
            if (path == null)
                return false;
            if (path == "")
                return false;

            if (System.IO.Directory.Exists(path))
                // The Directory exists => Nothing to do.
                return true;
            /*
           else
               // The Directory does not exist...
               if (System.IO.Directory.GetParent(path).FullName.StartsWith(Application.StartupPath))
           {
               // ... Is it a SubDirectory of application, create it
               try
               {
                   System.IO.Directory.CreateDirectory(path);
                   // everthing fine, Directory has been created.
                   return true;
               }
               catch (Exception EX)
               {
                   // Error on creation => ReThrow Exception
                   throw;
               }
           }
           else
               // ... The Directory does not exist and is no application Subdirectory
               return false;
               */
            return false;
        }

        private static IEnumerable<string> Sort(IEnumerable<string> SortObject)
        {
            return SortObject.OrderBy(x => x, StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 	''' Determines whether the given path refers to an existing directory on disk.
        /// 	''' This Function creates the Root-directory if it doesn't extists and is a subdirectory of the 
        /// 	''' Applications StartupPath. 
        /// 	''' </summary>
        /// 	''' <param name="path">The path to test. </param>
        /// 	''' <returns>true if path refers to an existing directory; false if the directory does not exist or 
        /// 	''' an error occurs when trying to determine if the specified file exists.</returns>
        public static bool Exists(string path)
        {
            // IF directory-check has failed return an empty String.Array
            return DirectoryCheck(path);
        }


        /// <summary>
        /// 	''' Returns the names of files (including their paths) in the specified directory.
        /// 	''' This Function creates the Root-directory if it doesn't extists and is a subdirectory of the 
        /// 	''' Applications StartupPath. 
        /// 	''' </summary>
        /// 	''' 
        /// 	''' <param name="path">The relative or absolute path to the directory to search. This string is not 
        /// 	''' case-sensitive.</param>
        /// 	''' 
        /// 	''' <returns>An array of the full names (including paths) for the files in the specified directory that 
        /// 	''' match the specified search pattern and option, or an empty array if no files are found.</returns>
        /// 	''' <seealso cref="System.IO.Directory.GetFiles(String)"/>
        public static string[] GetFiles(string path)
        {
            // IF directory-check has failed return an empty String.Array
            if (DirectoryCheck(path) == false)
                return new List<string>().ToArray();

            // Read all Files 
            string[] temp = System.IO.Directory.GetFiles(path);

            // Sort the result and return it
            return Sort(temp).ToArray();
        }

        /// <summary>
        /// 	''' Returns the names of files (including their paths) that match the specified search pattern in the 
        /// 	''' specified directory. This Function creates the Root-directory if it doesn't extists and is a 
        /// 	''' subdirectory of the Applications StartupPath. 
        /// 	''' </summary>
        /// 	''' 
        /// 	''' <param name="path">The relative or absolute path to the directory to search. This string is not 
        /// 	''' case-sensitive.</param>
        /// 	''' 
        /// 	''' <param name="searchPattern">The search string to match against the names of files in path. This 
        /// 	''' parameter can contain a combination of valid literal path and wildcard (* and ?) characters 
        /// 	''' (see Remarks), but doesn't support regular expressions.</param>
        /// 	''' 
        /// 	''' <returns>An array of the full names (including paths) for the files in the specified directory that 
        /// 	''' match the specified search pattern and option, or an empty array if no files are found.</returns>
        /// 	''' <seealso cref="System.IO.Directory.GetFiles(String, String) "/>
        public static string[] GetFiles(string path, string searchPattern)
        {
            // IF directory-check has failed return an empty String.Array
            if (DirectoryCheck(path) == false)
                return new List<string>().ToArray();

            // Read all Files with the given pattern
            string[] temp = System.IO.Directory.GetFiles(path, searchPattern);

            // Sort the result and return it
            return Sort(temp).ToArray();
        }

        /// <summary>
        /// 	''' Returns the names of files (including their paths) that match the specified search pattern in the 
        /// 	''' specified directory, using a value to determine whether to search subdirectories.
        /// 	''' This Function creates the Root-directory if it doesn't extists and is a subdirectory of the 
        /// 	''' Applications StartupPath. 
        /// 	''' </summary>
        /// 	''' 
        /// 	''' <param name="path">The relative or absolute path to the directory to search. This string is not 
        /// 	''' case-sensitive.</param>
        /// 	''' 
        /// 	''' <param name="searchPattern">The search string to match against the names of files in path. This 
        /// 	''' parameter can contain a combination of valid literal path and wildcard (* and ?) characters 
        /// 	''' (see Remarks), but doesn't  support regular expressions.</param>
        /// 	''' 
        /// 	''' <param name="searchOption">One of the enumeration values that specifies whether the search operation 
        /// 	''' should include all subdirectories or only the current directory. </param>
        /// 	''' 
        /// 	''' <returns>An array of the full names (including paths) for the files in the specified directory that 
        /// 	''' match the specified search pattern and option, or an empty array if no files are found.</returns>
        /// 	''' <seealso cref="System.IO.Directory.GetFiles(String, String, IO.SearchOption) "/>
        public static string[] GetFiles(string path, string searchPattern, System.IO.SearchOption searchOption)
        {
            // IF directory-check has failed return an empty String.Array
            if (DirectoryCheck(path) == false)
                return new List<string>().ToArray();

            // Read all Files with the given pattern and Searchoption
            string[] temp = System.IO.Directory.GetFiles(path, searchPattern, searchOption);

            // Sort the result and return it
            return Sort(temp).ToArray();
        }



        public static List<string> GetFilesExtension(string path, List<string> filter, System.IO.SearchOption searchOption = System.IO.SearchOption.AllDirectories)
        {
            try
            {
                // Convert all Extensions to LowerCase
                for (var i = 0; i <= filter.Count - 1; i++)
                    filter[i] = filter[i].ToLower();

                // Read get all Files with the given pattern
                List<string> temp = DirectoryExt.GetFiles(path, "*", searchOption)
                        .Where(f => filter.Contains(System.IO.Path.GetExtension(f).ToLower())).ToList();

                // Sort the result and return it
                return Sort(temp).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// =========================================================================================================
        /// 	''' <summary>
        /// 	''' Returns the names of videofiles (including their paths) in the specified directory, using a value 
        /// 	''' to determine whether to search subdirectories. This Function creates the Root-directory it doeesn't 
        /// 	''' extist and it is a subdirectory of the Applications StartupPath. 
        /// 	''' </summary>
        /// 	''' 
        /// 	''' <param name="path">The relative or absolute path to the directory to search. This string is not 
        /// 	''' case-sensitive.</param>
        /// 	''' 
        /// 	''' <param name="searchOption">One of the enumeration values that specifies whether the search operation 
        /// 	''' should include all subdirectories or only the current directory. This parameter is optional and 
        /// 	''' standard is to search all directories.</param>
        /// 	''' 
        /// 	''' <returns>A generic list containing all video-files in the directory.</returns>
        /// 	''' <exception cref="Exception">Rethrows all exceptions.</exception>
        public static List<string> GetFilesVideo(string path, System.IO.SearchOption searchOption = System.IO.SearchOption.AllDirectories)
        {
            List<string> supportedExtension = new List<string>() { ".wmv", ".avi", ".mp4", ".m4v", ".mpg", ".mov", ".flv", ".webm", ".mkv" };

            return GetFilesExtension(path, supportedExtension, searchOption);
        }

        /// =========================================================================================================
        /// 	''' <summary>
        /// 	''' Returns the names of videofiles (including their paths) in the specified directory, using a value 
        /// 	''' to determine whether to search subdirectories. This Function creates the Root-directory it doeesn't 
        /// 	''' extist and it is a subdirectory of the Applications StartupPath. 
        /// 	''' </summary>
        /// 	''' 
        /// 	''' <param name="path">The relative or absolute path to the directory to search. This string is not 
        /// 	''' case-sensitive.</param>
        /// 	''' 
        /// 	''' <param name="searchOption">One of the enumeration values that specifies whether the search operation 
        /// 	''' should include all subdirectories or only the current directory. This parameter is optional and 
        /// 	''' standard is to search all directories.</param>
        /// 	''' 
        /// 	''' <returns>A generic list containing all video-files in the directory.</returns>
        /// 	''' <exception cref="Exception">Rethrows all exceptions.</exception>
        public static List<string> GetFilesImages(string path, System.IO.SearchOption searchOption = System.IO.SearchOption.AllDirectories)
        {
            List<string> supportedExtension = new List<string>() { ".png", ".jpg", ".gif", ".bmp", ".jpeg" };

            return GetFilesExtension(path, supportedExtension, searchOption);
        }



        /// <summary>
        /// 	''' Returns the names of subdirectories (including their paths) in the specified directory.
        /// 	''' This Function creates the Root-directory if it doesn't extists and is a subdirectory of the 
        /// 	''' Applications StartupPath. 
        /// 	''' </summary>
        /// 	''' <param name="path">The relative or absolute path to the directory to search. This string is not 
        /// 	''' case-sensitive.</param>
        /// 	''' 
        /// 	''' <returns></returns>
        /// 	''' <remarks>IO.Directory.GetDirectories throws a Directory not FoundException, if the Directory of the file cannot 
        /// 	''' be found. This Functions create the Directory, as long as it is in die Application.StartupPath.
        /// 	''' </remarks>
        /// 	''' <seealso cref="System.IO.Directory.GetDirectories(String) "/>
        public static string[] GetDirectories(string path)
        {
            // IF directory-check has failed return an empty String.Array
            if (DirectoryCheck(path) == false)
                return new List<string>().ToArray();

            return Sort(System.IO.Directory.GetDirectories(path)).ToArray();
        }

        /// <summary>
        /// 	''' Returns the names of the subdirectories (including their paths) that match the specified search 
        /// 	''' pattern in the specified directory, and optionally searches subdirectories. This Function creates 
        /// 	''' the Root-directory if it doesn't extists and is a subdirectory of the Applications StartupPath. 
        /// 	''' </summary>
        /// 	''' 
        /// 	''' <param name="path">The relative or absolute path to the directory to search. This string is not 
        /// 	''' case-sensitive.</param>
        /// 	''' 
        /// 	''' <param name="searchPattern">The search string to match against the names of subdirectories in path. 
        /// 	''' This parameter can contain a combination of valid literal and wildcard characters (see Remarks), 
        /// 	''' but doesn't support regular expressions.</param>
        /// 	''' 
        /// 	''' <returns>An array of the full names (including paths) of the subdirectories that match the specified 
        /// 	''' criteria, or an empty array if no directories are found.</returns>
        /// 	''' <seealso cref="System.IO.Directory.GetDirectories(String, String, IO.SearchOption)"/>
        public static string[] GetDirectories(string path, string searchPattern)
        {
            // IF directory-check has failed return an empty String.Array
            if (DirectoryCheck(path) == false)
                return new List<string>().ToArray();

            return Sort(System.IO.Directory.GetDirectories(path, searchPattern)).ToArray();
        }



        /// <summary>
        /// 	''' Returns the names of the subdirectories (including their paths) that match the specified search 
        /// 	''' pattern in the specified directory, and optionally searches subdirectories. This Function creates 
        /// 	''' the Root-directory if it doesn't extists and is a subdirectory of the Applications StartupPath. 
        /// 	''' </summary>
        /// 	''' 
        /// 	''' <param name="path">The relative or absolute path to the directory to search. This string is not 
        /// 	''' case-sensitive.</param>
        /// 	''' 
        /// 	''' <param name="searchPattern">The search string to match against the names of subdirectories in path. 
        /// 	''' This parameter can contain a combination of valid literal and wildcard characters (see Remarks), 
        /// 	''' but doesn't support regular expressions.</param>
        /// 	''' 
        /// 	''' <param name="searchOption">One of the enumeration values that specifies whether the search operation 
        /// 	''' should include all subdirectories or only the current directory.</param>
        /// 	''' 
        /// 	''' <returns>An array of the full names (including paths) of the subdirectories that match the specified 
        /// 	''' criteria, or an empty array if no directories are found.</returns>
        /// 	''' <seealso cref="System.IO.Directory.GetDirectories(String, String, IO.SearchOption)"/>
        public static string[] GetDirectories(string path, string searchPattern, System.IO.SearchOption searchOption)
        {
            // IF directory-check has failed return an empty String.Array
            if (DirectoryCheck(path) == false)
                return new List<string>().ToArray();

            return Sort(System.IO.Directory.GetDirectories(path, searchPattern, searchOption)).ToArray();
        }
    }

}