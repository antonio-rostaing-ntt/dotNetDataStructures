using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetTree
{
    public static class GlobalExtension
    {
        /// <summary>
        /// Generic extension to print list content as string.
        /// </summary>
        public static string ToStringItem<T>(this List<T> list)
        {
            StringBuilder sb = new StringBuilder();

            if (list != null && list.Count > 0)
            {
                foreach (T element in list)
                {
                    sb.AppendFormat("{0} ", element.ToString());
                }
            }

            // Remove the last separator
            string results = sb.ToString();
            if (results.Length == 0)
            {
                return String.Empty;
            }
            else
            {
                return results.Substring(0, results.Length - 1);
            }
        }
    }
}
