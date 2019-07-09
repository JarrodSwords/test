using System.Data.SqlClient;

namespace InterviewExercise
{
    public interface ICheckDatabaseAvailability
    {
        void CheckAvailability(string connectionString);
    }

    public class DatabaseConnectionService : ICheckDatabaseAvailability
    {
        public void CheckAvailability(string connectionString)
        {
            try
            {
                var connection = new SqlConnection(connectionString);
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
