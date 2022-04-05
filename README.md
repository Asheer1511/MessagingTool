# MessagingTool
This is a demo messaging application to simulate posting a message per user and follower.

# Requirements (Problem statement)
We will recieve 2 text files, 1 with user data and 1 with message data, combine the 2 to create a console that will display the users name 
together with their messages and messages of who they follow.

Given the above, we can extract 3 main components to the system.
1. Retrieve users and followers.
2. Retrieve messages linked to users.
3. Post messages linked to users and followers.

# Steps to Run/Test
1. To ensure minimum requirements met, Navigate to: \MessagingTool\MessagingTool\bin\Debug\MessagingTool.exe
2. Open solution in visual studio to inspect source code.
3. Open visual studio test explorer to run unit tests.

# Solution
Given the above requirements I have chosen to use a modified repository design pattern to implement the solution to meet the needs of the system.
I have broken down the solution into 4 layers viz. DataAccessLayer, Models, Persistance, Service.
I have further broken down each layer into an Agent and interface layer, for better readability.

1. DataAccessLayer:
This layer is responsible for retrieving data from a data source.
I have opted to store the data path which can be easily retrieved by the configuration manager and read using System.IO

2. Models: 
3 Models have been created to ensure the input and output of each method that uses them.
-TwitterUsers model: Opted to use sorted dictionary to store key vlue pair of Key: users and Value: List of followers
-TweetDetails: 2 strings, 1 for user and 1 for message 
-TweetMessage: contains TweetDetails, and can be used as a list later on to contain a list of messages.

3. Persistance:
The responibility of this layer is to handle the business logic of the system. 
By having the DataAccess extracted, we can call the DataAccess inteface to use its methods to retrieve data then manipulate it.

RetrieveUsers Class: Broken down into 3 methods 
-GetUsersAndFollowers(): Gets the users and associated followers and stores them in the TwitterUsers model.
-GetFollowersAsUsers(): Where the followers exist but not as users, they need to be added aas users. 
I have created these 2 seperate methods to avoid a heavy handed nested foreach statement to combine all this data.

-ProcessUsersAndFollowers(): used to combine the outpt from the above 2 methods into a consolidated TwitterUsers model.

RetrieveTweets Class: Contains just 1 method 
-ProcessTweets(): Process data in a single foreach and returns a list of TwitterMessages

4. Service: 
This layer is responsible for Posting all the Tweet data which it retrieves from the Persistance layer and collates all data and builds the final string to be printed.

PostTweetsService Class:
Just 1 void method to handle the printing of the final string, retieves data via calling persistance interfaces.

ProgramClass: all classes instanciated in the program class which is built and ran on runtime to produce desired outcome.

Lastly I have created unit tests to cover functionality.
The structure and design I have opted use, allowded me to be able to mock data using NUnit substitute.

