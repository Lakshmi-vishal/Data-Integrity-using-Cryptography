using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
namespace encrypt
{
    public partial class login : System.Web.UI.Page
    {
        UnicodeEncoding ByteConverter = new UnicodeEncoding();
        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
        byte[] plaintext;
        byte[] encryptedtext;
        static public byte[] Encryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey);
                    encryptedData = RSA.Encrypt(Data, DoOAEPPadding);
                }
                return encryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }

  

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("page2.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            plaintext = ByteConverter.GetBytes(TextBox1.Text);
            encryptedtext = Encryption(plaintext, RSA.ExportParameters(false), false);
            TextBox3.Text = ByteConverter.GetString(encryptedtext);
        }
    }
}
