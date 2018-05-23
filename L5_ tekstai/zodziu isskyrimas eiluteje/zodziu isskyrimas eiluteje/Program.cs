using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace zodziu_isskyrimas_eiluteje
{
    class Program
    {
        const string CFd = "..\\..\\duom.txt";
        static void Main(string[] args)
        {
            char[] skyrikliai = {' ','.',',','!','?',':',';','(',')','\t' };
            Console.WriteLine("Sutampanciu zodziu {0,3:d}", Apdoroti(CFd,skyrikliai));
        }
        static int Apdoroti(string fv, char[] skyrikliai)
        {
            string[] lines = File.ReadAllLines(fv, Encoding.GetEncoding(1257));
            int sutampa = 0;
            foreach (string line in lines)
                if (line.Length > 0)
                    sutampa += Zodziai(line, skyrikliai);
            return sutampa;
        }
        //-----------------------------------------------------
        static int Zodziai(string eilute, char[] skyrikliai)
        {
            string[] parts = eilute.Split(skyrikliai, StringSplitOptions.RemoveEmptyEntries);
            int sutampa = 0;
            foreach (string zodis in parts)
                if (zodis[0] == zodis[zodis.Length - 1])
                    sutampa++;
            return sutampa;
        }
        //-----------------------------------------------------
    }
}
