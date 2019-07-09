using System;

namespace InterviewExercise
{
    public class UpdateUserArgs
    {
        public long Id { get; set; }

        public UpdateUserArgs(long id)
        {
            Id = id;
        }
    }

    public class UpdateUserArgsValidator
    {
        public bool Validate(UpdateUserArgs args)
        {
            if (args.Id == 1)
                throw new ArgumentException("Id not found");
            
            return true;
        }
    }

    public class UserManagementService
    {
        public void Update(UpdateUserArgs args)
        {
            var updateArgsValidator = new UpdateUserArgsValidator();

            if (!updateArgsValidator.Validate(args))
                return;
        }
    }
}
