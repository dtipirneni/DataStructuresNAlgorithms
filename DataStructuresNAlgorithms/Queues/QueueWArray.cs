using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresNAlgorithms.Queues
{
    public class QueueWArray<T> : IEnumerable<T>
    {
        private T[] Items { get; set; }

        public int Count { get; private set; }

        // Index of the last item enqueued
        int _tail = -1;
        // Index of the first (oldest) item enqueued
        int _head = 0;

        public QueueWArray()
        {
            Items = new T[0];
            Count = 0;
        }

        public void Clear()
        {                      
            Count = 0;
            _head = 0;
            _tail = -1;
            Items = new T[0];
        }
        public void Enqueue(T item)
        {
            if(Count == Items.Length)
            {
                int newLength = (Count == 0 ? 4 : Count * 2);
                var newArray = new T[newLength];
                

                if (Count > 0) // Copy Contents to new array
                {
                    int targetIndex = 0;
                    // Check if wrapping occurred. ie Tail is less than head.
                    if (_tail < _head)
                    {
                        // first copy all items from head to end of array and then 0 to tail
                        for (int i = _head; i < Items.Length; i++)
                        {
                            newArray[targetIndex] = Items[i];
                            targetIndex++;
                        }
                        for (int i = 0; i < _tail + 1; i++)
                        {
                            newArray[targetIndex] = Items[i];
                            targetIndex++;
                        }
                    }
                    else
                    {
                        // if no wrapping just copy items from head to tail
                        for (int i = _head; i <= _tail; i++)
                        {
                            newArray[targetIndex] = Items[i];
                            targetIndex++;
                        }
                    }
                    _head = 0;
                    _tail = targetIndex--; // Compensate for an extra increment in the loop
                }
                else
                {
                    _head = 0;
                    _tail = -1;
                }
                Items = newArray;
            }
            // now we have a properly sized array and can focus on wrapping issues

            // if tail is at the end of the array we need to wrap around
            if(_tail == Items.Length -1)
            {
                _tail = 0;
            }
            else
            {
                _tail++;
            }
            Items[_tail] = item;            
            Count++;
        }

        public T Dequeue()
        {
            if(Count == 0)
            {
                throw new Exception("The Queue is empty");
            }
            var item = Items[_head];
            if(_head == Items.Length -1)
            {
                _head = 0;
            }
            else
            {
                _head++;
            }
            Count--;
            return item;
        }   

        public T Peek()
        {
            if (Count == 0)
            {
                throw new Exception("The Queue is empty");
            }
            return Items[_head];            
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (Count > 0) // Copy Contents to new array
            {                
                // Check if wrapping occurred. ie Tail is less than head.
                if (_tail < _head)
                {
                    // first copy all items from head to end of array and then 0 to tail
                    for (int i = _head; i < Items.Length; i++)
                    {
                        yield return Items[i];                        
                    }
                    for (int i = 0; i < _tail + 1; i++)
                    {
                        yield return Items[i];                        
                    }
                }
                else
                {
                    // if no wrapping just copy items from head to tail
                    for (int i = _head; i <= _tail; i++)
                    {
                        yield return Items[i];                        
                    }
                }               
            }                  
        }    

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
