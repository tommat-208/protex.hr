using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace protex.hr.Services
{
    public class Hasher
    {
        // Lunghezza del salt in byte
        private const int SaltSize = 16; // 128-bit
        // Lunghezza del hash in byte
        private const int HashSize = 32; // 256-bit
        // Numero di iterazioni per PBKDF2
        private const int Iterations = 100_000;

        // Metodo per generare l'hash della password
        public static string HashPassword(string password)
        {
            // Genera un salt casuale
            using var rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);

            // Crea l'hash della password usando il salt
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            // Combina salt e hash in un'unica stringa codificata in base64
            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            return Convert.ToBase64String(hashBytes);
        }

        // Metodo per verificare una password
        public static bool VerifyPassword(string password, string storedHash)
        {
            // Decodifica la stringa hash in byte
            byte[] hashBytes = Convert.FromBase64String(storedHash);

            // Estrai salt e hash dai byte
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            byte[] storedPasswordHash = new byte[HashSize];
            Array.Copy(hashBytes, SaltSize, storedPasswordHash, 0, HashSize);

            // Ricrea l'hash della password fornita usando lo stesso salt
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] computedHash = pbkdf2.GetBytes(HashSize);

            // Confronta hash calcolato con hash salvato
            return CryptographicOperations.FixedTimeEquals(storedPasswordHash, computedHash);
        }


    }
}
