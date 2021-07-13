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
    }
}
