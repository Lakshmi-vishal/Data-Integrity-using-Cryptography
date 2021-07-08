using System;
using System, Collections Generic;
Using System.Linq;

using System.Web;

Using System.Web.UI;

using System.Web.UI .WebControls;

using System. Security. Cryptography ;

namespace encrypt

{
public partial class page2: System.Web.UI.Page
{
string Encryptionkey = “123456789”;

protected void Page_Load(object sender, EventArgs e)
{
}
protected void btnDecrypt_click(object sender, EventArgs e)
{

TextBox2.Text= DecryptString(textEncryptedValue.Text, Encryptionkey);
}
public  void Button1_Click(object sender, EventArgs e)
{
TextBox1.Text=EncryptString(txtNormalValue.Text,Encryption Key);
}
public string EncryptString(string Message, string passphrase)
{
byte[] Results;
System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
MD5CryptoServiceProvider Hashprovider = new MD5CryptoServiceProvider();
byte[] TDESkey= Hashprovider.ComputeHash(UTF8.GetBytes(Passphrase));
TripleDES CryptoServiceProvider TDESAlgorithm= new TripleDES CryptoServiceProvider();
TDESAlgorithm.Key = TDESKey;
TDESAlgorithm.Mode=CipherMode.ECB;
TDESAlgorithm.Padding=PaddingMode.PKCS7;
byte[] DataToEncrypt = UTF8.GetBytes(Message);
try
{
ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
Results=Encryptor.TransformFinalBlock(DataToEncrypt,0,DataToEncrypt.Length);
}
finally
{
TDESAlgorithm.Clear();
HashProvider.Clear();
}
return Convert.toBase64String(Results);
}
public string DecryptString(string Message, string Passphrase)
{
byte[] Results;
System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
MD5CryptoServiceProvider Hashprovider = new MD5CryptoServiceProvider();
byte[] TDESkey= Hashprovider.ComputeHash(UTF8.GetBytes(Passphrase));
TripleDES CryptoServiceProvider TDESAlgorithm= new TripleDES CryptoServiceProvider();
TDESAlgorithm.Key = TDESKey;
TDESAlgorithm.Mode=CipherMode.ECB;
TDESAlgorithm.Padding=PaddingMode.PKCS7;
byte[] DataToDecrypt = Convert.FromBase64String(Message);
try
{
ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
Results = Decryptor.TransformFinalBlock(DataToDecrypt,0,DataToDecrypt.Length);
}
finally
{
TDESAlgorithm.Clear();
HashProvider.Clear();
}
return UTF8.GetString(Results);
}

