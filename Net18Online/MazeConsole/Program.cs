using MazeConsole;
using MazeConsole.Builders;

var mazeBuilder = new MazeBuilder();
var mazeDrawer = new MazeDrawer();

var maze = mazeBuilder.Build(10, 20);
mazeDrawer.Draw(maze);

