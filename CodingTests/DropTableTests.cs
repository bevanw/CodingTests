
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

        private readonly List<Drop> Entries = new();

        private readonly Random rand = new();

        public double accumulatedWeight;

        /// <summary>
        /// Adds a drop to the drop table.
        /// </summary>
        /// <param name="item">Item to drop.</param>
        /// <param name="dropWeight">Drop weight as a fraction.</param>
        public void AddDrop(T item, Fraction dropWeight)
        {
            accumulatedWeight += dropWeight.ToDouble();
            Entries.Add(new Drop(item, accumulatedWeight));
        }

        /// <summary>
        /// Generates a drop based on the drop rate.
        /// </summary>
        /// <returns>A drop.</returns>
        /// <exception cref="InvalidOperationException">No entries added.</exception>
        public T GenerateDrop()
        {
            // Useful incase we allow drop weighting <> 1 - has no effect if accumulatedWeight is 1 as X*1 = X.
            double r = rand.NextDouble() * accumulatedWeight;

            // An example:
            //    - Three items are added, all with 1/10 drop rate.
            //    - Each drop is accumulated at 0.1, 0.2, 0.3.
            //    - Add a blank drop at 0.7 to accomodate the rest.
            //    - As R has been multiplied by the total weighting, it will always be restricted to within the ranges of drops.
            foreach (Drop entry in Entries)
                if (entry.AccumulatedWeight >= r)
                    return entry.Item;

            throw new InvalidOperationException("No entries added");
        }
    }
}
