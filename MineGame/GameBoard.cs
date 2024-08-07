namespace MineGame;

public interface IGameBoard
{
    public int BoardSize { get; }
    public Tuple<int, int> PlayerLocation { get; }
    public int NumberOfMoves { get; }
    public MoveResult MovePlayerUp();
    public MoveResult MovePlayerDown();
    public MoveResult MovePlayerLeft();
    public MoveResult MovePlayerRight();
}

public class GameBoard : IGameBoard
{
    private HashSet<Tuple<int, int>> mineLocations;

    public int BoardSize {  get; private set; }

    public Tuple<int, int> PlayerLocation { get; private set; }

    public int NumberOfMoves { get; private set; }

    public GameBoard(int boardSize, int noMines)
    {
        //TODO check boardsize is 2 or greater
        BoardSize = boardSize;
        mineLocations = GenerateMineLocations(noMines);
        PlayerLocation = new Tuple<int, int>(1, 1);
        NumberOfMoves = 0;
    }

    //Note boards starts at 1,1 not 0,0
    
    private HashSet<Tuple<int, int>> GenerateMineLocations(int noMines)
    {
        if (noMines >= (BoardSize*BoardSize - 2))
        {
            throw new Exception("Not enough available squares for the number of mines. Must be less than boardSize^2 - 2 mines");
        }

        Random random = new Random();
        
        HashSet<Tuple<int, int>> mines = new HashSet<Tuple<int, int>>();

        //This is very inefficient and could take a long time if generating a number of mines close to the size of the board, especially with a larger board.
        //It should do for this task, but could be improved using a sortedset and by not constantly looping and potentially adding the same mines again
        while (mines.Count < noMines)
        {
            int x = random.Next(1, BoardSize);
            int y = random.Next(1, BoardSize);

            //Don't place mines on the starting or end position
            if ((x == 0 ) && (y == 0) || (x == BoardSize && y == BoardSize))
            {
                continue;
            }

            mines.Add(Tuple.Create(x, y));
        }

        return mines;
    }

    public MoveResult MovePlayerUp()
    {
        return MovePlayer(0, 1);
    }

    public MoveResult MovePlayerDown()
    {
        return MovePlayer(0, -1);
    }

    public MoveResult MovePlayerLeft()
    {
        return MovePlayer(-1, 0);
    }

    public MoveResult MovePlayerRight()
    {
        return MovePlayer(1, 0);
    }

    /// <summary>
    /// Moves the player location by the number of cells passed.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>
    /// True if move is valid. Otherwise False.
    /// </returns>
    private MoveResult MovePlayer(int x, int y)
    {
        int newX = PlayerLocation.Item1;
        int newY = PlayerLocation.Item2;
        if (x != 0)
        {
            newX = PlayerLocation.Item1 + x;
            if (newX < 1)
            {
                //Out of bounds to the left
                return MoveResult.Invalid;
            }
            if (newX > BoardSize)
            {
                //Out of bounds to the right
                return MoveResult.Invalid;
            }
            
        }
        if (y != 0)
        {
            newY = PlayerLocation.Item2 + y;
            if (newY < 1)
            {
                //Out of bounds to the bottom
                return MoveResult.Invalid;
            }
            if (newY > BoardSize)
            {
                //Out of bounds to the top
                return MoveResult.Invalid;
            }
            
        }

        PlayerLocation = Tuple.Create(newX, newY);
        NumberOfMoves++;

        if (PlayerLocation.Item1 == BoardSize && PlayerLocation.Item2 == BoardSize)
        {
            return MoveResult.Destination;
        }

        if (mineLocations.Contains(PlayerLocation))
        {
            mineLocations.Remove(PlayerLocation);
            return MoveResult.Hit;
        }

        return MoveResult.Miss;
    }
}

public enum MoveResult
{
    Invalid,
    Hit,
    Miss,
    Destination,
    Skip
}
