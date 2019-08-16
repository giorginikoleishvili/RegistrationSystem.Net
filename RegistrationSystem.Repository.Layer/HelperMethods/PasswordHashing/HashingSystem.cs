﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystem.Repository.Layer.HelperMethods.PasswordHashing
{
    public static  class HashingSystem
    {
        private const int SaltSize = 16;
        private const int HashSize = 20;
       
        /// <param name="password">the password</param>
        /// <param name="iterations">number of iterations</param>
        
        public static string Hash(string password, int iterations)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);
          
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(HashSize);
          
            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);
         
            var base64Hash = Convert.ToBase64String(hashBytes);
          
            return string.Format("$MYHASH$V1${0}${1}", iterations, base64Hash);
        }
        
        /// <param name="password">the password</param>
       
        public static string Hash(string password)
        {
            return Hash(password, 10000);
        }
       
        /// <param name="hashString">the hash</param>
        
        private static bool IsHashSupported(string hashString)
        {
            return hashString.Contains("$MYHASH$V1$");
        }
      
        /// <param name="password">the password</param>
        /// <param name="hashedPassword">the hash</param>
       
        public static bool Verify(string password, string hashedPassword)
        {
            //check hash
            if (!IsHashSupported(hashedPassword))
            {
                throw new NotSupportedException("The hashtype is not supported");
            }
          
            var splittedHashString = hashedPassword.Replace("$MYHASH$V1$", "").Split('$');
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];
         
            var hashBytes = Convert.FromBase64String(base64Hash);
            
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);
           
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);
            
            for (var i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
