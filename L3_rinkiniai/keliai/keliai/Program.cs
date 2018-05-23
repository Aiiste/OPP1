using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace keliai
{
    class Kelias
    {
        private string pavadinimas;
        private double ilgis;
        private int leistgreitis;
        
        public Kelias(string pavadinimas, double ilgis, int leistgreitis)
        {
            this.pavadinimas = pavadinimas;
            this.ilgis = ilgis;
            this.leistgreitis = leistgreitis;
        }
        public string ImtiPav() { return pavadinimas; }
        public double ImtiIlgi() { return ilgis; }
        public int ImtiLeistgreiti() { return leistgreitis; }
    }
    class Program
    {
        const int Cn = 100;
        const string CFd = "..\\..\\duom.txt";
        const string CFrez = "..\\..\\rez.txt";
        static void Main(string[] args)
        {
            Kelias[] K = new Kelias[Cn];
            int n;
            Skaityti(CFd, K, out n);
            //Console.WriteLine("{0}", n);
            //for (int i = 0; i < n; i++)
            //{
            //    Console.WriteLine("{0,5:f2}", K[i].ImtiIlgi());
            //}
            if (File.Exists(CFd))
                File.Delete(CFrez);            SpausdintiDuomenis(CFrez, K, n);            
        }
        //--------------------------------------------
        static void Skaityti(string fv, Kelias[] K, out int n)
        {
            using (StreamReader reader = new StreamReader(fv))
            {
                string line;
                int i = 0;
                while ((line = reader.ReadLine()) != null && (i < Cn))
                {
                    string[] parts = line.Split(';');
                    string pavadinimas = parts[0];
                    double ilgis = double.Parse(parts[1]);
                    int greitis = int.Parse(parts[2]);
                    K[i++] = new Kelias(pavadinimas, ilgis, greitis);
                }
                n = i;

            }
        }
        //----------------------------------------------------
        static void SpausdintiDuomenis (string fv, Kelias []K, int n)
        {
            const string keliai = "LIETUVOS KELIAI";
            const string virsus =
                "|----------------------------|------------------|-----------|\r\n"
              + "|          Kelio             |      Ilgis       |  Greicio  | \r\n"
              + "|       pavadinimas          |                  |   riba    |  \r\n"
              + "|----------------------------|------------------|-----------|";
            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine(keliai);
                fr.WriteLine(virsus);
                Kelias tarp;
                for(int i=0; i < n; i ++)
                {
                    tarp = K[i];
                    fr.WriteLine("| {0,-15}   \t     | {1,8:f2} \t|{2, 6}\t    |", tarp.ImtiPav(), tarp.ImtiIlgi(),tarp.ImtiLeistgreiti());
                }
                fr.WriteLine("|----------------------------|------------------|-----------|");
            }
        }
    }
}

