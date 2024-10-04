using MazeCore.Builders;
using MazeCore.Helpers;
using MazeCore.Models;
using MazeCore.Models.Cells;
using MazeCore.Models.Cells.Character;
using Moq;
using NUnit.Framework;

namespace MazeCoreTest.Models.Cells
{
    public class CatTest
    {
        private Mock<Maze> _mazeMock;
        private Mock<IBaseCharacter> _characterMock;

        private Cat _cat;

        [SetUp]
        public void SetUp()
        {
            _mazeMock = new Mock<Maze>();
            _characterMock = new Mock<IBaseCharacter>();

            _cat = new Cat(1, 1, _mazeMock.Object);
        }

        [Test]
        [TestCase(1, 3)]
        [TestCase(15, 17)]
        [TestCase(33, 35)]
        public void InteractWithCell_CheckThatCharacterGetCoin(
            int countOfCoinsBefore,
            int countOfCoinsAfter)
        {
            // Prepare
            _characterMock.SetupProperty(x => x.Coins);
            _characterMock.Object.Coins = countOfCoinsBefore;

            // Act
            _cat.InteractWithCell(_characterMock.Object);

            // Assert
            Assert.That(
                _characterMock.Object.Coins,
                Is.EqualTo(countOfCoinsAfter),
                message: "Hero has to get 2 coins");
        }

        [Test]
        [TestCase(4, 5)]
        [TestCase(15, 16)]
        [TestCase(57, 58)]
        public void InteractWithCell_CheckThatCharacterGetHealthPoint(
            int countOfHealthBefore,
            int countOfHealthAfter
            )
        {
            // Prepare
            _characterMock.SetupProperty(x => x.Health);
            _characterMock.Object.Health = countOfHealthBefore;

            // Act
            _cat.InteractWithCell(_characterMock.Object);

            // Assert
            Assert.That(
                _characterMock.Object.Health,
                Is.EqualTo(countOfHealthAfter),
                message: "Hero has to get Health point");
        }

        [Test]
        public void Move_ShouldNotChangePosition_WhenNoNearCells()
        {
            // Prepare            
            _mazeMock.Object.Cells.Add(new Wall(_cat.X, _cat.Y - 1, _mazeMock.Object)); 
            _mazeMock.Object.Cells.Add(new Wall(_cat.X, _cat.Y + 1, _mazeMock.Object)); 
            _mazeMock.Object.Cells.Add(new Wall(_cat.X - 1, _cat.Y, _mazeMock.Object)); 
            _mazeMock.Object.Cells.Add(new Wall(_cat.X + 1, _cat.Y, _mazeMock.Object)); 

            Console.WriteLine($"Cat starts at X: {_cat.X}, Y: {_cat.Y}");

            // Act
            _cat.Move();

            Console.WriteLine($"Cat ends at X: {_cat.X}, Y: {_cat.Y}");

            // Assert
            Assert.That(
                _cat.X,
                Is.EqualTo(_cat.X),
                message: "Cat's X position should not change.");
            Assert.That(
                _cat.Y,
                Is.EqualTo(_cat.Y),
                message: "Cat's Y position should not change.");
        }

        [Test]
        public void Move_ShouldChangePosition_WhenStepIsSuccessful()
        {
            // Prepare
            var groundCell = new Ground(_cat.X + 1, _cat.Y, _mazeMock.Object);
            _mazeMock.Object.Cells.Add(groundCell);

            Console.WriteLine($"Cat starts at X: {_cat.X}, Y: {_cat.Y}");

            // Act
            _cat.Move();

            Console.WriteLine($"Cat ends at X: {_cat.X}, Y: {_cat.Y}");


            // Assert
            Assert.That(
                _cat.X,
                Is.EqualTo(groundCell.X),
                message: "Cat's X position should change.");
            Assert.That(
                _cat.Y,
                Is.EqualTo(groundCell.Y),
                message: "Cat's Y position should change.");
        }
    }
}