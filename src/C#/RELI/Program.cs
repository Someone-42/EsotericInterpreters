using System;

namespace runtime
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("RELI 1.0 - Esoterics language interpreter");
            Console.WriteLine("Write 'exit' to exit the program, write 'help' for help\n");
            string? line;
            do
            {
                Console.Write('>');
                line = Console.ReadLine();
                if (!(line is null))
                    Commands.TryInvokeCommand(line);
            } while (line != "exit");
        }
    }
}