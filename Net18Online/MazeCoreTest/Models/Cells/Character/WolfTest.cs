using MazeCore.Models;
using MazeCore.Models.Cells;
using MazeCore.Models.Cells.Character;
using Moq;
using NUnit.Framework;
using System;

namespace MazeCoreTest.Models.Cells
{
    public class WolfTest
    {
        private Mock<IMaze> _mazeMock;
        private Mock<IBaseCharacter> _characterMock;
        private Maze _maze; // Add real Maze object, otherwise it won't working
        private Wolf _wolf;

        [SetUp]
        public void Setup()
        {
            _mazeMock = new Mock<IMaze>(); // Fake Maze
            _characterMock = new Mock<IBaseCharacter>(); // Fake Hero
            _maze = new Maze();

            _wolf = new Wolf(1, 2, _maze); // Real Wolf otherwise it won't work
        }

        [Test]
        public void TryStep_CheckThatWolfCanStepBaseCell()
        {
            // Prepare
            var groundCellMock = new Mock<IBaseCell>();

            // Act
            var result = groundCellMock.Object.TryStep(_wolf);

            // Assert
            Assert.That(result, Is.False, "Wolf should be able to step on the ground cell.");
        }

        [Test]
        public void TryStep_CheckThatWolfCanStepGround()
        {
            // Prepare
            var groundCell = new Ground(1, 1, _maze); // Real Ground, not Mock

            // Act
            var result = groundCell.TryStep(_wolf);

            // Assert
            Assert.That(result, Is.True, "Wolf should be able to step on the ground cell.");
        }


        [Test]
        public void InteractWithCell_CheckWolfAttackNonCritical()
        {
            // Prepare
            _characterMock.SetupProperty(c => c.Health, 10); // Character has 10 health before

            var randomMock = new Mock<Random>();
            randomMock.Setup(r => r.Next(1, 6)).Returns(1); // Common attack

            // Act
            _wolf.InteractWithCell(_characterMock.Object);

            // Assert
            Assert.That(_characterMock.Object.Health, Is.EqualTo(9), "Non-critical attack should reduce health by 1.");
        }

        [Test]
        public void InteractWithCell_CheckWolfAttackCritical()
        {
            // Prepare
            _characterMock.SetupProperty(c => c.Health, 10); // Character has 10 health before

            var randomMock = new Mock<Random>();
            randomMock.Setup(r => r.Next(1, 6)).Returns(3); // Attack now is critical

            // Act
            _wolf.InteractWithCell(_characterMock.Object);

            // Assert
            Assert.That(_characterMock.Object.Health, Is.EqualTo(8), "Critical attack should reduce health by 2.");
        }

        [Test]
        public void Move_CheckWolfMovesToValidCell()
        {
            // Prepare
            var destinationCellMock = new Mock<BaseCell>(_wolf.X + 1, _wolf.Y, _mazeMock.Object);
            destinationCellMock.Setup(cell => cell.TryStep(_wolf)).Returns(true);

            // Act
            _wolf.Move();

            // Assert
            Assert.That(_wolf.X, Is.EqualTo(destinationCellMock.Object.X));
            Assert.That(_wolf.Y, Is.EqualTo(destinationCellMock.Object.Y));
        }
    }
}
