// 16. Библиотека классов, Стек, Алгоритм Шелла

namespace StackShellSort
{
    class Program
    {
        static void Main(string[] args)
        {
            MyStack stack = new MyStack(new []{6, 2, 1, 4, 3, 5, 11, 23, 5});
            stack.Show();

            stack.ShellSort();
            stack.Show();
        }
    }
}