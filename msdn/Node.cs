using System;

namespace skmDataStructures
{
	/// <summary>
	/// Summary description for Node.
	/// </summary>
	public class Node : ICloneable
	{
		private IComparable data;
		private Node left, right;

		#region Constructors
		public Node() : this(null) {}
		public Node(IComparable data) : this(data, null, null) {}
		public Node(IComparable data, Node left, Node right)
		{
			this.data = data;
			this.left = left;
			this.right = right;
		}
		#endregion

		#region Public Methods
		public object Clone()
		{
			Node clone = new Node();
			if (data is ICloneable)
				clone.Value = (IComparable) ((ICloneable) data).Clone();
			else
				clone.Value = data;

			if (left != null)
				clone.Left = (Node) left.Clone();
			
			if (right != null)
				clone.Right = (Node) right.Clone();

			return clone;
		}
		#endregion

		#region Public Properties
		public IComparable Value
		{
			get
			{
				return data;
			}
			set
			{
				data = value;
			}
		}

		public Node Left
		{
			get
			{
				return left;
			}
			set
			{
				left = value;
			}
		}

		public Node Right
		{
			get
			{
				return right;
			}
			set
			{
				right = value;
			}
		}
		#endregion

	}
}
