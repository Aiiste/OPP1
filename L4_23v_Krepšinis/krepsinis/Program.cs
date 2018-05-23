//Aistė Sudintaitė, IFZ-6/2
//P175B117
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//U4-23. Krepšininkai
//Tekstiniame faile pateikta informacija apie krepšinio komandos žaidėjus.Eilutėje yra tokia informacija
//apie krepšininkus: komanda, pavardė, vardas, ūgis, gimimo metai, žaidimo pozicija(puolėjas, gynėjas,
//centras), žaista rungtynių, įmesta taškų.
//Sudarykite kiekvienos pozicijos geriausiųjų žaidėjų sąrašus(taškai/rungtynių skaičius). 
//Iš sąrašų pašalinkite žaidėjus, kurie žaidė ne daugiau kaip vienas rungtynes.
//Nustatykite, kelių skirtingų komandų žaidėjai užima pirmąsias tris vietas geriausiųjų žaidėjų sąrašuose.
//Atspausdinkite tas komandas.

namespace krepsinis
{
    // Klasė žaidėjo duomenims saugoti
    class Zaidejas
    {
        private string komanda, // Žaidėjo komandos pavadinimas
                           pav, // Žaidėjo pavardė
                           var, // Žaidėjo vardas
                      pozicija; // Žaidėjo pozicija
        private int ugis,       // Žaidėjo ūgis cm
                   metai,       // Žaidėjo gimimo metai
                rungtynes,      // Sužaistų rungytnių skaicius
                  taskai;       // Per visas rungtynes surinkti taškai

        public Zaidejas(string komanda, string pav, string var,
                        string pozicija, int ugis, int metai, int rungtynes, int taskai)
        {
            this.komanda = komanda;
            this.pav = pav;
            this.var = var;
            this.pozicija = pozicija;
            this.ugis = ugis;
            this.metai = metai;
            this.rungtynes = rungtynes;
            this.taskai = taskai;
        }

        // Grąžina pozicija
        public string ImtiPozicija() { return pozicija; }
        // Grąžina taškų vidurkį
        public double ImtiVid()
        {
            if (rungtynes != 0 && taskai != 0)
            {
                double vid = 0;
                vid = (double)(taskai / rungtynes);
                return vid;
            }
            return 0;
        }
        // Grąžina sužaistu rungtynių skaičius
        public int ImtiSk() { return rungtynes; }
        // Grąžina komandos pavadinimą
        public string ImtiKom() {return komanda;}
        
        public override string ToString()
        {
            string eilute;
            eilute = string.Format("|{0,-16}|{1,-14}|{2,-10}| {3,-13}  |{4, -20:f1}|",
                var, pav, ugis, komanda, Math.Round((double)(taskai / rungtynes), 1));
            return eilute;
        }
  
    }

    /// <summary>
    /// Komandos konteineris
    /// </summary>
    class Komanda
    {
        const int CMax = 100;
        Zaidejas[] zaid;
        int n;

        public Komanda()
        {
            zaid = new Zaidejas[CMax];
            n = 0;
        }

        public int Imti() { return n; }
        public Zaidejas Imti(int i) { return zaid[i]; }
        public void Deti(Zaidejas ob) { zaid[n++] = ob;}
        public void Salinti(int i)
        {
            for (int y = i; y < n; y++)
            {
                zaid[y] = zaid[y + 1];
            }
            n--;
        }
    }
 //------------------------------------------------------------------------------
    class Program
    {
        const string CFd = "...\\...\\duom.txt"; // Duomenų failo pavadinimas
        const string CFr = "...\\...\\rez.txt"; // Rezultatų failo pvadinimas
        const int Cn = 100;
//-----------------------------------------------------------------------------------
        static void Main(string[] args)
        {
            Komanda K = new Komanda();
            Komanda P = new Komanda(); // Puolejų konteineris
            Komanda G = new Komanda(); // Gynejų konteineris
            Komanda C = new Komanda(); // Centrų konteineris

            int k = 0;
            // Masyvas geriausių komandų
            string[] komandos = new string[Cn];

            // Kreipinys į duomenų skaitymo metodą
            Skaityti(K, CFd);

            // Kreipiniai į atrinkimo metodus
            Atrinkti(K, P, "Puolejas");
            Atrinkti(K, G, "Gynejas");
            Atrinkti(K, C, "Centras");
           
            // Kreipiniai į šalinimo metodus
            Salinti(P);
            Salinti(G);
            Salinti(C);
           
            // Nustatyti , kurių komandų žaidėjas užima pirmąsias tris vietas
            Nustatyti(P, ref k, ref komandos);
            Nustatyti(G, ref k, ref komandos);
            Nustatyti(C, ref k, ref komandos);

            // Išvesti duomenis į failą
            if (File.Exists(CFr))
            {
                File.Delete(CFr);
            }
            Isvesti(CFr, K, "Krepšinio komandų žaidėjai");
            Isvesti(CFr, P, "\nGERIAUSIUJU PUOLEJU SARASAS");
            Isvesti(CFr, G, "\nGERIAUSIUJU GYNEJU SARASAS");
            Isvesti(CFr, C, "\nGERIAUSIUJU CENTRU SARASAS");
            Isvesti2(CFr, ref komandos, ref k);

        }

        /// <summary>
        /// Duomenų nuskaitymo iš failo metodas
        /// </summary>
        /// <param name="K"> Krepšininkų duomenys </param>
        /// <param name="fv"> Duomenys </param>
        
