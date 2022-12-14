using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;

namespace AOC22
{
    public class Tests
    {
        [Test]
        public void D11()
        {
            var max = 0;
            var cur = 0;
            foreach (var i in _.i1.GetStrings())
            {
                if (string.IsNullOrEmpty(i))
                {
                    if (max < cur) max = cur;
                    cur = 0;
                    continue;
                }
                cur += int.Parse(i);
            }
            Console.WriteLine(max);
        }

        [Test]
        public void D12()
        {
            var sum = new List<int>();
            var cur = 0;
            foreach (var item in _.i1.GetStrings())
            {
                if (string.IsNullOrEmpty(item))
                {
                    sum.Add(cur);
                    cur = 0;
                    continue;
                }
                cur += int.Parse(item);
            }
            sum.Sort();
            Console.WriteLine(sum[^1] + sum[^2] + sum[^3]);
        }

        [Test]
        public void D21()
        {
            var score = 0;
            foreach (var i in _.i2.GetStrings())
            {
                if (string.IsNullOrWhiteSpace(i)) break; 
                var a = (int)i[0];
                var b = i[2] - 23;

                score += b - 64;
                if (a == b) score += 3;
                else if (b == 65 && a == 67 || b == 66 && a == 65 || b == 67 && a == 66) score += 6;
            }
            Console.WriteLine(score);
        }

        // score += 6 + (a == 65 ? 2 : a == 66 ? 3 : 1), needs to be enclosed in parenthesis otherwise adds result of ternary operator only
        [Test]
        public void D22()
        {
            var score = 0;
            foreach (var i in _.i2.GetStrings())
            {
                if (string.IsNullOrWhiteSpace(i)) break;
                var a = (int)i[0];
                if (i[2] == 'X') score += a == 65 ? 3 : a == 66 ? 1 : 2;
                else if (i[2] == 'Y') score += 3 + a - 64;
                else if (i[2] == 'Z') score += 6 + (a == 65 ? 2 : a == 66 ? 3 : 1);
            }
            Console.WriteLine(score);
        }

        [Test]
        public void D31()
        {
            var priority = 0;
            foreach (var i in _.i3.GetStrings())
            {
                var a = i[..(i.Length / 2)].ToList();
                var b = i[(i.Length / 2)..].ToList();
                var dup = a.Intersect(b).ToList();
                priority += dup.Aggregate(0, (acc, x) => acc + (x > 96 ? x - 96 : x -38));
            }
            Console.WriteLine(priority);
        }

        [Test]
        public void D32()
        {
            var priority = 0;
            var s = _.i3.GetStrings().ToArray();
            for (int i = 0; i < s.Length - 3; i += 3)
            {
                var a = s[i].ToList();
                var b = s[i+1].ToList();
                var c = s[i+2].ToList();
                var dup = a.Intersect(b).ToList().Intersect(c).ToList();
                priority += dup.Aggregate(0, (acc, x) => acc + (x > 96 ? x - 96 : x - 38));
            }
            Console.WriteLine(priority);
        }

        [Test]
        public void D41()
        {
            int containing = 0;
            foreach (var i in _.i4.GetStrings())
            {
                if (string.IsNullOrEmpty(i)) break;
                var pair = i.Split(',');
                var a = Helpers.GetPair(pair[0]);
                var b = Helpers.GetPair(pair[1]);

                if ((a.x >= b.x && a.y <= b.y) || (b.x >= a.x && b.y <= a.y)) containing++;
            }
            Console.WriteLine(containing);
        }

        [Test]
        public void D42()
        {
            int overlaps = 0;
            foreach (var i in _.i4.GetStrings())
            {
                if (string.IsNullOrEmpty(i)) break;
                var pair = i.Split(',');
                var a = Helpers.GetPair(pair[0]);
                var b = Helpers.GetPair(pair[1]);

                if (b.x <= a.x && a.x <= b.y || a.x <= b.x && b.x <= a.y) overlaps++;
            }
            Console.WriteLine(overlaps);
        }

