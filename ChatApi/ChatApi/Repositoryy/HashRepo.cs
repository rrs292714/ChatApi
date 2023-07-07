using ChatApi.Interface;
using System.Security.Cryptography;
using System.Text;

namespace ChatApi.Repositoryy
{
    public class HashRepo:IHash
    {
        public string Hash(string message, string salt)
        {
            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(salt));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
            return Convert.ToBase64String(hash);
        }

        public string OriginalMessage(string hash, string salt, Encoding encoding)
        {
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

            using (var hmac = new HMACSHA256(saltBytes))
            {
                byte[] hashBytes = Convert.FromBase64String(hash);
                byte[] originalBytes = hmac.ComputeHash(hashBytes);
                string originalMessage = encoding.GetString(originalBytes);

                return originalMessage;
            }
        }

    }
}
