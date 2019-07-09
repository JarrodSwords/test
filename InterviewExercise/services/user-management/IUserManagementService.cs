using System;
using FluentValidation;

namespace InterviewExercise
{
    public interface IUserManagementService
    {
        User Update(UpsertUserArgs args);
    }

    public class UserManagementService : IUserManagementService
    {
        private readonly ICheckDatabaseAvailability databaseConnectionService;
        private readonly UpdateUserArgsValidator updateUserArgsValidator;
        private readonly IUserRepository userRepository;

        public UserManagementService(
            ICheckDatabaseAvailability databaseConnectionService,
            UpdateUserArgsValidator updateUserArgsValidator,
            IUserRepository userRepository)
        {
            this.databaseConnectionService = databaseConnectionService;
            this.updateUserArgsValidator = updateUserArgsValidator;
            this.userRepository = userRepository;
        }

        public User Update(UpsertUserArgs args)
        {
            databaseConnectionService.CheckAvailability();
            updateUserArgsValidator.ValidateAndThrow(args);

            var user = userRepository.Find(args.Id);
            user.Name = args.Name;

            return userRepository.Update(user);
        }
    }
}
