using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using dotNetTree;
using CodeSample.Words;

namespace CodeSample
{
    /// <summary>
    /// Pass from a word to another word using only existing words in a dictionary
    /// </summary>
    class WordToWord_01
    {
        /// <summary>
        /// Return numbers of difference
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        private static int iDistance (string s1, string s2)
        {
            int iDistance = int.MaxValue;

            if (s1 == null || s2 == null)
            {
                throw new ArgumentNullException("Input null");
            }
            else if (s1.Length != s2.Length)
            {
                throw new ArgumentException("Both string must have the same length");
            }

            s1 = s1.ToLower();
            s2 = s2.ToLower();

            iDistance = 0;
            for (int i=0; i<s1.Length; i++)
            {
                if (s1[i] != s2[i])
                {
                    iDistance++;
                }
            }

            return iDistance;
        }

        /// <summary>
        /// Return existing words changing only one letter
        /// </summary>
        /// <param name="sOldString">Initial string</param>
       /// <returns></returns>
        private static List<string> GetNewStrings(string sWord, ThreeLetterWordsDictionary dicc)
        {
            List<string> listNewStrings = new List<string>();

            if (string.IsNullOrEmpty(sWord))
            {
                sWord = string.Empty;
            }

            if (dicc != null)
            {
                for (int i = 0; i < sWord.Length; i++)
                {
                    char letter = sWord.ToLower()[i];
                    for (char l = 'a'; l <= 'z'; l++)
                    {
                        if (l == sWord[i]) continue;

                        StringBuilder sNewWord = new StringBuilder(sWord);
                        sNewWord[i] = l;
                        if (dicc.Contains(sNewWord.ToString()))
                        {
                            listNewStrings.Add(sNewWord.ToString());
                        }
                    }
                }
            }
            
            return listNewStrings;
        }

        /// <summary>
        /// Find a solution (stop recursion when first solution is found)
        /// </summary>
        /// <param name="sIni"></param>
        /// <param name="sEnd"></param>
        /// <param name="dicc"></param>
        /// <param name="lPreviousExplored"></param>
        /// <param name="lResultSecuence"></param>
        /// <returns></returns>
        private static bool FindSecuenceFirst (string sIni, string sEnd, ThreeLetterWordsDictionary dicc, List<string> lPreviousExplored, ref List<string> lResultSecuence)
        {
            if (lPreviousExplored == null)
            {
                lPreviousExplored = new List<string>();
            }
            if (lResultSecuence == null)
            {
                lResultSecuence = new List<string>();
            }

            if (iDistance (sIni, sEnd) == 1)
            {
                // Solution found!
                //System.Diagnostics.Debug.WriteLine(String.Format("Path with solution: {0}", lResultSecuence.ToStringItem()));
                return true;
            }
            else
            {
                // Available choices
                List<string> lCandidate = GetNewStrings(sIni, dicc);
                lCandidate = lCandidate.Except(lPreviousExplored).ToList();
                lPreviousExplored.AddRange(lCandidate);

                // No more choices!
                if (lCandidate.Count == 0)
                {
                    // Path without solution
                    // System.Diagnostics.Debug.WriteLine(String.Format("Path without solution: {0}", lResultSecuence.ToStringItem()));
                    return false;
                }

                // Explore all available choices
                foreach (string s in lCandidate)
                {
                    lResultSecuence.Add(s);

                    if (FindSecuenceFirst(s, sEnd, dicc, lPreviousExplored, ref lResultSecuence))
                    {
                        // Stop recursion when a solution is found
                        return true;
                    }

                    lResultSecuence.RemoveAt(lResultSecuence.Count - 1);
                }

                return false;
            }
        }

        /// <summary>
        /// Find All Solution
        /// </summary>
        /// <param name="sIni"></param>
        /// <param name="sEnd"></param>
        /// <param name="dicc"></param>
        /// <param name="lPreviousExplored"></param>
        /// <param name="lResultSecuence"></param>
        private static void FindSecuenceAll(string sIni, string sEnd, ThreeLetterWordsDictionary dicc, Dictionary<string, int> diccPreviousExplored, ref List<string> lResultSecuence)
        {
            if (diccPreviousExplored == null)
            {
                diccPreviousExplored = new Dictionary<string, int>();
            }
            if (lResultSecuence == null)
            {
                lResultSecuence = new List<string>();
            }

            if (iDistance(sIni, sEnd) == 1)
            {
                // Solution found!
                System.Diagnostics.Debug.WriteLine(String.Format("Path with solution ({0} words): {1}", lResultSecuence.Count, lResultSecuence.ToStringItem()));
                return;
            }
            else
            {
                // Available choices
                List<string> lCandidate = GetNewStrings(sIni, dicc);

                // Remove choices if it have been visited in previous stage
                int iPosition = lResultSecuence.Count;
                for (int i=lCandidate.Count-1; i>=0; i--)
                {
                    if (!diccPreviousExplored.ContainsKey (lCandidate[i]))
                    {
                        diccPreviousExplored.Add(lCandidate[i], iPosition);
                    }
                    else
                    {
                        if (diccPreviousExplored[lCandidate[i]] < iPosition)
                        {
                            lCandidate.RemoveAt(i);
                        }
                        else
                        {
                            diccPreviousExplored[lCandidate[i]] = iPosition;
                        }
                    }
                }

                // Explore all available choices
                foreach (string s in lCandidate)
                {
                    lResultSecuence.Add(s);

                    // Continue recursion in order to found all solution
                    FindSecuenceAll(s, sEnd, dicc, diccPreviousExplored, ref lResultSecuence);
                   
                    lResultSecuence.RemoveAt(lResultSecuence.Count - 1);                  
                }
            }
        }

        /// <summary>
        /// Test
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string sIni = "ado";
            string sEnd = "ale";
            Words.ThreeLetterWordsDictionary wordsDictionary = new ThreeLetterWordsDictionary();

            List<string> lResultSecuence = new List<string>();
            FindSecuenceAll(sIni, sEnd, wordsDictionary, null, ref lResultSecuence);
           
        }
    }
}
