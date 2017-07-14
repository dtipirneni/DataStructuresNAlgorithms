using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresNAlgorithms.Trees
{
    public class BinaryTree<T> : IEnumerable<T>
        where T: IComparable<T>
    {
        private BinaryTreeNode<T> Root { get; set; }

        public int Count
        {
            get;private set;
        }

        public void Add(T value)
        {
            // Case 1: Tree is empty. Set the Root
            if (Root == null)
            {
                Root = new BinaryTreeNode<T>(value);
            }
            // Case 2: Tree is not empty. So insert in the right location
            else
            {
                AddTo(Root, value);
            }
            Count++;
        }

        /// <summary>
        /// Recursive Add Algorithm
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        private void AddTo(BinaryTreeNode<T> node, T value)
        {
            if (node == null)
                throw new InvalidOperationException("Cannot add to an empty node.");

            // If value is less than the current node value, then add to the left Child
            if(node.Value.CompareTo(value) > 0)
            {
                if(node.LeftChild == null)
                {
                    node.LeftChild = new BinaryTreeNode<T>(value);
                    return;
                }
                else
                {
                    AddTo(node.LeftChild, value);
                }
            }
            // if value if greater than the current node value, then add to the right child. Treat equal values as greater
            else if(node.Value.CompareTo(value) <= 0)
            {
                if (node.RightChild == null)
                {
                    node.RightChild = new BinaryTreeNode<T>(value);
                    return;
                }
                else
                {
                    AddTo(node.LeftChild, value);
                }
            }            
        }

        public bool Contains(T value)
        {
            if (Root == null) return false;
            BinaryTreeNode<T> parent;
            return FindWithParent(value, out parent) != null;
        }

        private BinaryTreeNode<T> FindWithParent(T value, out BinaryTreeNode<T> parent)
        {
            parent = null;
            BinaryTreeNode<T> currentNode = Root;
            while (currentNode != null)
            {                
                // If value is less than the current node value, then look at the left child
                if (currentNode.Value.CompareTo(value) > 0)
                {
                    parent = currentNode;
                    currentNode = currentNode.LeftChild;
                }
                // if value if greater than the current node value, then add to the right child. Treat equal values as greater
                else if (currentNode.Value.CompareTo(value) <= 0)
                {
                    parent = currentNode;
                    currentNode = currentNode.RightChild;
                }
                // we have a match
                else
                {
                    break;
                }
            }                   
            return currentNode;
        }

        public void Remove(T value)
        {
            BinaryTreeNode<T> parent;
            BinaryTreeNode<T> nodeToRemove = FindWithParent(value, out parent);
            if(nodeToRemove == null)
            {
                // Nothing to remove
                return;
            }
            Count--;

            // Figure out if the node to remove is the parent's left or right child
            int result = parent.Value.CompareTo(nodeToRemove.Value);
            bool isLeftChild = (result > 0);

            // Case 1: The node to remove has no right child.
            if (nodeToRemove.RightChild == null)
            {
                // Then replace the node to remove with the Left Child
                if (parent == null)
                {
                    // no parent means the node to remove is the Root node
                    Root = nodeToRemove.LeftChild;
                }
                else
                {
                    if (isLeftChild)
                    {
                        parent.LeftChild = nodeToRemove.LeftChild;
                    }
                    else
                    {
                        parent.RightChild = nodeToRemove.LeftChild;
                    }
                }
            }
            // Case 2: The node's Right child has no Left child
            if(nodeToRemove.RightChild.LeftChild == null)
            {
                // Then replace the node with the Right Child's
                nodeToRemove.RightChild.LeftChild = nodeToRemove.LeftChild;
                var replaceWith = nodeToRemove.RightChild;
                if (parent == null)
                {
                    // no parent means the node to remove is the Root node
                    Root = replaceWith;
                }
                else
                {
                    if (isLeftChild)
                    {
                        parent.LeftChild = replaceWith;                        
                    }
                    else
                    {
                        parent.RightChild = replaceWith;                        
                    }
                }
            }
            // Case 3: The node's right child has a left child, replace the node with the node's right child's left most child
            else
            {
                // Find the right Child's left most child
                var leftmost = nodeToRemove.RightChild.LeftChild;
                var leftmostParent = nodeToRemove.RightChild;
                while (leftmost.LeftChild != null)
                {
                    leftmostParent = leftmost;
                    leftmost = leftmost.LeftChild;
                }
                // SInce the left most is going to inherit the position  of the nodeto remove, if it has any right children, move them up to the leftmost's position
                leftmostParent.LeftChild = leftmost.RightChild;

                leftmost.LeftChild = nodeToRemove.LeftChild;
                leftmost.RightChild = nodeToRemove.RightChild;
                if (parent == null)
                {
                    Root = leftmost;
                }
                else
                {
                    if (isLeftChild)
                    {
                        parent.LeftChild = leftmost;                       
                    }
                    else
                    {
                        parent.RightChild = leftmost;                       
                    }
                }
            }
        }

        public void PreOrderTraversal(Action<T> action)
        {
            PreOrderTraversal( action, Root);
        }

        private void PreOrderTraversal (Action<T> action, BinaryTreeNode<T> node)
        {
            if(node != null)
            {
                action(node.Value);
                PreOrderTraversal(action, node.LeftChild);
                PreOrderTraversal(action, node.RightChild);
            }
        }

        public void InOrderTraversal(Action<T> action)
        {
            InOrderTraversal(action, Root);
        }
        private void InOrderTraversal(Action<T> action, BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                InOrderTraversal(action, node.LeftChild);
                action(node.Value);
                InOrderTraversal(action, node.RightChild);
            }
        }

        // Non Recursive using a stack.
        public IEnumerator<T> InOrderTraversal()
        {
            if (Root != null)
            {
                Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();

                // When removing recursion, we need to keep track of whether or not we go left or right next
                bool goLeftNext = true;
                var current = Root;
                // Start bu pushing Root into Stack
                while (stack.Count > 0)
                {
                    // We go left first
                    if (goLeftNext)
                    {
                        // Push everything but the leftmost node to the stack
                        // we'll yield the leftmost after this block
                        while (current.LeftChild != null)
                        {
                            stack.Push(current);
                            current = current.LeftChild;
                        }
                    }

                    // in-order is left -> yield--> Right
                    yield return current.Value;

                    // if we can go right do so
                    if(current.RightChild != null)
                    {
                        current = current.RightChild;
                        goLeftNext = true;
                    }
                    else
                    {
                        current = stack.Pop();
                        goLeftNext = false;
                    }
                }
            }
        }

        public void PostOrderTraversal(Action<T> action)
        {
            PostOrderTraversal(action, Root);
        }

        private void PostOrderTraversal(Action<T> action, BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                PostOrderTraversal(action, node.LeftChild);
                PostOrderTraversal(action, node.RightChild);
                action(node.Value);
            }
        }

        public void Clear()
        {
            Root = null;
            Count = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class BinaryTreeNode<T>: IComparable<T>
        where T: IComparable<T>
    {       
        public BinaryTreeNode<T> LeftChild
        {
            get; set;
        }

        public BinaryTreeNode<T> RightChild
        {
            get; set;
        }

        public T Value
        {
            get; set;
        }

        public BinaryTreeNode(T value)
        {
            Value = value;
        }

        int IComparable<T>.CompareTo(T other)
        {
            return Value.CompareTo(other);
        }
    }
}