        // picovina
        [Test]
        public void D51()
        {
            var x = _.i5d.GetColumnsFromBottomUp();
            var y = _.i5m.GetStrings().GetArrayOfInts();

            foreach (var i in y)
            {
                var howMany = i[0];
                x[i[2] - 1] += x[i[1] - 1].Substring(x[i[1] - 1].Length - howMany, howMany).Reverse();
                x[i[1] - 1] = x[i[1] - 1][..^howMany];
            }
            
            foreach (var i in x)
            {
                Console.Write(i[^1]);
            }
        }

        [Test]
        public void D52()
        {
            var x = _.i5d.GetColumnsFromBottomUp();
            var y = _.i5m.GetStrings().GetArrayOfInts();

            foreach (var i in y)
            {
                var howMany = i[0];
                x[i[2] - 1] += x[i[1] - 1].Substring(x[i[1] - 1].Length - howMany, howMany);
                x[i[1] - 1] = x[i[1] - 1][..^howMany];
            }

            foreach (var i in x)
            {
                Console.Write(i[^1]);
            }
        }

        [Test]
        [TestCase(4)]
        [TestCase(14)]
        public void D612(int c)
        {
            var str = _.i6.ToArray();
            var arr = new char[c];
            for (var i = 0; i < str.Length - c; i++)
            {
                Array.Copy(str, i, arr, 0, c);
                if (arr.Distinct().Count() == c)
                {
                    Console.WriteLine(i + c);
                    break;
                }
            }
        }

        [Test]
        public void D71()
        {
            var allItems = Helpers.GetItems(_.i7);
            var total = 0;
            foreach (var item in allItems)
            {
                if (item.Children.Count > 0 && item.Size < 100000) total += item.Size;
            }
            Console.WriteLine(total);
        }

        [Test]
        public void D72()
        {
            var allItems = Helpers.GetItems(_.i7);
            var freeSpace = 70000000 - allItems[0].Size;
            var spaceRequired = 30000000 - freeSpace;
            var res = allItems[0].Size;
            foreach (var item in allItems)
            {
                if (item.Children.Count > 0 && item.Size < res && item.Size > spaceRequired) res = item.Size;
            }
            Console.WriteLine(res);
        }

        [Test]
        public void D81()
        {
            var grid = _.i8.GetStrings().ToArray();
            var visibleCount = grid.Count() * 2 + grid[0].Length * 2 - 4;

            for (var i = 1; i < grid.Length - 1; i++)
                for (var j = 1; j < grid[i].Length - 1; j++)
                {
                    if (!Helpers.IsAnyHigher(grid, i, j, Helpers.Side.LEFT) ||
                        !Helpers.IsAnyHigher(grid, i, j, Helpers.Side.RIGHT) ||
                        !Helpers.IsAnyHigher(grid, i, j, Helpers.Side.TOP) ||
                        !Helpers.IsAnyHigher(grid, i, j, Helpers.Side.BOTTOM)) visibleCount++;
                }

            Console.WriteLine(visibleCount);
        }

        [Test]
        public void D82()
        {
            var grid = _.i8.GetStrings().ToArray();
            var max = 0;

            for (var i = 1; i < grid.Length - 1; i++)
                for (var j = 1; j < grid[i].Length - 1; j++)
                {
                    var score = Helpers.GetTreeScore(grid, i, j, Helpers.Side.LEFT) *
                                Helpers.GetTreeScore(grid, i, j, Helpers.Side.RIGHT) *
                                Helpers.GetTreeScore(grid, i, j, Helpers.Side.TOP) *
                                Helpers.GetTreeScore(grid, i, j, Helpers.Side.BOTTOM);
                    if (score > max) max = score;
                }
            Console.WriteLine(max);
        }

        [Test]
        public void D91()
        {
            var head = new Pair(0, 0);
            var tail = new Pair(0, 0);
            var visited = new List<string>();

            foreach (var row in _.i9.GetStrings())
            {
                if (string.IsNullOrEmpty(row)) break;

                var a = row.Split(' ');
                for (var i = 0; i < int.Parse(a[1]); i++)
                {
                    visited.Add($"{tail.x},{tail.y}");
                    Helpers.SetNextPositions(head, tail, char.Parse(a[0]));
                }
            }
            visited.Add($"{tail.x},{tail.y}");
            Console.WriteLine(visited.Distinct().Count());
        }

        [Test]
        public void D92() { }

        [Test]
        public void D()
        {

        }
    }
}