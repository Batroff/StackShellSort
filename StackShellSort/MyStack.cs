using System;
using System.Collections.Generic;

namespace StackShellSort
{
    public class MyStack 
    {
        private Stack<int> _tempStack = new Stack<int>();
        private Stack<int> _stack;
        private Random _random;
        private ulong _nOp;

        public MyStack(int[] arr)
        {
            _stack = new Stack<int>(arr);
        }

        public MyStack()
        {
            _nOp++;
            _stack = new Stack<int>();
        }

        private void Show()
        {
            Console.WriteLine("===================Stack====================");

            foreach (int num in _stack)
                Console.Write($"{num} ");

            Console.WriteLine("\n============================================\n");
        }

        private int this[int index]
        {
            get
            {
                // Pop elements before index-element and pushing into tempStack
                _nOp++;
                for (int i = 0; i < index; i++)
                {
                    _tempStack.Push(_stack.Pop());
                    _nOp += 4;
                }

                // Get index-element
                int res = _stack.Peek();
                _nOp += 2;
                
                // Pushing other elements into tempStack
                _nOp++;
                while (_stack.Count > 0)
                {
                    _tempStack.Push(_stack.Pop());
                    _nOp += 3;
                }
                
                // Restore stacks
                _stack = new Stack<int>(_tempStack);
                _tempStack.Clear();
                _nOp += 3;
                
                return res;
            }
            set
            {
                // Pop elements before index-element and pushing into tempStack
                _nOp++;
                for (int i = 0; i < index; i++)
                {
                    _tempStack.Push(_stack.Pop());
                    _nOp += 4;
                }

                _stack.Pop();
                _stack.Push(value);
                _nOp += 2;
                
                // Pushing other elements into tempStack
                _nOp++;
                while (_stack.Count > 0)
                {
                    _tempStack.Push(_stack.Pop());
                    _nOp += 3;
                }

                // Restore stacks
                _stack = new Stack<int>(_tempStack);
                _tempStack.Clear();
                _nOp += 2;
            }
        }

        private void ShellSort()
        {
            _nOp += 2;
            int gap = _stack.Count / 2;

            while (gap >= 1)
            {
                _nOp++;

                _nOp += 2;
                for (int i = gap; i < _stack.Count; i++) // 2 + s(2 + ..)
                {
                    _nOp += 2;
                    
                    int curr = this[i]; // 2
                    int j = i; // 2
                    _nOp += 4;
                    
                    while (j > 0 && this[j - gap] > curr) // 5
                    {
                        _nOp += 5;
                        
                        this[j] = this[j - gap]; // 3
                        j -= gap; // 2
                        _nOp += 2;

                        _nOp += 2;
                        if (j - gap < 0) break;
                    }

                    this[j] = curr;
                    _nOp++;
                }

                gap /= 2;
                _nOp += 2;
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
            Console.WriteLine("Sorting...");
            ShellSort();
            Console.WriteLine($"N_op = {_nOp}");
            Show();
            _nOp = 0;
        }
    }
}