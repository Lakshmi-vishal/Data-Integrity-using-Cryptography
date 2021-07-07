using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;



namespace ConsoleApplication10
{
    class Program
    {
        public static class SHA
        {
            public static string GenerateSHA256String(string inputString)
            {
                SHA256 sha256 = SHA256Managed.Create();
                byte[] bytes = Encoding.UTF8.GetBytes(inputString);
                byte[] hash = sha256.ComputeHash(bytes);
                return GetStringFromHash(hash);
            }
            public static string GenerateSHA512String(string inputString)
            {
                SHA512 sha512 = SHA512Managed.Create();
                byte[] bytes = Encoding.UTF8.GetBytes(inputString);
                byte[] hash = sha512.ComputeHash(bytes);
                return GetStringFromHash(hash);
            }
            private static string GetStringFromHash(byte[] hash)
            {
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    result.Append(hash[i].ToString("X2"));
                   
                }
                return result.ToString();
            }
            public static void Main(string[] arg)
            {
                Console.WriteLine("Enter the string to be hashed using SHA256");
                string yourvalue = Console.ReadLine();
                string strhashed = GenerateSHA256String(yourvalue);
                Console.WriteLine("Hashed Value" + strhashed);
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("Enter the string to be hashed using SHA512");
                string yourvalue1 = Console.ReadLine();
                string strhashed1 = GenerateSHA256String(yourvalue1);
                Console.WriteLine("Hashed Value" + strhashed1);


            }
        }
       
    }
}
