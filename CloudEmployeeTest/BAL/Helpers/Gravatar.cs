// http://codehill.com/2009/03/displaying-gravatars-using-c/
namespace CloudEmployee.BAL.Helpers
{
    using System;
    using Windows.Security.Cryptography;
    using Windows.Security.Cryptography.Core;
    using Windows.Storage.Streams;

    public class GravatarImage
    {
        private const string _url = "http://www.gravatar.com/avatar.php?gravatar_id=";

        /// <summary&gr;
        /// Get the URL of the image
        /// </summary&gr;
        /// <param name="email"&gr;The email address</param&gr;
        /// <param name="size"&gr;The size of the image (1 - 600)</param&gr;
        /// <param name="rating"&gr;The MPAA style rating(g, pg, r, x)</param&gr;
        /// <returns>The image URL</returns&gr;
        public static string GetURL(string email, int size, string rating)
        {
            email = email.ToLower();
            email = ComputeMD5(email);

            if (size < 1 | size > 600)
            {
                throw new ArgumentOutOfRangeException("size",
                    "The image size should be between 20 and 80");
            }

            rating = rating.ToLower();
            if (rating != "g" & rating != "pg" & rating != "r" & rating != "x")
            {
                throw new ArgumentOutOfRangeException("rating",
                    "The rating can only be one of the following: g, pg, r, x");
            }

            return _url + email + "&s=" + size.ToString() + "&r=" + rating + "&d=retro";
        }

        /// <summary&gr;
        /// Hash an input string and return the hash as a 32 character hexadecimal string
        /// </summary&gr;
        /// <param name="input"&gr;The string to hash</param&gr;
        /// <returns&gr;The MD5 hash</returns&gr;
        static string ComputeMD5(string str)
        {
            var alg = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            IBuffer buff = CryptographicBuffer.ConvertStringToBinary(str, BinaryStringEncoding.Utf8);
            var hashed = alg.HashData(buff);
            var res = CryptographicBuffer.EncodeToHexString(hashed);
            return res;
        }
    }
}
