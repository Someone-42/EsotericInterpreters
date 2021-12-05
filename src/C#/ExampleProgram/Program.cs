using System;

namespace ExampleProgram
{
    public static class Program
    {
        static void Main(string[] args)
        {

            ChickenPrograms.HelloWorld_CSx11();

            ChickenPrograms.CompareAB_CSx11(42, 45);
            ChickenPrograms.CompareAB_CSx11(int.MaxValue - 3, 1); // For speed reasons :]

        }
    }
}
