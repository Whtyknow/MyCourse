using System.IdentityModel.Selectors;
using System.Security;
using System.Security.Cryptography.X509Certificates;

namespace MyGoogleDrive
{
    public class UserNameValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (userName != "TestUser" || password != "123")
            {
                throw new SecurityException("Неверный логин/пароль");
            }
        }
    }
}