using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Mal.Lib.Tests
{
    public class TestReader
    {
        [Test]
        public void Peek_ReturnsTheFirstToken()
        {
            var reader = new Reader(new string[] { "first", "second" });

            var peek1 = reader.Peek();
            var peek2 = reader.Peek();

            peek1.Should().BeEquivalentTo(peek2);
        }

        [Test]
        public void Next_ReturnsTheFirstTokenAndIncrementsThePosition()
        {
            var reader = new Reader(new string[] { "first", "second" });

            var next1 = reader.Next();
            var next2 = reader.Next();

            next1.Should().Be("first");
            next2.Should().Be("second");
        }

        [Test]
        public void Next_RaisesWhenThereAreNoTokens()
        {
            var reader = new Reader(new string[0]);

            reader.Invoking(r => r.Next())
                .Should().Throw<Reader.EofException>();
        }

        [Test]
        public void Peek_RaisesWhenThereAreNoTokens()
        {
            var reader = new Reader(new string[0]);

            reader.Invoking(r => r.Peek())
                .Should().Throw<Reader.EofException>();
        }

        [Test]
        public void Tokenize_TokenizesTheInput()
        {
            var input = "(+ 1 1)";

            Reader.Tokenize(input).Should().BeEquivalentTo(
                new string[] { "(", "+", "1", "1", ")" });
        }


        [Test]
        public void Tokenize_TreatsCommasAsWhitespace()
        {
            var input = "(1 2, 3)";

            Reader.Tokenize(input).Should().BeEquivalentTo(
                new string[] { "(", "1", "2", "3", ")" });
        }

        /// ~read_form~ peeks at the first token of the given ~Reader~ object and calls
        /// either ~read_list~ or ~read_atom~ based on that token. It returns a mal type.

        [Test]
        public void ReadForm_ReadsAnAtom()
        {
            var input = "1";
            var reader = Reader.ReadStr(input);

            Reader.ReadForm(reader).Should().BeEquivalentTo(new MalAtom("1"));
        }

        [Test]
        public void ReadForm_ReadsAList()
        {
            var input = "(+ 1 1)";

            var reader = Reader.ReadStr(input);

            var result = new MalList(null);
            result.Add(new MalAtom("+"));
            result.Add(new MalAtom("1"));
            result.Add(new MalAtom("1"));

            Reader.ReadForm(reader).Should().BeEquivalentTo(result);
        }

        [Test]
        public void ReadForm_ReadsNestedLists()
        {
            var input = "(+ 1 (+ 2 3) 4)";

            var reader = Reader.ReadStr(input);

            var innerList = new MalList(null);
            innerList.Add(new MalAtom("+"));
            innerList.Add(new MalAtom("2"));
            innerList.Add(new MalAtom("3"));

            var outerList = new MalList(null);
            outerList.Add(new MalAtom("+"));
            outerList.Add(new MalAtom("1"));
            outerList.Add(innerList);
            outerList.Add(new MalAtom("4"));

            Reader.ReadForm(reader).Should().BeEquivalentTo(outerList);
        }

        [Test]
        public void ReadForm_FailsWhenTheListNeverEnds()
        {
            var input = "(+ 1";

            var reader = Reader.ReadStr(input);

            FluentActions.Invoking(() => Reader.ReadForm(reader))
                .Should()
                .Throw<Reader.EofException>();
        }
    }
}
