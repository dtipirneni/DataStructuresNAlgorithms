using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructuresNAlgorithms.LinkedLists
{
    public class LinkedList<T> : ICollection<T>, IEnumerable<LinkedListNode<T>>
    {
        public LinkedListNode<T> Head
        {
            get; private set;
        }
        public LinkedListNode<T> Tail
        {
            get; private set;
        }       

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        /// <summary>
        /// Complexity = O(1)
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            // Add at the end
            // Create the node
            LinkedListNode<T> newNode  = new LinkedListNode<T>(item);
            // If first node
            if (Count == 0)
            {
                // Set Head and Tail to the new node
                Head = newNode;                
            }
            else
            {
                Tail.Next = newNode;
            }
            Tail = newNode;           
            Count++;
        }

        /// <summary>
        /// Complexity = O(1)
        /// </summary>
        /// <param name="item"></param>
        public void AddFirst(T item)
        {
            // Create a new Node 
            LinkedListNode<T> newItem = new LinkedListNode<T>(item);
            // Save current head to a temp variable
            var tempHead = Head;
            // Make the new Item the Head meaning Insert the new node at the beginning of the linked list
            Head = newItem;
            // Insert the rest of the list behind the head
            Head.Next = tempHead;
            // Increment counter
            Count++;
            // If th elist was empty before inserting the new node, Point the tail to the new node as well
            if(Count == 1)
            {
                Tail = Head;
            }            
        }

        public bool Remove(T data)
        {            
            LinkedListNode<T> currentNode = Head;
            LinkedListNode<T> previousNode = null;

            /*
             * 1: Empty list - do nothing
             * 2: Single Node: Previous is null
             * 3: Many nodes
             *      a: node to remove is the first node
             *      b: node is remove is the middle or last
             */
            while(currentNode != null)
            {
                if (currentNode.Data.Equals(data))
                {
                    // its a node in the middle or end
                    if (previousNode != null)
                    {
                        previousNode.Next = currentNode.Next;

                        // Its the end, so set tail
                        if (currentNode.Next == null)
                        {
                            Tail = previousNode;
                        }
                        Count--;
                    }
                    else
                    {
                        // its the first node
                        RemoveFirst();
                    }
                    return true;
                }
                previousNode = currentNode;
                currentNode = currentNode.Next;                
            }
            return false;
        }

        /// <summary>
        /// Worst Case Complexity = O(1)
        /// </summary>
        public void RemoveFirst()
        {
            // If count is 0 no need to remove anything
            if (Count == 0) return;

            // Set Head to the node next to the current Head
            Head = Head.Next;
            // Decrement counter
            Count--;
            // If we now wnd up with 0 nodes, then set the tail to null;
            if (Count == 0)
            {                
                Tail = null;
            }
        }

        /// <summary>
        /// Worst Case Complexity = O(n)
        /// </summary>
        public void RemoveLast()
        {
            if(Count == 1)
            {
                Head = null;
                Tail = null;
            }
            else
            {
                // We have to get the node before the tail and set it as tail
                var currentNode = Head;
                // So go through the nodes until you find the node that links to the Tail
                while (currentNode.Next != Tail)
                {
                    currentNode = currentNode.Next;
                }
                Tail = currentNode;
            }
            Count--;
        }

        public void PrintItems()
        {
            var node = Head;
            while(node != null)
            {
                // print
                node = node.Next;
            }
        }

        public void SortItems()
        {
            throw new NotImplementedException();
        }        

        public void Clear()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }       

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = Head;
            while (currentNode != null)
            {
                yield return currentNode.Data;
                currentNode = currentNode.Next;
            }
        }        

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (GetEnumerator());
        }

        IEnumerator<LinkedListNode<T>> IEnumerable<LinkedListNode<T>>.GetEnumerator()
        {
            var currentNode = Head;
            while (currentNode != null)
            {
                yield return currentNode;
                currentNode = currentNode.Next;
            }
        }
    }

    public class LinkedListNode<T>
    {
        public T Data { get; set; }
        public LinkedListNode<T> Next { get; set; }
        public LinkedListNode(T data)
        {
            Data = data;            
        }
    }
}
