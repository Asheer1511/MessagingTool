using MessagingTool.DataAccessLayer;
using MessagingTool.Models;
using System;
using System.Collections.Generic;

namespace MessagingTool.Persistance
{
    public class RetrieveTweets : IRetrieveTweets 
    {
        private IDataAccess _dataAccess;

        public RetrieveTweets(IDataAccess userDataAccess)
        {
            _dataAccess = userDataAccess;
        }

        public List<TwitterMessages> ProcessTweets(string userFileName)
        {
            //create new instance
            List<TwitterMessages> twitterMessages = new List<TwitterMessages>();

            //Read data from data access
            var tweetMessages = _dataAccess.ReadData(userFileName);

            try
            {
                foreach (var item in tweetMessages)
                {
                    //create instances
                    TwitterMessages newTwitterMessage = new TwitterMessages();
                    TweetDetails newTwitterDetails = new TweetDetails();

                    //split tweets and usernames
                    string[] userTweets = item.ToString().Split('>');
                    string userName = userTweets[0].Trim();
                    string tweetMessage = userTweets[1].Trim();
                    
                    //Assign the splits to twitter details
                    newTwitterDetails.TweetUser = userName;
                    newTwitterDetails.TweetMessage = tweetMessage;

                    //Check if message is not greater than 140 else throw exception
                    if (tweetMessage.Length <= 140)
                    {
                        newTwitterMessage.Tweets = newTwitterDetails;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException($"Tweet character length: {tweetMessage.Length} has exceeded the maximum limit.");
                    }
                    twitterMessages.Add(newTwitterMessage);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return twitterMessages;
        }
    }
}
