using System;
using System.Collections.Generic;

namespace Codenation.Challenge
{
    public class Math
    {
        public List<int> Fibonacci()
        {
            var fibonacciList = new List<int>();
            int currentNumber = 0;
            int nextNumber = 1;

            for (int i = 0; currentNumber < 350; i++)
            {
                fibonacciList.Add(currentNumber);
                currentNumber = nextNumber;
                if (i > 0)
                {
                    nextNumber = fibonacciList[i] + currentNumber;
                }
                else
                {
                    nextNumber = 1;
                }
            }
            return fibonacciList;
        }

        public bool IsFibonacci(int numberToTest)
        {
            return Fibonacci().Contains(numberToTest);
        }
    }
}
