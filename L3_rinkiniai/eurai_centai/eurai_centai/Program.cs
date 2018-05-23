using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace eurai_centai
{
    class Pinigai
    {
        private int eurai, centai;
        public Pinigai (int eurai, int centai)
        {
            this.eurai = eurai;
            this.centai = centai;
        }
        public int ImtiEurus() { return eurai; }
        public int ImtiCentus() { return centai; }

    }
    class Program
    { 
        const int Cn=100;
        const string CFd = "..\\..\\Duom.txt";
        static void Main(string[] args)
        {
            Pinigai[] P = new Pinigai[Cn];
            int n;
            Skaityti(CFd, P, out n);
            Console.WriteLine("{0}", n);
            
            for (int i = 0; i < n; i++)
                Console.WriteLine(" {0}   {1}", P[i].ImtiEurus(), P[i].ImtiCentus());

            // Kiek is viso turi pinigu-------------
            int euruSuma = 0;
            double centuSuma=0;
            double bendraSuma=0;
            for (int i = 0; i < n; i++)
            {
                euruSuma = euruSuma + P[i].ImtiEurus();
                centuSuma = centuSuma + P[i].ImtiCentus();
                
            }
            centuSuma = centuSuma / 100;
            bendraSuma = euruSuma + centuSuma;
            Console.WriteLine("Visi turstai is viso turi {0,2:f2} pinigu",bendraSuma);

            // kiek vidutiniskai pinigu gaunasi asmeniui -------------------------
            double vid = bendraSuma / n;
            Console.WriteLine("Vidutiniskai vienam turistui tektu {0,2:f2} pinigu", vid);

            // kiek surinks pinigu i bendra grupes fonda -------------------------------------------
            double grupesislaidos=0;
            for (int i = 0; i < n; i ++)
            {
                grupesislaidos = (double)P[i].ImtiEurus() + ((double)P[i].ImtiCentus() /100) * 0.25;
            }
            Console.WriteLine("Grupes islaidom yra skirta {0,2:f2} pinigu", grupesislaidos);
        }
        //----------------------------------------------------------
        static void Skaityti(string fv, Pinigai[] P, out int n)
        {
            int eurai;
            int centai;
            using (StreamReader reader = new StreamReader(fv))
            {
                string line;
                line = reader.ReadLine();
                string[] parts;
                n = int.Parse(line);
                for(int i = 0; i < n; i ++)
                {
                    line = reader.ReadLine();
                    parts = line.Split(';');
                    eurai = int.Parse(parts[0]);
                    centai = int.Parse(parts[1]);
                    P[i] = new Pinigai(eurai, centai);
                }
            }
        }
        //--------------------------------------------------
    }
}
