using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BinaryTree
{
    public class BinaryTree<TData> : ICollection<TData>, IEnumerable<TData>, IEnumerable, ITree<TData>
    {
        private BinaryTree<TData> leftSubtree;
        private BinaryTree<TData> rightSubtree;

        /// <summary>
        /// Gets the number of children at this level, which can be at most two.
        /// 
        /// </summary>
        public int Count
        {
            get
            {
                int num = 0;
                if (this.leftSubtree != null)
                    ++num;
                if (this.rightSubtree != null)
                    ++num;
                return num;
            }
        }

        /// <summary>
        /// Gets or sets the data of this tree.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The data.
        /// 
        /// </value>
        public TData Data { get; set; }

        /// <summary>
        /// Gets the degree.
        /// 
        /// </summary>
        public int Degree
        {
            get
            {
                return this.Count;
            }
        }

        /// <summary>
        /// Gets the height.
        /// 
        /// </summary>
        public virtual int Height
        {
            get
            {
                if (this.Degree == 0)
                    return 0;
                else
                    return 1 + this.FindMaximumChildHeight();
            }
        }

        /// <summary>
        /// Gets whether both sides are occupied, i.e. the left and right positions are filled.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// <c>true</c> if this instance is full; otherwise, <c>false</c>.
        /// 
        /// </value>
        public bool IsComplete
        {
            get
            {
                if (this.leftSubtree != null)
                    return this.rightSubtree != null;
                else
                    return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this tree is empty.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// 
        /// </value>
        public bool IsEmpty
        {
            get
            {
                return this.Count == 0;
            }
        }

        /// <summary>
        /// Gets whether this is a leaf node, i.e. it doesn't have children nodes.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// <c>true</c> if this instance is leaf node; otherwise, <c>false</c>.
        /// 
        /// </value>
        public virtual bool IsLeafNode
        {
            get
            {
                return this.Degree == 0;
            }
        }

        /// <summary>
        /// Returns <c>false</c>; this tree is never read-only.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// <c>true</c> if this instance is read only; otherwise, <c>false</c>.
        /// 
        /// </value>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets or sets the left subtree.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The left subtree.
        /// </value>
        public virtual BinaryTree<TData> Left
        {
            get
            {
                return this.leftSubtree;
            }
            set
            {
                if (this.leftSubtree != null)
                    this.RemoveLeft();
                if (value != null)
                {
                    if (value.Parent != null)
                        value.Parent.Remove(value);
                    value.Parent = this;
                }
                this.leftSubtree = value;
            }
        }

        /// <summary>
        /// Gets the parent of the current node.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The parent of the current node.
        /// </value>
        public BinaryTree<TData> Parent { get; set; }

        /// <summary>
        /// Gets or sets the right subtree.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The right subtree.
        /// </value>
        public virtual BinaryTree<TData> Right
        {
            get
            {
                return this.rightSubtree;
            }
            set
            {
                if (this.rightSubtree != null)
                    this.RemoveRight();
                if (value != null)
                {
                    if (value.Parent != null)
                        value.Parent.Remove(value);
                    value.Parent = this;
                }
                this.rightSubtree = value;
            }
        }

        /// <summary>
        /// Gets the root of the binary tree.
        /// 
        /// </summary>
        public BinaryTree<TData> Root
        {
            get
            {
                for (BinaryTree<TData> parent = this.Parent; parent != null; parent = parent.Parent)
                {
                    if (parent.Parent == null)
                        return parent;
                }
                return this;
            }
        }

        ITree<TData> ITree<TData>.Parent
        {
            get
            {
                return (ITree<TData>)this.Parent;
            }
        }

        /// <summary>
        /// Gets the BinaryTree at the specified index.
        /// 
        /// </summary>
        public BinaryTree<TData> this[int index]
        {
            get
            {
                return this.GetChild(index);
            }
        }


        public BinaryTree(TData data, TData left, TData right)
            : this(data, new BinaryTree<TData>(left, (BinaryTree<TData>)null, (BinaryTree<TData>)null), new BinaryTree<TData>(right, (BinaryTree<TData>)null, (BinaryTree<TData>)null))
        {
        }


        public BinaryTree(TData data, BinaryTree<TData> left = null, BinaryTree<TData> right = null)
        {
            this.leftSubtree = left;
            if (left != null)
                left.Parent = this;
            this.rightSubtree = right;
            if (right != null)
                right.Parent = this;
            this.Data = data;
        }

        /// <summary>
        /// Adds the given item to this tree.
        /// 
        /// </summary>
        /// <param name="item">The item to add.</param>
        public virtual void Add(TData item)
        {
            this.AddItem(new BinaryTree<TData>(item, (BinaryTree<TData>)null, (BinaryTree<TData>)null));
        }

        public void Add(BinaryTree<TData> subtree)
        {
            this.AddItem(subtree);
        }

        public virtual void BreadthFirstTraversal(IVisitor<TData> visitor)
        {
            Queue<BinaryTree<TData>> queue = new Queue<BinaryTree<TData>>();
            queue.Enqueue(this);
            while (queue.Count > 0 && !visitor.HasCompleted)
            {
                BinaryTree<TData> binaryTree = queue.Dequeue();
                visitor.Visit(binaryTree.Data);
                for (int index = 0; index < binaryTree.Degree; ++index)
                {
                    BinaryTree<TData> child = binaryTree.GetChild(index);
                    if (child != null)
                        queue.Enqueue(child);
                }
            }
        }

        /// <summary>
        /// Clears this tree of its content.
        /// 
        /// </summary>
        public virtual void Clear()
        {
            if (this.leftSubtree != null)
            {
                this.leftSubtree.Parent = (BinaryTree<TData>)null;
                this.leftSubtree = (BinaryTree<TData>)null;
            }
            if (this.rightSubtree == null)
                return;
            this.rightSubtree.Parent = (BinaryTree<TData>)null;
            this.rightSubtree = (BinaryTree<TData>)null;
        }

        /// <summary>
        /// Returns whether the given item is contained in this collection.
        /// 
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// <c>true</c> if is contained in this collection; otherwise, <c>false</c>.
        /// 
        /// </returns>
        public bool Contains(TData item)
        {
            return Enumerable.Contains<TData>((IEnumerable<TData>)this, item);
        }

        /// <summary>
        /// Copies the tree to the given array.
        /// 
        /// </summary>
        /// <param name="array">The array.</param><param name="arrayIndex">Index of the array.</param>
        public void CopyTo(TData[] array, int arrayIndex)
        {
            foreach (TData data in this)
            {
                if (arrayIndex >= array.Length)
                    throw new ArgumentException("ArrayIndex should not exceed array.Length", "array");
                array[arrayIndex++] = data;
            }
        }

        /// <summary>
        /// Performs a depth first traversal on this tree with the specified visitor.
        /// 
        /// </summary>
        /// <param name="visitor">The ordered visitor.</param><exception cref="T:System.ArgumentNullException"><paramref name="visitor"/> is a null reference (<c>Nothing</c> in Visual Basic).</exception>
        public virtual void DepthFirstTraversal(IVisitor<TData> visitor)
        {
            if (visitor.HasCompleted)
                return;
            IBeforAfterVisitor<TData> prePostVisitor = visitor as IBeforAfterVisitor<TData>;
            if (prePostVisitor != null)
                prePostVisitor.BeforVisit(this.Data);
            if (this.leftSubtree != null)
                this.leftSubtree.DepthFirstTraversal(visitor);
            visitor.Visit(this.Data);
            if (this.rightSubtree != null)
                this.rightSubtree.DepthFirstTraversal(visitor);
            if (prePostVisitor == null)
                return;
            prePostVisitor.AfterVisit(this.Data);
        }

        /// <summary>
        /// Seeks the tree node containing the given data.
        /// 
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns/>
        public BinaryTree<TData> Find(TData value)
        {
            Queue<BinaryTree<TData>> queue = new Queue<BinaryTree<TData>>();
            queue.Enqueue(this.Root);
            while (queue.Count > 0)
            {
                BinaryTree<TData> binaryTree = queue.Dequeue();
                if (EqualityComparer<TData>.Default.Equals(binaryTree.Data, value))
                    return binaryTree;
                for (int index = 0; index < binaryTree.Degree; ++index)
                {
                    BinaryTree<TData> child = binaryTree.GetChild(index);
                    if (child != null)
                        queue.Enqueue(child);
                }
            }
            return (BinaryTree<TData>)null;
        }

        /// <summary>
        /// Finds the node with the specified condition.  If a node is not found matching
        ///             the specified condition, null is returned.
        /// 
        /// </summary>
        /// <param name="condition">The condition to test.</param>
        /// <returns>
        /// The first node that matches the condition supplied.  If a node is not found, null is returned.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="condition"/> is a null reference (<c>Nothing</c> in Visual Basic).</exception>
        public BinaryTree<TData> FindNode(Predicate<TData> condition)
        {
            if (condition(this.Data))
                return this;
            if (this.leftSubtree != null)
            {
                BinaryTree<TData> node = this.leftSubtree.FindNode(condition);
                if (node != null)
                    return node;
            }
            if (this.rightSubtree != null)
            {
                BinaryTree<TData> node = this.rightSubtree.FindNode(condition);
                if (node != null)
                    return node;
            }
            return (BinaryTree<TData>)null;
        }

        /// <summary>
        /// Gets the left (index zero) or right (index one) subtree.
        /// 
        /// </summary>
        /// <param name="index">The index of the child in question.</param>
        /// <returns>
        /// The child at the specified index.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/>There are at most two children at each level of a binary tree, the index can hence only be zero or one.</exception>
        public BinaryTree<TData> GetChild(int index)
        {
            switch (index)
            {
                case 0:
                    return this.leftSubtree;
                case 1:
                    return this.rightSubtree;
                default:
                    throw new ArgumentOutOfRangeException("index");
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// 
        /// </returns>
        public IEnumerator<TData> GetEnumerator()
        {
            Stack<BinaryTree<TData>> stack = new Stack<BinaryTree<TData>>();
            stack.Push(this);
            while (stack.Count > 0)
            {
                BinaryTree<TData> tree = stack.Pop();
                yield return tree.Data;
                if (tree.leftSubtree != null)
                    stack.Push(tree.leftSubtree);
                if (tree.rightSubtree != null)
                    stack.Push(tree.rightSubtree);
            }
        }

        /// <summary>
        /// Removes the specified item from the tree.
        /// 
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns/>
        public virtual bool Remove(TData item)
        {
            if (this.leftSubtree != null && this.leftSubtree.Data.Equals((object)item))
            {
                this.RemoveLeft();
                return true;
            }
            else
            {
                if (this.rightSubtree == null || !this.rightSubtree.Data.Equals((object)item))
                    return false;
                this.RemoveRight();
                return true;
            }
        }

        /// <summary>
        /// Removes the specified child.
        /// 
        /// </summary>
        /// <param name="child">The child.</param>
        /// <returns>
        /// Returns whether the child was found (and removed) from this tree.
        /// </returns>
        public virtual bool Remove(BinaryTree<TData> child)
        {
            if (this.leftSubtree != null && this.leftSubtree == child)
            {
                this.RemoveLeft();
                return true;
            }
            else
            {
                if (this.rightSubtree == null || this.rightSubtree != child)
                    return false;
                this.RemoveRight();
                return true;
            }
        }

        /// <summary>
        /// Removes the left child.
        /// 
        /// </summary>
        public virtual void RemoveLeft()
        {
            if (this.leftSubtree == null)
                return;
            this.leftSubtree.Parent = (BinaryTree<TData>)null;
            this.leftSubtree = (BinaryTree<TData>)null;
        }

        /// <summary>
        /// Removes the left child.
        /// 
        /// </summary>
        public virtual void RemoveRight()
        {
            if (this.rightSubtree == null)
                return;
            this.rightSubtree.Parent = (BinaryTree<TData>)null;
            this.rightSubtree = (BinaryTree<TData>)null;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents this instance.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.String"/> that represents this instance.
        /// 
        /// </returns>
        public override string ToString()
        {
            string str = (string)null;
            switch (this.Count)
            {
                case 0:
                    str = "No children";
                    break;
                case 1:
                    str = this.Left == null ? "One right child." : "One left child.";
                    break;
                case 2:
                    str = "Is full (two children).";
                    break;
            }
            return string.Format("{0}; {1}", (object)this.Data, (object)str);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this.GetEnumerator();
        }

        void ITree<TData>.Add(ITree<TData> child)
        {
            this.AddItem((BinaryTree<TData>)child);
        }

        ITree<TData> ITree<TData>.FindNode(Predicate<TData> condition)
        {
            return (ITree<TData>)this.FindNode(condition);
        }

        ITree<TData> ITree<TData>.GetChild(int index)
        {
            return (ITree<TData>)this.GetChild(index);
        }

        bool ITree<TData>.Remove(ITree<TData> child)
        {
            return this.Remove((BinaryTree<TData>)child);
        }

        /// <summary>
        /// Finds the maximum height between the child nodes.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// The maximum height of the tree between all paths from this node and all leaf nodes.
        /// </returns>
        protected virtual int FindMaximumChildHeight()
        {
            int num1 = this.leftSubtree != null ? this.leftSubtree.Height : 0;
            int num2 = this.rightSubtree != null ? this.rightSubtree.Height : 0;
            if (num1 <= num2)
                return num2;
            else
                return num1;
        }

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// 
        /// </summary>
        /// <param name="subtree">The sub tree.</param>
        private void AddItem(BinaryTree<TData> subtree)
        {
            if (this.leftSubtree == null)
            {
                if (subtree.Parent != null)
                    subtree.Parent.Remove(subtree);
                this.leftSubtree = subtree;
                subtree.Parent = this;
            }
            else
            {
                if (this.rightSubtree != null)
                    throw new InvalidOperationException("This binary tree is full.");
                if (subtree.Parent != null)
                    subtree.Parent.Remove(subtree);
                this.rightSubtree = subtree;
                subtree.Parent = this;
            }
        }
    }
}
