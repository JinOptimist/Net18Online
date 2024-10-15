using MazeCore.Models;
using MazeCore.Models.Cells;
using MazeCore.Models.Cells.Character;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;


namespace MazeCoreTest.Models.Cells.Character
{
    public class GoblinTest
    {
        private Mock<Maze> _mazeMock;
        private Mock<IBaseCharacter> _characterMock;
        private Mock<IBaseCell> _ground;

        private Goblin _goblin;



        // @BeforEach
        [SetUp]
        public void Setup()
        {
            // Fake
            _mazeMock = new Mock<Maze>();
            _characterMock = new Mock<IBaseCharacter>();
            _ground = new Mock<IBaseCell>();

            //Real
            _goblin = new Goblin(1, 2, _mazeMock.Object);
        }

        [Test]
        public void Goblin_Symbol_ShouldBeG()
        {
            Assert.That(_goblin.Symbol, Is.EqualTo('g'));
        }

        [Test]
        [TestCase(100, 99)]
        [TestCase(1, 0)]
        [TestCase(-10, -11)]
        public void Goblin_InteractWithCell_ShouldDecreaseCharacterHealth(int initialHealth, int expectedHealth)
        {
            // Arrange
            var character = new Lamer(2, 2, _mazeMock.Object) { Health = initialHealth };

            // Act
            _goblin.InteractWithCell(character);

            // Assert
            Assert.That(() => expectedHealth == character.Health, Is.True, "Character's health should decrease after interacting with a goblin.");
        }

        [Test]
        public void Goblin_Move_ShouldChangeCoordinates()
        {
            var groundCell = new Ground(_goblin.X + 1, _goblin.Y, _mazeMock.Object);
            _mazeMock.Object.Cells.Add(groundCell);

            _goblin.Move();

            Assert.That(
                _goblin.X,
                Is.EqualTo(groundCell.X),
                message: "Position should change.");
            Assert.That(
                _goblin.Y,
                Is.EqualTo(groundCell.Y),
                message: "Position should change.");
        }

    }
}
