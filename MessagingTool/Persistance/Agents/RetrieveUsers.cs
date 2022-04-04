using MessagingTool.DataAccessLayer;
using MessagingTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MessagingTool.Persistance
{
    public class RetrieveUsers : IRetrieveUsers
    {
        private IDataAccess _dataAccess;
        private string[] separatingUser = new string[] { "follows" };
        private string[] separatingFollowers = new string[] { ", " };

        public RetrieveUsers(IDataAccess userDataAccess)
        {
            _dataAccess = userDataAccess;
        }

        public TwitterUsers ProcessUsersAndFollowers(string userFileName)
        {
            //Create instances
            TwitterUsers twitterUsers = new TwitterUsers();
            twitterUsers.UsersAndFollowers = new SortedDictionary<string, List<string>>();

            var users = _dataAccess.ReadData(userFileName);

            try
            {
                //Seperated getting users and followers and followers as users to avoid multi nested foreach loop
                var usersAndFollowers = GetUsersAndFollowers(twitterUsers, users);
                var allUsersAndFolloers = GetFollowersAsUsers(usersAndFollowers, users);
                return allUsersAndFolloers;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public TwitterUsers GetUsersAndFollowers(TwitterUsers twitterUsers, string[] users)
        {
            try
            {
                foreach (var itemX in users)
                {
                    //Split users and followers
                    string[] userFollows = itemX.ToString().Split(separatingUser, StringSplitOptions.None);
                    string userName = userFollows[0].Trim();
                    List<string> followers = userFollows[1].Split(separatingFollowers, StringSplitOptions.None).ToList().Select(x => x.Trim()).ToList();

                    //First add all users to stored dictionary key if they dont exist 
                    if (!twitterUsers.UsersAndFollowers.ContainsKey(userName))
                    {
                        twitterUsers.UsersAndFollowers.Add(userName, followers);
                    }

                    //Else if they do exis go get followers
                    else if (twitterUsers.UsersAndFollowers.ContainsKey(userName))
                    {
                        foreach (var itemY in followers)
                        {
                            //for each follower for each user where doesn't exist, add follower  
                            if (!twitterUsers.UsersAndFollowers[userName].Contains(itemY))
                            {
                                twitterUsers.UsersAndFollowers[userName].Add(itemY);
                            }

                        }
                    }
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return twitterUsers;
        }

        public TwitterUsers GetFollowersAsUsers(TwitterUsers twitterUsers, string[] users)
        {
            try
            {
                foreach (var item in users)
                {
                    //Split users and followers
                    string[] userFollows = item.ToString().Split(separatingUser, StringSplitOptions.None);
                    string userName = userFollows[0].Trim();
                    List<string> followers = userFollows[1].Split(separatingFollowers, StringSplitOptions.None).ToList().Select(x => x.Trim()).ToList();

                    foreach (var follower in followers)
                    {
                        //fo each follower who doesn't exist as a user, add as user with empty follower list
                        if (!twitterUsers.UsersAndFollowers.ContainsKey(follower))
                        {
                            twitterUsers.UsersAndFollowers.Add(follower, new List<string>());
                        }
                    }
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return twitterUsers;
        }
    }
}
