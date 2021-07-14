using System;
using Mal.Lib;

namespace step1_read_print
{
    class Program
    {
        static MalType READ(string s)
        {
            return Reader.ReadForm(Reader.ReadStr(s));
        }

        static MalType EVAL(MalType expression)
        {
            return expression;
        }

        static string PRINT(MalType expression)
        {
            return Printer.PrStr(expression);
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
                try
                {
                    Console.WriteLine(rep(line));
                }
                catch (Reader.EofException)
                {
                    Console.WriteLine("ERROR: Unexpected end of input");
                }
            }
        }
    }
}
