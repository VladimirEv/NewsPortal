namespace NewsPortal.BusinessLogic.Models.RequestModels
{
    public sealed class ChangePasswordModel
    {
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? NewPasswordConfirmation { get; set; }
    }
}
