// ===========================================================================================
// 
// ScriptVariables.vb.vb
// 
// This file contains functions and methods to handle script Variables.
// 
// ===========================================================================================
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

namespace Tai.Common
{
    public partial class Variables
    {
        private string VariableFolder;
        public Variables(string path)
        {
            VariableFolder = path;
        }
        public string GetVariableFolder()
        {
            //VariableFolder = Application.StartupPath + @"\Scripts\" + dompersonalitycombobox.Text + @"\System\Variables\";
            //Directory.CreateDirectory(VariableFolder);
            return VariableFolder;
        }

        public bool VariableExists(string varName)
        {
            return File.Exists(Path.Combine(VariableFolder, varName));
        }


        public void SetVariable(string varName, string value)
        {
            File.WriteAllText(Path.Combine(VariableFolder, varName), value);
        }

        public string GetVariable(string varName)
        {
            if (VariableExists(varName))
                return Common.TxtReadLine(Path.Combine(VariableFolder, varName));
            else
                return "0"; // ??? seems unlikely
        }

        public void DeleteVariable(string varName)
        {
            File.Delete(Path.Combine(VariableFolder, varName));
        }

        public int ChangeVariable(string varName, string operand1, string mathOperator, string operand2)
        {
            int Val1, Val2, Result;

            // Integer.TryParse will return 0 if conversion failed.
            if (!int.TryParse(operand1, out Val1))
            {
                if (VariableExists(operand1))
                    int.TryParse(GetVariable(operand1), out Val1);
            }

            if (!int.TryParse(operand2, out Val2))
            {
                if (VariableExists(operand2))
                    int.TryParse(GetVariable(operand2), out Val2);
            }

            if (mathOperator.Contains("+"))
                Result = Val1 + Val2;
            else if (mathOperator.Contains("-"))
                Result = Val1 - Val2;
            else if (mathOperator.Contains("*"))
                Result = Val1 * Val2;
            else if (mathOperator.Contains("/"))
            {
                if (Val2 == 0)
                {
                    Val2 = 1;
                    Result = Int32.MaxValue;
                    Debug.Print("Division by zero fun coming up");
                    //ChatAddWarning("\"" + operand2 + "\" has returned zero. Modified to \"1\" to avoid an DivideByZeroException.");
                }
                else Result = Val1 / Val2;
            }
            else
                Result = 0;

            SetVariable(varName, Result.ToString()); // type coercion shenanigans

            return Result;
        }



        public bool CheckVariable(string StringCLean)
        {
            if (StringCLean.Contains("]AND["))
                StringCLean = StringCLean.Replace("]AND[", "]And[");
            if (StringCLean.Contains("]OR["))
                StringCLean = StringCLean.Replace("]OR[", "]Or[");
            do
            {
                string[] SCIfVar = StringCLean.Split(' ');
                string SCGotVar = "Null";
                for (int i = 0; i <= SCIfVar.Length - 1; i++)
                {
                    if (SCIfVar[i].Contains("@Variable["))
                    {
                        int IFJoin = 0;
                        if (!SCIfVar[i].Contains("] "))
                        {
                            do
                            {
                                IFJoin += 1;
                                SCIfVar[i] = SCIfVar[i] + " " + SCIfVar[i + IFJoin];
                                SCIfVar[i + IFJoin] = "";
                            }
                            while (!SCIfVar[i].Contains("] ") | SCIfVar[i].EndsWith("]"));
                        }

                        SCGotVar = SCIfVar[i].Trim();
                        SCIfVar[i] = "";
                        StringCLean = string.Join(" ", SCIfVar);
                        do
                            StringCLean = StringCLean.Replace("  ", " ");
                        while (!!StringCLean.Contains("  "));
                        break;
                    }
                }

                if (SCGotVar.Contains("]And["))
                {
                    bool AndCheck = true;
                    for (int x = 0; x <= SCGotVar.Replace("]And[", "").Count() - 1; x++)
                    {
                        if (GetIf("[" + Common.GetParentheses(SCGotVar, "@Variable[", 2) + "]") == false)
                        {
                            AndCheck = false;
                            break;
                        }

                        SCGotVar = SCGotVar.Replace("[" + Common.GetParentheses(SCGotVar, "@Variable[", 2) + "]And", "");
                    }

                    return AndCheck;
                }
                else if (SCGotVar.Contains("]Or["))
                {
                    bool OrCheck = false;
                    for (int x = 0; x <= SCGotVar.Replace("]Or[", "").Count() - 1; x++)
                    {
                        if (GetIf("[" + Common.GetParentheses(SCGotVar, "@Variable[", 2) + "]") == true)
                        {
                            OrCheck = true;
                            break;
                        }

                        SCGotVar = SCGotVar.Replace("[" + Common.GetParentheses(SCGotVar, "@Variable[", 2) + "]Or", "");
                    }

                    return OrCheck;
                }
                else if (GetIf("[" + Common.GetParentheses(SCGotVar, "@Variable[", 2) + "]") == true)
                    return true;
                else
                    return false;
            }
            while (!!StringCLean.Contains("@Variable"));
        }

