using System;
using FluentAssertions;
using Xunit;

namespace InterviewExercise.Test
{
    public class UserManagementServiceTest
    {
        private UserManagementService _userManagementService;

        public UserManagementServiceTest()
        {
            _userManagementService = new UserManagementService();
        }

        [Theory]
        [InlineData(-1)]
        public void WhenUpdatingAUser_ForAnInvalidId_ItThrowsAnArgumentException(long id)
        {
            var updateUserArgs = new UpdateUserArgs(id, "");

            Action updateUser = () => _userManagementService.Update(updateUserArgs);

            updateUser.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(1, "Joel")]
        [InlineData(1, "Mike")]
        public void WhenUpdatingAUser_ItReturnsAnUpdatedUser(long id, string name)
        {
            var updateUserArgs = new UpdateUserArgs(id, name);

            var user = _userManagementService.Update(updateUserArgs);

            user.Name.Should().Be(name);
        }
    }
}
