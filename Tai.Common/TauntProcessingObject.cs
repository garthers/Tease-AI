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

namespace Tai.Common
{
    /// <summary> Class to process TauntFiles </summary>
    public class TauntProcessingObject
    {

        /// <summary> Absolute path to taunt file. </summary>
        internal string FilePath { get; set; } = "";

        private readonly Random _random;

        /// <summary>gets tauntfile's name without extension. </summary>
        internal string FileName
        {
            get
            {
                return Path.GetFileNameWithoutExtension(FilePath);
            }
        }

        /// <summary> Filtered taunt file Lines.</summary>
        internal List<string> Lines { get; set; } = new List<string>();

        private int _TauntSize = -1;
        /// <summary> Gets the number of lines in a taunt. </summary>
        /// 	'''<returns>1-based value</returns>
        internal int TauntSize
        {
            get
            {
                if (_TauntSize == -1 && FileName.Length > 4)
                {
                    // get the last 4 numeric chars in filename and convert them to a number
                    string TmpString = "";

                    for (var i = FileName.Length - 1; i >= FileName.Length - 4; i += -1)
                    {
                        if (Char.IsDigit(FileName[i]))
                            TmpString = FileName[i] + TmpString;
                        else
                            break;
                    }

                    if (int.TryParse(TmpString, out var x))
                        _TauntSize = x;
                }

                return _TauntSize;
            }
        }

        /// <summary>Gets a random taunt start line. </summary>
        internal int RandomTauntLine
        {
            get
            {
                if (Avaialable)
                {
                    var GroupCount = Lines.Count / TauntSize; // 1-based => ?? count is index independent...
                    int RndGroup = _random.Next(1, GroupCount + 1) - 1; // 0-based
                    int ScriptLine = RndGroup * TauntSize;

                    return ScriptLine;
                }
                else
                    return -1;
            }
        }

        /// <summary> Returns if a Taunt is useable.</summary>
        /// 	''' <returns>Returns true, if taunt has valid lines and right ammount.</returns>
        public bool Avaialable
        {
            get
            {
                if ((Lines.Count > 0 & TauntSize > 0) && Lines.Count >= TauntSize)
                    return true;
                else
                    return false;
            }
        }

        /// <summary> Creates a new instance and loads the data from given filepath </summary>
        /// 	''' <param name="absolutePath">The file to load.</param>
        /// 	''' <param name="Form1Reference">Object Reference to run filtering on.</param>
        /// 	''' <remarks>Non-Threadsafe</remarks>
        public TauntProcessingObject(string absolutePath, Random random)
        {
            try
            {
                FilePath = absolutePath;
                _random = random;

                // Set TauntSize for filtering. // ??
                //Form1Reference.ssh.StrokeTauntCount = TauntSize;

                // Read lines.
                Lines = Common.Txt2List(FilePath);

                // Filter lines.
                //Form1Reference.ssh.StrokeFilter = true;
                //List<string> linesFiltered = Form1Reference.FilterList(Lines, true, -1);
                //Form1Reference.ssh.StrokeFilter = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}