using System;
using System.Configuration;
using System.IO;

namespace MessagingTool.DataAccessLayer
{
    public class DataAccess : IDataAccess
    {
        private string[] userData;
        public string GetDataPath(string fileName)
        {
            var dataPath = ConfigurationManager.AppSettings[fileName];

            if (!string.IsNullOrEmpty(dataPath))
            {
                return dataPath;
            }
            else
            {
                throw new ArgumentNullException(dataPath);
            }
        }

        public string[] ReadData(string userFileName)
        {
            //Validate input string
            if (string.IsNullOrEmpty(userFileName))
            {
                throw new ArgumentNullException(userFileName);
            }

            //Get data path from UserDataAccess
            string dataPath = GetDataPath(userFileName);

            //Validate data path is not null or empty
            if (dataPath != null && dataPath.Length != 0)
            {
                //Read all user data
                userData = File.ReadAllLines(dataPath);

                //Return user data or throw exception
                if (!userData.Length.Equals(0))
                {
                    return userData;
                }
                else
                {
                    throw new Exception($"Failed to read data file: { dataPath }. ");
                }
            }

            else
            {
                throw new Exception($"Failed to retreive data path: { userFileName }. ");
            }
        }
    }
}
