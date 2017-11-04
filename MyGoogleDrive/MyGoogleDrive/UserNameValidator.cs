using System.IdentityModel.Selectors;
using System.Security;
using System.Linq;


namespace MyGoogleDrive
{
    public class UserNameValidator : UserNamePasswordValidator
    {

        public override void Validate(string login, string password)
        {            
            Db db = new Db();
            User u = db.Users.Where(x => x.Login == login).SingleOrDefault();
            if(u == null || u.Password == password)
            {             
                throw new SecurityException("Bad login or password");
            }
            db.Dispose();
        }
    }
}