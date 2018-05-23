// Aistė Sudintaitė , IFZ-6/2
// P175B117

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

//U6–23. Darbo birža
//Pirmoje failo eilutėje nurodytas miestų skaičius ir mėnesių skaičius.Tolesnėse failo eilutėse nurodyta
//informacija apie miestus: miesto pavadinimas, gyventojų skaičius, jaunimo nuo 19 iki 25 metų
//skaičius.Žemiau pateikta informacija apie jaunimo nuo 19 iki 25 metų nedarbą miestuose: miestai
//(eilutės), kiek bedarbių registruota kiekvieną mėnesį(stulpeliai). 

//Nustatykite, kurį mėnesį buvo didžiausias nedarbas jaunimo tarpe.
//Suraskite, kurį mėnesį ir kuriame mieste santykinis nedarbo lygis buvo mažiausias.//Surikiuokite miestus pagal jaunimo skaičių ir gyventojų skaičių.
namespace L6_DarboBirža
{
    class Miestai
    {
        string pavadinimas;
        int gyvskaicius;
        int jaunimoskaicius;

        public Miestai()
        {
            pavadinimas = "";
            gyvskaicius = 0;
            jaunimoskaicius = 0;
        }
        public Miestai(string pav, int gyvsk, int jgyvsk)
        {
            pavadinimas = pav;
            gyvskaicius = gyvsk;
            jaunimoskaicius = jgyvsk;
        }

        public override string ToString()
        {
            string eilute;
            eilute = string.Format("{0,-10} {1,5} {2,5}", pavadinimas, gyvskaicius, jaunimoskaicius);
            return eilute;
        }

        public string ImtiPav() { return pavadinimas; }//grąžina miesto pavadinimą
        public int ImtiGyv() { return gyvskaicius; }//grąžina miesto gyventojų skaičių
        public int ImtiJaun() { return jaunimoskaicius; }//grąžina miesto jaunimo nuo 19 iki 25 metų skaičių
    }

    //---------------------------------------------------------------

    class Matrica
    {
        const int CMaxEil = 10;//miestų maksimumas
        const int CMaxST = 12;//mėnesių maksimumas
        private int[,] A;//duomenų matrica
        public Miestai[] miestai;
        public int n { get; set; }//eilučių skaičius
        public int m { get; set; }//stulpelių skaičius
   
        public Matrica()
        {
            n = 0;
            m = 0;
            miestai = new Miestai[CMaxEil];
            A = new int[CMaxEil, CMaxST];
        }
        public int Imti() { return n; }
        public Miestai Imti(int i) { return miestai[i];}
        public void Prideti(Miestai miest) { miestai[n++] = miest; }
        public void Deti(int i, int j, int pirk) {A[i, j] = pirk;}
        public int ImtiReiksme(int i, int j) {return A[i, j];}
    }

    //---------------------------------------------------------------

    class Program
    {
        const string CFd = "..//..//Duomenys.txt";
        const string CFr = "..//..//Rezultatai.txt";
        static void Main(string[] args)

        {
            int mėnnr = 0, inr = 0, jnr = 0;

            Matrica Birza = new Matrica();
            Miestai[] M = new Miestai[3];

            Skaityti(CFd, ref Birza, ref M);
            DidziausiasNedarbas(Birza, out mėnnr);
            Maziausias(M,Birza,out inr,out jnr);
            RikiuotiJ(M,Birza);
            Rikiuoti(M,Birza);
            Spausdinti(CFr, Birza, M);
        }

        /// <summary>
        /// Nuskaito duomenis iš failo
        /// </summary>
        /// <param name="fv"> duomenų failas </param>
        /// <param name="Birza"> dvimatis konteineris </param>
        /// <param name="M"> miestų masyvas </param>

        static void Skaityti(string fv, ref Matrica Birza, ref Miestai[] M)
        {
            int nn, mm, bed, gyvsk, jgyvsk;
            string pav;
            string line;
            using (StreamReader skaityk = new StreamReader(fv, Encoding.GetEncoding(1257)))
            {
                line = skaityk.ReadLine();
                string[] dalys = line.Split(' ');
                nn = int.Parse(dalys[0]);
                mm = int.Parse(dalys[1]);
                Birza.n = nn;
                Birza.m = mm;
                for (int i = 0; i < nn; i++)
                {
                    line = skaityk.ReadLine();
                    dalys = line.Split(' ');
                    pav = dalys[0];
                    gyvsk = int.Parse(dalys[1]);
                    jgyvsk = int.Parse(dalys[2]);
                    M[i] = new Miestai(pav, gyvsk, jgyvsk);
                }
                for (int i = 0; i < nn; i++)
                {
                    line = skaityk.ReadLine();
                    dalys = line.Split(' ');
                    for (int j = 0; j < mm; j++)
                    {
                        bed = int.Parse(dalys[j]);
                        Birza.Deti(i, j, bed);
                    }
                }
            }
        }

        /// <summary>
        /// suranda kurį mėnesį buvo didžiausias nedarbas jaunimo tarpe
        /// </summary>
        /// <param name="Birza">dvimatis masyvas</param>
        /// <param name="mėnnr">mėnesio numeris</param>
     
