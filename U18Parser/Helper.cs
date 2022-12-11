using System.Security.Cryptography;


namespace U18chanParser
{
    public static class Helper
    {
        public static string GetHash(this string inputstring)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(inputstring);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes);

            }
        }

        


    }
}
