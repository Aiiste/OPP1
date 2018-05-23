//Aiste Sudintaite, IFZ-6/2
//P175B177

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

//U5-23. „Didesnės“ raidės
//Tekstiniame faile pateiktas tekstas. Žodžiai iš eilutės į kitą eilutę nekeliami. Skyrikliai žinomi.. Kokių
//žodžių daugiausia: tų, kurių pirmoji raidė “didesnė” už paskutiniąją ar tų, kurių pirmoji raidė
//“mažesnė” už paskutiniąją? Pašalinti žodžius, kurių pirmosios dvi raidės sutampa su dviem
//paskutinėmis raidėmis. 

namespace U5_Didesnes_raides
{
    class Program
    {
       const string CFd = "..\\..\\Duomenys.txt";
        const string CFr = "..\\..\\Rezultatai.txt";

        static void Main(string[] args)

        {
            char[] skyrikliai = { ' ', '.', ',', '!', '?', ':', ';', '(', ')', '\t'};
            if (File.Exists(CFr))
                File.Delete(CFr);
            Console.WriteLine("{0}",Apdoroti(CFd,skyrikliai));
            Apdoroti(CFd, skyrikliai);
            ApdorotiBeZ(CFd, CFr, skyrikliai);
        }

        /*Pirma dalis*/

        /// <summary>
        /// Nuskaitomas failas, lyginama pirmų raidžių dydžiai
        /// </summary>
        /// <param name="fv"> Duomenų failas </param>
        /// <param name="skyrikliai"> Teksto skyrikliai </param>
        /// <returns></returns>
        
        static string Apdoroti(string fv, char[] skyrikliai)
        {
            string didesniu = "Daugiausia yra žodžių kuri pirmoji raidė yra didesnė už paskutiniąją";
            string mazesniu = "Daugiausia yra žodžių kuri pirmoji raidė yra mažesnė už paskutiniąją";
            string[] lines = File.ReadAllLines(fv, Encoding.GetEncoding(1257));
            int didesne = 0;
            int mazesne = 0;
            
                foreach (string line in lines)
                    if (line.Length > 0)
                    {
                        didesne += DidesneRaide(line, skyrikliai);
                        mazesne += MazesneRaide(line, skyrikliai);
                    }

                if (didesne > mazesne) return didesniu;
                else return mazesniu;
            
        }

        /// <summary>
        /// Ieško kiek žodžiųu pirmoji raidė yra didesnė
        /// </summary>
        /// <param name="eilute"> Eilutė su žodžiais </param>
        /// <param name="skyrikliai"> Teksto skyrikliai </param>
        /// <returns></returns>
        
        static int DidesneRaide(string eilute, char[] skyrikliai)
        {
            string[] parts = eilute.Split(skyrikliai, StringSplitOptions.RemoveEmptyEntries);
            int didesne = 0;
            foreach (string zodis in parts)
                if (zodis[0] > zodis[zodis.Length - 1])
                    didesne++;
            return didesne;
        }

        /// <summary>
        /// Ieško kiek žodžių pirmoji raidė yra mažesnė
        /// </summary>
        /// <param name="eilute"> Eilutė su žodžiais </param>
        /// <param name="skyrikliai"> Teksto skyrikliai </param>
        /// <returns></returns>
         
        static int MazesneRaide(string eilute, char[] skyrikliai)
        {
            string[] parts = eilute.Split(skyrikliai, StringSplitOptions.RemoveEmptyEntries);
            int mazesne = 0;
            foreach (string zodis in parts)
                if (zodis[0] < zodis[zodis.Length - 1])
                    mazesne++;
            return mazesne;
        }

        /*Antra dalis*/

        /// <summary>
        /// Žodžių šalinimas
        /// </summary>
        /// <param name="line"> Eilutė su žodžiais </param>
        /// <param name="skyrikliai"> Teksto skyrikliai </param>
        /// <param name="nauja"> Eilutė be žodžių </param>
        /// <returns></returns>

        static bool BeZodziu(string line, char[] skyrikliai, out StringBuilder nauja)
        {
            int poz;
            string[] parts = line.Split(skyrikliai, StringSplitOptions.RemoveEmptyEntries);
            nauja = new StringBuilder();
           
            poz = 0;
            foreach (string zodis in parts)
            {
                string z1 = zodis.Substring(0, 2); // Žodžio pirmos dvi raidės
                string z2 = zodis.Substring((zodis.Length - 2), 2); // Žodžio paskutinės dvi raidės

                if (z1 != z2)
                {
                    nauja.Append(zodis).Append(" ");
                    poz = 1;
                }

            }
            if (poz == 0)
                return false;
            else
                return true;

        }

        /// <summary>
        /// Analizuoja ir įrašo į rezultatų failą
        /// </summary>
        /// <param name="fd"> Duomenų failas </param>
        /// <param name="fr"> Rezultatų failas </param>
        /// <param name="skyrikliai"> Teksto skyrikliai </param>
        
        static void ApdorotiBeZ(string fd, string fr, char[] skyrikliai)
        {
            string[] lines = File.ReadAllLines(fd, Encoding.GetEncoding(1257));
            using (var far = File.AppendText(fr))
            {
                foreach (string line in lines)
                {
                    StringBuilder nauja = new StringBuilder();
                    BeZodziu(line, skyrikliai, out nauja);
                  //  far.WriteLine("{0}", Apdoroti(CFd, skyrikliai));
                    far.WriteLine(nauja);  
                }
            }
        }
        //-------------------------------------------------------------

    }
}
