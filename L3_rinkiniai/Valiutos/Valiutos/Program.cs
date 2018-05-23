using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Valiutos
{
    class Pinigai
    {
        private int valiuta, centai;
        private double kursas;
        public Pinigai(int valiuta, int centai, double kursas)
        {
            this.valiuta = valiuta;
            this.centai = centai;
            this.kursas = kursas;
        }
        public int ImtiValiuta() { return valiuta;}
        public int ImtiCentus() {return centai;}
        public double ImtiKursa() { return kursas; }

    }
    //------------------------------------------------
    class Program
    {   
       const int Cn= 100;
       const string CFd1 = "..\\..\\A.txt";
       const string CFd2 = "..\\..\\B.txt";
        static void Main(string[] args)
        {
            Pinigai[] P1 = new Pinigai[Cn];
            int n1;
            Pinigai[] P2 = new Pinigai[Cn];
            int n2;
            Skaityti(CFd1, P1, out n1);
            Skaityti(CFd2, P2, out n2);
            Console.WriteLine("---------------------------");
            Console.WriteLine("Barboros pinigai: ");
            Console.WriteLine("---------------------------");
            Console.WriteLine("Valiuta\tCentai\t Kursas ");
            for (int i = 0; i < n1; i++)
            {
                Console.WriteLine("{0}\t| {1}\t | {2,2:f2}", P1[i].ImtiValiuta(), P1[i].ImtiCentus(), P1[i].ImtiKursa());
            }
            Console.WriteLine("---------------------------");
            Console.WriteLine("Anrupo pinigai: ");
            Console.WriteLine("---------------------------");
            for (int i = 0; i < n2; i++)
            {
                Console.WriteLine("{0}\t| {1}\t | {2,2:f2}\n", P2[i].ImtiValiuta(), P2[i].ImtiCentus(), P2[i].ImtiKursa());
            }
            //--------------- konvertavimas -----------------------
            Console.WriteLine("Rezultatai: ");
            double viso1=0;
            for (int i=0;i<n1;i++)
            {
                viso1 = viso1 + ((P1[i].ImtiValiuta() + (P1[i].ImtiCentus() / 100)) * P1[i].ImtiKursa());
            }
            Console.WriteLine("Barbora turi {0} eurų",viso1);
            double viso2 = 0;
            for (int i = 0; i < n2; i++)
            {
                viso2 = viso2 + ((P2[i].ImtiValiuta() + (P2[i].ImtiCentus() / 100)) * P2[i].ImtiKursa());
            }
            Console.WriteLine("Anrupas turi {0} eurų", viso2);

            double viso3 = viso1 + viso2;
            Console.WriteLine("Abu kartu turi {0} eurų", viso3);

        }
        //-----------------------------------------
        static void Skaityti(string fv, Pinigai[] P, out int n)
        {
            using (StreamReader reader = new StreamReader(fv))
            {
                string line;
                int i = 0;
                while ((line = reader.ReadLine()) != null && (i < Cn))
                {
                    string[] parts = line.Split(';');
                    int valiuta= int.Parse(parts[0]);
                    int centai = int.Parse(parts[1]);
                   double kursas = double.Parse(parts[2]);
                    P[i++] = new Pinigai(valiuta, centai, kursas);
                }
                n = i;

            }
        }
    }
        //-------------------------------------------
}
