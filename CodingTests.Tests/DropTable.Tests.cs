using FluentAssertions;
using Fractions;
using NUnit.Framework;

namespace CodingTests.Tests
{
    [TestFixture]
    public class DropTableTests
    {
        [Test]
        public void GenerateDrop_Should_ListAllDropRates()
        {
            double rolls = 1000;

            var table = new DropTableTests<string>();
            //table.AddDrop("Legend Sword", new Fraction(1, 1000));
            //table.AddDrop("Rare Sword", new Fraction(1, 100));
            //table.AddDrop("Great Sword", new Fraction(1, 50));
            //table.AddDrop("Sword", new Fraction(1, 25));
            //table.AddDrop("Trash Sword", new Fraction(1, 10));
            //table.AddBlankDrop();

            table.AddDrop("L Sword", new Fraction(1, 10));
            table.AddDrop("R Sword", new Fraction(1, 10));
            table.AddBlankDrop();

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
                Console.WriteLine($"{item.Drop ?? "Blanks"}\t\t{item.Count}\t{Fraction.FromDoubleRounded(item.Count / rolls)}");
            }
        }

        [Test]
        public void GenerateDrop_Should_ReturnAmountOfDropsBasedOnWeight()
        {
            double rolls = 1000;
            double expectedPercentage = 50;

            var table = new DropTableTests<string>();
            table.AddDrop("Item1", 50);
            table.AddDrop("Item2", 50);

            var loot = new List<string>();
            for (int i = 0; i < rolls; i++)
            {
                var drop = table.GenerateDrop();
                loot.Add(drop);
            }

            int amountOfDrops = loot.Count(l => l == "Item2");
            (amountOfDrops / rolls * 100).Should().Be(expectedPercentage);
        }
    }
}

