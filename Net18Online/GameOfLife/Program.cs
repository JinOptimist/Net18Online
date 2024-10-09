using GameOfLife;
using GameOfLife.Builder;

var fieldBuilder = new FieldBuilder();
var fieldDrawer = new FieldDrawer();

var field = fieldBuilder.Build(20, 20);