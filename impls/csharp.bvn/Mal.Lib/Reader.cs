using System;
using System.Collections.Generic;
using System.Linq;

namespace Mal.Lib
{
    public class Reader
    {
        public class EofException : Exception { };


        private IEnumerable<string> _tokens;

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
