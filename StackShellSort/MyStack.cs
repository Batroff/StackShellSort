using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace StackShellSort
{
    public class MyStack 
    {
        private Stack<int> _tempStack = new Stack<int>();
        private Stack<int> _stack;
        private Random _random;
        private ulong _nop;

        public MyStack()
        {
            _nop++;
            _stack = new Stack<int>();
        }

        private void Show()
        {
            Console.WriteLine("===================Stack====================");

            foreach (int num in _stack)
                Console.Write($"{num} ");

            Console.WriteLine("\n============================================\n");
        }

        private ulong CalcFn(int n)
        {
            return (ulong) (
                Math.Log2(n) * (16 * Math.Pow(n, 2) + 50 * n + 13) -
                Math.Log2(n) * (Math.Log2(n) + 1) * (10 * n + 28) + 
                (8 * Math.Pow(n, 3) + 32 * Math.Pow(n, 2) + 24 * n) /
                    (Math.Log2(n) * (Math.Log2(n) + 1))
                );
        }

        private ulong CalcOfn(int n)
        {
            return (ulong) (Math.Pow(n, 3) / Math.Pow(Math.Log2(n), 2) + Math.Pow(n, 2) * Math.Log2(n));
        }

        private int this[int index] 
        {
            get // 4n + 8
            {
                // Pop elements before index-element and pushing into tempStack
                _nop += 2;
                for (int i = 0; i < index; i++) // 1 + 1 + (2 + 2) * index
                {
                    _tempStack.Push(_stack.Pop()); // +2
                    _nop += 4;
                }

                // Get index-element
                int res = _stack.Peek(); // + 1 + 1
                _nop += 2;
                
                // Pushing other elements into tempStack
                _nop += 2;
                while (_stack.Count > 0) // 1 + 1 + (2 + 2) * (n - index)
                {
                    _tempStack.Push(_stack.Pop()); // 2 + 2
                    _nop += 4;
                }
                
                // Restore stacks
                _stack = new Stack<int>(_tempStack); // + 1
                _tempStack.Clear(); // + 1
                _nop += 2;
                
                return res;
            }
            set // 4n + 8
            {
                // Pop elements before index-element and pushing into tempStack
                _nop += 2;
                for (int i = 0; i < index; i++) // 1 + 1 + (2 + 2) * index
                {
                    _tempStack.Push(_stack.Pop()); // + 2
                    _nop += 4;
                }

                _stack.Pop(); // + 1
                _stack.Push(value); // + 1
                _nop += 2;
                
                // Pushing other elements into tempStack
                _nop += 2;
                while (_stack.Count > 0) // 1 + 1 + (2 + 2) * (n - index)
                {
                    _tempStack.Push(_stack.Pop()); // + 2
                    _nop += 4;
                }

                // Restore stacks
                _stack = new Stack<int>(_tempStack); // + 1
                _tempStack.Clear(); // + 1 
                _nop += 2;
            }
        }

        private void ShellSort()
        {
            int gap = _stack.Count / 2; // 1 + 1
            _nop += 2;

            _nop += 1;
            while (gap >= 1) // 2 + 1 + (2 + (2 + (8n + 24 + (12n + 36) * (i / gap)) * (n - gap) + 2) * log2(n) = 
                             // = 3 ( 
            {
                _nop += 2;
                for (int i = gap; i < _stack.Count; i++) // 2 + (4n + 10 + 2 + 12 + 4n + (12n + 36) * (i / gap)) * (n - gap) =
                                                         // = 2 + (8n + 24 + (12n + 36) * (i / gap)) * (n - gap)
                {
                    int curr = this[i]; // +1 +(4n + 8) 
                    _nop++;
                    
                    int j = i; // + 1
                    _nop++;

                    _nop += 4; 
                    while (j > 0 && this[j - gap] > curr) // 
                    {
                        this[j] = this[j - gap]; // 4n + 8 + 1 + 4n + 8 =
                                                 // = 8n + 16
                        _nop++;
                        
                        j -= gap; // + 1
                        _nop++;

                        _nop += 2;
                        if (j - gap < 0) break; // +2
                        
                        _nop += 4;
                    }

                    this[j] = curr; // 4n + 8
                }

                gap /= 2; // + 1
                _nop++;
            }
        }
        
        
        public void FillStack(int count)
        {
            _random = new Random();
            for (int i = 0; i < count; i++)
                _stack.Push(_random.Next(count));

            Show();
        }

        public void Sort()
        {
            Stopwatch start = new Stopwatch();
            start.Start();
            
            Console.WriteLine("Sorting...");
            ShellSort();
            
            Show();
            
            TimeSpan end = start.Elapsed;
            Console.WriteLine($"N = {_stack.Count}");
            Console.WriteLine($"Time = {end}");
            Console.WriteLine($"N_op = {_nop}");
            Console.WriteLine($"Calculated F(N) = {CalcFn(_stack.Count)}");
            Console.WriteLine($"Calculated O(F(N)) = {CalcOfn(_stack.Count)}");
            _nop = 0;
            
            start.Reset();
        }
    }
}