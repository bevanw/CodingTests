
namespace CodingTests
{
    public class SmallestInteger
    {
        public int FindSmallestInteger(int[] A)
        {
            int smallestInt = 1;
            int length = A.Length;

            if (length == 0) return smallestInt;

            Array.Sort(A);

            if (A[0] > 1) return smallestInt;
            if (A[length - 1] <= 0) return smallestInt;

            for (int i = 0; i < length; i++)
            {
                if (A[i] == smallestInt)
                {
                    smallestInt++;
                }
            }

            return smallestInt;
        }
    }
}
