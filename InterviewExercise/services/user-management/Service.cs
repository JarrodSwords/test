using System;
using System.Data.SqlClient;

namespace InterviewExercise
{
    public class Service
    {
        const int NAME_CHARACTER_LIMIT = 20;

        public User Update(UpsertUserArgs args)
        {
            CheckDatabaseAvailability();

            if (args.Name.Length > NAME_CHARACTER_LIMIT)
                throw new ArgumentException(
                    $"Name '{args.Name}' exceeds {NAME_CHARACTER_LIMIT} characters."
                );

            var database = new UserRepository();
            User member;

            try
            {
                member = database.Find(args.Id);
            }
            catch (ArgumentException)
            {
                // log
                throw;
            }

            member.Name = args.Name;
            var updated = database.Update(member);
            return updated;
        }

        public void CheckDatabaseAvailability()
        {
            try
            {
                var connection = new SqlConnection("...");
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
