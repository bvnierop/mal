using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mal.Lib
{
    public class Reader
    {
        public class EofException : Exception { };


        private IEnumerable<string> _tokens;

        public static Reader ReadStr(string str)
        {
            return new Reader(Tokenize(str));
        }

        public static IEnumerable<string> Tokenize(string str)
        {

            var regex = new Regex(@"[\s,]*(~@|[\[\]{}()'`~^@]|""(?:\\.|[^\\""])*""?|;.*|[^\s\[\]{}('""`,;)]*)");
            var matches = regex.Matches(str);
            return matches.SelectMany(
                m => m.Captures.Select(c => c.ToString()))
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrEmpty(s));
        }

        public Reader(IEnumerable<string> tokens)
        {
            _tokens = tokens;
        }

        public string Next()
        {
            var next = Peek();
            _tokens = _tokens.Skip(1);
            return next;
        }

        public string Peek()
        {
            try
            {
                return _tokens.First();
            }
            catch (InvalidOperationException)
            {
                throw new EofException();
            }
        }
    }
}
