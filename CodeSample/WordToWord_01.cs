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
    /// 
    /// Bactracking template: Find single solution
    /// 
    /// findSolutions(n, other params)
    /// {
    /// 
    ///     if (found a solution) {
    ///         displaySolution();
    ///         return
    ///         true
    ///         ;
    ///     }
    /// 
    ///     for (val = first to last)
    ///     {
    ///         if (isValid(val, n))
    ///         {
    ///             applyValue(val, n);
    ///             if
    ///             (findSolutions(n + 1, other params))
    ///                 return true;
    ///             removeValue(val, n);
    ///        }
    ///     }
    /// 
    ///     return false;
    /// }
    /// 
    /// Bactracking template: Find all solution
    /// 
    /// void findSolutions(n, other params) {
    /// 
    ///     if (found a solution) {
    ///         solutionsFound++;
    ///         displaySolution();
    ///         if (solutionsFound >= solutionTarget)
    ///             System.exit(0);
    ///         return;
    ///     }  
    ///
    ///     for (val = first to last) {
    ///         if (isValid(val, n)) {
    ///             applyValue(val, n);
    ///             findSolutions(n + 1, other params);
    ///             removeValue(val, n);
    ///         }
    ///     }
    /// 
    /// }
    /// 
    /// </summary>
    class WordToWord_01
    {
        /// <summary>
        /// Return numbers of differents char
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
        private static List<string> GetNewStrings(string sWord, WordDictionary dicc)
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
        private static bool FindSecuenceFirst (string sIni, string sEnd, WordDictionary dicc, List<string> lPreviousExplored, ref List<string> lResultSecuence)
        {
            // Validate input
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
                System.Diagnostics.Debug.WriteLine(String.Format("Path with solution: {0}", lResultSecuence.ToStringItem()));
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
        /// Find shorted path to a solution (explore all path in order to find the minimun number of intermediate words)
        /// </summary>
        /// <param name="sIni"></param>
        /// <param name="sEnd"></param>
        /// <param name="dicc"></param>
        /// <param name="lPreviousExplored"></param>
        /// <param name="lStepSecuence"></param>
        private static void FindSecuenceMin(string sIni, string sEnd, WordDictionary dicc, Dictionary<string, int> diccPreviousExplored, List<string> lStepSecuence, ref int iMin, ref List<string> lMinSecuence)
        {
            // Validate input
            if (diccPreviousExplored == null)
            {
                diccPreviousExplored = new Dictionary<string, int>();
            }
            if (lStepSecuence == null)
            {
                lStepSecuence = new List<string>();
            }

            if (lStepSecuence.Count >= iMin)
            {
                // A previous solution is better. Stop exploring this branch.
                return;
            }
            if (iDistance(sIni, sEnd) == 1)
            {
                // Solution found!
                iMin = lStepSecuence.Count;
                lMinSecuence = new List<string>(lStepSecuence);
                //System.Diagnostics.Debug.WriteLine(String.Format("Path with solution ({0} words): {1}", lStepSecuence.Count, lStepSecuence.ToStringItem()));
                return;
            }
            else
            {
                // Available choices
                List<string> lCandidate = GetNewStrings(sIni, dicc);

                // Remove a choice if it have been visited in previous stage
                int iPosition = lStepSecuence.Count;
                for (int i=lCandidate.Count-1; i>=0; i--)
                {
                    if (!diccPreviousExplored.ContainsKey (lCandidate[i]))
                    {
                        // Choice not explored yet
                        diccPreviousExplored.Add(lCandidate[i], iPosition);
                    }
                    else
                    {
                        // Choice explored before
                        if (diccPreviousExplored[lCandidate[i]] < iPosition)
                        {
                            // The explored options is better, the word was in a low secuence position.
                            // Discard this choice!
                            lCandidate.RemoveAt(i);
                        }
                        else
                        {
                            // Current options is better, the word currently is in a low secuence position.
                            // Give it a chance...
                            diccPreviousExplored[lCandidate[i]] = iPosition;
                        }
                    }
                }

                // Explore available choices
                foreach (string s in lCandidate)
                {
                    lStepSecuence.Add(s);

                    // Continue recursion in order to found all solution
                    FindSecuenceMin(s, sEnd, dicc, diccPreviousExplored, lStepSecuence, ref iMin, ref lMinSecuence);
                   
                    lStepSecuence.RemoveAt(lStepSecuence.Count - 1);                  
                }
            }
        }

        /// <summary>
        /// Testing algorithm
        /// </summary>
        /// <param name="args"></param>
        public static List<string> WordToWord (string sIni, string sEnd)
        {
            List<string> lSolution = new List<string>();
            Words.WordDictionary wordsDictionary = new WordDictionary();

            // Find a Solution
            System.Diagnostics.Debug.WriteLine("FindSecuenceFirst:");
            List<string> lSecuence = new List<string>();
            FindSecuenceFirst(sIni, sEnd, wordsDictionary, null, ref lSecuence);

            if (lSecuence.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("Path not found");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(string.Format("[{0} Steps] {1} {2} {3}", lSecuence.Count, sIni, lSecuence.ToStringItem(), sEnd));
            }

            // Find the shorted path to a solution
            System.Diagnostics.Debug.WriteLine("FindSecuenceAll:");
            List<string> lMinSecuence = new List<string>();
            int iMin = int.MaxValue;
            FindSecuenceMin(sIni, sEnd, wordsDictionary, null, null, ref iMin, ref lMinSecuence);

            if (iMin == int.MaxValue)
            {
                System.Diagnostics.Debug.WriteLine("Path not found");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(string.Format("[{0} Steps] {1} {2} {3}", lMinSecuence.Count, sIni, lMinSecuence.ToStringItem(), sEnd));
            }

            return lMinSecuence;
        }
    }
}
