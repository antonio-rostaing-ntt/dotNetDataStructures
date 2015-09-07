using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;

namespace CodeSample.Words
{
    public class ThreeLetterWordsDictionary
    {
        private static HashSet<string> hashSet;

        static ThreeLetterWordsDictionary()
        {
            try
            {
                hashSet = new HashSet<string>();
                using (System.IO.StreamReader sr = new System.IO.StreamReader(@".\Words\Three-Letter Word List.txt"))
                {
                    string line;
                    
                    while ((line = sr.ReadLine()) != null)
                    {
                        hashSet.Add(line);
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
