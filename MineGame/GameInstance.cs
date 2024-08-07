namespace MineGame;

public class GameInstance
{
    private int numberOfLives;
    private IGameBoard board;
    private IPlatform platform;

    public GameInstance(IPlatform platform, IGameBoard gameBoard)
    {
        this.board = gameBoard;
        this.platform = platform;
        numberOfLives = 3;
    }

    public void Play()
    {
        platform.WriteLine(Resources.INTRO_TEXT);
        platform.WriteLine(Resources.COMMAND_REMINDER_TEXT);

        platform.WriteLine(Resources.LIVES_TEXT + numberOfLives);
        
        while (true)
        {
            MoveResult moveResult = MoveResult.Invalid;

            platform.WriteLine(Resources.LOCATION_TEXT + GetUserSquareString());
            platform.WriteLine(Resources.COMMAND_PROMPT_TEXT);

            UserInput command = InputParser.ParseUserText(platform.ReadLine());

            switch (command)
            {
                case UserInput.Up:
                    moveResult = board.MovePlayerUp();
                    break;
                case UserInput.Down:
                    moveResult = board.MovePlayerDown();
                    break;
                case UserInput.Left:
                    moveResult = board.MovePlayerLeft();
                    break;
                case UserInput.Right:
                    moveResult = board.MovePlayerRight();
                    break;
                case UserInput.Exit:
                    platform.ExitApp();
                    return;
                case UserInput.Unknown:
                    moveResult = MoveResult.Skip;
                    platform.WriteLine(Resources.INVALID_COMMAND_TEXT);
                    platform.WriteLine(Resources.COMMAND_REMINDER_TEXT);
                    break;
            }

            switch (moveResult)
            {
                case MoveResult.Hit:
                    platform.WriteLine(Resources.HIT_TEXT);
                    if (numberOfLives > 1)
                    {
                        platform.WriteLine(Resources.LIVES_TEXT + --numberOfLives);
                    }
                    else
                    {
                        platform.WriteLine(Resources.FAIL_TEXT);
                        platform.ExitApp();
                        return;
                    }
                    break;
                case MoveResult.Miss:
                    break;
                case MoveResult.Destination:
                    platform.WriteLine(Resources.WIN_TEXT);
                    platform.ExitApp();
                    return;
                case MoveResult.Invalid:
                    platform.WriteLine(Resources.INVALID_MOVE_TEXT);
                    break;
            }
        }
    }

    private string GetUserSquareString()
    {
        //Currently only works for 1-9. If expanding to a bigger board this would need to be updated
        return (char)('A' + board.PlayerLocation.Item1 - 1) + board.PlayerLocation.Item2.ToString();
    }
}

public interface IPlatform
{
    public void WriteLine(string msg);
    public string? ReadLine();
    public void ExitApp();
}

public class Platform : IPlatform
{
    public void WriteLine(string msg)
    {
        Console.WriteLine(msg);
    }

    public string? ReadLine()
    {
        return Console.ReadLine();
    }

    public void ExitApp()
    {
        Environment.Exit(0);
    }
}