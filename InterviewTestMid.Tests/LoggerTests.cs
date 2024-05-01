using InterviewTestMid.Interface;

namespace InterviewTestMid.Tests
{
    [TestFixture]
    public class LoggerTests
    {
        private Mock<IDebugWrapper> _debugWrapperMock;
        private ILogger _logger;
        private Mock<IFileSystem> _fileSystemMock;

        [OneTimeSetUp]
        public void OnetimeSetup()
        {
            _debugWrapperMock = new Mock<IDebugWrapper>();
            _fileSystemMock = new Mock<IFileSystem>();
            _logger = new Logger(_debugWrapperMock.Object, _fileSystemMock.Object);
        }

        //[SetUp]
        //public void Setup()
        //{
            
        //}

        [Test]
        public void WriteLogMessage_ValidMessage_Sucess_Test()
        {
            string logMessage = "Test log message";

            _logger.WriteLogMessage(logMessage);

            _debugWrapperMock.Verify(d => d.WriteLine(logMessage), Times.Once);
        }

        [Test]
        public void WriteLogMessage_ThrowsArgumentEmptyException_Test()
        {
            string emptyMessage = "";

            Assert.Throws<ArgumentException>(() => _logger.WriteLogMessage(emptyMessage));
        }


        [Test]
        public void WriteErrorMessage_LogsErrorMessageAndStackTrace_Test()
        {
            var exception = new Exception("Test exception");

            _logger.WriteErrorMessage(exception);

            _debugWrapperMock.Verify(d => d.WriteLine($"Error recieved: {exception.Message}"), Times.Once);
            _debugWrapperMock.Verify(d => d.WriteLine($"{exception.StackTrace}"), Times.Once);
        }

        [Test]
        public void WriteErrorMessage_ThrowsArgumentNullException_Test()
        {
            Exception nullException = null;

            Assert.Throws<ArgumentException>(() => _logger.WriteErrorMessage(nullException));
        }

        [Test]
        public void WriteCSV_CallsFileSystem_Sucess_Test()
        {
            
            var messages = new List<string> { "Message1", "Message2" };

            _logger.WriteCSV(messages);

            _fileSystemMock.Verify(fs => fs.AppendAllLines(It.IsAny<string>(), messages), Times.Once);
        }

        [Test]
        public void WriteCSV_ThrowsNullArgumentException_Test()
        {

            Assert.Throws<ArgumentException>(() => _logger.WriteCSV(null));
        }

        [Test]
        public void WriteCSV_ThrowsArgumentEmptyException_Test()
        {

            Assert.Throws<ArgumentException>(() => _logger.WriteCSV(new List<string>()));
        }

    }
}