using System.Collections.Generic;

namespace Mal.Lib
{
    public class MalType
    {
        public string Raw { get; }

        public MalType(string raw)
        {
            Raw = raw;
        }
    }

    public class MalAtom : MalType
    {
        public MalAtom(string raw) : base(raw)
        {
        }
    }

    public class MalList : MalType
    {
        private List<MalType> _elements = new List<MalType>();
        public IEnumerable<MalType> Elements { get => _elements; }

        public MalList(string raw) : base(raw)
        {
        }

        public void Add(MalType element)
        {
            _elements.Add(element);
        }
    }
}
