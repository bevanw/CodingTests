using FluentAssertions;
using NUnit.Framework;

namespace CodingTests.Tests
{
    [TestFixture]
    public class RetailStoreTests
    {
        [Test]
        public void RetailStore_Should_FindTwoLocations_When_HousesHaveTwoEmptyPlots()
        {
            RetailStore store = CreateRetailStore();

            int[][] A = new int[][] { new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 1, 0 }, new int[] { 1, 0, 0, 1 } };
            int K = 2;

            store.FindLocations(K, A).Should().Be(2);
        }

        [Test]
        public void RetailStore_Should_FindTwoLocations_When_HousesHaveAdjacentSpace()
        {
            RetailStore store = CreateRetailStore();

            int[][] A = new int[][] { new int[] { 0, 1 }, new int[] { 0, 0 } };
            int K = 1;

            store.FindLocations(K, A).Should().Be(2);
        }

        [Test]
        public void RetailStore_Should_FindEightLocations_When_HousesHaveMultipleEmpltyPlots()
        {
            RetailStore store = CreateRetailStore();

            int[][] A = new int[][] { new int[] { 0, 0, 0, 1 }, new int[] { 0, 1, 0, 0 }, new int[] { 0, 0, 1, 0 }, new int[] { 1, 0, 0, 0 }, new int[] { 0, 0, 0, 0 } };
            int K = 4;

            store.FindLocations(K, A).Should().Be(8);
        }

        [Test]
        public void RetailStore_Should_FindNoLocations_When_SingleHouse()
        {
            RetailStore store = CreateRetailStore();

            int[][] A = new int[][] { new int[] { 1 } };
            int K = 4;

            store.FindLocations(K, A).Should().Be(0);
        }

        /// <summary>
        /// Test factory.
        /// </summary>
        /// <returns>A new retail store.</returns>
        private RetailStore CreateRetailStore()
        {
            return new RetailStore();
        }
    }
}
