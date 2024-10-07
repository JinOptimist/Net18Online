using Castle.DynamicProxy;
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

        [SetUp]
        public void Setup()
        {
            _mazeMock = new Mock<IMaze>(); // Fake

            _characterMock = new Mock<IBaseCharacter>();

            _treasury = new Treasury(1, 2, _mazeMock.Object);
        }

        [Test]
        public void TryStep_CheckThatWeCanStepIntoTreasury()
        {
            // Act
            var result = _treasury.TryStep(_characterMock.Object);

            // Assert
            Assert.That(
                result, 
                Is.True, 
                "Character should be able to step into the Treasury");
        }

        [Test]
        [TestCase(10,15)]
        [TestCase(0, 5)]
        [TestCase(26, 31)]  
        public void InteractWithCell_CheckTreasuryIncreasesCoins(
            int initialCoinsCount,
            int resultCoinsCount
            )
        {
            // Prepare
            _mazeMock.Setup(m => m.HistoryOfEvents).Returns(new List<string>());
            _characterMock.SetupProperty(c => c.Coins, initialCoinsCount); // Make character have 10 coins at start

            // Act
            _treasury.InteractWithCell(_characterMock.Object);

            // Assert
            Assert.That(
                _characterMock.Object.Coins, 
                Is.EqualTo(resultCoinsCount), 
                "Treasury should give the character 5 coins.");
        }

        [Test]
        public void InteractWithCell_LogsEventInMazeHistory()
        {
            //Prepare
            _mazeMock.Setup(m => m.HistoryOfEvents).Returns(new List<string>());

            // Act
            _treasury.InteractWithCell(_characterMock.Object);

            // Verify check event log
            Assert.That(_mazeMock.Object.HistoryOfEvents, Contains.Item("You found a Treasury"));
        }
    }
}