        string PoundClean(string text)
        {
            throw new NotImplementedException();
        }
        public bool GetIf(string CompareString)
        {

            // CompareString = [x]operator[y]

            bool ReturnVal = false;

            string[] CompareArray = CompareString.Split(']');
            string C_Operator = CompareArray[1].Split('[')[0];
            string Val1 = CompareArray[0].Replace("[", "");
            string Val2 = CompareArray[1].Replace(C_Operator + "[", "");

            if (Val1.StartsWith("#"))
                Val1 = PoundClean(Val1);
            if (Val2.StartsWith("#"))
                Val2 = PoundClean(Val2);

            Debug.Print("CompareString = " + CompareString);
            Debug.Print("C_Operator = " + C_Operator);
            Debug.Print("Val1 = " + Val1);
            Debug.Print("Val2 = " + Val2);

            var isVal1Numeric = double.TryParse(Val1, out var dVal1);
            var isVal2Numeric = double.TryParse(Val2, out var dVal2);

            if (!isVal1Numeric && VariableExists(Val1))
            {
                Val1 = GetVariable(Val1);
                isVal1Numeric = double.TryParse(Val1, out dVal1);
            }

            if (!isVal2Numeric && VariableExists(Val2))
            {
                Val2 = GetVariable(Val2);
                isVal2Numeric = double.TryParse(Val2, out dVal2);
            }

            if (C_Operator == "=" | C_Operator == "==")
            {
                if (string.Equals(Val1, Val2, StringComparison.CurrentCultureIgnoreCase))
                    return true;
            }

            if (C_Operator == "<>")
            {
                if (!string.Equals(Val1, Val2, StringComparison.CurrentCultureIgnoreCase))
                    return true;
            }

            if (!isVal1Numeric && !isVal2Numeric)
                return false;
            
            if (C_Operator == ">")
            {
                if (dVal1 > dVal2)
                    return true;
            }

            if (C_Operator == "<")
            {
                if (dVal1 < dVal2)
                    return true;
            }

            if (C_Operator == ">=")
            {
                if (dVal1 >= dVal2)
                    return true;
            }

            if (C_Operator == "<=")
            {
                if (dVal1 <= dVal2)
                    return true;
            }

            return ReturnVal;
        }

        public DateTime GetDate(string VarName)
        {
            DateTime r;
            if (VariableExists(VarName))
                DateTime.TryParse(GetVariable(VarName), out r);
            else
                r = DateTime.Now;
            return r;
        }

        public string GetTime(string VarName)
        {
            return GetDate(VarName).ToLongTimeString();
        }

        public bool CheckDateList(string DateString, bool Linear = false)
        {
            string DateFlag = Common.GetParentheses(DateString, "@CheckDate(");

            if (DateFlag.Contains(","))
            {
                throw new NotImplementedException("This code was broken");
                //DateFlag = Common.Common.FixCommas(DateFlag);

                //string[] DateArray = DateFlag.Split(',');
                //long DDiff = 18855881;

                //long DCompare;
                //long DCompare2;

                //if (Linear == false)
                //{
                //    if (DateArray.Count() == 2)
                //    {
                //        DDiff = GetDateDifference(DateArray[0], DateArray[1]);
                //        DCompare = GetDateCompare(DateArray[0], DateArray[1]);
                //        if (DDiff >= DCompare)
                //            return true;
                //        return false;
                //    }

                //    if (DateArray.Count() == 3)
                //    {
                //        DDiff = GetDateDifference(DateArray[0], DateArray[1]);
                //        DCompare = GetDateCompare(DateArray[0], DateArray[1]);
                //        long DDiff2 = GetDateDifference(DateArray[0], DateArray[2]);
                //        DCompare2 = GetDateCompare(DateArray[0], DateArray[2]);
                //        if (DDiff >= DCompare & DDiff2 <= DCompare2)
                //            return true;
                //        return false;
                //    }
                //}
                //else
                //{
                //    if (DateArray.Count() == 2)
                //    {
                //        if (CompareDatesWithTime(GetDate(DateArray[0])) != 1)
                //            return true;
                //        return false;
                //    }

                //    if (DateArray.Count() == 3)
                //    {
                //        DDiff = GetDateDifference(DateArray[0], DateArray[1]);
                //        DCompare = GetDateCompare(DateArray[0], DateArray[1]);
                //        if (DDiff >= DCompare)
                //            return true;
                //        return false;
                //    }

                //    if (DateArray.Count() == 4)
                //    {
                //        DDiff = GetDateDifference(DateArray[0], DateArray[1]);
                //        DCompare = GetDateCompare(DateArray[0], DateArray[1]);
                //        long DDiff2 = GetDateDifference(DateArray[0], DateArray[2]);
                //        DCompare2 = GetDateCompare(DateArray[0], DateArray[2]);
                //        if (DDiff >= DCompare & DDiff2 <= DCompare2)
                //            return true;
                //        return false;
                //    }
                // }
            }
            else
            {
                if (CompareDatesWithTime(GetDate(DateFlag)) != 1)
                    return true;
                return false;
            }

            // return false;
        }
        public int CompareDates(DateTime CheckDate)
        {
            int result = DateTime.Compare(CheckDate.Date, DateTime.Now.Date);
            //Debug.Print("Compare dates: " + Strings.FormatDateTime(CheckDate, DateFormat.ShortDate) + " <-> " + Strings.FormatDateTime(DateTime.Now, DateFormat.ShortDate) + " = " + result);
            return result;
        }

