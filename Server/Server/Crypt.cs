using System;
using System.Security.Cryptography;

namespace Server
{
    static internal class Crypt
    {
        static public byte[] RSAEncrypt(string chiavePubblicaStringa, string robaDaCriptare)
        {
            RSAParameters pubblica;
            {
                var sr = new System.IO.StringReader(chiavePubblicaStringa);
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                pubblica = (RSAParameters)xs.Deserialize(sr);
            }
            var csp = new RSACryptoServiceProvider();
            csp.ImportParameters(pubblica);
            var bytesDaCriptare = System.Text.Encoding.Unicode.GetBytes(robaDaCriptare);
            return csp.Encrypt(bytesDaCriptare, false);
        }
        static public string RSADecrypt(string chiavePrivataStringa, byte[] robaDaDecriptare)
        {
            RSAParameters privata;
            {
                var sr = new System.IO.StringReader(chiavePrivataStringa);
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                privata = (RSAParameters)xs.Deserialize(sr);
            }
            var csp = new RSACryptoServiceProvider();
            csp.ImportParameters(privata);
            var bytesPlainTextData = csp.Decrypt(robaDaDecriptare, false);
            return System.Text.Encoding.Unicode.GetString(bytesPlainTextData);
        }
    }
}
