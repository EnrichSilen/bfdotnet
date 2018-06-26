 using System;
 using System.Text;

namespace bfdotnet
{
    class Program
    {
        static byte[] memory;
        static int memoryPtr;

        static string testProgram = "++++++++++[>+>+++>+++++++>++++++++++<<<<-]>>>++.>+.+++++++..+++.<<++.>+++++++++++++++.>.+++.------.--------.<<+.<.";

        static void Main(string[] args)
        {

            Console.WriteLine("Hello Brainfuck");
            Initialisation(20);
            Console.WriteLine("Inicialisation with pointer on {0} and memory with size of {1} bytes", memoryPtr, memory.Length);
            ExecuteProgram(testProgram);

            //printMemory();




            Console.ReadLine();
        }

        internal static char[] loadSource()
        {
            //TODO: loading source from file
            return new char[1];
        }

        internal static void Initialisation(int workspaceSize)
        {
            memoryPtr = 0;
            memory = new byte[workspaceSize];

            //Init of warkspace to 0.
            for (int i = 0; i < memory.Length; i++)
            {
                memory[i] = 0;
            }
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

        internal static void printMemory()
        {
            Console.Clear();
            Console.WriteLine("Printing content of memory");

            foreach (var cell in memory)
            {
                Console.Write(cell.ToString() + " | ");    
            }
        }

        internal static void ExecuteProgram(string program)
        {
            for (int i = 0; i < program.Length; i++)
            {
                switch (program[i])
                {
                    case '+':
                        memory[memoryPtr]++;
                        break;
                    case '-':
                        memory[memoryPtr]--;
                        break;
                    case '>':
                        memoryPtr++;
                        break;
                    case '<':
                        memoryPtr--;
                        break;
                    case '.':
                        Console.WriteLine("");
                        Console.Write(Encoding.ASCII.GetString(new byte[] { memory[memoryPtr] }));
                        break;
                    case ',':
                        memory[memoryPtr] = byte.Parse(Console.ReadLine());
                        break;
                    case '[':

                        if (memory[memoryPtr] == 0)
                        {
                            int skip = 0;
                            int localPtr = i + 1;
                            while (program[localPtr] != ']' || skip > 0)
                            {
                                if (memory[memoryPtr] == 0)
                                {
                                    skip++;
                                }
                                else if (program[localPtr] == ']')
                                {
                                    skip--;
                                }
                                localPtr++;
                                i = localPtr;
                            } 
                        }
                        break;

                    case']':
                        if(memory[memoryPtr] != 0)
                        {
                            int skip = 0;
                            int localPtr = i - 1;
                            while(program[localPtr] != '[' | skip > 0)
                            {
                                if(program[localPtr] == ']')
                                {
                                    skip++;
                                }
                                else if(program[localPtr] == '[')
                                {
                                    skip--;
                                }
                                localPtr--;
                                i = localPtr;
                            }
                        }
                        break;
                }
            }
        }

    }
}
