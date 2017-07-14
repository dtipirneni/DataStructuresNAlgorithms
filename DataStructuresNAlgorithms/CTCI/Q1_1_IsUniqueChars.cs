using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresNAlgorithms.CTCI
{
    protected class Q1_1_IsUniqueChars : Models.IQuestion
    {
        public void Run()
        {
            string[] words = { "abcde", "hello", "apple", "kite", "padle" };

            foreach (var word in words)
            {
                Console.WriteLine(word + ": " + IsUniqueChars(word) + " " + IsUniqueCharsNoDS(word));
            }
        }

        /// <summary>
        /// What if cannot use additional Data Structure
        /// </summary>
        /// <param name="word"></param>
        /// <returns>Whether Unique or not</returns>
        private bool IsUniqueCharsNoDS(string word)
        {
            throw new NotImplementedException();
        }

        private bool IsUniqueChars(string word)
        {
            throw new NotImplementedException();
        }
    }
}
