using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC22
{
    public static class Extensions
    {
        public static List<int> GetInts(this string obj)
        {
            return obj.Split("\r\n").Select(x => int.Parse(x)).ToList();
        }
        public static List<string> GetStrings(this string str) => str.Split("\r\n").ToList();
    }
}
