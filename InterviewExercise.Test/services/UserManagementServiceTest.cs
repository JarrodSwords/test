using System;
using System.Data.SqlClient;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace InterviewExercise.Test
{
    public class UserManagementServiceTest
    {
        private UserManagementService _userManagementService;

        public UserManagementServiceTest()
        {
            var userRepository = new MockUserRepository();
            var updateUserArgsValidator = new UpdateUserArgsValidator(userRepository);
            _userManagementService = new UserManagementService(updateUserArgsValidator, userRepository);
        }

        // [Theory]
        // [InlineData("")]
        // public void WhenUpdatingAUser_AndTheDatabaseCannotBeReached_ItThrowsASqlException()
        // {
        //     var updateUserArgs = new UpsertUserArgs(1, "John");

        //     Action updateUser = () => _userManagementService.Update(updateUserArgs);

        //     updateUser.Should().Throw<SqlException>();
        // }

        [Theory]
        [InlineData(-1)]
        public void WhenUpdatingAUser_ForAnInvalidId_ItThrowsAnArgumentException(long id)
        {
            var updateUserArgs = new UpsertUserArgs(id, "");

            Action updateUser = () => _userManagementService.Update(updateUserArgs);

            updateUser.Should().Throw<ValidationException>();
        }

        [Theory]
        [InlineData(1, "Scarlett")]
        [InlineData(2, "Joel")]
        public void WhenUpdatingAUser_ItReturnsAnUpdatedUser(long id, string name)
        {
            var updateUserArgs = new UpsertUserArgs(id, name);

            var user = _userManagementService.Update(updateUserArgs);

            user.Name.Should().Be(name);
        }
    }
}
