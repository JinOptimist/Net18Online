using Castle.DynamicProxy;
using MazeCore.Models;
using MazeCore.Models.Cells;
using MazeCore.Models.Cells.Character;
using Moq;
using NUnit.Framework;

namespace MazeCoreTest.Models.Cells
{
    public class SnakeTest
    {
        private Mock<IMaze> _mazeMock;
        private Mock<IBaseCharacter> _characterMock;

        private Snake _snake;

        [SetUp]
        public void Setup()
        {
            _mazeMock = new Mock<IMaze>(); //Fake
            _characterMock = new Mock<IBaseCharacter>(); // Fake
            
            _snake = new Snake(5, 6, _mazeMock.Object); // Real
        }

        [Test]
        [TestCase(2, 3)]
        [TestCase(13, 14)]
        [TestCase(24, 25)]
        public void InteractWithCell_CharacterGetACoin(
            int countOfCoinsBefore,
            int countOfCoinsAfter)
        {
            // Prepare
            _characterMock.SetupProperty(x => x.Coins);
            _characterMock.Object.Coins = countOfCoinsBefore;

            // Act
            _snake.InteractWithCell(_characterMock.Object);

            // Assert
            Assert.That(
                _characterMock.Object.Coins,
                Is.EqualTo(countOfCoinsAfter),
                message: "Hero has to get a coins");
        }

        [Test]
        public void Move_ShouldnotChange_NoNearCells()
        {
            // Prepare            
            _mazeMock.Object.Cells.Add(new Wall(_snake.X, _snake.Y - 1, _mazeMock.Object)); 
            _mazeMock.Object.Cells.Add(new Wall(_snake.X, _snake.Y + 1, _mazeMock.Object)); 
            _mazeMock.Object.Cells.Add(new Wall(_snake.X - 1, _snake.Y, _mazeMock.Object)); 
            _mazeMock.Object.Cells.Add(new Wall(_snake.X + 1, _snake.Y, _mazeMock.Object)); 
            Console.WriteLine($"Snake move start at X: {_snake.X}, Y: {_snake.Y}");

            // Act
            _snake.Move();
            Console.WriteLine($"Snake stop move at X: {_snake.X}, Y: {_snake.Y}");

            // Assert
            Assert.That(
                _snake.X,
                Is.EqualTo(_snake.X),
                message: "Snake's X coordinate should not change.");
            Assert.That(
                _snake.Y,
                Is.EqualTo(_snake.Y),
                message: "Snake's Y coordinate should not change.");
        }
        

        [Test]
        public void Move_ShouldChangeCoordinate()
        {
            // Prepare
            var cellToStep = new Ground(_snake.X + 1, _snake.Y, _mazeMock.Object);
            _mazeMock.Object.Cells.Add(cellToStep);
            Console.WriteLine($"Snake move start at X: {_snake.X}, Y: {_snake.Y}");

            // Act
            _snake.Move();
            Console.WriteLine($"Snake stop move at X: {_snake.X}, Y: {_snake.Y}");

            // Assert
            Assert.That(
                _snake.X,
                Is.EqualTo(cellToStep.X),
                message: "Snake's X coodinate should change.");
            Assert.That(
                _snake.Y,
                Is.EqualTo(cellToStep.Y),
                message: "Snake's Y coordinate should change.");
         }

    }
}    
