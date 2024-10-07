using MazeCore.Models.Cells.Character;
using MazeCore.Models.Cells;
using MazeCore.Models;
using Moq;
using NUnit.Framework;

namespace MazeCoreTest.Models.Cells
{
    [TestFixture]
    public class TreasuryTest
    {
        private Mock<IMaze> _mazeMock;
        private Mock<IBaseCharacter> _characterMock;
        private Treasury _treasury;
        private Maze _maze; 

        [SetUp]
        public void Setup()
        {
            _maze = new Maze(); 

            _characterMock = new Mock<IBaseCharacter>();

            _treasury = new Treasury(1, 2, _maze);
        }

        [Test]
        public void TryStep_CheckThatWeCanStepIntoTreasury()
        {
            // Act
            var result = _treasury.TryStep(_characterMock.Object);

            // Assert
            Assert.That(result, Is.True, "Character should be able to step into the Treasury");
        }

        [Test]
        public void InteractWithCell_CheckTreasuryIncreasesCoins()
        {
            // Prepare
            _characterMock.SetupProperty(c => c.Coins, 10); // Make character have 10 coins at start

            // Act
            _treasury.InteractWithCell(_characterMock.Object);

            // Assert
            Assert.That(_characterMock.Object.Coins, Is.EqualTo(15), "Treasury should give the character 5 coins.");
        }

        [Test]
        public void InteractWithCell_LogsEventInMazeHistory()
        {
            // Act
            _treasury.InteractWithCell(_characterMock.Object);

            // Verify check event log
            _maze.HistoryOfEvents.Add("You found a Treasury");
            Assert.That(_maze.HistoryOfEvents, Contains.Item("You found a Treasury"));
        }
    }
}
