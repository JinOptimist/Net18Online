using Castle.DynamicProxy;
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
        private Mock<Maze> _mazeMock;
        private Mock<IMaze> _imazeMock;
        private Mock<IBaseCharacter> _characterMock;
        private Mock<IBaseCell> _cellMock;
        private Mock<Random> _randomMock;
        private Wolf _wolf;

        [SetUp]
        public void Setup()
        {
            _mazeMock = new Mock<Maze>(); // Fake Maze
            _characterMock = new Mock<IBaseCharacter>(); // Fake Hero
            _cellMock = new Mock<IBaseCell>();
            _randomMock = new Mock<Random>();

            _wolf = new Wolf(1, 2, _mazeMock.Object, _randomMock.Object);
        }


        [Test]
        public void TryStep_CheckThatWolfCanStepBaseCell()
        {
            // Prepare
            _cellMock.Setup(cell => cell.TryStep(_wolf)).Returns(false);

            // Act
            var result = _cellMock.Object.TryStep(_wolf);

            // Assert
            Assert.That(
                result, 
                Is.False, 
                "Wolf should be able to step on the basecell.");
        }

        [Test]
        [TestCase(10, 9)]
        [TestCase(4, 3)]
        [TestCase(33, 32)]
        public void InteractWithCell_CheckWolfAttackNonCritical(
            int initialCharacterHealth,
            int resultCharacterHealth
            )
        {
            // Prepare
            _characterMock.SetupProperty(c => c.Health, initialCharacterHealth); // Character has 10 health before
            _characterMock.Object.Health = initialCharacterHealth;
            _randomMock.Setup(r => r.Next(1, 6)).Returns(1); // Common attack

            // Act
            _wolf.InteractWithCell(_characterMock.Object);

            // Assert
            Assert.That(
                _characterMock.Object.Health, 
                Is.EqualTo(resultCharacterHealth), 
                "Non-critical attack should reduce health by 1.");
        }

        [Test]
        [TestCase(10, 8)]
        [TestCase(4, 2)]
        [TestCase(33, 31)]
        public void InteractWithCell_CheckWolfAttackCritical(
            int initialCharacterHealth,
            int resultCharacterHealth
            )
        {
            // Prepare
            _characterMock.SetupProperty(c => c.Health, initialCharacterHealth); // Character has 10 health before
            _characterMock.Object.Health = initialCharacterHealth;
            _randomMock.Setup(r => r.Next(1, 6)).Returns(3); // Attack now is critical

            // Act
            _wolf.InteractWithCell(_characterMock.Object);

            // Assert
            Assert.That(
                _characterMock.Object.Health, 
                Is.EqualTo(resultCharacterHealth), 
                "Critical attack should reduce health by 2.");
        }
    }
}
