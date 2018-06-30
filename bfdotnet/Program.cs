 using System;
 using System.Text;
 using System.Reflection;

namespace bfdotnet
{
    class Program
    {
        static byte[] memory;
        static int memoryPtr;
        static int liner = 0;

        static string testProgram = "++++++++++[>+>+++>+++++++>++++++++++<<<<-]>>>++.>+.+++++++..+++.<<++.>+++++++++++++++.>.+++.------.--------.<<+.<.";
        


        static void Main(string[] args)
        {
            ArgsHandler(args);

            Console.ReadLine();
        }

        internal static char[] loadSource(string path)
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
            bool interactive = true;
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-l":
                        interactive = false;
                        //loading code from sourse
                        break;
                    case "-m":
                        int l = 200;
                        int.TryParse(args[i + 1], out l);
                        Initialisation(l);
                        break;

                    default:
                        loadSource(args[i]);
                        break;
                }
            }
            if(interactive)
                InteractiveLoop();
        }


        internal static void InteractiveLoop()
        {
            if (memory == null)
                Initialisation(200);
            Utils.printProgramHead(memory.Length);

            while (true)
            {
                Console.Write("[{0}]: ", liner);
                codeEval(Console.ReadLine());

                liner++;
                Console.WriteLine();
            }
        }

        internal static void codeEval(string text)
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
                    case "#head":
                        Utils.printProgramHead(memory.Length);
                        break;
                    case "#ptr":
                        Console.WriteLine("Memory pointer: " + memoryPtr);
                        break;

                    case"#help":
                        Console.WriteLine(
                            "#exit\tExit BF\n" +
                            "#dump\tPrints memory dump\n" +
                            "#clear\tClear shell\n" +
                            "#head\tPrints head info\n" +
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
