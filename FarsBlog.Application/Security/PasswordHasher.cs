using System.Security.Cryptography;
using System.Text;

namespace FarsBlog.Application.Security;

public static class PasswordHasher
{
    //Encrypt using MD5   
    public static string EncodePasswordMd5(this string pass)
    {
        Byte[] originalBytes;
        Byte[] encodedBytes;
        MD5 md5;
        //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)   
#pragma warning disable SYSLIB0021 // Type or member is obsolete
        md5 = new MD5CryptoServiceProvider();
#pragma warning restore SYSLIB0021 // Type or member is obsolete
        originalBytes = ASCIIEncoding.Default.GetBytes(pass);
        encodedBytes = md5.ComputeHash(originalBytes);
        //Convert encoded bytes back to a 'readable' string   
        return BitConverter.ToString(encodedBytes);
    }
}