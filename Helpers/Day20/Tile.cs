using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Helpers.Day20
{
    class Tile
    {
        #region Properties

        private bool Oriented { get; set; }
        public int Number { get; set; }
        public List<List<bool>> Pixels { get; set; } = new();
        // Top, Bot, Left, Right
        public List<List<bool>> Edges { get; set; } = new();
        public List<bool> UniqueEdges { get; } = new List<bool> { true, true, true, true };
        public HashSet<Tile> Neighbors { get; } = new();

        public List<bool> Top => Edges[0];
        public List<bool> Bot => Edges[1];
        public List<bool> Left => Edges[2];
        public List<bool> Right => Edges[3];

        #endregion

        #region Constructors 

        public Tile(int num, string[] lines)
        {
            Number = num;

            foreach (var line in lines)
            {
                Pixels.Add(line.Select(x => x == '#').ToList());
            }

            Edges.Add(new List<bool>(Pixels[0]));
            Edges.Add(new List<bool>(Pixels[9]));
            Edges.Add(Pixels.Select(x => x[0]).ToList());
            Edges.Add(Pixels.Select(x => x[9]).ToList());
        }

        #endregion

        public IEnumerable<List<bool>> MatchedEdges()
        {
            for (int i = 0; i < 4; i++)
            {
                if (!UniqueEdges[i])
                {
                    yield return Edges[i];
                }
            }
        }

        public void Orient(Tile target = null)
        {
            if (Oriented)
            {
                return;
            }

            Oriented = true;

            if (target != null)
            {
                foreach (var tEdge in target.MatchedEdges())
                {
                    foreach (var mEdge in MatchedEdges())
                    {
                        if (mEdge.SequenceEqual(tEdge))
                        {

                        }

                        mEdge.Reverse();

                        if (mEdge.SequenceEqual(tEdge))
                        {

                        }

                    }
                }

                // Orient

            }

            foreach (var tile in Neighbors)
            {
                tile.Orient(this);
            }
        }

        public void RemoveEdges()
        {
            Pixels = Pixels.Skip(1).SkipLast(1).ToList();
            for (int i = 0; i < Pixels.Count; i++)
            {
                Pixels[i] = Pixels[i].Skip(1).SkipLast(1).ToList();
            }
        }

        public void Rotate90()
        {
            Pixels = Pixels
                .SelectMany(inner => inner.Select((item, index) => new { item, index }))
                .GroupBy(i => i.index, i => i.item)
                .Select(g => g.Reverse().ToList())
                .ToList();
        }

        public void FlipHorizontal()
        {
            Pixels.ForEach(x => x.Reverse());
        }

        public void FlipVertical()
        {
            Pixels.Reverse();
        }

        public void Print()
        {
            foreach (var line in Pixels)
            {
                line.ForEach(x => Console.Write(x ? "#" : "."));
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public void MatchEdges(Tile t)
        {
            for (int i = 0; i < 4; i++)
            {
                if (UniqueEdges[i])
                {
                    bool match = t.Edges.Any(x => x.SequenceEqual(Edges[i]));

                    if (!match)
                    {
                        Edges[i].Reverse();
                        match = t.Edges.Any(x => x.SequenceEqual(Edges[i]));
                    }

                    if (match)
                    {
                        Neighbors.Add(t);
                        UniqueEdges[i] = false;
                    }
                }
            }
        }
    }
}
