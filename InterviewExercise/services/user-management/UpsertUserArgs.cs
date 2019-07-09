using FluentValidation;

namespace InterviewExercise
{
    public class UpsertUserArgs
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public UpsertUserArgs(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    
    public class UpdateUserArgsValidator : AbstractValidator<UpsertUserArgs>
    {
        public UpdateUserArgsValidator(IUserRepository userRepository)
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(x => userRepository.Exists(x))
                .WithMessage("User with {PropertyName} '{PropertyValue}' does not exist.");

            RuleFor(x => x.Name)
                .MaximumLength(20)
                .WithMessage("User {PropertyName} '{PropertyValue}' exceeds the 20 character limit.");
        }
    }
}
