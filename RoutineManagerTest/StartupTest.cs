using RoutineManager.MVVM.Service;

namespace RoutineManagerTest
{
    public class StartupTest
    {
        private readonly StartupService _startupService = new StartupService();

        [Fact]
        public void isValidURL_WithValidHTTP_ShouldReturnTrue()
        {
            //Arrange
            String url = "http://www.testingmcafeesites.com/";

            //Act
            bool result = _startupService.isValidURL(url);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void isValidURL_WithValidHTTPS_ShouldReturnTrue()
        {
            //Arrange
            String url = "https://www.google.com/";

            //Act
            bool result = _startupService.isValidURL(url);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void isValidURL_WithInvalidURL_ShouldReturnFalse()
        {
            //Arrange
            String url = "htts://www.gogle.cm/";

            //Act
            bool result = _startupService.isValidURL(url);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void isValidURL_WithFilePath_ShouldReturnFalse()
        {
            //Arrange
            String url = "C:\\Users";

            //Act
            bool result = _startupService.isValidURL(url);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void isValidFilePath_WithValidFilePath_ShouldReturnTrue()
        {
            //Arrange
            String filePath = "C:\\Users";

            //Act
            bool result = _startupService.isValidFilePath(filePath);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void isValidFilePath_WithInvalidFilePath_ShouldReturnFalse()
        {
            //Arrange
            String filePath = "C:/HelloWorld";

            //Act
            bool result = _startupService.isValidFilePath(filePath);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void isValidFilePath_WithUrlLink_ShouldReturnFalse()
        {
            //Arrange
            String filePath = "https://www.google.com/";

            //Act
            bool result = _startupService.isValidFilePath(filePath);

            //Assert
            Assert.False(result);
        }

    }
}