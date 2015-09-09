using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CodeSample.Words;

namespace CodeSample
{
    
    public class PhoneKeyboard
    {
        /// <summary>
        /// Relation between phone numbers and letters
        /// Key: Keyboard number
        /// Value: List of letters associated to the key
        /// </summary>
        private static Dictionary<int, List<char>> diccPhoneKeyboard;

        /// <summary>
        /// Static constructor: Initialize diccPhoneKeyboard
        /// </summary>
        static PhoneKeyboard()
        {
            diccPhoneKeyboard = new Dictionary<int, List<char>>();
            diccPhoneKeyboard.Add(2, new List<char>(){ 'a', 'b', 'c' });
            diccPhoneKeyboard.Add(3, new List<char>(){ 'd', 'e', 'f' });
            diccPhoneKeyboard.Add(4, new List<char>(){ 'g', 'h', 'i' });
            diccPhoneKeyboard.Add(5, new List<char>(){ 'j', 'k', 'l' });
            diccPhoneKeyboard.Add(6, new List<char>(){ 'm', 'n', 'o' });
            diccPhoneKeyboard.Add(7, new List<char>(){ 'p', 'q', 'r', 's' });
            diccPhoneKeyboard.Add(8, new List<char>(){ 't', 'u', 'v' });
            diccPhoneKeyboard.Add(9, new List<char>(){ 'w', 'x', 'y', 'z' });
        }

        /// <summary>
        /// Generate new strings adding the letters asociated to the given number to the end of given string
        /// </summary>
        /// <param name="sOldString">Initisl string</param>
        /// <param name="iNumber">Keyboard number</param>
        /// <returns></returns>
        private static List<string> GetNewStrings (string sOldString, int iNumber)
        {
            List<string> listNewStrings = new List<string>();

            if (string.IsNullOrEmpty (sOldString))
            {
                sOldString = string.Empty;
            }

            if (diccPhoneKeyboard.ContainsKey(iNumber))
            {
                foreach (char letter in diccPhoneKeyboard[iNumber])
                {
                    listNewStrings.Add(sOldString + letter);
                }
            }

            return listNewStrings;
        }

        /// <summary>
        /// Get a list of words given a secuence of phone keyboards numbers
        /// </summary>
        /// <param name="listNumber"></param>
        /// <returns></returns>
        public static List<string> GetWords (List<int> listNumber)
        {
            List<string> listWords = new List<string>();

            // Validate input
            if (listNumber == null || listNumber.Count == 0)
            {
                throw new ArgumentNullException("listNumber", "List null or empty");
            }
            if (listNumber.Exists (p=> p < 2 || p > 9))
            {
                throw new ArgumentOutOfRangeException("listNumber", "Numbers must be in range 2 - 9");
            }

            // Initialize queue 
            Queue<string> queue = new Queue<string>();
            foreach (string s in GetNewStrings(string.Empty, listNumber[0]))
            {
                queue.Enqueue(s);
            }
            
            // Generate possible words
            for (int i=1; i<listNumber.Count; i++)
            {
                while (queue.Count > 0 && queue.Peek().Count() <= i)
                {
                    foreach (string s in GetNewStrings(queue.Dequeue(), listNumber[i]))
                    {
                        queue.Enqueue(s);
                    }
                }
            }

            // Check possible words with a dictionary
            WordDictionary dicc = new WordDictionary();
            while (queue.Count > 0)
            {
                string sAux = queue.Dequeue();
                if (dicc.Contains(sAux))
                {
                    listWords.Add(sAux);
                }

                // Add all words generate
                // listWords.Add(queue.Dequeue());
            }
            return listWords;
        }

        /// <summary>
        /// Test
        /// </summary>
        /// <param name="args"></param>
        static void Mainn(string[] args)
        {
            List<int> listNumber = new List<int>() { 2,2,7,2 };
            List<string> listWord = GetWords(listNumber);

        }
    }
}
