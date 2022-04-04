using System.Collections.Generic;
using MessagingTool.Models;
using MessagingTool.Persistance;
using MessagingTool.Service;
using NSubstitute;
using NUnit.Framework;

namespace MessagingToolTests
{
    [TestFixture]
    public class PostTweetsServiceTests
    {
        [Test]
        public void PostTweet_GivenAllData_ShouldRecieve2DataCalls()
        {
            //Arange
            const string userDataPath = "UserDataFile";
            const string tweetData = "TweetDataFile";
            var retrieveUsers = Substitute.For<IRetrieveUsers>();
            var retrieveTweets = Substitute.For<IRetrieveTweets>();

            var UsersAndFollowers = new SortedDictionary<string, List<string>>();

            retrieveUsers.ProcessUsersAndFollowers(userDataPath).Returns(new TwitterUsers()
            {
                UsersAndFollowers = UsersAndFollowers
            });

            retrieveTweets.ProcessTweets(tweetData).Returns(new List<TwitterMessages>());

            var service = new PostTweetsService(retrieveUsers, retrieveTweets);

            //Act

            service.PostTweets();

            //Assert
            retrieveUsers.Received(1).ProcessUsersAndFollowers(userDataPath);
            retrieveTweets.Received(1).ProcessTweets(tweetData);

        }
    }
}
