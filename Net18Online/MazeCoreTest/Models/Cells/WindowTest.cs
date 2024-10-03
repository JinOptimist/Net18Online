using MazeCore.Models;
using MazeCore.Models.Cells;
using MazeCore.Models.Cells.Character;
using Moq;
using NUnit.Framework;

namespace MazeCoreTest.Models.Cells
{
    public class WindowTest
    {
        private Mock<Maze> _mazeMock;
        private Mock<IBaseCharacter> _characterMock;

        private Window _window;

        [SetUp]
        public void SetUp()
        {
            _mazeMock = new Mock<Maze>();
            _characterMock = new Mock<IBaseCharacter>();

            _window = new Window(1, 1, _mazeMock.Object);
        }

        [Test]
        public void TryStep_CheckThatCharacterCanStepToWindow()
        {
            // Prepare

            // Act
            var answer = _window.TryStep(_characterMock.Object);

            // Assert
            Assert.That(
                answer,
                Is.True,
                message: "Hero has to have possible to step Window");
        }

        [Test]
        public void TryStep_CheckThatCharacterLoseCoins()
        {
            // Prepare
            _characterMock.SetupProperty(x => x.Coins);
            // Act
            var answer = _window.TryStep(_characterMock.Object);
            // Assert

            Assert.That(
                _characterMock.Object.Coins,
                Is.EqualTo(0),
                message: "Hero should lost all his coins");
        }

        [Test]
        [TestCase(23, 22)]
        [TestCase(55, 54)]
        [TestCase(5, 4)]
        public void TryStep_CheckThatCharacterLoseHealth(
            int previosHealth,
            int currentHealth)
        {
            // Prepare
            _characterMock.SetupProperty(x => x.Health);
            _characterMock.Object.Health = previosHealth;

            // Act
            var answer = _window.TryStep(_characterMock.Object);

            // Assert
            Assert.That(
                _characterMock.Object.Health,
                Is.EqualTo(currentHealth),
                message: "Hero has to lose 1 point of his health");
        }

    }
}