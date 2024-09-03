using COM.JOMA.EMP.DOMAIN.Entities;
using COM.JOMA.EMP.DOMAIN.Parameters;
using System.Security.Cryptography;
using System.Text;

namespace COM.JOMA.EMP.DOMAIN.Tools
{
    public class JOMAOtpManager
    {
        //private readonly ConcurrentDictionary<string, JOMAOtp> _otpStore = new();
        public JOMAOtp GenerateOtp(string userId)
        {
            int otpValue = RandomNumberGenerator.GetInt32(0, (int)Math.Pow(10, DomainParameters.JOMA_OTP_LENGTH));
            string otp = otpValue.ToString($"D{DomainParameters.JOMA_OTP_LENGTH}");

            //string key = GenerateKey(userId, otp);
            var otpDetails = new JOMAOtp
            {
                Otp = otp,
                Expiry = DateTime.UtcNow.AddMinutes(DomainParameters.JOMA_OTP_TIEMPO_EXP_MINUTOS),
                Attempts = 0
            };


            return otpDetails;
        }

        public bool ValidateOtp(JOMAOtp _otpStore)
        {
            //string key = GenerateKey(userId, otp);

            //if (_otpStore.TryGetValue(key, out var otpDetails))
            //{
            // Verifica si el OTP ha caducado
            if (DateTime.UtcNow > _otpStore.Expiry)
            {
                //_otpStore.TryRemove(key, out _);
                return false; // OTP expirado
            }

            _otpStore.Attempts++;
            // Verifica si ha superado el número máximo de intentos
            if (_otpStore.Attempts >= DomainParameters.JOMA_OTP_INTENTOS_MAXIMOS)
            {
                //_otpStore.TryRemove(key, out _);
                return false; // Demasiados intentos fallidos
            }

            // OTP válido
            //_otpStore.TryRemove(key, out _); // Invalida el OTP después de un uso exitoso
            return true;
            //}

            //return false; // OTP no válido
        }

        private string GenerateKey(string userId, string otp)
        {
            // Combina el userId con el OTP y aplica un hash para generar un identificador único
            using (var sha256 = SHA256.Create())
            {
                var combined = Encoding.UTF8.GetBytes(userId + otp);
                var hash = sha256.ComputeHash(combined);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
