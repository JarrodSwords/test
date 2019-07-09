using System;
using FluentValidation;

namespace InterviewExercise
{

    public class UserManagementService
    {
        private readonly UpdateUserArgsValidator updateUserArgsValidator;
        private readonly IUserRepository userRepository;

        public UserManagementService(
            UpdateUserArgsValidator updateUserArgsValidator,
            IUserRepository userRepository)
        {
            this.updateUserArgsValidator = updateUserArgsValidator;
            this.userRepository = userRepository;
        }

        public User Update(UpsertUserArgs args)
        {
            updateUserArgsValidator.ValidateAndThrow(args);

            var user = userRepository.Find(args.Id);
            user.Name = args.Name;

            return userRepository.Update(user);
        }
    }
}
