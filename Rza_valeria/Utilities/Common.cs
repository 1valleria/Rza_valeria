// This utility code in the `Rza_valeria.Utilities` namespace provides password management and user session functionality. 
// The `PasswordUtils` class includes methods to hash passwords securely and validate them according to set criteria.
//The `UserSession` class holds user session information, specifically the `UserId` property to track the current user's ID.
using System.Text;
using System.Security.Cryptography;

namespace Rza_valeria.Utilities
{
    public static class PasswordUtils
    {

        private static readonly char[] specialCharacters = new char[]
        {
            '!','£','$','%','^','&','*','(',')','-','=','_','+','[',']','{','}',';',':','@','#','~','<','>'
        };
        private static readonly char[] digits = new char[]
        {
            '1','2','3','4','5','6','7','8','9','0'
        };
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Convert the password to a byte array
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Compute the hash
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                // Convert the byte array to a hexadecimal string
                var sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));  // "x2" for hexadecimal formatting
                }

                return sb.ToString();  // Return the hashed password as a string
            }
        }
        // Validates the password based on criteria: length, presence of digits, special characters, and mixed case.
        public static async Task<bool> ValidPassword(string password)
        {
            bool valid = true;
            if (password.Length < 8)
            {
                return false;
            }
            else if (!digits.Any(d => password.Contains(d)))
            {
                return false;
            }
            else if (!specialCharacters.Any(sc => password.Contains(sc)))
            {
                return false;
            }
            else if (password.ToUpper() == password)
            {
                return false;
            }
            return valid;
        }
    
    }
    // Class to manage user session information, currently storing the user's ID.
    public class UserSession
    {
        public int UserId { get; set; }
    }
}
