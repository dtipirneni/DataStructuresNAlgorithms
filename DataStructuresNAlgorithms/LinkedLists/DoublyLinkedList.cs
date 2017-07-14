using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresNAlgorithms.LinkedLists
{

    /*
     * Commom LinkedList implementation c# LinkedList<T>
     * Its a Doubly linked list
     */
    public class DoublyLinkedList<T> : ICollection<T>, IEnumerable<DoublyLinkedNode<T>>
    {       
        public DoublyLinkedNode<T> Head { get; private set; }
        public DoublyLinkedNode<T> Tail { get; private set; }

        #region ICollection Implementation

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            var newItem = new DoublyLinkedNode<T>(item);
            if (Count == 0)
            {
                Head = newItem;                
            }
            else
            {               
                Tail.Next = newItem;
                newItem.Previous = Tail;
            }
            Tail = newItem;
            Count++;
        }

        public void Clear()
        {
            Count = 0;
            Head = null;
            Tail = null;
        }

        public bool Remove(T item)
        {
            var currentNode = Head;            
            if (Tail.Data.Equals(item)) return true;

            while (currentNode!= null)
            {
                if(currentNode.Data.Equals(item))
                {
                    var previousNode = currentNode.Previous;
                    // not 1st item
                    if (previousNode != null)
                    {
                        previousNode.Next = currentNode.Next;
                        if(currentNode.Next == null)
                        {
                            Tail = previousNode;
                        }
                        else
                        {
                            currentNode.Next.Previous = previousNode;
                        }
                        Count--;
                    }
                    else
                    {
                        RemoveFirst();
                    }
                    return true;
                }
            }
            
            return false;
        }

        public bool Contains(T item)
        {
            var currentNode = Head;
            if (Head == null) return false;
            if (Tail.Data.Equals(item)) return true;

            while (!currentNode.Data.Equals(item))
            {
                currentNode = currentNode.Next;
            }
            if(currentNode != null)
            {
                return currentNode.Data.Equals(item);
            }
            return false;
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
            throw new NotImplementedException();
        }
        #endregion ICollection Implementation

        public void AddFirst(T item)
        {
            var currentHead = Head;
            Head = new DoublyLinkedNode<T>(item);            
            Head.Next = currentHead;
            if(Count == 0)
            {
                Tail = Head;
            }
            else
            {
                currentHead.Previous = Head;
            }
            Count++;
        }

        public void RemoveFirst()
        {     
            if(Count == 0)
            {
                return;
            }
            if(Count == 1 )
            {
                Clear();
                return;
            }
            Head = Head.Next;
            Head.Previous = null;
            Count--;
            
        }

        public void RemoveLast()
        {
            if (Count == 0) return;
            if(Count == 1)
            {
                Clear();
                return;
            }
            Tail = Tail.Previous;
            Tail.Next = null;
            Count--;            
        }

        IEnumerator<DoublyLinkedNode<T>> IEnumerable<DoublyLinkedNode<T>>.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class DoublyLinkedNode<T>
    {
        public T Data { get; set; }
        public DoublyLinkedNode<T> Previous { get; set; }
        public DoublyLinkedNode<T> Next { get; set; }
        public DoublyLinkedNode(T data)
        {
            Data = data;
        }
    }
}
