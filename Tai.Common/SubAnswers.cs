using System;
using System.Collections.Generic;
using System.Text;
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
    using Microsoft.VisualBasic;

    [Serializable]
    public class subAnswers
    {
        private List<string> checkList = new List<string>();
        private List<string> answerList = new List<string>();

        public subAnswers(ISettings settings)
        {
            checkList.Add(settings.SubGreeting);
            checkList.Add(settings.SubYes);
            checkList.Add(settings.SubNo);
            checkList.Add(settings.SubSorry);
            checkList.Add("thank,thanks");
            checkList.Add("please");
        }

        public string returnWords(string s)
        {
            switch (s)
            {
                case "hi":
                    {
                        return checkList[0];
                    }

                case "yes":
                    {
                        return checkList[1];
                    }

                case "no":
                    {
                        return checkList[2];
                    }

                case "sorry":
                    {
                        return checkList[3];
                    }

                case "thanks":
                    {
                        return checkList[4];
                    }

                case "please":
                    {
                        return checkList[5];
                    }

                default:
                    {
                        return checkList[0];
                    }
            }
        }

        public List<string> returnAll()
        {
            return checkList;
        }

        public List<string> returnAnswerList()
        {
            return answerList;
        }

        public string returnSystemWord(string wordList)
        {
            for (int i = 0; i <= checkList.Count() - 1; i++)
            {
                string[] list = SessionState.obtainSplitParts(checkList[i], false);
                for (int n = 0; n <= list.Count() - 1; n++)
                {
                    
                    if (string.Equals(wordList.Trim(), list[n].Trim(),StringComparison.CurrentCultureIgnoreCase))
                    {
                        switch (i)
                        {
                            case 0:
                                {
                                    return "hi";
                                }

                            case 1:
                                {
                                    return "yes";
                                }

                            case 2:
                                {
                                    return "no";
                                }

                            case 3:
                                {
                                    return "sorry";
                                }

                            case 4:
                                {
                                    return "thanks";
                                }

                            case 5:
                                {
                                    return "please";
                                }

                            default:
                                {
                                    return "hi";
                                }
                        }
                    }
                }
            }
            return "";
        }

        public void addToAnswerList(string words)
        {
            var split = words.Split(',');
            for (int i = 0; i <= split.Count() - 1; i++)
                answerList.Add(split[i].Trim());
        }

        public void clearAnswers()
        {
            answerList.Clear();
        }

        public string triggerWord(string chatstring)
        {

            // we first order the list based on lenght of the answer option (and if equal lenght, by the order in which they are in the answer list)

            var sorted = answerList.OrderByDescending(x => x.Length).ThenBy(x => answerList.IndexOf(x)).ToArray();

            // we then check only the answers with more than 1 word to see if the chat strings contain any of them

            for (int i = 0; i <= sorted.Count() - 1; i++)
            {
                if (sorted[i].Contains(' ')) // Strings.InStr(sorted[i], " ") > 0)
                {
                    if (chatstring.ToLower().Contains(sorted[i].Trim().ToLower())) //  Strings.LCase(chatstring).Contains(Strings.LCase(sorted[i]).Trim()))
                        return sorted[i].Trim();
                }
            }

            // if all multiple words answers didn't return an answer, we check for the single words in the chat to see if any of them matches

            var singleWords = SessionState.obtainSplitParts(chatstring, true);
            for (int i = 0; i <= singleWords.Count() - 1; i++)
            {
                for (int n = 0; n <= answerList.Count - 1; n++)
                {
                    if (string.Equals(answerList[n],singleWords[i],StringComparison.CurrentCultureIgnoreCase))
                        return singleWords[i];
                }
            }
            return "";
        }

        public int answerNumber()
        {
            return answerList.Count;
        }
    }

}
