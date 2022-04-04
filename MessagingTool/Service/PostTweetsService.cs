using MessagingTool.Persistance;
using System;
using System.Linq;
using System.Text;

namespace MessagingTool.Service
{
    public class PostTweetsService
    {
        private IRetrieveUsers _retrieveUsers;
        private IRetrieveTweets _retrieveTweets;
        private const string userData = "UserDataFile";
        private const string tweetData = "TweetDataFile";
        public PostTweetsService(IRetrieveUsers retrieveUsers, IRetrieveTweets retrieveTweets)
        {
            _retrieveUsers = retrieveUsers;
            _retrieveTweets = retrieveTweets;
        }

        public void PostTweets()
        {
            //call users and twitter messages
            var twitterUsers = _retrieveUsers.ProcessUsersAndFollowers(userData);
            var twitterMessages = _retrieveTweets.ProcessTweets(tweetData);

            StringBuilder buildTweet = new StringBuilder();

            try
            {
                foreach(var user in twitterUsers.UsersAndFollowers)
                {
                    //write username from users
                    buildTweet.AppendLine(user.Key);

                    //write each user name where appears in both twitterusers and twitter messages or where twittermessage user appears in twitter user
                    foreach (var tweet in twitterMessages.Where(x => x.Tweets.TweetUser == user.Key || user.Value.Contains(x.Tweets.TweetUser)))
                    {
                        buildTweet.AppendLine(String.Format($"\t@{tweet.Tweets.TweetUser}: {tweet.Tweets.TweetMessage}"));
                    }
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

            Console.WriteLine(buildTweet);
        }
    }
}
