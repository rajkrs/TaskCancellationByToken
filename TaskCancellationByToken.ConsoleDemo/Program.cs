using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCancellationByToken.ConsoleDemo
{
    class Program
    {

        public static Task PrintWithCancellationToken(int count, CancellationToken cancellationToken)
        {
            for (int i = 1; i <= count; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
                else {
                    Console.Write(nameof(PrintWithCancellationToken));
                    Console.WriteLine(i);
                }
                Thread.Sleep(10);
            }

            return Task.FromResult(0);
        }


        public static Task Print(int count)
        {

            for (int i = 1; i <= count; i++)
            {
                Console.Write(nameof(Print));
                Console.WriteLine(i);
                Thread.Sleep(10);
            }
            return Task.FromResult(0);

        }




        static void Main(string[] args)
        {
            Parallel.Invoke(async () =>
                await Print(500),
                async () =>
                {
                    using (CancellationTokenSource cTokenSource = new CancellationTokenSource())
                    {
                        var keyBoardTask = Task.Run(() =>
                        {
                            Console.WriteLine("Press enter to cancel");
                            Console.ReadKey();

                            // Cancel the task
                            cTokenSource.Cancel();
                        });

                        await PrintWithCancellationToken(500, cTokenSource.Token);
                    }
                }
                );

            Console.WriteLine("Hello World!");
        }
    }
}
