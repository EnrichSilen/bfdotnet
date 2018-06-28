 using System;
 using System.Text;
 using System.Reflection;

namespace bfdotnet
{
    class Program
    {
        static byte[] memory;
        static int memoryPtr;

        static string testProgram = "++++++++++[>+>+++>+++++++>++++++++++<<<<-]>>>++.>+.+++++++..+++.<<++.>+++++++++++++++.>.+++.------.--------.<<+.<.";
#if DEBUG
        static string buildType = "Debug";
#else
        static string buildType = "Release"; 
#endif

        static void Main(string[] args)
        {
            string test = typeof(Program).Assembly.GetName().Version.ToString();
            
            Console.WriteLine("BrainFu*k.NET ver." + test +" "+ buildType + " build");
            Initialisation(200);
            Console.WriteLine("VM inicialised with memory size of {0} bytes", memory.Length);

            printMemory();
            
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

            //foreach (var cell in memory)
            //{
            //    Console.Write(cell.ToString() + " | ");    
            //}

            int lines = memory.Length / 10;
            int counter = 0;

            for (int i = 1; i <= lines; i++)
            {
                for (int l = 0; l < 10; l++)
                {
                    Console.Write(normalizeValue(memory[counter]) + " ");
                    counter++;
                }
                counter -= 10;
                Console.Write("\t\t");
                for (int l = 0; l < 10; l++)
                {
                    if (memory[counter] > 31 && memory[counter] < 127)
                        Console.Write(Encoding.ASCII.GetString(new byte[] { memory[counter] }) + " ");
                    else
                        Console.Write(". ");
                    counter++;
                }
                Console.Write("\n");
            }
            
        }

        internal static string normalizeValue(byte value)
        {
            if (value < 10)
                return "00" + value.ToString();
            else if (value < 100)
                return "0" + value.ToString();
            else
                return value.ToString();
        }

        internal void InteractiveLoop()
        {
            while(true)
            {
                //TODO: infinite REP loop
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
