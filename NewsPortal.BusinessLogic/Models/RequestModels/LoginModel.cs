﻿namespace NewsPortal.BusinessLogic.Models.RequestModels
{
    public sealed class LoginModel
    {
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
