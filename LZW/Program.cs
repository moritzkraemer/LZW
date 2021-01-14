using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LZW
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "TOBEORNOTTOBEORTOBEORNOT";
            Console.WriteLine(s);
            int[] compressed = Encode(s);
            Console.WriteLine(string.Join(", ", compressed));
            Console.WriteLine(Decode(compressed));
            
        }

        static int[] Encode(string input)
        {
            List<int> output = new List<int>();
            List<string> dictionary = new List<string>();
            for (int i = 0; i < 256; i++)
                dictionary.Add(((char)i).ToString());

            string w = string.Empty;
            foreach(char c in input)
            {
                string wc = w + c;
                if (dictionary.Contains(wc))
                {
                    w = wc;
                }
                else
                {
                    output.Add(dictionary.IndexOf(w));
                    dictionary.Add(wc);
                    w = c.ToString();
                }
            }

            if (!string.IsNullOrEmpty(w))
            {
                output.Add(dictionary.IndexOf(w));
            }

            return output.ToArray();
        }

        static string Decode(int[] input)
        {
            List<int> compressed = input.ToList();
            List<string> dictionary = new List<string>();
            for (int i = 0; i < 256; i++)
                dictionary.Add(((char)i).ToString());

            string w = dictionary[compressed[0]];
            compressed.RemoveAt(0);
            StringBuilder decompressed = new StringBuilder(w);

            foreach(int k in compressed)
            {
                string entry = null;
                if(dictionary.Count > k)
                {
                    entry = dictionary[k];
                }
                else if (k == dictionary.Count)
                {
                    entry = w + w[0];
                }

                decompressed.Append(entry);

                dictionary.Add(w + entry[0]);

                w = entry;
            }

            return decompressed.ToString();
        }


    }
}
