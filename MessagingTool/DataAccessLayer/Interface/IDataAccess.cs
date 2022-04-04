namespace MessagingTool.DataAccessLayer
{
    public interface IDataAccess
    {
        string GetDataPath(string fileName);
        string[] ReadData(string userFileName);
    }
}
