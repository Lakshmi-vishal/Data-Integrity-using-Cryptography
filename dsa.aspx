using System;
using System.Collections .Generic;

using System. linq;

using System.Web;

using System.Web.UI;

using System.Web.UI .WebControls;

using System.Security.Cryptography;

using System.I0;

using System. Text;



namespace encrypt 
{
public partial class dsa : System.Web.UI.page
{
 protected void Page Load( object sender, EventArgs e)
{
}
 protected void Button Click( object sender, EventArgs e)
{
byte[] hashedDocument; 

var document = Encoding.UTF8.GetBytes(“Secret message verifying using digital signature algorithm”);

using (var sha256 = SHA256.Create())
{
hashedDocument = sha256.ComputeHash (document);
}
var digitalSignature = new DigitalSignature ();
digitalSignature.AssignNewKey();

 hashedDocument = sha256.ComputeHash(document);
}

var digitalSignature= new DigitalSignature();
digitalSignature.AssignNewkey();
 var signature= digitalSignature.SignData(hashedDocument);
var verified= digitalSignature.VerifySignature(hashedDocument,signature);
Label3.Text = Encoding.Default.GetString(document);
TextBox1.Text=Convert.ToBase64String(signature);

if(verified)
{
Response.Write(“The digital Signature has been correctly verified”);
}
else
{
Response.Write(“The digital Signature has not  been correctly verified”);
}
}
public sealed class DigitalSignature
{
private RSAParameters publickey;
private RSAParameters privatekey;
public void AssignNewKey()
{
using( var rsa = new RSACryptoServiceProvider(2048))
{
rsa.PersistKeyInCsp = false;

rsa. ImportParameters(privateKey) ;

var rsaFormatter = new RSAPKCS1SignatureFormatter (rsa);
rsaFormatter .SetHashAlgorithm("SHA256") ;

return rsaFormatter. CreateSignature(hashOfDataToSign);

}
}
Public bool VerifySignature(byte[] hashOfDataToSign,byte[] signature)
{
using (var rsa = new RSACryptoServiceProvider(2048) )

{

rsa.importParameters(publickey);
var rsaDeformatter= new RSAPKCS1SignatureDeFormatter(rsa);
rsaDeformatter.SetHashAlgorithm("SHA256");

return rsaDeformatter.VeritySignature(hashOfDataToSign,signature);
}
}
}
}
}