        static void DidziausiasNedarbas(Matrica Birza, out int mėnnr)
        {
            int max = 0;
            mėnnr = 0;
            for (int i = 0; i < Birza.m; i++)
            {
                int sum = 0;
                for (int j = 0; j < Birza.n; j++)
                    sum = sum + Birza.ImtiReiksme(i, j);
                if (sum > max)
                {
                    max = sum;
                    mėnnr = i;
                }
            }
        }

        /// <summary>
        /// randa miestą ir mėnesį, kai santykinis nedarbo lygis buvo mažiausias
        /// </summary>
        /// <param name="M">miestų masyvas</param>
        /// <param name="Birza">dvimatis masyvas</param>
        /// <param name="inr">eilutės indeksas</param>
        /// <param name="jnr">stulpelio indeksas</param>
       
        static void Maziausias(Miestai[] M, Matrica Birza, out int inr, out int jnr)
        {
            inr = 0;
            jnr = 0;
            double min = 1000;
            for (int i = 0; i < Birza.n; i++)
            {
                for (int j = 0; j < Birza.m; j++)
                {
                    int santyk = Birza.ImtiReiksme(i, j) * 1000 / M[i].ImtiGyv();

                    if (min > santyk)
                    {
                        min = santyk;
                        inr = i;
                        jnr = j;
                    }
                }
            }
        }

        /// <summary>
        /// Surikiuoja miestų sąrašą pagal jaunimo kiekį mažėjimo tvarka
        /// </summary>
        /// <param name="M"> miestų masyvas </param>
        /// <param name="Birza"> dvimatis masyvas </param>
        
        static void RikiuotiJ(Miestai[] M, Matrica Birza)
        {
            Miestai laik;
            int indeksas;
            for (int i = 0; i < Birza.n - 1; i++)
            {
                indeksas = i;
                for (int j = i + 1; j < Birza.n; j++)
                {
                    if (M[indeksas].ImtiJaun() > M[j].ImtiJaun())
                    {
                        indeksas = j;
                    }
                    laik = M[indeksas];
                    M[indeksas] = M[j];
                    M[j] = laik;
                }
            }
        }

        /// <summary>
        /// Surikiuoja miestų sąrašą pagal gyventojų kiekį mažėjimo tvarka
        /// </summary>
        /// <param name="M">miestų masyvas</param>
        /// <param name="Birza">dvimatis masyvas</param>

        static void Rikiuoti(Miestai[] M, Matrica Birza)
        {
            RikiuotiJ(M, Birza);
            Miestai laik;
            int indeksas;
            for (int i = 0; i < Birza.n - 1; i++)
            {
                indeksas = i;
                for (int j = i + 1; j < Birza.n; j++)
                {
                    if (M[indeksas].ImtiGyv() > M[j].ImtiGyv())
                    {
                        indeksas = j;
                    }
                    laik = M[indeksas];
                    M[indeksas] = M[j];
                    M[j] = laik;
                }
            }
        }

        /// <summary>
        /// Spausdina duomenis į rezultatų failą
        /// </summary>
        /// <param name="fv">rezultatų failas</param>
        /// <param name="Birza">dvimatis masyvas</param>
        /// <param name="M">miestų masyvas</param>
    
        static void Spausdinti(string fv, Matrica Birza, Miestai[] M)
        {
            using (var fr = File.CreateText(fv))
            {
                fr.WriteLine("------------------------------------------------------------");
                fr.WriteLine("Pradiniai duomenys");
                fr.WriteLine("------------------------------------------------------------");
                fr.WriteLine("Miestų sąrašas:");
                fr.WriteLine();
                for (int i = 0; i < Birza.Imti(); i++)
                {
                    fr.WriteLine(M[i].ToString());
                }
                fr.WriteLine();
                fr.WriteLine("Jaunimo nuo 19 iki 25 metų nedarbas miestuose:");
                fr.WriteLine();
                for (int i = 0; i < Birza.n; i++)
                {
                    for (int j = 0; j < Birza.m; j++)
                    {
                        fr.Write("{0,2:d}", Birza.ImtiReiksme(i, j));
                    }
                    fr.WriteLine();
                }

                fr.WriteLine("------------------------------------------------------------");
                fr.WriteLine("Rezultatai");
                fr.WriteLine("------------------------------------------------------------");
                int mėnnr;
                DidziausiasNedarbas(Birza, out mėnnr);
                fr.WriteLine("Didžiausias nedarbas jaunimo tarpe buvo {0} mėnesį.", mėnnr + 1);
                fr.WriteLine();
                int inr;
                int jnr;
                Maziausias(M, Birza, out inr, out jnr);
                fr.WriteLine("Santykinis nedarbo lygis mažiausias: miestas {0}, mėnuo {1}", M[inr].ImtiPav(), jnr + 1);
                fr.WriteLine("------------------------------------------------------------");
                fr.WriteLine("Rikiuotas miestų sąrašas pagal gyventojų skaičių (mažėjimo tvarka)");
                fr.WriteLine("------------------------------------------------------------");
                Rikiuoti(M, Birza);
                for (int i = 0; i < Birza.n; i++)
                {
                    fr.WriteLine(M[i].ToString());
                }
                fr.WriteLine("------------------------------------------------------------");
            }
        }
    }
}