using System.Security.Cryptography;
using System.Text;

namespace KoBooksStoreWeb.Repository
{
    public class HashData
    {
        public string passcode { get; }
        public HashData (string input)
        {
            passcode = HashPasscode(input); 
        }
        private string HashPasscode(string passcode)
        {
            byte[] tmpSource;
            byte[] tmpNewHash;
            string hashedPasscode;
            tmpSource = ASCIIEncoding.ASCII.GetBytes(passcode);
            tmpNewHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

            static string ByteArrayToString(byte[] arrInput)
            {
                int i;
                StringBuilder sOutput = new StringBuilder(arrInput.Length);
                for (i = 0; i < arrInput.Length; i++)
                {
                    sOutput.Append(arrInput[i].ToString("X2"));
                }
                return sOutput.ToString();
            }

            hashedPasscode = ByteArrayToString(tmpNewHash);

            return hashedPasscode;
        }




    }
}
