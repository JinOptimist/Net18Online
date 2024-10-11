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
        /// <summary>
        ///         Оба приведённых фрагмента кода на C# связаны с вызовом метода TryStep, но они отличаются контекстом вызова и объектами, к которым применяется этот метод.
        ///
        /// 1. var ansactual1 = _magic.TryStep((IBaseCharacter)_characterMock);
        ///    В этом случае вызывается метод TryStep на объекте _magic, а _characterMock приводится к интерфейсу IBaseCharacter.Предположительно, _magic представляет собой некий объект или систему, которая управляет логикой, связанной с персонажами, возможно, магические взаимодействия или проверки действий персонажа.
        ///        Что происходит:
        ///             Метод TryStep вызывается для объекта _magic с передачей персонажа в качестве аргумента.
        ///             _characterMock — это объект типа Mock, вероятно, созданный для тестирования, и приводится к интерфейсу IBaseCharacter, что указывает на необходимость передать метод именно этот интерфейс.
        ///  2. var ansactual2 = _mazeMock.Object.Cells.FirstOrDefault(c => c.X == 3 && c.Y == 3).TryStep(_characterMock.Object);
        ///       Здесь метод TryStep вызывается на конкретной клетке лабиринта, которую находят с помощью метода FirstOrDefault, проверяя координаты по X == 3 и Y == 3. После того как клетка найдена, вызывается её метод TryStep, передавая объект персонажа.
        ///         Что происходит:
        ///   Ищется первая клетка в коллекции Cells с координатами (3, 3).
        ///   Метод TryStep вызывается непосредственно у этой клетки с передачей персонажа(также тестовый мок-объект).
        ///   </summary>    
        public void TryStep_CheckThatWeCanStepToMagic()
        {
            var actual1 = _magic.TryStep((IBaseCharacter)_characterMock);
            var actual2 = _mazeMock.Object.Cells.FirstOrDefault(c => c.X == 3 && c.Y == 3).TryStep(_characterMock.Object);

            Assert.That(actual1, Is.True);
            Assert.That(actual2, Is.True);
        }

    }
}