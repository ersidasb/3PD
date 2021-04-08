using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    class RSA_algorithm
    {
        public void encrypt(int p, int q, string x)
        {
            int n = p * q;
            int fi = (q - 1) * (p - 1);
            int e=2;
            while(e<fi)
            {
                if (DBD(Convert.ToUInt64(e), Convert.ToUInt64(fi)) == 1)
                    break;
                else
                    e++;
            }
            string encrypted = "";
            foreach(char c in x.ToCharArray())
            {
                encrypted += $"{Convert.ToString(Math.Pow(Convert.ToInt32(c),e)%n)},";
            }
            encrypted = encrypted.Remove(encrypted.Length - 1);
            EncryptedModel model = new EncryptedModel();
            model.e = e;
            model.n = n;
            model.y = encrypted;
            DataBase.SaveEncrypted(model);
            //string[] i = encrypted.Split(',');
            //int[] ch = Array.ConvertAll(i, int.Parse);
        }

        public string decrypt(EncryptedModel model)
        {
            string decrypted;
            return decrypted;
        }

        private ulong DBD(ulong a, ulong b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }
            return a | b;
        }
    }


}
