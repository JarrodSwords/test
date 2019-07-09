using System.Data.SqlClient;

namespace InterviewExercise.Test
{
    public class MockConnectionAvailableService : ICheckDatabaseAvailability
    {
        public void CheckAvailability() { }
    }

    public class MockConnectionUnavailableService : ICheckDatabaseAvailability
    {
        const string CONNECTION_STRING = "Server=myServer;Database=myDataBase;User Id=myUsername;Password=myPassword;";

        public void CheckAvailability()
        {
            try
            {
                var connection = new SqlConnection(CONNECTION_STRING);
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
