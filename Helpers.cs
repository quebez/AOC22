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
    }

    public class Pair
    {
        public int l;
        public int h;
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
