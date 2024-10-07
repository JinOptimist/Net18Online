using MazeCore.Models;
using MazeCore.Models.Cells.Character;
using Moq;
using NUnit.Framework;

namespace MazeCoreTest.Models.Cells.Character
{
    public class KingTest
    {
        private Mock<Maze> _mazeMock;
        private Mock<IBaseCharacter> _characterMock;

        private King _king;

        [SetUp]
        public void Setup()
        {
            _mazeMock = new Mock<Maze>();
            _characterMock = new Mock<IBaseCharacter>();

            _king = new King(5, 2, _mazeMock.Object);
        }

        [Test]
        public void TryStep_CheckThatWeCanStepToKing()
        {
            //Prepare

            //Act
            var answer = _king.TryStep(_characterMock.Object);

            //Assert
            Assert.That(answer, Is.False);
        }

        [Test]
        [TestCase(1,1001)]
        [TestCase(654,1654)]
        [TestCase(1025,2025)]
        public void TryStep_CheckThatCharacterGetAHealth(
            int countOfHealthBefore,
            int countOfHealthAfter)
        {
            //Prepare
            _characterMock.SetupProperty(x => x.Health);
            _characterMock.Object.Health = countOfHealthBefore;

            //Act
            var answer = _king.TryStep(_characterMock.Object);

            //Assert
            Assert.That(
                _characterMock.Object.Health, 
                Is.EqualTo(countOfHealthAfter), 
                message: "Hero has to get a Health");
        }
    }
}
