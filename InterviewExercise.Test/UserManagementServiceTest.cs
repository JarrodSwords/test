using System;
using FluentAssertions;
using Xunit;

namespace InterviewExercise.Test
{
    public class UserManagementServiceTest
    {
        [Theory]
        [InlineData(1)]
        public void WhenUpdatingAUser_ItValidatesTheUserExists(long id)
        {
            var userManagementService = new UserManagementService();
            var updateUserArgs = new UpdateUserArgs(id);

            Action updateUser = () => userManagementService.Update(updateUserArgs);

            updateUser.Should().Throw<ArgumentException>();
        }
    }
}
