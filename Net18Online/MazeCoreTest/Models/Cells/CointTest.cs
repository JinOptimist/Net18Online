using Castle.DynamicProxy;
using MazeCore.Models;
using MazeCore.Models.Cells;
using MazeCore.Models.Cells.Character;
using Moq;
using NUnit.Framework;

namespace MazeCoreTest.Models.Cells
{
    public class CointTest
    {
        private Mock<IMaze> _mazeMock;
        private Mock<IBaseCharacter> _characterMock;

        private Coin _coin;

        [SetUp]
        public void Setup()
        {
            _mazeMock = new Mock<IMaze>(); // Fake
            _characterMock = new Mock<IBaseCharacter>(); // Fake
            
            _coin = new Coin(1, 2, _mazeMock.Object); // Real
        }

        [Test]
        public void TryStep_CheckThatWeCanStepToCoin()
        {
            // Prepare

            // Act
            var answer = _coin.TryStep(_characterMock.Object);

            // Assert
            Assert.That(
                answer,
                Is.True,
                message: "Hero has to have possibility to step to Coin");
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(5, 6)]
        [TestCase(42, 43)]
        public void TryStep_CheckThatCharacterGetACoin(
            int countOfCoinsBefore,
            int countOfCoinsAfter)
        {
            // Prepare
            _characterMock.SetupProperty(x => x.Coins);
            _characterMock.Object.Coins = countOfCoinsBefore;

            // Act
            var answer = _coin.TryStep(_characterMock.Object);

            // Assert
            Assert.That(
                _characterMock.Object.Coins,
                Is.EqualTo(countOfCoinsAfter),
                message: "Hero has to get a Coin");
        }

        [Test]
        public void TryStep_CheckThatCointWasReplacedToGround()
        {
            // Prepare

            // Act
            var answer = _coin.TryStep(_characterMock.Object);

            // Assert
            //mazeMock.Verify(x => x.ReplaceCellToGround(It.IsAny<IBaseCell>()), Times.Once());
            _mazeMock.Verify(x => x.ReplaceCellToGround(_coin), Times.Once());
        }
    }
}
