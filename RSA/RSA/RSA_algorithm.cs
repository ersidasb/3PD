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
        public string encrypt(int p, int q, string x)
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
            return encrypted;
        }

        public string decrypt(EncryptedModel model)
        {
            int[] encrypted = Array.ConvertAll(model.y.Split(','), int.Parse);
            string decrypted = "";
            foreach (int i in encrypted)
            {
                int temp3 = phi(model.n);
                int temp2 = IEA(model.e, temp3);
                int temp = (Int32)BigInteger.ModPow(i, temp2, model.n);
                decrypted += Convert.ToChar(temp);
            }
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
        public int IEA(int e, int N)
        {
            int r0, r1, s0, s1, t0, t1, ri, qi;
            int maz;
            int r0const, r1const;

            int s = 0;
            int t = 0;
            int d;
            // a = N || b = e
            if (N < e)
            {
                maz = N;
                r0 = N;
                r0const = e;
                r1 = e;
                r1const = N;
            }
            else
            {
                maz = e;
                r0 = e;
                r0const = N;
                r1 = N;
                r1const = e;
            }

            ri = maz;
            s0 = 1;
            s1 = 0;

            t0 = 0;
            t1 = 1;

            // pirma karta kai eina, buna 0, nes 89 eilutes kintamieji 0, 1

            // ri yra ieskant DBD isskaidomas skaicius (neskaitant pirmos eilutes)
            while (ri >= 1)
            {
                ri = r0 % r1;
                if (ri < 1)
                    break;

                qi = (r0 - ri) / r1; // qi yra liekana is praito skaiciaus 60 - liekana yra 56  tada 56 / 7 ir gausim 8

                t = s0 - qi * s1;
                s = t0 - qi * t1;

                s0 = s1;
                s1 = t;

                t0 = t1;
                t1 = s;

                r0 = r1; // pasibaigus iteracijai pasikeicia rėžiai t.y r0 ir r1, į kitos iteracijos skaičius. r0 pasikeicia į liekaną, ri 
                r1 = ri; // 

                //Console.WriteLine(s.ToString() + " " + t.ToString());
            }

            //Console.WriteLine(s.ToString());
            //Console.WriteLine(r0const.ToString() + " " + r1const.ToString());
            if ((s * r0const) + (t * r1const) == 1)
            {
                d = t;
                //Console.WriteLine(d);
                if (d < 0)
                {
                    d = d + N;
                    return d;
                }
                return d;
            }
            return 0;
        }

        /*public int IEA(int a, int b)
        {
            int x0 = 1, xn = 1, y0 = 0, yn = 0, x1 = 0, y1 = 1, f, r = a % b;

            while (r > 0)
            {
                f = a / b;
                xn = x0 - f * x1;
                yn = y0 - f * y1;

                x0 = x1;
                y0 = y1;
                x1 = xn;
                y1 = yn;
                a = b;
                b = r;
                r = a % b;
            }
            return Convert.ToInt32(x1);
        }*/
    }


}
