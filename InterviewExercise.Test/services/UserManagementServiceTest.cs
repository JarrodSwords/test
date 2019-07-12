using System;
using System.Data.SqlClient;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace InterviewExercise.Test
{
    public class UserManagementServiceTest
    {
        private UserManagementService userManagementService;

        public UserManagementServiceTest()
        {
            var userRepository = new MockUserRepository();
            userManagementService = new UserManagementService(
                new MockConnectionAvailableService(),
                new UpdateUserArgsValidator(userRepository),
                userRepository
            );
        }

        [Fact]
        public void WhenUpdatingAUser_AndTheDatabaseCannotBeReached_ItThrowsASqlException()
        {
            // arrange
            var updateUserArgs = new UpsertUserArgs(1, "John");
            var userRepository = new MockUserRepository();
            userManagementService = new UserManagementService(
                new MockConnectionUnavailableService(),
                new UpdateUserArgsValidator(userRepository),
                userRepository
            );            

            // act
            Action updateUser = () => userManagementService.Update(updateUserArgs);

            // assert
            updateUser.Should().Throw<SqlException>();
        }

        [Theory]
        [InlineData(-1)]
        public void WhenUpdatingAUser_ForAnInvalidId_ItThrowsAValidationException(long id)
        {
            var updateUserArgs = new UpsertUserArgs(id, "");
            Action updateUser = () => userManagementService.Update(updateUserArgs);
            updateUser.Should().Throw<ValidationException>();
        }

        [Theory]
        [InlineData(2, "Israel Kaʻanoʻi Kamakawiwoʻole")]
        public void WhenUpdatingAUser_WithAnInvalidName_ItThrowsAValidationException(long id, string name)
        {
            var updateUserArgs = new UpsertUserArgs(id, name);
            Action updateUser = () => userManagementService.Update(updateUserArgs);
            updateUser.Should().Throw<ValidationException>();
        }

        [Theory]
        [InlineData(1, "Scarlett")]
        [InlineData(2, "Joel")]
        public void WhenUpdatingAUser_ItReturnsAnUpdatedUser(long id, string name)
        {
            var updateUserArgs = new UpsertUserArgs(id, name);
            var user = userManagementService.Update(updateUserArgs);
            user.Name.Should().Be(name);
        }
    }
}
