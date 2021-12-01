using FluentAssertions;
using NUnit.Framework;

namespace CodingTests.Tests
{
    [TestFixture]
    public class ChainLinkTests
    {

        [Test]
        public void ChainLink_Should_HaveNoLongestSide_When_ChainLinkIsClosed()
        {
            ChainLink leftMore = new ChainLink();
            ChainLink left = new ChainLink();
            ChainLink right = new ChainLink();
            ChainLink rightMore = new ChainLink();
            leftMore.Append(left);
            left.Append(right);
            right.Append(rightMore);
            rightMore.Append(leftMore);

            leftMore.LongerSide().Should().Be(Side.None);
            left.LongerSide().Should().Be(Side.None);
            right.LongerSide().Should().Be(Side.None);
            rightMore.LongerSide().Should().Be(Side.None);
        }

        [Test]
        public void ChainLink_Should_ReturnLongestSide_When_CheckingEachLengthSide()
        {
            ChainLink left = new ChainLink();
            ChainLink middle = new ChainLink();
            ChainLink right = new ChainLink();
            left.Append(middle);
            middle.Append(right);

            left.LongerSide().Should().Be(Side.Right);
            middle.LongerSide().Should().Be(Side.None);
            right.LongerSide().Should().Be(Side.Left);
        }
    }
}
