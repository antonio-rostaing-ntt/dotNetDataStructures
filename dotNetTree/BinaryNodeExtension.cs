using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace dotNetTree
{
    public static class BinaryNodeExtension
    {
        //  Operation	            Best Case Running Time	    Worst Case Running Time
        //  ---------------------------------------------------------------------------
        //  Search	                log2 n	                    n
        //  Add	                    log2 n	                    n
        //  Remove	                log2 n	                    n
        //  Preorder Traversal	 	                            n
        //  Inorder Traversal	 	                            n
        //  Postorder Traversal	 	                            n
        //
        // Net. Framework balanced Trees
        // - SortedDictionary<K,V>: Red-black tree based map of key-value pairs
        // - OrderedSet<T>:         Red-black tree based set of elements

        #region GetEnumerator

        public static IEnumerator GetEnumerator<T>(this BinaryNode<T> node)
        {
            return TraverseInorderRec(node).GetEnumerator();
        }

        #endregion

        #region Basic Operations

        public static BinaryNode<T> Search<T>(this BinaryNode<T> node, T data)
        {
            BinaryNode<T> targetNode = node;

            // Default comparer
            Comparer<T> comparer = Comparer<T>.Default;
            int result;

            while (targetNode != null)
            {
                result = comparer.Compare(targetNode.Value, data);

                if (result == 0)
                {
                    // We found data
                    break;
                }
                else if (result > 0)
                {
                    // Current.Value > data, search current's left subtree
                    targetNode = targetNode.left;
                }
                else if (result < 0)
                {
                    // Current.Value < data, search current's right subtree
                    targetNode = targetNode.right;
                }
            }

           return targetNode;
        }

        public static bool Add<T>(this BinaryNode<T> node, T data)
        {
            bool bInserted = false;

            // Default comparer
            Comparer<T> comparer = Comparer<T>.Default;

            // Create a new Node instance
            BinaryNode<T> newNode = new BinaryNode<T>(data);
            int result;

            // Now, insert n into the tree
            // Trace down the tree until we hit a NULL
            BinaryNode<T> current = node, parent = null;
            while (current != null)
            {
                result = comparer.Compare(current.Value, data);
                if (result == 0)
                {
                    // They are equal - attempting to enter a duplicate - do nothing
                    return false;
                }
                else if (result > 0)
                {
                    // Current.Value > data, must add n to current's left subtree
                    parent = current;
                    current = current.left;
                }
                else if (result < 0)
                {
                    // Current.Value < data, must add n to current's right subtree
                    parent = current;
                    current = current.right;
                }
            }

            // We're ready to add the node!
            bInserted = true;
            if (parent == null)
            {
                // The tree was empty, make n the root
                node = newNode;
            }
            else
            {
                result = comparer.Compare(parent.Value, data);
                if (result > 0)
                {
                    // parent.Value > data, therefore n must be added to the left subtree
                    parent.left = newNode;
                }
                else
                {
                    // parent.Value < data, therefore n must be added to the right subtree
                    parent.right = newNode;
                }
            }

            return bInserted;
        }

        public static bool Remove<T>(this BinaryNode<T> node, T data)
        {
            bool bDeleted = false;

            // Default comparer
            Comparer<T> comparer = Comparer<T>.Default;

            // First make sure there exist some items in this tree
            if (node == null)
            {
                return false;       // no items to remove
            }

            // Now, try to find data in the tree
            BinaryNode<T> current = node, parent = null;
            int result = comparer.Compare(current.Value, data);
            while (result != 0)
            {
                if (result > 0)
                {
                    // current.Value > data, if data exists it's in the left subtree
                    parent = current;
                    current = current.left;
                }
                else if (result < 0)
                {
                    // current.Value < data, if data exists it's in the right subtree
                    parent = current;
                    current = current.right;
                }

                // If current == null, then we didn't find the item to remove
                if (current == null)
                {
                    return false;
                }
                else
                {
                    result = comparer.Compare(current.Value, data);
                }
            }

            // At this point, we've found the node to remove
            bDeleted = true;

            // We now need to "rethread" the tree
            // CASE 1: If current has no right child, then current's left child becomes
            //         the node pointed to by the parent
            if (current.right == null)
            {
                if (parent == null)
                {
                    node = current.left;
                }
                else
                {
                    result = comparer.Compare(parent.Value, current.Value);
                    if (result > 0)
                    {
                        // parent.Value > current.Value, so make current's left child a left child of parent
                        parent.left = current.left;
                    }
                    else if (result < 0)
                    {
                        // parent.Value < current.Value, so make current's left child a right child of parent
                        parent.right = current.left;
                    }
                }
            }
            // CASE 2: If current's right child has no left child, then current's right child
            //         replaces current in the tree
            else if (current.right.left == null)
            {
                current.right.left = current.left;

                if (parent == null)
                {
                    node = current.right;
                }
                else
                {
                    result = comparer.Compare(parent.Value, current.Value);
                    if (result > 0)
                    {
                        // parent.Value > current.Value, so make current's right child a left child of parent
                        parent.left = current.right;
                    }
                    else if (result < 0)
                    {
                        // parent.Value < current.Value, so make current's right child a right child of parent
                        parent.right = current.right;
                    }
                }
            }
            // CASE 3: If current's right child has a left child, replace current with current's
            //          right child's left-most descendent
            else
            {
                // We first need to find the right node's left-most child
                BinaryNode<T> leftmost = current.right.left, lmParent = current.right;
                while (leftmost.left != null)
                {
                    lmParent = leftmost;
                    leftmost = leftmost.left;
                }

                // The parent's left subtree becomes the leftmost's right subtree
                lmParent.left = leftmost.right;

                // Assign leftmost's left and right to current's left and right children
                leftmost.left = current.left;
                leftmost.right = current.right;

                if (parent == null)
                {
                    node = leftmost;
                }
                else
                {
                    result = comparer.Compare(parent.Value, current.Value);
                    if (result > 0)
                    {
                        // parent.Value > current.Value, so make leftmost a left child of parent
                        parent.left = leftmost;
                    }
                    else if (result < 0)
                    {
                        // parent.Value < current.Value, so make leftmost a right child of parent
                        parent.right = leftmost;
                    }
                }
            }

            return bDeleted;
        }

        public static int MaxDepth<T> (this BinaryNode<T> node)
        {
            if (node == null)
                return 0;
            else
            {
                // Compute the depth of each subtree
                int lDepth = MaxDepth(node.left);
                int rDepth = MaxDepth(node.right);

                // Use the larger one
                return lDepth > rDepth ? lDepth + 1 : rDepth + 1;
            }
        } 

        #endregion

        #region Traversal Methods. Preorder, Inorder, PostOrder

        public static List<T> TraversePreorderRec<T>(this BinaryNode<T> node)
        {
            List<T> result = new List<T>();

            if (node != null) 
            {
                result.Add(node.Value);
                result.AddRange(TraversePreorderRec(node.left));
                result.AddRange(TraversePreorderRec(node.right));
            }

            return result;
        }

        public static List<T> TraverseInorderRec<T>(this BinaryNode<T> node)
        {
            List<T> result = new List<T>();

            if (node != null)
            {
                result.AddRange(TraverseInorderRec(node.left));
                result.Add(node.Value);
                result.AddRange(TraverseInorderRec(node.right));
            }

            return result;
        }

        public static List<T> TraversePostorderRec<T>(this BinaryNode<T> node)
        {
            List<T> result = new List<T>();

            if (node != null)
            {
                result.AddRange(TraversePostorderRec(node.left));
                result.AddRange(TraversePostorderRec(node.right));
                result.Add(node.Value);
            }

            return result;
        }

        #endregion
    }
}
