using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace teksto_eiluciu_salinimas
{
    class Program
    {
        const string CFd = "..\\..\\duom.txt";
        const string CFr = "..\\..\\rez.txt";
        const string CFa = "..\\..\\analize.txt";

        static void Main(string[] args)
        {
            Apdoroti(CFd, CFr, CFa);
        }
        //------------------------------------------------------------
        static void Apdoroti(string fv, string fvr, string fa)
        {
            string[] lines = File.ReadAllLines(fv, Encoding.GetEncoding(1257));
            using (var fr = File.CreateText(fvr))
            {
                using (var far = File.CreateText(fa))
                {
                    foreach (string line in lines)
                    {
                        if (line.Length > 0)
                        {
                            string nauja = line;
                            if (BeKomentaru(line, out nauja))
                                far.WriteLine(line);
                            if (nauja.Length > 0)
                                fr.WriteLine(nauja);
                        }
                        else
                            fr.WriteLine(line);
                    }
                }
            }
        }
        //--------------------------------------------------------
        static bool BeKomentaru(string line, out string nauja)
        {
            nauja = line;
            for(int i = 0; i < line.Length - 1; i++)
                if (line[i] == '/' && line[i + 1] == '/')
                {
                    nauja = line.Remove(i);
                    return true;
                }
            return false;
        }
    }
}
