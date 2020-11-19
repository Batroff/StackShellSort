using System;

namespace StackShellSort
{
    public class Menu
    {
        private MyStack _stack;
        private bool _inMenu = true;
        private ConsoleKeyInfo _keyInfo;
        
        public void Start()
        {
            while (_inMenu)
            {
                Console.Write("Press F to start program, Q to exit >> ");
                _keyInfo = Console.ReadKey(true);

                switch (_keyInfo.Key)
                {
                    case ConsoleKey.F:
                        Console.Write("\nInsert stack length >> ");
                        int count = Int32.Parse(Console.ReadLine() ?? "10");
                        _stack.FillStack(count);
                        _stack.Sort();
                        break;
                    case ConsoleKey.Q:
                        _inMenu = false;
                        break;
                }
            }
        }
    }
}