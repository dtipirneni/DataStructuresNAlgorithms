using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresNAlgorithms.Stacks
{
    /// <summary>
    /// DataStructure with Last In First Out (LIFO) extraction mechanism
    /// Other IMplementations: 
    ///  Undo
    ///  Balanced Paranthesis Validation
    /// </summary>
    public class StackWLinkedList<T> : IEnumerable<T>
    {
        private LinkedList<T> List { get; set; }
        public int Count => List.Count;

        public StackWLinkedList()
        {
            List = new LinkedList<T>();
        }

        public void Push(T item)
        {
            List.AddLast(item);
        }

        public T Peek()
        {
            if(Count == 0)
            {
                throw new InvalidOperationException("The Stack is empty.");
            }
            return List.First.Value;
        }
        public T Pop()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The Stack is empty.");
            }
            var data =  List.First.Value;
            List.RemoveFirst();
            return data;
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
