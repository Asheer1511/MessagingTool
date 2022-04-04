using MessagingTool.DataAccessLayer;
using NUnit.Framework;
using System;

namespace MessagingToolTests.DataAccessLayer
{
    [TestFixture]
    public class DataAccessTests
    {
        [Test]
        public void GetDataPath_GivenIncorrectDataPath_ShouldThrowError()
        {
            //Arrange
            const string fileName = "UserData";

            var dataAccess = new DataAccess();

            //Act
            //var results = dataAccess.GetDataPath(fileName);
            var results = Assert.Throws<ArgumentNullException>(() => dataAccess.GetDataPath(fileName));

            //Assert
            Assert.That(results.Message, Is.EqualTo("Value cannot be null."));
        }

        [Test]
        public void GetDataPath_GivenFileName_ShouldReturnDataPath()
        {
            //Arrange
            const string fileName = "UserDataFile";

            var dataAccess = new DataAccess();

            //Act
            var results = dataAccess.GetDataPath(fileName);

            //Assert
            Assert.That(results, Is.EqualTo("user.txt"));
        }

        [Test]
        public void ReadData_GivenDataPath_ShouldReturnDataPath()
        {
            //Arrange
            const string fileName = "";

            
            var dataAccess = new DataAccess();

            //Act
            var results = Assert.Throws<ArgumentNullException>(() => dataAccess.ReadData(fileName));

            //Assert
            Assert.That(results.Message, Is.EqualTo("Value cannot be null."));
        }
    }
}
