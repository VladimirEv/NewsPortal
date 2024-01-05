namespace NewsPortal.BusinessLogic.Validators
{
    public class ChangePasswordModelValidator : AbstractValidator<ChangePasswordModel>
    {
        public ChangePasswordModelValidator()
        {
            RuleFor(x => x.NewPassword)
                .NotEqual(x => x.CurrentPassword)
                .Equal(x => x.NewPasswordConfirmation);
        }
    }
}
