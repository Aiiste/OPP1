using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace bandymas2__zodzio__arba__eilutes_salinimas
{
    class Program
    {
        const string CFd = "..//..//Duomenys.txt";
        const string CFr = "..//..//Rezultatai.txt";

        static void Main(string[] args)
        {
            string sk = " .,:;?!()-";
            int me = 0;
            string zod = "";
            RastiZTekste(CFd, sk, out zod, ref me);
            SalintiEilute(CFd, CFr, me);
 
        }

        // Grąžina eilutės (žodžio) e skirtingų balsių skaičių.

        static int SkirtBalsiuSkaicius(string e)
        {
            char[] sk = { ' ', ',', '.', ';', ':', '(', ')', '-' };
            char[] balses = { 'a', 'o', 'u', 'i', 'e' };
            string found = "";
            for (int i = 0; i < e.Length; i++)
            {
                if (balses.Contains(e[i]) && !found.Contains(e[i]))
                {
                    found += e[i];
                }
            }
            return found.Length;
        }

        // Eilutėje e randa ilgiausią žodį iš tų, kuriuose yra 3 skirtingos balsės.
        // Neradus grąžina tuščią eilutę.
        // sk – skyrikliai.

        static string RasiZodiEil(string e, string sk)
        {
            char[] skyr = sk.ToCharArray();
            string[] zodziai = e.Split(skyr, StringSplitOptions.RemoveEmptyEntries);
            string ilg = "";
            foreach (string zodis in zodziai)
            {
                if (SkirtBalsiuSkaicius(e) == 3 && zodis.Length > ilg.Length)
                {
                    ilg = zodis;
                }
            }
            return ilg;
        }

        // Faile fv randa ilgiausią žodį zod iš tų, kuriuose yra 3 skirtingos
        //balsės, ir jo eilutės numerį me.
        // sk – skyrikliai.

        static void RastiZTekste(string fv, string sk, out string zod, ref int me)
        {
            string Rzod = ""; zod = "";
            int i = 0;
            string eilute;
            char[] skyr = sk.ToCharArray();
            using (StreamReader reader = new StreamReader(fv))
            {
                while ((eilute = reader.ReadLine()) != null)

                {
                    Rzod = RasiZodiEil(eilute, sk);
                    i++;
                    if (Rzod.Length > 0 && Rzod.Length > zod.Length)
                    {
                        zod = Rzod;
                        me = i;
                        //int eil = eilute.IndexOf(zod);
                        //int eil2 = eilute.IndexOf(sk, eil);
                        //string eil3 = eilute.Substring(eil,eil2-eil);
                        //Console.WriteLine("{0}", eil3);
                    }
                }
            }
            
        }

        // Eilutė, kurios numeris n, salinama ; fvd – pradinio,
        // fvr – redaguoto failų vardai.

        static void SalintiEilute(string fvd, string fvr, int n)
        {
            string[] tekstas = File.ReadAllLines(fvd);

            using (var fr = File.CreateText(fvr))
            {
                for (int i = 0; i < tekstas.Length; i++)
                {
                    if (i != (n - 1))
                        fr.WriteLine(tekstas[i]);
                }
            }
        }

    }
}
