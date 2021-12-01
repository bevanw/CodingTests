using NUnit.Framework;

namespace CodingTests.Tests
{
    [TestFixture]
    public class SmallestIntegerTests
    {
        [TestCase(new int[] { 1, 3, 6, 4, 1, 2 }, ExpectedResult = 5)]
        [TestCase(new int[] { 1, 2, 3 }, ExpectedResult = 4)]
        [TestCase(new int[] { -1, -3 }, ExpectedResult = 1)]
        public int SmallestInteger_Should_ReturnSmallestIntegerInPositiveArray(int[] array)
        {
            return new SmallestInteger().FindSmallestInteger(array);
        }
    }
}
