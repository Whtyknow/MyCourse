using System.IdentityModel.Selectors;
using System.Security;
using System.Security.Cryptography.X509Certificates;

namespace MyGoogleDrive
{
    public class CustomCertificateValidator : X509CertificateValidator
    {
        public override void Validate(X509Certificate2 certificate)
        {
            //if (certificate.Thumbprint != "‎99 b0 49 a1 2a 34 d0 1c 8e a3 c0 58 45 2a d1 4d 79 ea c4 cf")
            //{
            //    throw new SecurityException("Неверный сертификат");
            //}
        }
    }
}