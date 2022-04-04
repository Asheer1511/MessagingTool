using MessagingTool.DataAccessLayer;
using MessagingTool.Persistance;
using MessagingTool.Service;
using System;

namespace MessagingTool
{
    class Program
    {
        static void Main(string[] args)
        {
            DataAccess dataAccess = new DataAccess();
            RetrieveUsers retrieveUsers = new RetrieveUsers(dataAccess);
            RetrieveTweets retrieveTweets = new RetrieveTweets(dataAccess);
            var service = new PostTweetsService(retrieveUsers, retrieveTweets);
            Console.SetWindowSize(130, 30);
            service.PostTweets();
            Console.ReadLine();
        }
    }
}
