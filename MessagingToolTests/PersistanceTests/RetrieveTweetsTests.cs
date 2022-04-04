using FluentAssertions;
using MessagingTool.DataAccessLayer;
using MessagingTool.Models;
using MessagingTool.Persistance;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MessagingToolTests.PersistanceTests
{
    [TestFixture]
    public class RetrieveTweetsTests
    {
        [Test]
        public void ProcessTweets_GivenTweetGreaterThan140_ShouldThrowException()
        {
            //Arrange
            string[] userData =
            {
                "Alan> If you have a procedure with 10 parameters, you probably missed some.There are only two hard things in Computer Science: cache invalidation, naming things and off-by-1 errors."
            };
            const string userDataPath = "TweetDataFile";

            var retrieveUserData = Substitute.For<IDataAccess>();
            retrieveUserData.ReadData(userDataPath).Returns(userData);

            var retrieveUsers = new RetrieveTweets(retrieveUserData);

            //Act
            var results = Assert.Throws<Exception>(() => retrieveUsers.ProcessTweets(userDataPath));

            //Assert
            Assert.That(results.Message, Is.EqualTo("Specified argument was out of the range of valid values.\r\nParameter name: Tweet character length: 175 has exceeded the maximum limit."));
        }

        [Test]
        public void ProcessTweets_GivenEmptyData_ShouldReturnTwitterMessagesModel()
        {
            //Arrange
            string[] userData = { };
            const string userDataPath = "TweetDataFile";

            var expectedList = new List<TwitterMessages>();

            var retrieveUserData = Substitute.For<IDataAccess>();
            retrieveUserData.ReadData(userDataPath).Returns(userData);

            var retrieveUsers = new RetrieveTweets(retrieveUserData);

            //Act
            var results = retrieveUsers.ProcessTweets(userDataPath);

            //Assert
            results.Should().BeEquivalentTo(expectedList);
        }

        [Test]
        public void ProcessTweets_GivenValidData_ShouldReturnTwitterMessagesModel()
        {
            //Arrange
            string[] userData = 
            {
                "Alan> If you have a procedure with 10 parameters, you probably missed some.",
                "Ward> There are only two hard things in Computer Science: cache invalidation, naming things and off-by-1 errors.",
                "Alan> Random numbers should not be generated with a method chosen at random."
            };
            const string userDataPath = "TweetDataFile";

            var tweetDetails1 = new TweetDetails() { TweetUser = "Alan", TweetMessage = "If you have a procedure with 10 parameters, you probably missed some." };
            var tweetDetails2 = new TweetDetails() { TweetUser = "Ward", TweetMessage = "There are only two hard things in Computer Science: cache invalidation, naming things and off-by-1 errors." };
            var tweetDetails3 = new TweetDetails() { TweetUser = "Alan", TweetMessage = "Random numbers should not be generated with a method chosen at random." };

            var expectedList = new List<TwitterMessages>();
            expectedList.Add(new TwitterMessages() { Tweets = tweetDetails1 });
            expectedList.Add(new TwitterMessages() { Tweets = tweetDetails2 });
            expectedList.Add(new TwitterMessages() { Tweets = tweetDetails3 });

            var retrieveUserData = Substitute.For<IDataAccess>();
            retrieveUserData.ReadData(userDataPath).Returns(userData);

            var retrieveUsers = new RetrieveTweets(retrieveUserData);

            //Act
            var results = retrieveUsers.ProcessTweets(userDataPath);

            //Assert
            results.Should().BeEquivalentTo(expectedList);
        }
    }
}
