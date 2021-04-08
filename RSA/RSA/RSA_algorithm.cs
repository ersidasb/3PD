using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
                if (DBD(e, fi) == 1)
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
        }

        public string decrypt(EncryptedModel model)
        {
            int[] encrypted = Array.ConvertAll(model.y.Split(','), int.Parse);
            string decrypted = "";
            foreach (int i in encrypted)
                decrypted += Convert.ToChar((Int32)BigInteger.ModPow(i, IEA(model.e, phi(model.n)), model.n));
            return decrypted;
        }
        private int phi(int n)
        {
            int result = 1;
            for (int i = 2; i < n; i++)
                if (DBD(i,n) == 1)
                    result++;
            return result;
        }
        private int DBD(int a, int b)
        {

            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }
            return  a | b;
        }
        public int IEA(int a, int b)
        {
            int x0 = 1, xn = 1, x1 = 0, f, r = a % b;

            while (r > 0)
            {
                f = a / b;
                xn = x0 - f * x1;

                x0 = x1;
                x1 = xn;
                a = b;
                b = r;
                r = a % b;
            }
            return Convert.ToInt32(x1);
        }
    }


}
