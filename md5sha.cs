using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace ConsoleApplication2
{
    class Program
    {
        static string GetMD5Hash(string plaintext)
        {

            MD5CryptoServiceProvider MD5provider = new MD5CryptoServiceProvider();
            byte[] hashedvalue= MD5provider.ComputeHash(Encoding.Default.GetBytes(plaintext));
            StringBuilder str = new StringBuilder();
            for (int counter = 0; counter < hashedvalue.Length; counter++)
            {
                str.Append(hashedvalue[counter].ToString("x2"));
            }
            return str.ToString();
        }
        static bool VerifyMD5hash(string PlainText, string prevhashedvalue)
        {
            string hashedvalue2 = GetMD5Hash(PlainText);
            StringComparer strcomparer = StringComparer.OrdinalIgnoreCase;
            if (strcomparer.Compare(hashedvalue2, prevhashedvalue).Equals(0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the value for hashing");
            string yourvalue = Console.ReadLine();
            string strhashed = GetMD5Hash(yourvalue);
            Console.WriteLine("Hashed Value" + strhashed);
            Console.WriteLine("\nDo you want to verify your hash ?   If yes press Y");
            char ch = Convert.ToChar(Console.ReadLine());
            if (ch == 'Y' || ch == 'y')
            {
                Console.WriteLine("Enter value again");
                string yourvalue2 = Console.ReadLine();
                bool res = VerifyMD5hash(yourvalue2, strhashed);
                Console.WriteLine("-------------");
                if (res)
                {
                    Console.WriteLine("Hash is matched");
                }
                else
                {
                    Console.WriteLine("Hash is not matching");
                }
                 Console.WriteLine("-------------");
            }
                else {Environment.Exit(1);}
            }
        }
    }
