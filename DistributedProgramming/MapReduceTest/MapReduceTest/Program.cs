using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Mapper
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                string[] split = line.Split('\t');
                if (split.Length == 2 && split[0] == "_")
                {
                    Console.WriteLine(line);
                    continue;
                }
                string onlyText = Regex.Replace(line, @"\.|;|:|,|[0-9]|'|_", string.Empty);
                MatchCollection words = Regex.Matches(onlyText, @"[\w]+");
                foreach (Match word in words)
                    Console.WriteLine("_\t{0}", word.Value);
            }
        }
    }
}
