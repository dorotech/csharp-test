using System.Security.Cryptography;
using System.Text;

namespace DoroTechCSharpTest.Domain.Utils
{
    public class EncryptionService
    {
        #region Public Methods
        /// <summary>
        /// Creates a random salt
        /// </summary>
        /// <returns></returns>
        public string CreateSalt()
        {
            var data = new byte[0x10];
            using (var cryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                cryptoServiceProvider.GetBytes(data);
                return Convert.ToBase64String(data);
            }
        }
        /// <summary>
        /// Generates a Hashed password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public string EncryptPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = string.Format("{0}{1}", salt, password);
                byte[] saltedPasswordAsBytes = Encoding.UTF8.GetBytes(saltedPassword);

                return Convert.ToBase64String(sha256.ComputeHash(saltedPasswordAsBytes));
            }
        }


        public bool ComparePasswords(string UserPassword, string UserSalt, string Password)
        {
            string InformedPassword = EncryptPassword(Password, UserSalt);
            return UserPassword.Equals(InformedPassword);
        }
        #endregion
    }
}