using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace xThread;

class Program
{
    static void Main(string[] args)
    {
        new xThread();
    }
}

partial class xThread
{
    readonly ConcurrentQueue<byte[]>? fifo = null;
    readonly ManualResetEvent? pauseEvent = null;
    readonly Thread? thread = null;

    internal xThread()
    {
        thread = new Thread(Method1);
        pauseEvent = new ManualResetEvent(false);
        fifo = new ConcurrentQueue<byte[]>();

        thread.Start();
        Task.Factory.StartNew(Method2);

        while (true)
        {
            Console.WriteLine("Please input 'S' to start or input 'Q' to quit ...");
            var key = Console.Read();
            if (key == 'S' | key == 's')
            {
                pauseEvent.Set();
                Console.WriteLine("Start ...");
                Thread.Sleep(100);
            }
            else if (key == 'Q' | key == 'q')
            {
                pauseEvent.Reset();
                Console.WriteLine("Quit ...");
                Thread.Sleep(100);
            }
        }
    }

    void Method1()
    {
        var random = new Random();
        do
        {
            pauseEvent.WaitOne(System.Threading.Timeout.Infinite);
            var array = new byte[10];
            random.NextBytes(array);
            fifo.Enqueue(array);
            Console.Write($"{nameof(array)} in Method1: ");
            for (var idx = 0; idx < array.Length; idx++)
            {
                Console.Write($"{array[idx]} ");
            }
            Console.WriteLine();
            System.Threading.Thread.Sleep(500);
        } while (true);
    }

    void Method2()
    {
        do
        {
            pauseEvent.WaitOne(System.Threading.Timeout.Infinite);
            var result = fifo.TryDequeue(out var array);
            if (result && array != null)
            {
                Console.Write($"{nameof(array)} in Method2: ");
                for (var idx = 0; idx < array.Length; idx++)
                {
                    Console.Write($"{array[idx]} ");
                }
            }
            Console.WriteLine();
            System.Threading.Thread.Sleep(800);
        } while (true);
    }
}
