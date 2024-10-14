using MazeCore.Models;
using MazeCore.Models.Cells;
using MazeCore.Models.Cells.Character;
using Microsoft.VisualBasic;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeCoreTest.Models.Cells
{
    public class TeleportTest
    {
        private Mock<IMaze> _mazeMock;
        private Mock<IBaseCharacter> _characterMock;       
        private Teleport _teleport;

        

        [SetUp]
        public void Setup()
        {
            _mazeMock = new Mock<IMaze>();
            _characterMock = new Mock<IBaseCharacter>();
            _teleport = new Teleport(1, 2, _mazeMock.Object);
         
        }

        [Test]
        public void TryStep_CheckThatWeCanStepToTeleport()
        {
            // Act
            var answer = _teleport.TryStep(_characterMock.Object);
            // Assert
            Assert.That(
                answer,
                Is.True,
                message: "Hero has to have possibility to step to Teleport");
        }
        [Test]
        
        public void InteractWithCell_TeleportMovement()
        {

            {
                // Prepare
                var cellsList = new List<IBaseCell>();
                cellsList.Add(_teleport);
                
                _characterMock.SetupProperty(x => x.X, 0);
                _characterMock.SetupProperty(x => x.Y, 0);
                _mazeMock.Setup(m => m.Cells).Returns(cellsList);
                // Act
                _teleport.InteractWithCell(_characterMock.Object);

                // Assert
                Assert.That(
                    _characterMock.Object.X,
                    Is.EqualTo(_teleport.X),
                    message: "Hero has moved");
                Assert.That(
                    _characterMock.Object.Y,
                    Is.EqualTo(_teleport.Y),
                    message: "Hero has moved");
            }
        }
    

    }
}
