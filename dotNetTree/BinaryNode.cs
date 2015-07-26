using System;

namespace dotNetTree
{
    public class BinaryNode<T> : Node<T>
    {
        #region Properties

        public BinaryNode<T> left
        {
            get
            {
                if (base.children == null)
                    return null;
                else
                    return (BinaryNode<T>)base.children[0];
            }
            set
            {
                if (base.children == null)
                    base.children = new NodeList<T>(2);

                base.children[0] = value;
            }
        }

        public BinaryNode<T> right
        {
            get
            {
                if (base.children == null)
                    return null;
                else
                    return (BinaryNode<T>)base.children[1];
            }
            set
            {
                if (base.children == null)
                    base.children = new NodeList<T>(2);

                base.children[1] = value;
            }
        }

        #endregion

        #region Constructor

        public BinaryNode() : base() { }
        public BinaryNode(T data) : base(data, null) { }
        public BinaryNode(T data, BinaryNode<T> left, BinaryNode<T> right)
        {
            base.Value = data;
            
            NodeList<T> children = new NodeList<T>(2);
            children[0] = left;
            children[1] = right;

            base.children = children;
        }

        #endregion
    }
}