        public int CompareDatesWithTime(DateTime CheckDate)
        {
            int result = DateTime.Compare(CheckDate, DateTime.Now);
            return result;
        }


        public long GetDateDifference(string DateVar, string DateString)
        {
            long DDiff = 0;
            //date1 > date2 -> 
            var date2 = DateTime.Now;
            var date1 = GetDate(DateVar);
            var timespan = (date2 - date1);


            if (DateString.IndexOf("SECOND", StringComparison.CurrentCultureIgnoreCase) != -1)
            {
                DDiff = (long)Math.Round(Common.Fix(timespan.TotalSeconds));
            }
            else if (DateString.IndexOf("MINUTE", StringComparison.CurrentCultureIgnoreCase) != -1)
            {
                DDiff = (long)Math.Round(Common.Fix(timespan.TotalMinutes)) * 60;
            }
            else if (DateString.IndexOf("HOUR", StringComparison.CurrentCultureIgnoreCase) != -1)
                DDiff = (long)Math.Round(Common.Fix(timespan.TotalHours)) * 3600;
            else if (DateString.IndexOf("DAY", StringComparison.CurrentCultureIgnoreCase) != -1)
                DDiff = (long)Math.Round(Common.Fix(timespan.TotalDays)) * 86400;
            else if (DateString.IndexOf("WEEK", StringComparison.CurrentCultureIgnoreCase) != -1)
                DDiff = (long)Math.Round(Common.Fix(timespan.TotalDays)) / 7 * 604800;
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

        public long GetDateCompare(string DateVar, string DateString)
        {
            /*
        long DDiff = 0;
        long Amount = Conversion.Val(DateString);

        if (Strings.UCase(DateString).Contains("SECOND"))
            DDiff = Amount;
        if (Strings.UCase(DateString).Contains("MINUTE"))
            DDiff = Amount * 60;
        if (Strings.UCase(DateString).Contains("HOUR"))
            DDiff = Amount * 3600;
        if (Strings.UCase(DateString).Contains("DAY"))
            DDiff = Amount * 86400;
        if (Strings.UCase(DateString).Contains("WEEK"))
            DDiff = Amount * 604800;
        if (Strings.UCase(DateString).Contains("MONTH"))
            DDiff = Amount * 2629746;
        if (Strings.UCase(DateString).Contains("YEAR"))
            DDiff = Amount * 31536000;

        return DDiff;
            */
            throw new NotImplementedException();
        }
    }

    public class Flags
    {
        private string _flagsFolder;
        private string _tempFlagsFolder;
        
        public Flags(string flagsFolder, string tempFlagsFolder)
        {
            _flagsFolder = flagsFolder;
            _tempFlagsFolder = tempFlagsFolder;
        }

        private string FlagPath(string name) => Path.Combine(_flagsFolder, name);
        private string TempFlagPath(string name) => Path.Combine(_tempFlagsFolder, name);

        /// <summary>Creates the given flag.</summary>
        /// 	''' <param name="FlagName">The flag name to set.</param>
        /// 	''' <param name="Temp">If set to true, the flag is temporary set otherwise permanent.</param>
        public void CreateFlag(string FlagName, bool Temp = false)
        {
            if (!Temp)
                FlagName = FlagPath(FlagName);
            else
                FlagName = TempFlagPath(FlagName);

            using (FileStream fs = new FileStream(FlagName, FileMode.Create))
            {
            }
        }
        /// <summary>Deletes the given flag. Deletes permanent and temporary flags.</summary>
        /// 	''' <param name="FlagName">The name of the flag to delete.</param>
        public void DeleteFlag(string FlagName)
        {
            File.Delete(FlagPath(FlagName));
            File.Delete(TempFlagPath(FlagName));
        }
        /// <summary> Checks if the given flag is set, permanent and temporary.</summary>
        /// 	''' <param name="FlagName">The flag name to search for.</param>
        /// 	''' <returns>Returns true if a permanent or temporary flag with the name is found.</returns>
        public bool FlagExists(string FlagName)
        {
            return File.Exists(FlagPath(FlagName)) || File.Exists(TempFlagPath(FlagName));
        }
    }

}