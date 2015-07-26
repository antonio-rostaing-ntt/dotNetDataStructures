using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetTree
{
    public class Node<T>
    {
        #region Properties

        public T Value {get; protected set;}
        public NodeList<T> children {get; protected set;}

        #endregion

        #region Constructor

        public Node() {}
        public Node(T data) : this(data, null) {}
        public Node(T data, NodeList<T> children)
        {
            this.Value = data;
            this.children = children;
        }

        #endregion
    }
}

