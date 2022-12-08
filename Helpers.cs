namespace AOC22
{
    public static class Helpers
    {
        public static Pair GetPair(string pair)
        {
            var arr = pair.Split('-');
            return new Pair
            {
                l = int.Parse(arr[0]),
                h = int.Parse(arr[1])
            };
        }
    }


    public class Pair
    {
        public int l;
        public int h;
    }
}
