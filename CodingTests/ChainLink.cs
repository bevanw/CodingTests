namespace CodingTests
{
    public enum Side { None, Left, Right }

    public class ChainLink
    {
        public ChainLink? Left { get; private set; }
        public ChainLink? Right { get; private set; }

        public void Append(ChainLink rightPart)
        {
            if (this.Right != null)
                throw new InvalidOperationException("Link is already connected.");

            this.Right = rightPart;
            rightPart.Left = this;
        }

        public Side LongerSide()
        {
            int rightLength = GetSideLength(this.Right, this, Side.Right);
            int leftLength = GetSideLength(this.Left, this, Side.Left);
            if (rightLength == leftLength)
                return Side.None;

            return rightLength > leftLength ? Side.Right : Side.Left;
        }

        private int GetSideLength(ChainLink? link, ChainLink? root, Side side)
        {
            if (link is null)
                return 0;
            if (link == root)
                return 0;

            return (side == Side.Left ? GetSideLength(link.Left, root, side) : GetSideLength(link.Right, root, side)) + 1;
        }
    }
}