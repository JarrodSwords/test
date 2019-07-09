using System.Linq;

namespace InterviewExercise
{
    public interface IUserRepository
    {
        bool Exists(long id);
        User Find(long id);
        User Update(User user);
    }
}
