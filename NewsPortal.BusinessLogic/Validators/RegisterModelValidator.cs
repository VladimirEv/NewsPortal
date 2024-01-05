namespace NewsPortal.BusinessLogic.Validators
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        private const int UserNameMaxLength = 100;

        public RegisterModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.PasswordConfirmation)
                .NotEmpty()
                .Equal(x => x.Password);

            RuleFor(x => x.UserName)
                .NotEmpty()
                .MaximumLength(UserNameMaxLength);
        }
    }
}
