using MessagingTool.Models;
using System.Collections.Generic;

namespace MessagingTool.Persistance
{
    public interface IRetrieveTweets
    {
        List<TwitterMessages> ProcessTweets(string userFileName);
    }
}
