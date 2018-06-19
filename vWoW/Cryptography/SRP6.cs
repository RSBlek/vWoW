using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace vWoW.Cryptography
{
    public class SRP6
    {
        public BigInteger B { get; private set; }
        public BigInteger A { get; private set; }
        public BigInteger g { get; private set; }
        public BigInteger N { get; private set; }
        public BigInteger M { get; private set; }
        public BigInteger Salt { get; private set; }
        public BigInteger S { get; private set; }
        public BigInteger a { get; private set; }
        public byte[] I { get; private set; }
        public BigInteger k = new BigInteger(3);
        private string username;
        private string password;

        private Random random = new Random();

        public SRP6(byte[] B, byte[] g, byte[] N, byte[] Salt, string username, string password)
        {
            this.B = new BigInteger(B);
            this.g = new BigInteger(g);
            this.N = new BigInteger(N);
            this.Salt = new BigInteger(Salt);
            this.username = username;
            this.password = password;
        }

        public void GenerateAll()
        {
            GenerateS();
            GenerateM();
        }

        public BigInteger Generatea()
        {
            byte[] bytes = new byte[19];
            random.NextBytes(bytes);
            this.a = new BigInteger(bytes);
            return a;
        }

        public BigInteger GenerateS()
        {
            GenerateLogonHash();
            do
            {
                Generatea();
                GenerateA();
                BigInteger x = Getx();
                BigInteger u = Getu();
                S = GetClientS(x,u);
            } while (S < 0);

            return S;
        }

        public BigInteger GenerateA()
        {
            this.A = g.modPow(a, N);
            return A;
        }

        public byte[] GenerateLogonHash()
        {
            Sha1Hash sha1Hash = new Sha1Hash();
            string input = $"{username.ToUpper()}:{password.ToUpper()}";
            sha1Hash.Update(input);
            this.I = sha1Hash.Final();
            return this.I;
        }

        public BigInteger Getx()
        {
            Sha1Hash sha1Hash = new Sha1Hash();
            sha1Hash.Update(Salt);
            sha1Hash.Update(I);
            return new BigInteger(sha1Hash.Final());
        }

        public BigInteger Getu()
        {
            Sha1Hash sha1Hash = new Sha1Hash();
            sha1Hash.Update(A);
            return new BigInteger(sha1Hash.Final(B));
        }

        private BigInteger GetClientS(BigInteger x, BigInteger u)
        {
            return S = (B - (k * g.modPow(x, N))).modPow(a + (u * x), N);
        }

        public BigInteger GenerateM()
        {
            Sha1Hash sha;

            sha = new Sha1Hash();
            byte[] hash = sha.Final(N);

            sha = new Sha1Hash();
            byte[] ghash = sha.Final(g);

            for (int i = 0; i < 20; ++i)
                hash[i] ^= ghash[i];

            BigInteger t3 = new BigInteger(hash);

            sha = new Sha1Hash();
            sha.Update(username.ToUpper());
            BigInteger t4 = new BigInteger(sha.Final());

            sha = new Sha1Hash();
            sha.Update(t3);
            sha.Update(t4);
            sha.Update(Salt);
            sha.Update(A);
            sha.Update(B);
            this.M = new BigInteger(sha.Final(ShaInterleave()));
            return M;
        }

        public byte[] ShaInterleave()
        {
            byte[] t = S;
            int HalfSize = t.Length / 2; // Untested.  I previously hard coded this as 16
            byte[] t1 = new byte[HalfSize];

            for (int i = 0; i < HalfSize; i++)
                t1[i] = t[i * 2];

            Sha1Hash sha = new Sha1Hash();
            byte[] t1hash = sha.Final(t1);

            byte[] vK = new byte[40];
            for (int i = 0; i < 20; i++)
                vK[i * 2] = t1hash[i];

            for (int i = 0; i < HalfSize; i++)
                t1[i] = t[i * 2 + 1];

            sha = new Sha1Hash();
            t1hash = sha.Final(t1);

            for (int i = 0; i < 20; i++)
                vK[i * 2 + 1] = t1hash[i];

            return vK;
        }

    }
}
