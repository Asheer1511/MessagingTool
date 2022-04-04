using MessagingTool.Models;

namespace MessagingTool.Persistance
{
    public interface IRetrieveUsers
    {
        TwitterUsers ProcessUsersAndFollowers(string userFileName);
        TwitterUsers GetFollowersAsUsers(TwitterUsers twitterUsers, string[] users);
        TwitterUsers GetUsersAndFollowers(TwitterUsers twitterUsers, string[] users);
    }
}
