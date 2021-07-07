using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
namespace aes
{
    class Program
    {
        static void Main(string[] args)
        { 
            string plaintext;
            string decrypted;
            string encryptedtext;
            byte[] encryptedbytes;

            RijndaelManaged crypto = new RijndaelManaged();
            System.Text.UTF8Encoding UTF=new  System.Text.UTF8Encoding();
            Console.WriteLine("TEXT TO BE ENCRYPTED");
            plaintext = Console.ReadLine();
            try
            {
                encryptedbytes = encrypt_function(plaintext, crypto.Key, crypto.IV);
                encryptedtext = UTF.GetString(encryptedbytes);
                decrypted = decrypt_function(encryptedbytes, crypto.Key, crypto.IV);
                Console.WriteLine("Start:{0}", plaintext);
                Console.WriteLine("Encrypted:{0}", encryptedtext);
                Console.WriteLine("decrypted:{0}", decrypted);


            }
            catch (Exception e)
            {
                Console.WriteLine("Exception", e.Message);

            }
            Console.WriteLine("Press enter to exit");
            Console.ReadKey();

        }
        private static byte[] encrypt_function(string plaintext, byte[] key, byte[] IV)
        {
            RijndaelManaged crypto = null;
            MemoryStream memstream = null;
            ICryptoTransform encryptor = null;
            CryptoStream cs = null;
            System.Text.UTF8Encoding byte_transform=new  System.Text.UTF8Encoding();
            byte[] plainbytes = byte_transform.GetBytes(plaintext);
            try
            {
                crypto = new RijndaelManaged();
                crypto.Key = key;
                crypto.IV = IV;
                memstream = new MemoryStream();
                encryptor = crypto.CreateEncryptor(crypto.Key,crypto.IV);
                cs = new CryptoStream(memstream, encryptor, CryptoStreamMode.Write);
                cs.Write(plainbytes, 0, plainbytes.Length);
            }
            finally
            {
                if(crypto!=null)
                {
                    crypto.Clear();
                    cs.Close();
                }
            }
                return memstream.ToArray();
            

            }
        private static string decrypt_function(byte[] ciphertext, byte[] key, byte[] IV)
        {
            RijndaelManaged crypto = null;
            MemoryStream memstream = null;
            ICryptoTransform decryptor = null;
            CryptoStream cs = null;
            StreamReader sr = null;
            string plaintext;
           
            try
            {
                crypto = new RijndaelManaged();
                crypto.Key = key;
                crypto.IV = IV;
                memstream = new MemoryStream(ciphertext);
                decryptor = crypto.CreateDecryptor(crypto.Key, crypto.IV);
                cs = new CryptoStream(memstream, decryptor, CryptoStreamMode.Read);
                sr = new StreamReader(cs);
                plaintext = sr.ReadToEnd();
            }
            finally
            {
                if (crypto != null)
                {
                    crypto.Clear();
                    memstream.Flush();
                    memstream.Close();
                }
            }
            return plaintext;


        }


        }
    }
