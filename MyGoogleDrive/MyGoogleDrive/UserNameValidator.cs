using System.IdentityModel.Selectors;
using System.Security;
using System.Linq;
using System.ServiceModel.Security;
using System.IdentityModel.Tokens;
using System.ServiceModel;

namespace MyGoogleDrive
{
    public class UserNameValidator : UserNamePasswordValidator
    {

        public override void Validate(string login, string password)
        {
            User u;            
            using (Db db = new Db())
            {
               u = db.Users.Where(x => x.Login == login).SingleOrDefault();
            }
                if (u == null || u.Password != password)
                {
                throw new FaultException("Wrong login or password");
                }            
        }
    }
}