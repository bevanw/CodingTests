
namespace CodingTests
{
    // A retail store chain wants to expand into a new neighborhood.
    // To maximize the number of clients, the new branch should be at a distance of no more than K from all the houses in the neighborhood.
    // A is a matrix of size N x M, representing the neighborhood as a rectangular grid, in which each cell is either an integer 0 (an empty plot) or 1 (a house).
    // The distance between two cells is calculated as the minimum number of cell borders(regardless of whether the cells on the way are empty or occupied) that one has to cross to move from the source to the target cell(without moving through corners).
    // A store can be only built on an empty plot.How many suitable locations are there?
    //
    // For example, given K = 2 and matrix A = [ [0, 0, 0, 0], [0, 0, 1, 0], [1, 0, 0, 1] ], houses are located in cells with coordinates (2, 3), (3, 1) and (3, 4).
    // We can build a new store on two empty plots that are close enough to all the houses. The first possible empty plot is located at (3, 2).
    // The distance to the first house at (2,3) is 2, the distance to the second house at (3, 1) is 1, and the third house at (3, 4) is at a distance of 2.
    // The second possible empty plot is located at (3, 3). The distances to the first, second and third houses are respectively, 1, 2 and 1.
    public class RetailStore
    {
        public int FindLocations(int K, int[][] A)
        {
            // Build our grid using objects.
            Grid grid = new Grid(A.Length + 1, A[0].Length + 1);
            for (int row = 0; row < A.Length; row++)
            {
                for (int column = 0; column < A[row].Length; column++)
                {
                    grid.AddGridSpace(row + 1, column + 1, A[row][column] == 0);
                }
            }

            return grid.FindEmptyLocations(K);
        }
    }

    public class Grid
    {
        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
        }

        public Dictionary<GridLocation, GridPlot> GridSpaces { get; } = new Dictionary<GridLocation, GridPlot>();
        public int Rows { get; }
        public int Columns { get; }

        public void AddGridSpace(int row, int column, bool isEmpty)
        {
            GridSpaces.Add(new GridLocation(row, column), new GridPlot(isEmpty));
        }

        public int FindEmptyLocations(int maxDistance)
        {
            // Must be at least one house.
            if (Rows == 1 && Columns == 1)
                return 0;

            // Attempt to work out if the empty plot is buildable based on the distance to the nearest house.
            foreach (var emptyGridSpace in GridSpaces.Where(g => g.Value.IsEmpty))
            {
                bool isBuildable = true;
                foreach (var house in GridSpaces.Where(g => !g.Value.IsEmpty))
                {
                    int rowDistance = Math.Abs(emptyGridSpace.Key.Row - house.Key.Row);
                    int columnDistance = Math.Abs(emptyGridSpace.Key.Column - house.Key.Column);
                    if ((rowDistance + columnDistance) > maxDistance)
                    {
                        isBuildable = false;
                        break;
                    }
                }

                emptyGridSpace.Value.IsBuildable = isBuildable;
            }

            return GridSpaces.Values.Count(g => g.IsBuildable);
        }
    }

    public class GridPlot
    {
        public GridPlot(bool isEmpty)
        {
            IsEmpty = isEmpty;
        }

        public bool IsEmpty { get; }
        public bool IsBuildable { get; set; }
    }

    public struct GridLocation
    {
        public GridLocation(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; }
        public int Column { get; }
    }
}
