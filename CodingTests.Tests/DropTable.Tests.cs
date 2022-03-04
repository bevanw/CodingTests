using FluentAssertions;
using Fractions;
using MathNet.Numerics.Distributions;
using NUnit.Framework;

namespace CodingTests.Tests
{
    [TestFixture]
    public class DropTableTests
    {
        [Test]
        public void GenerateDrop_Should_ListAllDropRates()
        {
            double rolls = 10000000;

            var table = new DropTableTests<string>();
            table.AddDrop("Leg Sword", new Fraction(1, 32767));
            table.AddDrop("Rare Sword", new Fraction(1, 512));
            table.AddDrop("Great Sword", new Fraction(1, 100));
            table.AddDrop("Sword", new Fraction(1, 50));
            table.AddDrop("Trash Sword", new Fraction(1, 10));
            table.AddDrop("Blank", new Fraction(1 - table.accumulatedWeight));

            var loot = new List<string>();
            for (int i = 0; i < rolls; i++)
            {
                var drop = table.GenerateDrop();
                loot.Add(drop);
            }

            Console.WriteLine($"Drop Table ({rolls:N0} rolls)");
            Console.WriteLine($"----------------------------");
            Console.WriteLine($"Item\t\tDrops\tRate");
            foreach (var item in loot.GroupBy(l => l).Select(g => new{ Drop = g.Key, Count = g.Count()}).OrderBy(g => g.Count))
            {
                Console.WriteLine($"{item.Drop}\t\t{item.Count}\t{Fraction.FromDoubleRounded(item.Count / rolls)}");
            }
        }

        [Test]
        public void CalculateDropRateChance()
        {
            var rareSwordDropRate = new Fraction(1, 512);
            int rolls = 2000;

            // Calculate the probability of getting 0 drops for a Rare Sword.
            // The reverse is the probability of getting AT LEAST 1.
            var b = new Binomial(rareSwordDropRate.ToDouble(), rolls);
            double result = 1 - b.Probability(0);
            Console.WriteLine($"Chance of getting at least 1 Rare Sword drop in 2k rolls: {result * 100}%");
        }

        [Test]
        public void GenerateDrop_Should_ReturnAmountOfDropsBasedOnWeight()
        {
            int rolls = 100000;
            int expectedPercentage = 50;

            var table = new DropTableTests<string>();
            table.AddDrop("Item1", new Fraction(1, 2));
            table.AddDrop("Item2", new Fraction(1, 2));

            var loot = new List<string>();
            for (int i = 0; i < rolls; i++)
            {
                var drop = table.GenerateDrop();
                loot.Add(drop);
            }

            int amountOfDrops = loot.Count(l => l == "Item2");
            double successProability = (amountOfDrops / (double)rolls * 100);
            Math.Round(successProability).Should().Be(expectedPercentage);
        }

        [Test]
        public void GenerateDrop_Should_ReturnAmountOfDropsBasedOnTotalWeighting()
        {
            double rolls = 100000;
            int expectedPercentage = 50;

            // 1/10 + 1/10 is 2/10, which means we still have 8/10 missing.
            // This should readjust to 1/2.
            var table = new DropTableTests<string>();
            table.AddDrop("Item1", new Fraction(1, 10));
            table.AddDrop("Item2", new Fraction(1, 10));

            var loot = new List<string>();
            for (int i = 0; i < rolls; i++)
            {
                var drop = table.GenerateDrop();
                loot.Add(drop);
            }

            int amountOfDrops = loot.Count(l => l == "Item2");
            double successProability = (amountOfDrops / (double)rolls * 100);
            Math.Round(successProability).Should().Be(expectedPercentage);
        }
    }
}

