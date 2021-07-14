using FluentAssertions;
using NUnit.Framework;

namespace Mal.Lib.Test
{
    public class TestPrinter
    {
        [Test]
        public void PrStr_PrintsAtoms()
        {
            var atom = new MalAtom("1");
            Printer.PrStr(atom).Should().Be("1");
        }

        [Test]
        public void PrStr_PrintsLists()
        {
            var list = Reader.ReadForm(Reader.ReadStr("(+ 1 1)"));

            Printer.PrStr(list).Should().Be("(+ 1 1)");
        }

        [Test]
        public void PrStr_PrintsNestedLists()
        {
            // Should be automatic because the obvious implementation is already recursive
            // Do make sure we strip spaces.

            var list = Reader.ReadForm(Reader.ReadStr("( +   1 ( - 2 3 ) 4 )"));

            Printer.PrStr(list).Should().Be("(+ 1 (- 2 3) 4)");
        }
    }
}
