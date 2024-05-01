using InterviewTestMid.Interface;
using InterviewTestMid.Model;
using InterviewTestMid.Service;

namespace InterviewTestMid.Tests
{
    [TestFixture]
    public class PartManipulationServiceTests
    {
        private Mock<ILogger> _loggerMock;
        private IPartManipulationService _partManipulationService;
        private Mock<IPartManipulationService> _partManipulationServiceMock;
        private Mock<IFileSystem> _fileSystemMock;
        private List<Part> _partsList;

        [OneTimeSetUp]
        public void OnetimeSetup()
        {
            _loggerMock = new Mock<ILogger>();
            _fileSystemMock = new Mock<IFileSystem>();
            _partManipulationServiceMock = new Mock<IPartManipulationService>();
            _partManipulationService = new PartManipulationService(_loggerMock.Object, _fileSystemMock.Object);
            _partsList = new List<Part>
            {
                new Part { PartId = 1,
                    PartDesc = "Part 1",
                    Meta = new Meta(),
                    PartWeight = new PartWeight(),
                    Materials = new List<MaterialEntry>() },
                new Part { PartId = 2,
                    PartDesc = "Part 2",
                    Meta = new Meta(),
                    PartWeight = new PartWeight(),
                    Materials = new List<MaterialEntry>() },
                new Part { PartId = 3,
                    PartDesc = "Part 3",
                    PartWeight = new PartWeight(),
                    Materials = new List<MaterialEntry>() }
            };
        }

        [Test]
        public void CountMetaObjects_NonMocked_Success_Test()
        {

            var metaObjectsCounts = _partsList.FindAll(p => p.Meta != null).Count;

            Assert.That(metaObjectsCounts, Is.EqualTo(2));
        }

        [Test]
        public void CountMetaObjects_Mocked_Sucess_Test()
        {
            
            _partManipulationServiceMock.Setup(x => x.ListAndLogPartsMaterialDesc(It.IsAny<string>())).Returns(_partsList);

            var resultParts = _partManipulationServiceMock.Object.ListAndLogPartsMaterialDesc("FOIL");

            Assert.IsNotNull(resultParts);
            Assert.That(resultParts.Count, Is.EqualTo(3));
        }

        [Test]
        public void LoadBaseJsonPartsData_ReturnsListOfParts_Test()
        {

            var jsonParts = "[{\"PartId\":11170,\"PartNbr\":\"101687\",\"PartDesc\":\"Packaging\",\"Meta\":{\"PartClassification\":{\"LookId\":1,\"LookNbr\":\"Cl\",\"LookDesc\":\"No colour\",\"LookExtra\":null},\"PartMasterType\":null,\"PartColour\":null,\"PartOpacity\":null},\"PartWeight\":{\"UoM\":1,\"Value\":10},\"ConversionsApplied\":false,\"Materials\":[{\"Material\":{\"LookId\":1,\"LookNbr\":\"1233\",\"LookDesc\":\"descrption\"},\"Percentage\":50,\"MatrIsBarrier\":null,\"MatrIsDensifier\":null,\"MatrIsOpacifier\":null}]}]";

            _fileSystemMock.Setup(fs => fs.ReadAllText(It.IsAny<string>())).Returns(jsonParts);

            // Act
            var result = _partManipulationService.LoadBaseJsonPartsData();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void LoadBaseJsonPartsEmptyJson_ReturnsNull_Test()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();
            var fileSystemMock = new Mock<IFileSystem>();
            var service = new PartManipulationService(loggerMock.Object, fileSystemMock.Object);

            fileSystemMock.Setup(fs => fs.ReadAllText(It.IsAny<string>())).Returns(string.Empty);

            // Act
            var result = service.LoadBaseJsonPartsData();

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void ModifyPartWeight_Success_Test()
        {
            var part = new Part
            {
                PartId = 1,
                PartWeight = new PartWeight { UoM = 1, Value = 10 }
            };

            var newWeight = 15.0;

            // Act
            var modifiedPart = _partManipulationService.ModifyPartWeight(part, newWeight);

            // Assert
            _loggerMock.Verify(l => l.WriteLogMessage($"PartWeight value of part with PartId {part.PartId} has been changed to {newWeight}"), Times.Once);
            Assert.AreEqual(newWeight, modifiedPart.PartWeight.Value);
        }
    }
}
