using Org.BouncyCastle.OpenSsl;

namespace whatsapp_flow_factory.Models.Extensions
{
    public class PasswordFinder : IPasswordFinder
    {
        private readonly char[] _password;

        public PasswordFinder(string password)
        {
            _password = password.ToCharArray();
        }

        public char[] GetPassword()
        {
            return _password;
        }
    }
}
