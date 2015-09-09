using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using dotNetTree;

namespace CodeSample
{
    class RunSample
    {
        static void Main(string[] args)
        {
            // Phonekeyboard 
            // Get a list of words given a secuence of phone keyboards numbers    
            List<string> listWord1 = PhoneKeyboard.GetWords(new List<int>() { 6, 2, 7 });
            System.Diagnostics.Debug.WriteLine(listWord1.ToStringItem());

            // WordToWord_01
            // Pass from a word to another word using only existing words in a dictionary
            //string s1 = "cat";
            //string s2 = "dog";
            //List<string> listWord2 = WordToWord_01.WordToWord(s1, s2);
            //System.Diagnostics.Debug.WriteLine(string.Format("{0} -> {1} -> {2}", s1, listWord2.ToStringItem(), s2));

        }
    }
}
