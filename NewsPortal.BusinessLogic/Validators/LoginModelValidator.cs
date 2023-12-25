namespace NewsPortal.BusinessLogic.Validators
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        private const int UserNameMaxLength = 100;

        public LoginModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.UserName)
                .NotEmpty()
                .MaximumLength(UserNameMaxLength);
        }
    }
}
