﻿using System.Security.Cryptography;
using System.Text;

public static class PasswordHasher
{
    public static string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }

    public static bool VerifyPassword(string password, string storedHash)
    {
        string computedHash = HashPassword(password);
        return computedHash == storedHash;
    }
}