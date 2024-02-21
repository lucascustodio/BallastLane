﻿
namespace Person.Domain.Core.Util;
public static class Crypto
{
    public static string HashPassword(string password)
    {
        string salt = BCrypt.Net.BCrypt.GenerateSalt();

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

        return hashedPassword;
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        bool passwordMatches = BCrypt.Net.BCrypt.Verify(password, hashedPassword);

        return passwordMatches;
    }
}
