﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace DigitalOceanBot.Helpers
{
    public static class SecureStateString
    {
        public static string Get(int userId, long chatId)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[50];
                rng.GetBytes(bytes, 0, bytes.Length);
                var sha256 = SHA256.Create();
                var hash = sha256.ComputeHash(bytes);
                var stringBuilder = new StringBuilder();

                foreach (var item in hash)
                {
                    stringBuilder.Append(item.ToString("x2"));
                }

                var result = stringBuilder.ToString();
                result += $";{userId.ToString()};{chatId.ToString()}";
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(result));
            }
        }
    }
}
