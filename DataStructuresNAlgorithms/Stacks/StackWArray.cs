using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructuresNAlgorithms.Stacks
{
    /// <summary>
    /// C# store Stack in a Array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StackWArray<T> : IEnumerable<T>
    {
        private T[] _Array { get; set; }

        public int Size { get; private set; }

        public StackWArray()
        {
            _Array = new T[0];
            Size = 0;
        }

        public void Clear()
        {
            Size = 0;
            _Array = new T[0];
        }

        public void Push(T item)
        {
            if(Size == _Array.Length)
            {
                int newLength = Size == 0 ? 4 : Size * 2;
                T[] newArray = new T[newLength];
                _Array.CopyTo(newArray, 0);
                _Array = newArray;
            }

            _Array[Size] = item;
            Size++;
        }

        public T Pop()
        {
            if(Size == 0)
            {
                throw new InvalidOperationException("The Stack is empty.");
            }
            
            Size--;
            return _Array[Size];       
        }

        public T Peek()
        {
            if (Size == 0)
            {
                throw new InvalidOperationException("The Stack is empty.");
            }
            return _Array[Size - 1];     
        }

        public IEnumerator<T> GetEnumerator()
        {
            for(int i = Size -1; i>= 0; i--)
            {
                yield return _Array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
