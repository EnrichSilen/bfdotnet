 using System;
 using System.Text;
 using System.Reflection;
using System.IO;

namespace bfdotnet
{
    class Program
    {
        static byte[] memory;
        static int memoryPtr;
        static int liner = 0;

        static string testProgram = "++++++++++[>+>+++>+++++++>++++++++++<<<<-]>>>++.>+.+++++++..+++.<<++.>+++++++++++++++.>.+++.------.--------.<<+.<.";
        static string program = string.Empty;
        


        static void Main(string[] args)
        {
            ArgsHandler(args);
            Console.ReadLine();
        }

        internal static void loadSource(string path)
        {
            program = File.ReadAllText(path);
        }

        internal static void Initialisation(int workspaceSize)
        {
            memoryPtr = 0;
            memory = new byte[workspaceSize];
            ProgramInfo.memorySize = memory.Length;

            //Init of workspace to 0.
            for (int i = 0; i < memory.Length; i++)
            {
                memory[i] = 0;
            }
        }


        internal static void ArgsHandler(string[] args)
        {
            bool interactive = true;
            int memorySize = 200;

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-m":
                        int.TryParse(args[i + 1], out memorySize);
                        break;

                    default:
                        interactive = false;
                        loadSource(args[i]);
                        break;
                }
            }
            Initialisation(memorySize);
            if (interactive)
                InteractiveLoop();
            else
                PassthroughExecution();
        }


        internal static void InteractiveLoop()
        {
            
            Utils.printProgramHead();

            while (true)
            {
                Console.Write("[{0}]: ", liner);
                inputEval(Console.ReadLine());

                liner++;
                Console.WriteLine();
            }
        }

        internal static void PassthroughExecution()
        {
            ExecuteProgram(program);
        }


        internal static void inputEval(string text)
        {
            if (text.Length == 0)
            {
                liner--;
                return;
            }
                

            if(text[0] == '#')
            {
                switch(text.ToLower())
                {
                    case "#exit":
                        Environment.Exit(0);
                        break;
                    case "#dump":
                        Utils.MemoryDump(memory);
                        break;
                    case "#clear":
                        Console.Clear();
                        break;
                    case "#info":
                        Utils.printProgramHead();
                        break;
                    case "#ptr":
                        Console.WriteLine("Memory pointer: " + memoryPtr);
                        break;
                    case "#reboot":
                        Initialisation(ProgramInfo.memorySize);
                        break;

                    case "#help":
                        Console.WriteLine(
                            "#exit\tExit BF\n" +
                            "#dump\tPrints memory dump\n" +
                            "#reboot\tReboots VM\n" +
                            "#clear\tClear shell\n" +
                            "#info\tPrints VM info\n" +
                            "#ptr\tPrints memory pointer value"
                            );
                        break;


                    default:
                        Console.WriteLine("Unknown command " + text + "");
                        break;

                }
            }
            else
            {
                ExecuteProgram(text);
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

                    //ignores all non program chars
                    default: break;
                }
            }
        }

    }
}
