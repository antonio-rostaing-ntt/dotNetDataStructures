using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace dotNetTree
{
    public static class NodeExtension
    {
        /// <summary>
        /// BFS print with one line per level
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string Print<T>(this Node<T> node)
        {
            StringBuilder sb = new StringBuilder();

            Queue<Node<T>> q = new Queue<Node<T>>();

            q.Enqueue(node);

            int iCurrent = 1;
            int iNext = 0;

            while (q.Count > 0)
            {
                node = q.Dequeue();
                iCurrent -= 1;

                sb.AppendFormat("{0} ", node.Value);

                if (node.children != null)
                {
                    foreach (BinaryNode<T> child in node.children)
                    {
                        if (child != null)
                        {
                            q.Enqueue(child);
                            iNext++;
                        }
                    }
                }

                if (iCurrent == 0)
                {
                    // Finish printing current level
                    int iAux = iCurrent;
                    iCurrent = iNext;
                    iNext = iAux;
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// BFS: Breadth-first_search
        /// q = empty queue
        /// q.Enqueue(root)
        /// while not q.empty do
        ///     node := q.Dequeue()
        ///     visit(node)
        ///     Enqueue all children
        /// </summary>
        public static List<T> TraverseBFS<T>(this Node<T> node)
        {
            List<T> result = new List<T>();

            Queue<Node<T>> q = new Queue<Node<T>>();

            q.Enqueue(node);

            while (q.Count > 0)
            {
                node = q.Dequeue();
                result.Add(node.Value);

                if (node.children != null)
                {
                    foreach (BinaryNode<T> child in node.children)
                    {
                        if (child != null) q.Enqueue(child);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// DFS: Depth-first search
        /// s = stack empty
        /// s.Push(root)
        /// while not s.empty do
        ///     node := q.Pop()
        ///     visit(node)
        ///     Push all children
        /// </summary>
        public static List<T> TraverseDFS<T> (this Node<T> node)
        {
            List<T> result = new List<T>();

            Stack<Node<T>> s = new Stack<Node<T>>();

            s.Push(node);

            while (s.Count > 0)
            {
                node = s.Pop();
                result.Add(node.Value);

                if (node.children != null)
                {
                    for (int i = node.children.Count-1; i >= 0; i--)
                    {
                        if (node.children[i]!=null)
                        {
                            s.Push(node.children[i]);
                        }
                    }
                }
            }

            return result;
        }
    }
}
