using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace raides
{
    class RaidziuDazniai
    {
        private const int CMax = 256;
        private int[] Rn;
        public string eil { get; set; }

        public RaidziuDazniai()
        {
            eil = "";
            Rn = new int[CMax];
            for (int i = 0; i < CMax; i++)
                Rn[i] = 0;
        }
        public int Imti(char sim ) { return Rn[sim]; }

        //skaiciuoja raidziu pasikartojimus

        public void kiek()
        {
            for (int i = 0; i < eil.Length; i++)
            {
                if (('a' <= eil[i] && eil[i] <= 'z') ||
                   ('A' <= eil[i] && eil[i] <= 'z'))
                    Rn[eil[i]]++;
            }
        }
    }
      //---------------------------------------------------------------------
    class Program
    {
        const string CFr = "..\\..\\Rezultatai.txt";
        const string CFd = "..\\..\\U1.txt";
        static void Main(string[] args)
        {
            RaidziuDazniai eil = new raides.RaidziuDazniai();
            Dazniai(CFd, eil);
            Spausdinti(CFr, eil);     
        }
        //-------------------------------------------------------------------
        static void Dazniai(string fv, RaidziuDazniai eil)
        {
            using (StreamReader reader = new StreamReader(fv, Encoding.GetEncoding(1257)))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    eil.eil = line;
                    eil.kiek();
                }
            }
        }
        //---------------------------------------------------------------------
        static void Spausdinti(string fv, RaidziuDazniai eil)
        {
            using (var fr = File.CreateText(fv))
            {
                for (char sim = 'a'; sim <= 'z'; sim++)
                    fr.WriteLine("{0, 3:c} {1, 4:d} |{2, 3:c} {3, 4:d}",
                    sim, eil.Imti(sim),
                    Char.ToUpper(sim), eil.Imti(Char.ToUpper(sim)));
            }
        }
   //--------------------------------------------------------------------
      static void DazniausiaRaide(RaidziuDazniai eil)
        {  
            int max = 0;
            char maxraide='a';
            for (char sim = 'a'; sim <= 'z'; sim++)
            {
                if (eil.Imti(sim) > max)
                {
                    max = eil.Imti(sim);
                    maxraide = sim;
                }
            }
            Console.WriteLine("{0,3}  {1,4:c}", max, maxraide);
        }
  //------------------------------------------------------------------- 
    }
}
