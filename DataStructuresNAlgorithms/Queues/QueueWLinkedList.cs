using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresNAlgorithms.Queues
{
    public class QueueWLinkedList<T> : IEnumerable<T>
    {
        private LinkedList<T> List { get; set; }
        public int Count => List.Count;

        public QueueWLinkedList()
        {
            List = new LinkedList<T>();
        }

        public void Enqueue (T item)
        {
            List.AddLast(item);
        }

        public T Dequeue()
        {
            if(List.Count == 0)
            {
                throw new Exception("The Queue is empty.");
            }
            var item = List.Last;
            List.RemoveFirst();
            return item.Value;            
        }

        public T Peek()
        {
            if (List.Count == 0)
            {
                throw new Exception("The Queue is empty.");
            }
            return List.Last.Value;
        }

        public void Clear()
        {
            List.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)List).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)List).GetEnumerator();
        }
    }
}
