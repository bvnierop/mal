using System;

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
            while ((line = ReadLine.Read("user> ")) != null)
            {
                Console.WriteLine(rep(line));
            }
        }
    }
}
