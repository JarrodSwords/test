using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace InterviewExercise
{
    public interface IUserRepository
    {
        bool Exists(long id);
        User Find(long id);
        User Update(User user);
    }

    public class MockUserRepository : IUserRepository
    {
        private readonly List<User> users;

        public MockUserRepository()
        {
            var jane = new User { Id = 1, Name = "Jane" };
            var john = new User { Id = 2, Name = "John" };

            this.users = new List<User>() { jane, john };
        }

        public bool Exists(long id) => this.users.Any(x => x.Id == id);

        public User Find(long id) => this.users.Single(x => x.Id == id);

        public User Update(User user)
        {
            using(var updateUserTransaction = new TransactionScope())
            {
                this.users.RemoveAll(x => x.Id == user.Id);
                this.users.Add(user);
                updateUserTransaction.Complete();
            }

            return user;
        }
    }
}
