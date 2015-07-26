using System;
using System.Collections;
using System.Text;

namespace skmDataStructures
{
	/// <summary>
	/// Summary description for BinaryTree.
	/// </summary>
	public class BST : ICloneable, IEnumerable
	{
		private Node root = null;
		private int count = 0;

		public BST() {}

		#region Public Methods
		public virtual void Clear()
		{
			root = null;
			count = 0;
		}

		public virtual object Clone()
		{
			BST clone = new BST();
			clone.root = (Node) root.Clone();
			clone.count = this.count;
			return clone;
		}

		#region GetEnumerator
		public IEnumerator GetEnumerator()
		{
			// dump the contents of the BST's Inorder traversal into an ArrayList and
			// then return this ArrayList's enumerator
			ArrayList contents = new ArrayList(count);
			InorderTraversalBuildup(root, contents);
			return contents.GetEnumerator();
		}

		protected virtual void InorderTraversalBuildup(Node current, ArrayList contents)
		{
			if (current != null)
			{
				InorderTraversalBuildup(current.Left, contents);
				contents.Add(current);
				InorderTraversalBuildup(current.Right, contents);
			}
		}
		#endregion

		#region Add
		public virtual void Add(IComparable data)
		{
			// first, create a new Node
			Node n = new Node(data);
			int result;

			// now, insert n into the tree
			// trace down the tree until we hit a NULL
			Node current = root, parent = null;
			while (current != null)
			{
				result = current.Value.CompareTo(n.Value);
				if (result == 0)
					// they are equal - inserting a duplicate - do nothing
					return;
				else if (result > 0)
				{
					// current.Value > n.Value
					// therefore, n must be added to current's left subtree
					parent = current;
					current = current.Left;
				}
				else if (result < 0)
				{
					// current.Value < n.Value
					// therefore, n must be added to current's right subtree
					parent = current;
					current = current.Right;
				}
			}

			// ok, at this point we have reached the end of the tree
			count++;
			if (parent == null)
				// the tree was empty
				root = n;
			else
			{
				result = parent.Value.CompareTo(n.Value);
				if (result > 0)
					// parent.Value > n.Value
					// therefore, n must be added to parent's left subtree
					parent.Left = n;
				else if (result < 0)
					// parent.Value < n.Value
					// therefore, n must be added to parent's right subtree
					parent.Right = n;
			}
		}
		#endregion

		#region Contains
		public virtual bool Contains(IComparable data)
		{
			return Search(data) != null;
		}
		#endregion

		#region Search
		public virtual Node Search(IComparable data)
		{
			return SearchHelper(root, data);
		}

		protected virtual Node SearchHelper(Node current, IComparable data)
		{
			if (current == null)
				return null;	// node was not found
			else
			{
				int result = current.Value.CompareTo(data);
				if (result == 0)
					// they are equal - we found the data
					return current;
				else if (result > 0)
				{
					// current.Value > n.Value
					// therefore, if the data exists it is in current's left subtree
					return SearchHelper(current.Left, data);
				}
				else // result < 0
				{
					// current.Value < n.Value
					// therefore, if the data exists it is in current's right subtree
					return SearchHelper(current.Right, data);
				}
			}
		}
		#endregion

