using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reducer
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> words = new List<string>();

            string line;
            List<string> pairs = new List<string>();
            
            while ((line = Console.ReadLine()) != null)
            {
                string[] sArr = line.Split('\t');
                string word = sArr[1];
                pairs.Add(word);
                if (pairs.Count == 2)
                {
                    string text1 = pairs[0];
                    string text2 = pairs[1];
                    string result = MergeTwo(text1.Split(' '), text2.Split(' '));
                    words.Add(result);
                    pairs.Clear();
                }
            }

            if (pairs.Count > 0)
                words.Add(pairs[0]);

            foreach (string word in words)
                Console.WriteLine("_\t{0}", word);
        }

        static string MergeTwo(string[] array1, string[] array2)
        {
            List<string> result = new List<string>();
            int i = 0, j = 0;
            while (i < array1.Length && j < array2.Length)
            {
                if (string.CompareOrdinal(array1[i], array2[j]) > 0)
                    result.Add(array1[i++]);
                else
                    result.Add(array2[j++]);
            }
            if (i < array1.Length)
                result.AddRange(array1.Skip(i));
            if (j < array2.Length)
                result.AddRange(array2.Skip(j));
            return string.Join(" ", result);
        }
    }
}
