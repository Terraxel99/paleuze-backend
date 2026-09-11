using System.Security.Cryptography;
using System.Text;

using PaleuzeBackend.Business.Repositories;

namespace PaleuzeBackend.Providers.Security.Services.Hashing
{
    public class HashingRepository : IHashingRepository
    {
        public async Task<string> SHA256HashAsync(string input)
        {
            using var ms = new MemoryStream(Encoding.UTF8.GetBytes(input));
            var hashedBytes = await SHA256.HashDataAsync(ms);

            return Convert.ToHexString(hashedBytes);
        }
    }
}
