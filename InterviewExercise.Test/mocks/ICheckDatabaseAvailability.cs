using System.Data.SqlClient;

namespace InterviewExercise.Test
{
    public class MockConnectionAvailableService : ICheckDatabaseAvailability
    {
        public void CheckAvailability() { }
    }

    public class MockConnectionUnavailableService : ICheckDatabaseAvailability
    {
        const string CONNECTION_STRING = "Server=myServer;Data Source=myDataBase;User Id=myUsername;Password=myPassword;Timeout=1;";

        public void CheckAvailability()
        {
            using(var connection = new SqlConnection(CONNECTION_STRING))
            {
                try
                {
                    connection.Open();
                }
                catch (SqlException)
                {
                    // log
                    throw;
                }
            }
        }
    }
}
