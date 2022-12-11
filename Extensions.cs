using System.Text.RegularExpressions;

namespace AOC22
{
    public static class Extensions
    {
        public static List<string> GetStrings(this string str) => str.Split("\r\n").ToList();

        public static string[] GetColumnsFromBottomUp(this string str)
        {
            var list = str.Split("\r\n").ToList();
            list.Reverse();
            var arrCount = (list[0].Length + 1) / 4;
            var res = new string[arrCount];
            res.Initialize();

            foreach(var item in list)
            {
                for (var i = 0; i < arrCount; i++)
                {
                    var index = i * 4 + 1;
                    if (item[index] >= 65 && item[index] <= 90) res[i] += item[index];
                }
            }
            return res;
        }

        public static List<int[]> GetArrayOfInts(this List<string> strs)
        {
            var res = new List<int[]>();
            foreach(var item in strs)
            {
                if (string.IsNullOrEmpty(item)) continue;
                var arr = new int[3];//move 2 from 4 to 6
                var str = Regex.Replace(item, @"\D+", " ");
                res.Add(str.Trim().Split(' ').Select(int.Parse).ToArray());
            }
            return res;
        }

        public static string Reverse(this string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static void Assign(this Pair a, Pair b)
        {
            a.x = b.x;
            a.y = b.y;
        }
    }
}