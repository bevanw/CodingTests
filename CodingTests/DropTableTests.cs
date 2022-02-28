
using Fractions;

namespace CodingTests
{
    public class DropTableTests<T>
    {
        private class Drop
        {
            public Drop(T item, double accumulatedWeight)
            {
                Item = item;
                AccumulatedWeight = accumulatedWeight;
            }

            public double AccumulatedWeight;
            public T Item;
        }

        private readonly List<Drop> entries = new();
        private readonly Random rand = new();

        private double accumulatedWeight;

        /// <summary>
        /// Adds a drop to the drop table.
        /// </summary>
        /// <param name="item">Item to drop.</param>
        /// <param name="dropWeight">Drop weight as a fraction..</param>
        public void AddDrop(T item, Fraction dropWeight)
        {
            accumulatedWeight += dropWeight.ToDouble();
            entries.Add(new Drop(item, accumulatedWeight));
        }

        /// <summary>
        /// Inserts a blank drop to fill up gaps.
        /// </summary>
        public void AddBlankDrop()
        {
            double weight = 1 - accumulatedWeight;
            accumulatedWeight += weight;
            entries.Add(new Drop(default, accumulatedWeight));
        }

        /// <summary>
        /// Generates a drop based on the drop rate.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T GenerateDrop()
        {
            double r = rand.NextDouble() * accumulatedWeight;

            foreach (Drop entry in entries)
            {
                if (entry.AccumulatedWeight >= r)
                    return entry.Item;
            }

            throw new InvalidOperationException("No entries added");
        }
    }
}
