namespace AOC22
{
    public static class Helpers
    {
        public enum Side { TOP, BOTTOM, LEFT, RIGHT };

        public static Pair GetPair(string pair)
        {
            var arr = pair.Split('-');
            return new Pair(int.Parse(arr[0]), int.Parse(arr[1]));
        }

        public static List<Item> GetItems(string x)
        {
            var root = new Item("root", 0, null);
            var current = root;
            var allItems = new List<Item> { root };

            foreach (var line in x.GetStrings())
            {
                if (string.IsNullOrEmpty(line)) continue;
                if (line[0] == '$')
                {
                    // Command
                    switch (line[2])
                    {
                        case 'c':
                            current = line[5] switch
                            {
                                '/' => root,
                                '.' => current.Parent ?? current,
                                _ => current.Children[line[5..]]
                            };
                            break;
                        case 'l':
                            continue;
                    }
                }
                else
                {
                    // List of files and directories
                    var l = line.Split(' ');
                    var size = l[0] == "dir" ? 0 : int.Parse(l[0]);
                    var item = new Item(l[1], size, current);
                    current.Children.Add(l[1], item);
                    allItems.Add(item);
                }
            }
            root.SetSize();
            return allItems;
        }

        public static bool IsAnyHigher(string[] els, int rowI, int colI, Side fromSide)
        {
            switch (fromSide)
            {
                case Side.TOP:
                    for (int i = 0; i < rowI; i++) 
                        if (els[i][colI] >= els[rowI][colI]) return true;
                    break;
                case Side.BOTTOM:
                    for (int i = els.Count() - 1; i > rowI; i--) 
                        if (els[i][colI] >= els[rowI][colI]) return true;
                    break;
                case Side.LEFT:
                    for (int i = 0; i < colI; i++)
                        if (els[rowI][i] >= els[rowI][colI]) return true;
                    break;
                case Side.RIGHT:
                    for (int i = els[rowI].Length - 1; i > colI; i--)
                        if (els[rowI][i] >= els[rowI][colI]) return true;
                    break;
            }
            return false;
        }

        public static int GetTreeScore(string[] els, int rowI, int colI, Side fromSide)
        {
            int score = 0;

            switch (fromSide)
            {
                case Side.TOP:
                    for (int i = rowI - 1; i >= 0; i--)
                        if (els[i][colI] < els[rowI][colI]) ++score;
                        else if (els[i][colI] >= els[rowI][colI]) return ++score;
                    break;
                case Side.BOTTOM:
                    for (int i = rowI + 1; i < els.Count(); i++)
                        if (els[i][colI] < els[rowI][colI]) ++score;
                        else if (els[i][colI] >= els[rowI][colI]) return ++score;
                    break;
                case Side.LEFT:
                    for (int i = colI - 1; i >= 0; i--)
                        if (els[rowI][i] < els[rowI][colI]) ++score;
                        else if (els[rowI][i] >= els[rowI][colI]) return ++score;
                    break;
                case Side.RIGHT:
                    for (int i = colI + 1; i < els[rowI].Length; i++)
                        if (els[rowI][i] < els[rowI][colI]) ++score;
                        else if (els[rowI][i] >= els[rowI][colI]) return ++score;
                    break;
            }
            return score;
        }

        public static void SetNextPositions(Pair head, Pair tail, char direction)
        {
            switch (direction)
            {
                case 'R':
                    if (head.x - 1 == tail.x) tail.Assign(head);
                    head.x++;
                    break;
                case 'L':
                    if (head.x + 1 == tail.x) tail.Assign(head);
                    head.x--;
                    break;
                case 'U':
                    if (head.y - 1 == tail.y) tail.Assign(head);
                    head.y++;
                    break;
                case 'D':
                    if (head.y + 1 == tail.y) tail.Assign(head);
                    head.y--;
                    break;
            }
        }
    }

    public class Pair
    {
        public Pair(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int x;
        public int y;
    }

    public class Item
    {
        public Item(string name, int size, Item? parent)
        {
            Name = name;
            Size = size;
            Parent = parent;
        }

        public string Name { get; private set; }
        public int Size { get; set; }
        public Item? Parent { get; private set; }
        public Dictionary<string, Item> Children { get; private set; } = new Dictionary<string, Item>();
        public int SetSize()
        {
            foreach (var item in Children.Values)
            {
                Size += item.SetSize();
            }
            return Size;
        }
    }
}
