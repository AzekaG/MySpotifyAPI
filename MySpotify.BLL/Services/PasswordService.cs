using MySpotify.DAL.Entities;
using System.Security.Cryptography;
using System.Text;


namespace MySpotify.BLL.Services
{
    public static class PasswordService
    {
      public static Dictionary<string,string> CreatePass(string pass)
        {
            byte[] saltbuf = new byte[16];
            RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(saltbuf);

            StringBuilder sb = new StringBuilder(16);
            for (int i = 0; i < 16; i++)
                sb.Append(string.Format("{0:X2}", saltbuf[i]));
            string salt = sb.ToString();

            //переводим пароль в байт-массив  
            byte[] password = Encoding.Unicode.GetBytes(salt + pass);

            //создаем объект для получения средств шифрования  
            var md5 = MD5.Create();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = md5.ComputeHash(password);

            StringBuilder hash = new StringBuilder(byteHash.Length);
            for (int i = 0; i < byteHash.Length; i++)
                hash.Append(string.Format("{0:X2}", byteHash[i]));

            pass = hash.ToString();
            Dictionary<string, string> res = new Dictionary<string, string>();
            res["Password"] = pass;
            res["Salt"] = salt;
            return res;
        }

      public static bool isLogged(string logonPass, User user)
        {
            string? salt = user.Salt;

            byte[] password = Encoding.Unicode.GetBytes(salt + logonPass);
       
            var md5 = MD5.Create();

            byte[] byteHash = md5.ComputeHash(password);

            StringBuilder hash = new StringBuilder(byteHash.Length);
            for(int i = 0;i < byteHash.Length; i++)
            {
                hash.Append(string.Format("{0:X2}" , byteHash[i]));
            }

            if (user.Password != hash.ToString())
                return false;
            return true;
            
        }







    }
}
