using MineGame;

namespace MineGameTests;

[TestClass]
public class GameBoardTests
{
    [TestMethod]
    public void GameBoard_Init_CorrectSetup()
    {
        int boardSize = 8;
        int noMines = 3;
        GameBoard gameBoard = new GameBoard(boardSize, noMines);

        Assert.AreEqual(boardSize, gameBoard.BoardSize);
        Assert.AreEqual(Tuple.Create(1,1), gameBoard.PlayerLocation);
        Assert.AreEqual(0, gameBoard.NumberOfMoves);

        int hits = 0;
        MoveResult mr;
        //check that number of mines is correct
        for (int i = 0; i < boardSize-1; i++)
        {
            for (int j = 0; j < boardSize - 1; j++)
            {
                mr = gameBoard.MovePlayerRight();
                if (mr == MoveResult.Hit)
                {
                    hits++;
                }
            }
            for (int j = 0; j < boardSize - 1; j++)
            {
                gameBoard.MovePlayerLeft();
            }
            mr = gameBoard.MovePlayerUp();
            if (mr == MoveResult.Hit)
            {
                hits++;
            }
        }
        Assert.AreEqual(noMines, hits);
    }

    [TestMethod]
    public void GameBoard_MovePlayerUp()
    {
        GameBoard gameBoard = new GameBoard(8, 3);
        MoveResult result = gameBoard.MovePlayerUp();

        Assert.AreNotEqual(MoveResult.Invalid, result);
        Assert.AreEqual(Tuple.Create(1,2), gameBoard.PlayerLocation);
        Assert.AreEqual(1, gameBoard.NumberOfMoves);
    }

    [TestMethod]
    public void GameBoard_MovePlayerUp_Invalid()
    {
        GameBoard gameBoard = new GameBoard(2, 1);
        MoveResult result = gameBoard.MovePlayerUp();

        Assert.AreNotEqual(MoveResult.Invalid, result);
        Assert.AreEqual(Tuple.Create(1, 2), gameBoard.PlayerLocation);
        Assert.AreEqual(1, gameBoard.NumberOfMoves);
    }

    //TODO similar for other directions

    [TestMethod]
    public void GameBoard_MovePlayer_Destination()
    {
        GameBoard gameBoard = new GameBoard(2, 0);
        gameBoard.MovePlayerRight();
        var result = gameBoard.MovePlayerUp();

        Assert.AreEqual(MoveResult.Destination, result);
        Assert.AreEqual(2, gameBoard.NumberOfMoves);
    }
}
