using MessagingTool.DataAccessLayer;
using MessagingTool.Models;
using MessagingTool.Persistance;
using NSubstitute;
using NUnit.Framework;
using FluentAssertions;
using System.Collections.Generic;

namespace MessagingToolTests
{
    [TestFixture]
    public class RetrieveUsersTests
    {
        [Test]
        public void GetUsersAndFollowers_GivenEmptyData_ShouldReturnTwitterUsersModel()
        {
            //Arrange
            string[] data = { };
            const string userDataPath = "UserDataFile";

            var usersAndFollowers = new SortedDictionary<string, List<string>>();
            var twitterUsers = new TwitterUsers() { UsersAndFollowers = usersAndFollowers };

            var expected = new TwitterUsers() { UsersAndFollowers = usersAndFollowers };

            var retrieveUserData = Substitute.For<IDataAccess>();
            retrieveUserData.ReadData(userDataPath).Returns(data);

            var retrieveUsers = new RetrieveUsers(retrieveUserData);

            //Act
            var results = retrieveUsers.GetUsersAndFollowers(twitterUsers, data);

            //Assert
            results.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetUsersAndFollowers_GivenValidData_ShouldReturnTwitterUsersModel()
        {
            //Arrange
            string[] data = { "Ward follows Alan", "Alan follows Martin", "Ward follows Martin, Alan" };
            const string userDataPath = "UserDataFile";

            var usersAndFollowers = new SortedDictionary<string, List<string>>();
            var twitterUsers = new TwitterUsers() { UsersAndFollowers = usersAndFollowers };

            var expectedUsersAndFollowers = new SortedDictionary<string, List<string>>();
            expectedUsersAndFollowers.Add("Alan", new List<string> { "Martin" });
            expectedUsersAndFollowers.Add("Ward", new List<string> { "Alan", "Martin" });
            var expected = new TwitterUsers() { UsersAndFollowers = expectedUsersAndFollowers };

            var retrieveUserData = Substitute.For<IDataAccess>();
            retrieveUserData.ReadData(userDataPath).Returns(data);

            var retrieveUsers = new RetrieveUsers(retrieveUserData);

            //Act
            var results = retrieveUsers.GetUsersAndFollowers(twitterUsers, data);

            //Assert
            results.UsersAndFollowers.Should().BeEquivalentTo(expectedUsersAndFollowers);
            results.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetFollowersAsUsers_GivenValidData_ShouldReturnTwitterUsersModel()
        {
            //Arrange
            string[] data = { "Ward follows Alan", "Alan follows Martin", "Ward follows Martin, Alan" };
            const string userDataPath = "UserDataFile";

            var usersAndFollowers = new SortedDictionary<string, List<string>>();
            usersAndFollowers.Add("Alan", new List<string> { "Martin" });
            usersAndFollowers.Add("Ward", new List<string> { "Alan", "Martin" });
            var twitterUsers = new TwitterUsers() { UsersAndFollowers = usersAndFollowers };

            var expectedUsersAndFollowers = new SortedDictionary<string, List<string>>();
            expectedUsersAndFollowers.Add("Alan", new List<string> { "Martin" });
            expectedUsersAndFollowers.Add("Martin", new List<string> {  });
            expectedUsersAndFollowers.Add("Ward", new List<string> { "Alan", "Martin" });
            var expected = new TwitterUsers() { UsersAndFollowers = expectedUsersAndFollowers };

            var retrieveUserData = Substitute.For<IDataAccess>();
            retrieveUserData.ReadData(userDataPath).Returns(data);

            var retrieveUsers = new RetrieveUsers(retrieveUserData);

            //Act
            var results = retrieveUsers.GetFollowersAsUsers(twitterUsers, data);

            //Assert
            results.UsersAndFollowers.Should().BeEquivalentTo(expectedUsersAndFollowers);
            results.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void ProcessUsersAndFollowers_GivenEmptyData_ShouldReturnTwitterUsersModel()
        {
            //Arrange
            string[] userData = { };
            const string userDataPath = "UserDataFile";

            var usersAndFollowers = new SortedDictionary<string, List<string>>();
            var expected = new TwitterUsers() { UsersAndFollowers = usersAndFollowers };

            var retrieveUserData = Substitute.For<IDataAccess>();
            retrieveUserData.ReadData(userDataPath).Returns(userData);

            var retrieveUsers = new RetrieveUsers(retrieveUserData);

            //Act
            var results = retrieveUsers.ProcessUsersAndFollowers(userDataPath);

            //Assert
            results.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void ProcessUsersAndFollowers_GivenValidData_ShouldReturnSortedData()
        {
            //Arrange
            const string userDataPath = "UserDataFile";
            string[] userData = { "Ward follows Alan", "Alan follows Martin", "Ward follows Martin, Alan" };

            var usersAndFollowers = new SortedDictionary<string, List<string>>();
            usersAndFollowers.Add("Alan", new List<string> { "Martin" });
            usersAndFollowers.Add("Martin", new List<string> { });
            usersAndFollowers.Add("Ward", new List<string> { "Martin", "Alan" });

            //var expected = new TwitterUsers() { UsersAndFollowers = usersAndFollowers };
            var retrieveUserData = Substitute.For<IDataAccess>();
            retrieveUserData.ReadData(userDataPath).Returns(userData);

            var retrieveUsers = new RetrieveUsers(retrieveUserData);

            //Act
            var results = retrieveUsers.ProcessUsersAndFollowers(userDataPath);

            //Assert
            //results.Should().BeEquivalentTo(expected);
            results.UsersAndFollowers.Should().BeEquivalentTo(usersAndFollowers);
        }
    }
}
