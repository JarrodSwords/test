using System.Linq;

namespace InterviewExercise
{
    public interface IUserRepository
    {
        bool Exists(long id);
        User Find(long id);
        User Update(User user);
    }

    public class UserRepository : IUserRepository
    {
        public bool Exists(long id)
        {
            throw new System.NotImplementedException();
        }

        public User Find(long id)
        {
            throw new System.NotImplementedException();
        }

        public User Update(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}
