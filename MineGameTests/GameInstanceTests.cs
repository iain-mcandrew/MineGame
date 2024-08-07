using Moq;
using MineGame;
using Resources = MineGame.Resources;

namespace MineGameTests;

[TestClass]
public class GameInstanceTests
{
    private Mock<IPlatform> mockPlatform;
    private Mock<IGameBoard> mockGameBoard;

    [TestInitialize]
    public void SetUp()
    {
        mockPlatform = new Mock<IPlatform>();
        mockGameBoard = new Mock<IGameBoard>();
        mockGameBoard.Setup(x => x.PlayerLocation)
            .Returns(Tuple.Create(1, 1));
    }

    [TestMethod]
    public void GameInstance_HitMine_DecreaseLives()
    {
        mockGameBoard.Setup(x => x.MovePlayerUp()).Returns(MoveResult.Hit);

        GameInstance gameInstance = new GameInstance(mockPlatform.Object, mockGameBoard.Object);
        mockPlatform.SetupSequence(x => x.ReadLine())
            .Returns("up")
            .Returns("up")
            .Returns("up");

        gameInstance.Play();

        mockPlatform.Verify(x => x.WriteLine(Resources.HIT_TEXT), Times.Exactly(3));
        mockPlatform.Verify(x => x.WriteLine(Resources.LIVES_TEXT + "2"), Times.Once);
        mockPlatform.Verify(x => x.WriteLine(Resources.LIVES_TEXT + "1"), Times.Once);
        mockPlatform.Verify(x => x.WriteLine(Resources.FAIL_TEXT), Times.Once);
        mockPlatform.Verify(x => x.ExitApp(), Times.Once);
    }

    [TestMethod]
    public void GameInstance_MovePlayer_Destination()
    {
        mockGameBoard.Setup(x => x.MovePlayerUp()).Returns(MoveResult.Destination);

        GameInstance gameInstance = new GameInstance(mockPlatform.Object, mockGameBoard.Object);
        mockPlatform.Setup(x => x.ReadLine())
            .Returns("up");

        gameInstance.Play();

        mockPlatform.Verify(x => x.WriteLine(Resources.WIN_TEXT), Times.Once);
        mockPlatform.Verify(x => x.ExitApp(), Times.Once);
    }
}

