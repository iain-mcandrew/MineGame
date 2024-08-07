using MineGame;

//these could be set by the player, or could all go into a difficulty config
//For this version, only use chessboard size
int boardSize = 8;
int noMines = 20;

IPlatform platform = new Platform();
IGameBoard gb = new GameBoard(boardSize, noMines);

platform.WriteLine("MINE GAME - Iain McAndrew");

GameInstance gameInstance = new GameInstance(platform, gb);
gameInstance.Play();