		#region Delete
		public void Delete(IComparable data)
		{
			// find n in the tree
			// trace down the tree until we hit n
			Node current = root, parent = null;
			int result = current.Value.CompareTo(data);
			while (result != 0 && current != null)
			{				
				if (result > 0)
				{
					// current.Value > n.Value
					// therefore, n must be added to current's left subtree
					parent = current;
					current = current.Left;
				}
				else if (result < 0)
				{
					// current.Value < n.Value
					// therefore, n must be added to current's right subtree
					parent = current;
					current = current.Right;
				}

				result = current.Value.CompareTo(data);
			}

			// if current == null, then we did not find the item to delete
			if (current == null)
				throw new Exception("Item to be deleted does not exist in the BST.");


			// at this point current is the node to delete, and parent is its parent
			count--;
			
			// CASE 1: If current has no right child, then current's left child becomes the
			// node pointed to by the parent
			if (current.Right == null)
			{
				if (parent == null)
					root = current.Left;
				else
				{
					result = parent.Value.CompareTo(current.Value);
					if (result > 0)
						// parent.Value > current
						// therefore, the parent's left subtree is now current's Left subtree
						parent.Left = current.Left;
					else if (result < 0)
						// parent.Value < current.Value
						// therefore, the parent's right subtree is now current's left subtree
						parent.Right = current.Left;
				}
			}
			// CASE 2: If current's right child has no left child, then current's right child replaces
			// current in the tree
			else if (current.Right.Left == null)
			{
				if (parent == null)
					root = current.Right;
				else
				{
					result = parent.Value.CompareTo(current.Value);
					if (result > 0)
						// parent.Value > current
						// therefore, the parent's left subtree is now current's right subtree
						parent.Left = current.Right;
					else if (result < 0)
						// parent.Value < current.Value
						// therefore, the parent's right subtree is now current's right subtree
						parent.Right = current.Right;
				}
			}	
			// CASE 3: If current's right child has a left child, replace current with current's
			// right child's left-most node.
			else
			{
				// we need to find the right node's left-most child
				Node leftmost = current.Right.Left, lmParent = current.Right;
				while (leftmost.Left != null)
				{
					lmParent = leftmost;
					leftmost = leftmost.Left;
				}

				// the parent's left subtree becomes the leftmost's right subtree
				lmParent.Left = leftmost.Right;
				
				// assign leftmost's left and right to current's left and right
				leftmost.Left = current.Left;
				leftmost.Right = current.Right;

				if (parent == null)
					root = leftmost;
				else
				{
					result = parent.Value.CompareTo(current.Value);
					if (result > 0)
						// parent.Value > current
						// therefore, the parent's left subtree is now current's right subtree
						parent.Left = leftmost;
					else if (result < 0)
						// parent.Value < current.Value
						// therefore, the parent's right subtree is now current's right subtree
						parent.Right = leftmost;
				}
			}
		}
		#endregion

		#region Traversal Methods
		public override string ToString()
		{
			return ToString(TraversalMethods.Inorder);
		}

		public virtual string ToString(TraversalMethods traversalMethod)
		{
			return ToString(traversalMethod, " ");
		}

		public virtual string ToString(TraversalMethods traversalMethod, string separator)
		{
			string results = String.Empty;
			switch (traversalMethod)
			{
				case TraversalMethods.Preorder:
					results = PreorderTraversal(root, separator);
					break;

				case TraversalMethods.Inorder:
					results = InorderTraversal(root, separator);
					break;

				case TraversalMethods.Postorder:
					results = PostorderTraversal(root, separator);
					break;					
			}

			// finally, hack off the last separator
			if (results.Length == 0)
				return String.Empty;
			else
				return results.Substring(0, results.Length - separator.Length);
		}
		#region Pre-order
		protected virtual string PreorderTraversal(Node current, string separator)
		{
			if (current != null)
			{
				StringBuilder sb = new StringBuilder();
				sb.Append(current.Value.ToString());
				sb.Append(separator);

				sb.Append(PreorderTraversal(current.Left, separator));
				sb.Append(PreorderTraversal(current.Right, separator));
				return sb.ToString();
			}
			else
				return String.Empty;
		}
		#endregion

		#region In-order
		protected virtual string InorderTraversal(Node current, string separator)
		{
			if (current != null)
			{
				StringBuilder sb = new StringBuilder();
				sb.Append(InorderTraversal(current.Left, separator));
				sb.Append(current.Value.ToString());
				sb.Append(separator);
				sb.Append(InorderTraversal(current.Right, separator));
				return sb.ToString();
			}
			else
				return String.Empty;
		}
		#endregion

		#region Post-order
		protected virtual string PostorderTraversal(Node current, string separator)
		{
			if (current != null)
			{
				StringBuilder sb = new StringBuilder();
				sb.Append(PostorderTraversal(current.Left, separator));
				sb.Append(PostorderTraversal(current.Right, separator));

				sb.Append(current.Value.ToString());
				sb.Append(separator);
				return sb.ToString();
			}
			else
				return String.Empty;
		}
		#endregion
		#endregion
		#endregion

		#region Public Properties
		public virtual int Count
		{
			get
			{
				return count;
			}
		}
		#endregion
	}
}
