using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] hashedDocument;

            var document = Encoding.UTF8.GetBytes("Secret message");
           
            using (var sha256 = SHA256.Create())
            {
                hashedDocument = sha256.ComputeHash(document);
            }

            var digitalSignature = new DigitalSignature();
            digitalSignature.AssignNewKey();

            var signature = digitalSignature.SignData(hashedDocument);
            var verified = digitalSignature.VerifySignature(hashedDocument, signature);

            Console.WriteLine("Digital Signature Implementation in .NET");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("   Original Text = " + System.Text.Encoding.Default.GetString(document));
            Console.WriteLine();
            Console.WriteLine("   Digital Signature = " + Convert.ToBase64String(signature));
            Console.WriteLine();

            if (verified)
            {
                Console.WriteLine("The digital signature has been correctly verified.");
            }
            else
            {
                Console.WriteLine("The digital signature has NOT been correctly verified.");
            }

            Console.ReadLine();
        }

    }
}
public sealed class DigitalSignature
    {
private RSAParameters publicKey;
private RSAParameters privateKey;

public void AssignNewKey()
        {
using (var rsa = new RSACryptoServiceProvider(2048))
            {                
rsa.PersistKeyInCsp = false;               
publicKey = rsa.ExportParameters(false);
privateKey = rsa.ExportParameters(true);                
            }
        }

public byte[] SignData(byte[] hashOfDataToSign)
        {
using (var rsa = new RSACryptoServiceProvider(2048))
            {
rsa.PersistKeyInCsp = false;
rsa.ImportParameters(privateKey);

var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);                
rsaFormatter.SetHashAlgorithm("SHA256");

return rsaFormatter.CreateSignature(hashOfDataToSign);
            }
        }

public bool VerifySignature(byte[] hashOfDataToSign, byte[] signature)
        {
using (var rsa = new RSACryptoServiceProvider(2048))
            {
rsa.ImportParameters(publicKey);

var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
rsaDeformatter.SetHashAlgorithm("SHA256");

return rsaDeformatter.VerifySignature(hashOfDataToSign, signature);
            }
        }   
    }
