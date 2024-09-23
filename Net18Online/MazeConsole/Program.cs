using MazeConsole;
using MazeConsole.Builders;

var mazeBuilder = new MazeBuilder();
var mazeDrawer = new MazeDrawer();

var maze = mazeBuilder.Build(6, 8);
mazeDrawer.Draw(maze);

