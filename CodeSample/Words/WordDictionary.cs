using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSample.Words
{
    /// <summary>
    /// Word's dictionary in order to test some algorithms
    /// </summary>
    public class WordDictionary
    {
        // Test files
        private static List<string> listFiles = new List<string>()
            { @".\Words\Three-Letter Word List.txt",
            @".\Words\Four-Letter Word List.txt" };

        // Words collection
        private static HashSet<string> hashSet;

        //Static constructor
        static WordDictionary()
        {
            try
            {
                hashSet = new HashSet<string>();
                foreach (string sFile in listFiles)
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(sFile))
                    {
                        string line;

                        while ((line = sr.ReadLine()) != null)
                        {
                            hashSet.Add(line);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException("File not found", e);
            }
        }

        public bool Contains (string sWord)
        {
            return hashSet.Contains(sWord);
        }
    }
}
