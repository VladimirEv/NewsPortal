namespace NewsPortal.BusinessLogic.Validators
{
    public class RoleModelValidator : AbstractValidator<RoleModel>
    {
        private const int NameMaxLength = 100;

        public RoleModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(NameMaxLength);
        }
    }
}
