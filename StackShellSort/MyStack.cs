using System;
using System.Collections.Generic;

namespace StackShellSort
{
    public class MyStack : Stack<int>
    {
        private Stack<int> _tempStack = new Stack<int>();
        private Stack<int> _stack;

        public MyStack(int[] arr)
        {
            _stack = new Stack<int>();

            foreach (int a in arr)
                _stack.Push(a);
        }

        public void Show()
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
                for (int i = 0; i < index; i++)
                    _tempStack.Push(_stack.Pop());

                // Get index-element
                int res = _stack.Peek();

                // Pushing other elements into tempStack
                while (_stack.Count > 0)
                    _tempStack.Push(_stack.Pop());

                // Restore stacks
                _stack = new Stack<int>(_tempStack);
                _tempStack.Clear();

                return res;
            }
            set
            {
                // Pop elements before index-element and pushing into tempStack
                for (int i = 0; i < index; i++)
                    _tempStack.Push(_stack.Pop());

                _stack.Pop();
                _stack.Push(value);

                // Pushing other elements into tempStack
                while (_stack.Count > 0)
                    _tempStack.Push(_stack.Pop());

                // Restore stacks
                _stack = new Stack<int>(_tempStack);
                _tempStack.Clear();
            }
        }

        public void ShellSort()
        {
            int gap = _stack.Count / 2;

            while (gap >= 1)
            {
                for (int i = gap; i < _stack.Count; i++)
                {
                    int curr = this[i];
                    int j = i;

                    while (j > 0 && this[j - gap] > curr)
                    {
                        this[j] = this[j - gap];
                        j -= gap;

                        if (j - gap < 0) break;
                    }

                    this[j] = curr;
                }

                gap /= 2;
            }
        }
    }
}