        static void Skaityti(Komanda K, string fv)
        {
            int n = File.ReadAllLines(fv).Count();

            using (StreamReader reader = new StreamReader(fv, Encoding.GetEncoding(1257)))
            {
                for (int i = 0; i < n; i++)
                {
                    string line = reader.ReadLine();
                    string[] parts = line.Split(';');
                    string komanda = parts[0];
                    string pav = parts[1];
                    string var = parts[2];
                    int ugis = int.Parse(parts[3]);
                    int metai = int.Parse(parts[4]);
                    string poz = parts[5];
                    int suzaista = int.Parse(parts[6]);
                    int taskai = int.Parse(parts[7]);

                    Zaidejas Z = new Zaidejas(komanda, pav, var, poz, ugis, metai, suzaista, taskai);

                    K.Deti(Z);
                }
            }
        }

        /// <summary>
        /// Geriausių žaidėjų atranka pagal pozicija
        /// </summary>
        /// <param name="K"> Krepšininkų duomenys </param>
        /// <param name="Poz"> Krepšininkų pagal pozicijas duomenys </param>
        /// <param name="pozz"> Pozicija skirta atrinkimui </param>
       
        static void Atrinkti(Komanda K, Komanda Poz, string pozz)
        {
            int k = -1; // Indeksas geriausio žaidėjo
            double max = 0; // Daugiausiai surinktų taskų per varžybas
            
            for (int i = 0; i < K.Imti(); i++)
            {
                if (pozz == K.Imti(i).ImtiPozicija())
                {
                    if (K.Imti(i).ImtiVid() > max)
                    {
                        max = K.Imti(i).ImtiVid();
                        k = i;
                    }
                }
            }

            // Įdedamas žaidėjas į pozicijos kontruktorių
            if (k >= 0)
            {
                Poz.Deti(K.Imti(k));
            }

            // Tikrinama ar yra žaidėjų turinčių tokį patį vidurkį
            for (int i = 0; i < K.Imti(); i++)
            {
                if (i != k && pozz == K.Imti(i).ImtiPozicija())
                {
                    if (K.Imti(i).ImtiVid() == max)
                    {
                        Poz.Deti(K.Imti(i));
                    }
                }
            }
        }

        /// <summary>
        ///  Šalinami žaidėjai kurie dalyvavo tik vienose rungtynėse
        /// </summary>
        /// <param name="Poz">Krepšininkų pagal pozicijas duomenys</param>
        
        static void Salinti(Komanda Poz)
        {
            int k = Poz.Imti();
            for (int i = 0; i < k; i++)
            {
                if (Poz.Imti(i).ImtiSk() <= 1)
                {
                    Poz.Salinti(i);
                    k--;
                    i--;
                }
            }
        }

        /// <summary>
        /// Tikrinti ar yra tų pacių komandų
        /// </summary>
        /// <param name="kom"> Krepšininkų komandos pavadinimas </param>
        /// <param name="k"></param>
        /// <param name="komandos"> Naujas komandų masyvas </param>
        /// <returns></returns>
        
        static int TikrintiArYra(string kom, int k, string[] komandos)
        {
            for (int i = 0; i < k; i++)

                if (kom == komandos[i])

                    return -1;

            return 1;
        }

        /// <summary>
        /// Kelių skirtingų komandų žaidėjai užima pirmasias vietas
        /// </summary>
        /// <param name="Poz"> Krepšininkų pagal pozicijas duomenys </param>
        /// <param name="k"> Komandų skaičius </param>
        /// <param name="komandos"> Naujas komandų masyvas </param>
      
        static void Nustatyti(Komanda Poz, ref int k, ref string[] komandos)
        {
            for (int i = 0; i < Poz.Imti(); i++)
            {
                if (i <= 3)
                {
                    int a = TikrintiArYra(Poz.Imti(i).ImtiKom(), k, komandos);
                    if (a > 0)
                    {
                        komandos[k] = Poz.Imti(i).ImtiKom();
                        k++;
                    }
                }
            }
        }
      /// <summary>
      /// Komandų pagal pozicijas išvedimas į failą
      /// </summary>
      /// <param name="fv"> Duomenų falias </param>
      /// <param name="Poz"> Krepšininkų pagal pozicijas duomenys </param>
      /// <param name="antraste"> Antraštė skirta pasakyti kokios pozicijos žaidėjas </param>
        
        static void Isvesti(string fv, Komanda Poz, string antraste)
        {

            string virsus = "-------------------------------------------------------------------------------\r\n"
              + "|    Vardas      |   Pavarde    |   Ugis   |   Komanda    |  Vid.Pelnyti taskai|\r\n"
              + "-------------------------------------------------------------------------------";

            using (StreamWriter fr = new StreamWriter(fv, true, Encoding.GetEncoding(1257)))
            {
                fr.WriteLine(antraste);
                fr.WriteLine(virsus);
                for (int i = 0; i < Poz.Imti(); i++)
                {
                    fr.WriteLine(Poz.Imti(i).ToString());
                }

                fr.WriteLine("-------------------------------------------------------------------------------");
            }

        }
        /// <summary>
        /// Geriausių komandų išvedimas į failą
        /// </summary>
        /// <param name="fv"> Duomenų failas </param>
        /// <param name="komandos"> Geriausios komandos </param>
        /// <param name="k"> Indeksas </param>
        
        static void Isvesti2(string fv, ref string[] komandos, ref int k)
        {
            using (StreamWriter fr = new StreamWriter(fv, true, Encoding.GetEncoding(1257)))
            {
                fr.WriteLine("\nPirmąsias tris vietas sarasuose uzima {0} skirtingos komandos:\n", k);
                for (int i = 0; i < k; i++)
                {
                    fr.WriteLine(komandos[i]);
                }
            }
        }
    }
}