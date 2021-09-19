using AoCHelper;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    class Day23 : BaseDay
    {
        private readonly string input;
        private LinkedList<int> cups;
        public Day23()
        {
            input = File.ReadAllText(InputFilePath);
        }

        public override string Solve_1()
        {
            cups = new LinkedList<int>(input.Select(x => int.Parse(x.ToString())));
            var lookup = new Dictionary<int, LinkedListNode<int>>();
            int lowestCup = cups.Min();

            var current = cups.First;
            do
            {
                lookup.Add(current.Value, current);
                current = current.Next;
            } while (current != null);

            current = cups.First;
            for (int i = 0; i < 100; i++)
            {
                var cup1 = current.Next ?? cups.First;
                var cup2 = cup1.Next ?? cups.First;
                var cup3 = cup2.Next ?? cups.First;

                cups.Remove(cup1);
                cups.Remove(cup2);
                cups.Remove(cup3);

                lookup.TryGetValue(current.Value - 1, out LinkedListNode<int> destination);
                if (destination?.List == null)
                {
                    lookup.TryGetValue(current.Value - 2, out destination);
                }
                if (destination?.List == null)
                {
                    lookup.TryGetValue(current.Value - 3, out destination);
                }
                if (destination?.List == null)
                {
                    destination = cups.Find(cups.Max());
                }

                cups.AddAfter(destination, cup1);
                cups.AddAfter(cup1, cup2);
                cups.AddAfter(cup2, cup3);

                current = current.Next ?? cups.First;
            }

            return string.Join("", cups);
        }

        public override string Solve_2()
        {
            cups = new LinkedList<int>(input.Select(x => int.Parse(x.ToString())));

            for (int i = cups.Max() + 1; i <= 1000000; i++)
            {
                cups.AddLast(i);
            }

            var lookup = new Dictionary<int, LinkedListNode<int>>();
            int lowestCup = cups.Min();

            var current = cups.First;
            do
            {
                lookup.Add(current.Value, current);
                current = current.Next;
            } while (current != null);

            current = cups.First;
            for (int i = 0; i < 10000000; i++)
            {
                var cup1 = current.Next ?? cups.First;
                var cup2 = cup1.Next ?? cups.First;
                var cup3 = cup2.Next ?? cups.First;

                cups.Remove(cup1);
                cups.Remove(cup2);
                cups.Remove(cup3);

                lookup.TryGetValue(current.Value - 1, out LinkedListNode<int> destination);
                if (destination?.List == null)
                {
                    lookup.TryGetValue(current.Value - 2, out destination);
                }
                if (destination?.List == null)
                {
                    lookup.TryGetValue(current.Value - 3, out destination);
                }
                if (destination?.List == null)
                {
                    destination = cups.Find(cups.Max());
                }

                cups.AddAfter(destination, cup1);
                cups.AddAfter(cup1, cup2);
                cups.AddAfter(cup2, cup3);

                current = current.Next ?? cups.First;
            }

            var cupOne = cups.Find(1);
            return ((long)cupOne.Next.Value * cupOne.Next.Next.Value).ToString();
        }
    }
}
