namespace MineGame;

public static class InputParser
{
    static string[] UP_COMMANDS = { "up", "u" };
    static string[] DOWN_COMMANDS = { "down", "d" };
    static string[] RIGHT_COMMANDS = { "right", "r" };
    static string[] LEFT_COMMANDS = { "left", "l" };
    static string[] EXIT_COMMANDS = { "exit", "quit" };

    public static UserInput ParseUserText(string text)
    {
        if (UP_COMMANDS.Contains(text, StringComparer.OrdinalIgnoreCase))
        {
            return UserInput.Up;
        }
        if (DOWN_COMMANDS.Contains(text, StringComparer.OrdinalIgnoreCase))
        {
            return UserInput.Down;
        }
        if (RIGHT_COMMANDS.Contains(text, StringComparer.OrdinalIgnoreCase))
        {
            return UserInput.Right;
        }
        if (LEFT_COMMANDS.Contains(text, StringComparer.OrdinalIgnoreCase))
        {
            return UserInput.Left;
        }
        if (EXIT_COMMANDS.Contains(text, StringComparer.OrdinalIgnoreCase))
        {
            return UserInput.Exit;
        }
        return UserInput.Unknown;
    }
}

public enum UserInput
{
    Unknown,
    Up,
    Down, 
    Left, 
    Right,
    Exit
}
