using System;

namespace InterviewExercise
{
    public class UpdateUserArgs
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public UpdateUserArgs(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class UpdateUserArgsValidator
    {
        public bool Validate(UpdateUserArgs args)
        {
            if (args.Id == -1)
                throw new ArgumentException("Id not found");

            return true;
        }
    }

    public interface IUserRepository
    {
        User Find(long id);
        User Update(User user);
    }

    public class UserRepository : IUserRepository
    {
        public User Find(long id)
        {
            return new User()
            {
                Id = id,
                    Name = "John"
            };
        }

        public User Update(User user)
        {
            return user;
        }
    }

    public class UserManagementService
    {
        public User Update(UpdateUserArgs args)
        {
            var updateArgsValidator = new UpdateUserArgsValidator();

            if (!updateArgsValidator.Validate(args))
                return null;

            var userRepository = new UserRepository();
            var user = userRepository.Find(args.Id);

            user.Name = args.Name;

            return userRepository.Update(user);
        }
    }
}
