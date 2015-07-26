using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using dotNetTree;

namespace TreeSample
{
    class BinarySearchTreeSample
    {
        static void Main(string[] args)
        {
           // BinaryNode<int> bst = GetSampleTree();
            BinaryNode<int> bst = GetBalancedTree(50);
            WriteOutput( bst.Print());

            // Max Deepth
            WriteOutput(string.Format ("Max Deepth: {0}", bst.MaxDepth()));

            //BFS: Breadth-first_search
            WriteOutput(string.Format ("BFS: Breadth-first_search: {0}", bst.TraverseBFS().ToStringItem()));

            //DFS: Depth-first search
            WriteOutput(string.Format("DFS: Depth-first search: {0}", bst.TraverseDFS().ToStringItem()));

            //Preorder
            WriteOutput(string.Format("Preorder: {0}", bst.TraversePreorderRec().ToStringItem()));

            //Inorder
            WriteOutput(string.Format("Inorder: {0}", bst.TraverseInorderRec().ToStringItem()));

            //Postorder
            WriteOutput(string.Format("Postorder: {0}", bst.TraversePostorderRec().ToStringItem()));

            //Search
            BinaryNode<int> node = bst.Search(7);
            node = bst.Search(50);
        }

        static void WriteOutput (string s)
        {
            System.Console.WriteLine(s);
            System.Diagnostics.Debug.WriteLine(s);
        }

        static BinaryNode<int> GetSampleTree()
        {
            int[] content = { 5, 3, 7, 2, 4, 6, 8 };

            BinaryNode<int> root = new BinaryNode<int>(content[0]);

            for (int i = 1; i < content.Length; i++)
            {
                root.Add(content[i]);
            }

            return root;
        }

        /// <summary>
        /// Create a balanced binary search tree with n nodes
        /// </summary>
        static BinaryNode<int> GetBalancedTree (int n)
        {
            List<int> list = new List<int>(n);
            for (int i = 1; i <= n; i++) list.Add(i);

            List<int> balancedList = GetBalancedList(list);

            BinaryNode<int> root = new BinaryNode<int>(balancedList[0]);
            for (int i = 1; i < balancedList.Count; i++)
            {
                root.Add(balancedList[i]);
            }

            return root;
        }

        /// <summary>
        /// Prepare a list in order to create a balanced binary search tree
        /// </summary>
        static List<int> GetBalancedList(List<int> list)
        {
            List<int> result = new List<int>();

            if (list.Count > 0)
            {
                int iMiddle = list.Count / 2;
                result.Add(list[iMiddle]);
                result.AddRange(GetBalancedList(new List<int>(list.GetRange(0, iMiddle))));
                result.AddRange(GetBalancedList(new List<int>(list.GetRange(iMiddle + 1, list.Count-iMiddle-1))));
            }

            return result;
        }
    }
}
