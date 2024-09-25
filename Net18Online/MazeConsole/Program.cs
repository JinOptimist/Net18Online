using MazeConsole;
using MazeConsole.Builders;

var mazeBuilder = new MazeBuilder();
var mazeDrawer = new MazeDrawer();

var maze = mazeBuilder.Build(8, 12);
mazeDrawer.Draw(maze);

