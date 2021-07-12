using System;
using System.Linq;

namespace step0_repl
{
    class Program
    {
        static string READ(string s)
        {
            return s;
        }

        static string EVAL(string s)
        {
            return s;
        }

        static string PRINT(string s)
        {
            return s;
        }

        static string rep(string line)
        {
            return PRINT(EVAL(READ(line)));
        }

        static void Main(string[] args)
        {

            string line = "";
            do
            {
                Console.Write("user> ");
                line = Console.ReadLine();
                if (line != null) {
                    Console.WriteLine(rep(line));
                }
            } while (line != null);
        }
    }
}
