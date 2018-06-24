using System;

namespace bfdotnet
{
    class Program
    {
        static byte[] workspace;
        static int spacePointer;

        static string testProgram = "++++>+++>++>+<<<";

        static void Main(string[] args)
        {

            Console.WriteLine("Hello Brainfuck");
            Initialisation(20);
            Console.WriteLine("Inicialisation with pointer on {0} and workspace with size of {1} bytes", spacePointer, workspace.Length);





            Console.ReadLine();
        }

        internal static char[] loadSource()
        {
            //TODO: loading source from file
            return new char[1];
        }

        internal static void Initialisation(int workspaceSize)
        {
            spacePointer = 0;
            workspace = new byte[workspaceSize];
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
            foreach( char c in program)
            {

            }

        }

    }
}
