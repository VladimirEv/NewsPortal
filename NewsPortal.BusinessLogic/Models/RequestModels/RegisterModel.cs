﻿namespace NewsPortal.BusinessLogic.Models.RequestModels
{
    public sealed class RegisterModel
    {
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? PasswordConfirmation { get; set; }
    }
}