// 16. Библиотека классов, Стек, Алгоритм Шелла

namespace StackShellSort
{
    class Program
    {
        static void Main(string[] args)
        {
            MyStack stack = new MyStack(new []{6, 10, 1, 5, 2, 4, 3, 7, 9, 8});
            stack.Show();

            stack.ShellSort();
            stack.Show();
        }
    }
}