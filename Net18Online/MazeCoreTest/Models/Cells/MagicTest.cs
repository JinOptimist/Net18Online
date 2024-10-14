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

namespace MazeCoreTest.Models.Cells
{
    public class MagicTest
    {
        private Mock<IMaze> _mazeMock;
        private Mock<IBaseCharacter> _characterMock;

        private Magic _magic;


        // @BeforEach
        [SetUp]
        public void Setup()
        {
            // Fake
            _mazeMock = new Mock<IMaze>();
            _characterMock = new Mock<IBaseCharacter>();
            //Real
            _magic = new Magic(3, 3, _mazeMock.Object);
        }
        //@Test
        [Test]
        public void TryStep_CheckThatHaracterCanStepToMagic()
        {
            /// <summary>
            /// В тестах с использованием библиотеки Moq, возникает ошибка,
            /// когда вместо передачи самого объекта мока
            /// передается объект самого мока (то есть Mock<T>, а не Mock<T>.Object).
            ///        var actual1 = _magic.TryStep((IBaseCharacter)_characterMock);
            ///        </summary>
            var actual1 = _magic.TryStep((_characterMock.Object));

            Assert.That(actual1, Is.True, "You can`t step this"); // Message up , if test fails
        }
        [Test]
        [TestCase(0, 1)]
        [TestCase(12, 13)]
        [TestCase(26, 27)]
        [TestCase(111, 112)]
        public void TryStep_CheckThatWhereHaracterStepThanHeGetAMagic
            (int magicOnStart, int magicOnFinish)
        {
            _characterMock.SetupProperty(x => x.Magic);
            _characterMock.Object.Magic = magicOnStart;

            _magic.TryStep(_characterMock.Object);
            Assert.That(_characterMock.Object.Magic,
                Is.EqualTo(magicOnFinish),
                "Where our _many_ magic ? Billy ?"); // Message up , if test fails
        }
    }
}