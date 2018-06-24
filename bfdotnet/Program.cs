using System;

namespace bfdotnet
{
    class Program
    {
        byte[] workspace;
        int spacePointer;

        static void Main(string[] args)
        {

            Console.WriteLine("Hello Brainfuck");




            Console.ReadLine();
        }

        internal static char[] loadSource()
        {
            //TODO: loading source from file
            return new char[1];
        }

        internal static void Initialisation(int workspaceSize)
        {
            //TODO: Initialise all necessary components.
        }

        internal static string prepareProgram()
        {
            //TODO: preperation of program for execution
            return "";
        }

        internal static void ArgsHandler(string[] args)
        {
            //TODO: handling params and program flow
        }

        internal static void ExecuteProgram(string program)
        {
            //BF program execution
        }

    }
}
