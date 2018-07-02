using System;
using System.Collections.Generic;
using System.Text;

namespace bfdotnet
{
    class Utils
    {
        internal static void MemoryDump(byte[] memory)
        {
            //Console.Clear();
            //Console.WriteLine("Printing content of memory");

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

        private static string normalizeValue(byte value)
        {
            if (value < 10)
                return "00" + value.ToString();
            else if (value < 100)
                return "0" + value.ToString();
            else
                return value.ToString();
        }

        internal static void printProgramHead(int memoryLenght)
        {

#if DEBUG
            string buildType = "Debug build";
#else
            string buildType = "Release"; 
#endif

            Console.WriteLine("BrainFu*k.NET ver." + ProgramInfo.version + " " + buildType + " \nVM inicialised with memory size of " + memoryLenght + " bytes");
        }
    }
}
