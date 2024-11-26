using System.Collections.Concurrent;

namespace ConcurrentLesson
{
    internal class Program
    {
        static ConcurrentDictionary<string, int> _dic = new();
        static void Main(string[] args)
        {
            Parallel.Invoke(
                ClientProcess,
                BackgroundProcess
            );
        }

        static void ClientProcess()
        {
            while (true)
            {
                Console.WriteLine("1 - Добавить книгу;\n2 - Вывести список непрочитанного;\n3 - Выйти");

                var userCommand = Console.ReadLine();

                switch (userCommand)
                {
                    case "1":
                        Console.WriteLine("Введите название книги:");
                        var userBook = Console.ReadLine();
                        _dic.TryAdd(userBook, 0);
                        Console.WriteLine(_dic[userBook]);
                        break;
                    case "2":
                        foreach (var book in _dic)
                        {
                            Console.WriteLine($"{book.Key} -- {book.Value}");
                        }
                        break;
                    case "3":
                        return;

                }
            }
        }

        static void BackgroundProcess()
        {
            Console.WriteLine("ParallelStart");
            while (true)
            {
                foreach (var book in _dic)
                {
                    if (book.Value < 100)
                    {
                        _dic[book.Key] += 1;
                    }
                    else
                    {
                        _dic.TryRemove(book);
                    }
                }
                Thread.Sleep(1000);
            }
        }
    }
}
