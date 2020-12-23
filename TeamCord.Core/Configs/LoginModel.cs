using Forge.Forms.Annotations;
using System;

namespace TeamCord.Core
{
    [Title("Log in")]
    [Action("cancel", "Cancel", IsCancel = true, ClosesDialog = true)]
    [Action("login", "Login", IsDefault = true, ClosesDialog = true, Validates = true)]
    public class LoginModel
    {
        [Field(Icon = "Account", Name = "E-Mail")]
        [Value(Must.NotBeEmpty)]
        public string Username { get; set; }

        [Field(Icon = "Key")]
        [Value(Must.NotBeEmpty)]
        [Password]
        public string Password { get; set; }
    }
}