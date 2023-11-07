namespace NewsPortal.BusinessLogic.Models
{
    public sealed class UserModel
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
