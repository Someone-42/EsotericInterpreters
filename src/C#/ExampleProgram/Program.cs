using System;
using System.Collections.Generic;

namespace ExampleProgram
{
    public static class Program
    {
        static void Main(string[] args)
        {
            /*
            ChickenPrograms.HelloWorld_CSx11();

            ChickenPrograms.CompareAB_CSx11(42, 45); // Returns falsy
            ChickenPrograms.CompareAB_CSx11(int.MaxValue - 3, 1); // For speed reasons :] // Returns truthy
            */
            //PspspsCodeTest.RunFibonnaci();

            string s = Console.ReadLine();

            PspspsCodeTest.RunHashing(s);

            List<short> str = new List<short>(4);
            foreach (char c in s)
                str.Add((short)c);

            Console.WriteLine(JavaHash(str.ToArray()));
            Console.ReadKey();
        }

        public static int JavaHash(Int16[] arr)
        {
            int h = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                h = h * 31 + arr[i];
            }
            return h;
        }

    }
}
