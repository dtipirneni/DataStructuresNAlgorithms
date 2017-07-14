using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresNAlgorithms.Queues
{
    public class PriorityQueue<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        LinkedList<T> Items = new LinkedList<T>();
        public int Count => Items.Count;

        public void Enqueue(T item)
        {
            if (Count == 0)
            {
                Items.AddLast(item);
            }
            else
            {
                // Find the insertion point based on priority
                var currentItem = Items.First;
                while(currentItem != null && currentItem.Value.CompareTo(item) >= 0)
                {
                    currentItem = currentItem.Next;
                }

                if(currentItem == null)// reached the end of the list
                {
                    Items.AddLast(item);
                }
                else
                {
                    Items.AddBefore(currentItem, item);
                }
            }
        }

        public T Dequeue()
        {
            if(Count == 0)
            {
                throw new InvalidOperationException("The Priority Queue is empty.");
            }
            var value = Items.Last.Value;
            Items.RemoveLast();
            return value;
        }

        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The Priority Queue is empty.");
            }
            return Items.Last.Value;
        }

        public void Clear()
        {
            Items.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)Items).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)Items).GetEnumerator();
        }
    }
}
