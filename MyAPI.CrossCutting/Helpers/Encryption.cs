using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MyAPI.CrossCutting.Helpers
{
    public class Encryption
    {
        private HashAlgorithm _algorithm;

        public Encryption(HashProvider hashProvider = HashProvider.SHA1)
        {
            switch (hashProvider)
            {
                case HashProvider.SHA1:
                    _algorithm = new SHA1Managed();
                    break;
                case HashProvider.SHA256:
                    _algorithm = new SHA256Managed();
                    break;
                case HashProvider.SHA384:
                    _algorithm = new SHA384Managed();
                    break;
                case HashProvider.SHA512:
                    _algorithm = new SHA512Managed();
                    break;
                case HashProvider.MD5:
                    _algorithm = new MD5CryptoServiceProvider();
                    break;
            }
        }

        public string GetHash(string plainText)
        {
            byte[] hash = this._algorithm.ComputeHash(Encoding.ASCII.GetBytes(plainText));
            return Convert.ToBase64String(hash, 0, hash.Length);
        }

        public string GetHash(FileStream fileStream)
        {
            byte[] hash = this._algorithm.ComputeHash((Stream)fileStream);
            fileStream.Close();
            return Convert.ToBase64String(hash, 0, hash.Length);
        }

        public enum HashProvider
        {
            SHA1,
            SHA256,
            SHA384,
            SHA512,
            MD5,
        }
    }
}
