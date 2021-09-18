using AdventOfCode.Helpers.Day20;
using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    class Day20 : BaseDay
    {
        private readonly List<Tile> Tiles = new();
        private readonly HashSet<Tile> Corners = new();
        private readonly HashSet<Tile> Edges = new();
        private readonly List<List<Tile>> FinishedGrid = new();
        public Day20()
        {
            var input = File.ReadAllText(InputFilePath).Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var line in input)
            {
                var split = line.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                Tiles.Add(new Tile(int.Parse(split[0][5..^1]), split[1..]));
            }
        }

        public override string Solve_1()
        {
            long product = 1;
            foreach (var t in Tiles)
            {
                foreach (var t2 in Tiles)
                {
                    if (t == t2)
                    {
                        continue;
                    }

                    t.MatchEdges(t2);
                }

                if (t.Neighbors.Count == 2)
                {
                    Corners.Add(t);
                    product *= t.Number;
                }

                if (t.Neighbors.Count == 1)
                {
                    Edges.Add(t);
                }
            }

            return product.ToString();
        }

        public override string Solve_2()
        {
            //Tile startingTile = Corners.Where(x => x.UniqueEdges[0]).First();
            //startingTile.Orient();

            return "2";
        }
        
        private void PrintGrid()
        {
            foreach (var list in FinishedGrid)
            {
                foreach (var tile in list)
                {
                    Console.Write($"{tile.Number}    ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private void BuildGrid()
        {
            Tile lastTile = null;
            int size = Tiles.Count;
            while (FinishedGrid.Count < size)
            {
                List<Tile> row = new();

                while (row.Count < size)
                {
                    if (lastTile == null)
                    {
                        lastTile = Corners.First();
                        row.Add(lastTile);
                        continue;
                    }
                    


                }
                FinishedGrid.Add(row);
            }
        }

    }
}
