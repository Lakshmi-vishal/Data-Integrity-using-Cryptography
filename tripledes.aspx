using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace tripdes
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] key = Encoding.ASCII.GetBytes("passwordDR0wSS@P6660juht");
            byte[] iv = Encoding.ASCII.GetBytes("password");
            byte[] data = Encoding.ASCII.GetBytes("Test");
            byte[] enc = new byte[0];
            TripleDES tdes = TripleDES.Create();
            tdes.IV = iv;
            tdes.Key = key;
            tdes.Mode = CipherMode.CBC;
            tdes.Padding = PaddingMode.Zeros;
            ICryptoTransform ict = tdes.CreateEncryptor();
            enc = ict.TransformFinalBlock(data, 0, data.Length);
            Console.Write("Original Text: This is secret message");
            Console.Write(" \nEncrypted Text");
            Console.WriteLine(Bin2Hex(enc));
            
            Console.ReadLine();

        }
        static string Bin2Hex(byte[] bin)
        {
            StringBuilder sb = new StringBuilder(bin.Length * 2);
            foreach (byte b in bin)
            {
                sb.Append(b.ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
            }

        }
    }
