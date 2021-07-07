using System;
using System.IO;
using System.Security.Cryptography;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var csp = new RSACryptoServiceProvider(2048);

        //how to get the private key
        var privKey = csp.ExportParameters(true);

        //and the public key ...
        var pubKey = csp.ExportParameters(false);

        //converting the public key into a string representation
        string pubKeyString;
            {
                //we need some buffer
                var sw = new System.IO.StringWriter();
        //we need a serializer
        var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
        //serialize the key into the stream
        xs.Serialize(sw, pubKey);
                //get the string from the stream
                pubKeyString = sw.ToString();
            }

            //converting it back
            {
                //get a stream from the string
                var sr = new System.IO.StringReader(pubKeyString);
    //we need a deserializer
    var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
    //get the object back from the stream
    pubKey = (RSAParameters) xs.Deserialize(sr);
}

//conversion for the private key is no black magic either ... omitted

//we have a public key ... let's get a new csp and load that key
csp = new RSACryptoServiceProvider();
csp.ImportParameters(pubKey);

            //we need some data to encrypt
            var plainTextData = "LAKSHMI";
Console.WriteLine("Original text" + plainTextData);
            //for encryption, always handle bytes...
            var bytesPlainTextData = System.Text.Encoding.Unicode.GetBytes(plainTextData);

//apply pkcs#1.5 padding and encrypt our data 
var bytesCypherText = csp.Encrypt(bytesPlainTextData, false);

//we might want a string representation of our cypher text... base64 will do
var cypherText = Convert.ToBase64String(bytesCypherText);


/*
 * some transmission / storage / retrieval
 * 
 * and we want to decrypt our cypherText
 */

//first, get our bytes back from the base64 string ...
bytesCypherText = Convert.FromBase64String(cypherText);

            //we want to decrypt, therefore we need a csp and load our private key
            csp = new RSACryptoServiceProvider();
csp.ImportParameters(privKey);

            //decrypt and strip pkcs#1.5 padding
            bytesPlainTextData = csp.Decrypt(bytesCypherText, false);
            Console.WriteLine("Encrypted Text" + cypherText);
            //get our original plainText back...
            plainTextData = System.Text.Encoding.Unicode.GetString(bytesPlainTextData);
            Console.WriteLine("decrypted text" + plainTextData);
        }

        public static string Encryption(string strText)
{
    var publicKey = "<RSAKeyValue><Modulus>21wEnTU+mcD2w0Lfo1Gv4rtcSWsQJQTNa6gio05AOkV/Er9w3Y13Ddo5wGtjJ19402S71HUeN0vbKILLJdRSES5MHSdJPSVrOqdrll/vLXxDxWs/U0UT1c8u6k/Ogx9hTtZxYwoeYqdhDblof3E75d9n2F0Zvf6iTb4cI7j6fMs=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

    var testData = Encoding.UTF8.GetBytes(strText);

    using (var rsa = new RSACryptoServiceProvider(1024))
    {
        try
        {
            // client encrypting data with public key issued by server                    
            rsa.FromXmlString(publicKey.ToString());

            var encryptedData = rsa.Encrypt(testData, true);

            var base64Encrypted = Convert.ToBase64String(encryptedData);

            return base64Encrypted;
        }
        finally
        {
            rsa.PersistKeyInCsp = false;
        }
    }
}

public static string Decryption(string strText)
{
    var privateKey = "<RSAKeyValue><Modulus>21wEnTU+mcD2w0Lfo1Gv4rtcSWsQJQTNa6gio05AOkV/Er9w3Y13Ddo5wGtjJ19402S71HUeN0vbKILLJdRSES5MHSdJPSVrOqdrll/vLXxDxWs/U0UT1c8u6k/Ogx9hTtZxYwoeYqdhDblof3E75d9n2F0Zvf6iTb4cI7j6fMs=</Modulus><Exponent>AQAB</Exponent><P>/aULPE6jd5IkwtWXmReyMUhmI/nfwfkQSyl7tsg2PKdpcxk4mpPZUdEQhHQLvE84w2DhTyYkPHCtq/mMKE3MHw==</P><Q>3WV46X9Arg2l9cxb67KVlNVXyCqc/w+LWt/tbhLJvV2xCF/0rWKPsBJ9MC6cquaqNPxWWEav8RAVbmmGrJt51Q==</Q><DP>8TuZFgBMpBoQcGUoS2goB4st6aVq1FcG0hVgHhUI0GMAfYFNPmbDV3cY2IBt8Oj/uYJYhyhlaj5YTqmGTYbATQ==</DP><DQ>FIoVbZQgrAUYIHWVEYi/187zFd7eMct/Yi7kGBImJStMATrluDAspGkStCWe4zwDDmdam1XzfKnBUzz3AYxrAQ==</DQ><InverseQ>QPU3Tmt8nznSgYZ+5jUo9E0SfjiTu435ihANiHqqjasaUNvOHKumqzuBZ8NRtkUhS6dsOEb8A2ODvy7KswUxyA==</InverseQ><D>cgoRoAUpSVfHMdYXW9nA3dfX75dIamZnwPtFHq80ttagbIe4ToYYCcyUz5NElhiNQSESgS5uCgNWqWXt5PnPu4XmCXx6utco1UVH8HGLahzbAnSy6Cj3iUIQ7Gj+9gQ7PkC434HTtHazmxVgIR5l56ZjoQ8yGNCPZnsdYEmhJWk=</D></RSAKeyValue>";

    var testData = Encoding.UTF8.GetBytes(strText);

    using (var rsa = new RSACryptoServiceProvider(1024))
    {
        try
        {
            var base64Encrypted = strText;

            // server decrypting data with private key                    
            rsa.FromXmlString(privateKey);

            var resultBytes = Convert.FromBase64String(base64Encrypted);
            var decryptedBytes = rsa.Decrypt(resultBytes, true);
            var decryptedData = Encoding.UTF8.GetString(decryptedBytes);
            return decryptedData.ToString();
        }
        finally
        {
            rsa.PersistKeyInCsp = false;
        }
    }
}
    }
}
        
