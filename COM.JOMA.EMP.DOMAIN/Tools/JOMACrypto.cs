using System.Security.Cryptography;
using System.Text;

namespace COM.JOMA.EMP.DOMAIN.Tools
{
    public class JOMACrypto
    {
        public enum SHAFamily
        {
            SHA256,
            SHA384,
            SHA512
        }
        public static byte[] ComputeMD5Hash(string value)
        {
            using (var md5 = MD5.Create()) // Usar el método Create() en lugar de MD5CryptoServiceProvider
            {
                return md5.ComputeHash(Encoding.ASCII.GetBytes(value));
            }
        }

        public static string CifrarClave(string dataToEncrypt, string password, string salt)
        {
            using (var aes = Aes.Create())
            {
                // En .NET 6 y versiones posteriores, especificamos explicitamente el iteraciones y hash algorithm
                using (var key = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt), 10000, HashAlgorithmName.SHA256))
                {
                    aes.Key = key.GetBytes(32); // AES-256
                    aes.IV = key.GetBytes(16);

                    using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                    using (var ms = new MemoryStream())
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(dataToEncrypt);
                        cs.Write(bytes, 0, bytes.Length);
                        cs.FlushFinalBlock();
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        public static string DescifrarClave(string dataToDecrypt, string password, string salt)
        {
            try
            {
                dataToDecrypt = dataToDecrypt.Replace(" ", "+");

                using (var aes = Aes.Create())
                {
                    using (var key = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt), 10000, HashAlgorithmName.SHA256))
                    {
                        aes.Key = key.GetBytes(32); // AES-256
                        aes.IV = key.GetBytes(16);

                        using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                        using (var ms = new MemoryStream())
                        using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                        {
                            byte[] bytes = Convert.FromBase64String(dataToDecrypt);
                            cs.Write(bytes, 0, bytes.Length);
                            cs.FlushFinalBlock();
                            return Encoding.UTF8.GetString(ms.ToArray());
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public static string ComputeHashV1(string plainText, byte[] salt = null, SHAFamily algorithm = SHAFamily.SHA512)
        {
            if (salt == null)
            {
                salt = new byte[16];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
            }

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] plainTextWithSaltBytes = new byte[plainTextBytes.Length + salt.Length];

            Buffer.BlockCopy(plainTextBytes, 0, plainTextWithSaltBytes, 0, plainTextBytes.Length);
            Buffer.BlockCopy(salt, 0, plainTextWithSaltBytes, plainTextBytes.Length, salt.Length);

            byte[] hashBytes;

            using (var hashAlgorithm = GetHashAlgorithm(algorithm))
            {
                hashBytes = hashAlgorithm.ComputeHash(plainTextWithSaltBytes);
            }

            byte[] hashWithSaltBytes = new byte[hashBytes.Length + salt.Length];
            Buffer.BlockCopy(hashBytes, 0, hashWithSaltBytes, 0, hashBytes.Length);
            Buffer.BlockCopy(salt, 0, hashWithSaltBytes, hashBytes.Length, salt.Length);

            return Convert.ToBase64String(hashWithSaltBytes);
        }

        private static HashAlgorithm GetHashAlgorithm(SHAFamily algorithm)
        {
            return algorithm switch
            {
                SHAFamily.SHA256 => SHA256.Create(),
                SHAFamily.SHA384 => SHA384.Create(),
                SHAFamily.SHA512 => SHA512.Create(),
                _ => throw new InvalidOperationException("Invalid SHAFamily")
            };
        }

        public static bool ConfirmHashV1(string plainText, string hashValue, SHAFamily algorithm = SHAFamily.SHA512)
        {
            byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

            int hashSizeInBytes;

            switch (algorithm)
            {
                case SHAFamily.SHA256:
                    hashSizeInBytes = 32;
                    break;
                case SHAFamily.SHA384:
                    hashSizeInBytes = 48;
                    break;
                case SHAFamily.SHA512:
                    hashSizeInBytes = 64;
                    break;
                default:
                    throw new InvalidOperationException("Invalid SHAFamily");
            }

            byte[] saltBytes = new byte[hashWithSaltBytes.Length - hashSizeInBytes];
            Buffer.BlockCopy(hashWithSaltBytes, hashSizeInBytes, saltBytes, 0, saltBytes.Length);

            string expectedHashString = ComputeHashV1(plainText, saltBytes, algorithm);

            return hashValue == expectedHashString;
        }

        public static (string Hash, string HashSalt) ComputeHashV2(string plainText, byte[] salt = null, SHAFamily algorithm = SHAFamily.SHA512)
        {
            if (salt == null)
            {
                salt = new byte[16];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
            }

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] plainTextWithSaltBytes = new byte[plainTextBytes.Length + salt.Length];

            Buffer.BlockCopy(plainTextBytes, 0, plainTextWithSaltBytes, 0, plainTextBytes.Length);
            Buffer.BlockCopy(salt, 0, plainTextWithSaltBytes, plainTextBytes.Length, salt.Length);

            byte[] hashBytes;

            using (var hashAlgorithm = GetHashAlgorithm(algorithm))
            {
                hashBytes = hashAlgorithm.ComputeHash(plainTextWithSaltBytes);
            }

            return (Convert.ToBase64String(hashBytes), Convert.ToBase64String(salt));
        }

        public static bool ConfirmHashV2(string plainText, string hashValue, string hashSalt, SHAFamily algorithm = SHAFamily.SHA512)
        {
            try
            {
                byte[] salt = Convert.FromBase64String(hashSalt);
                string expectedHash = ComputeHashV2(plainText, salt, algorithm).Hash;
                return hashValue == expectedHash;
            }
            catch
            {
                return false;
            }
        }

    }
}