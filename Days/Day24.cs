using AdventOfCode.Helpers.Day24;
using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    class Day24 : BaseDay
    {
        private readonly string[] input;
        private readonly List<List<string>> Instructions = new();
        private HashSet<(double x, int y)> Tiles = new();
        public Day24()
        {
            input = File.ReadAllText(InputFilePath).Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in input)
            {
                List<string> instruction = new();
                string ins = string.Empty;

                for (int i = 0; i < line.Length; i++)
                {
                    ins += line[i];

                    if (ins == "e" || ins == "w" || ins.Length == 2)
                    {
                        instruction.Add(ins);
                        ins = string.Empty;
                    }
                }

                Instructions.Add(instruction);
            }
        }

        public override string Solve_1()
        {
            foreach (var instruction in Instructions)
            {
                double x = 0;
                int y = 0;

                foreach (var dir in instruction)
                {
                    switch (dir)
                    {
                        case "e":
                            x++;
                            break;
                        case "se":
                            y--;
                            x += .5;
                            break;
                        case "sw":
                            y--;
                            x -= .5;
                            break;
                        case "w":
                            x--;
                            break;
                        case "nw":
                            y++;
                            x -= .5;
                            break;
                        case "ne":
                            y++;
                            x += .5;
                            break;
                    }
                }

                if (!Tiles.Add((x, y)))
                {
                    Tiles.Remove((x, y));
                }
            }

            return Tiles.Count.ToString();
        }

        public override string Solve_2()
        {
            for (int i = 0; i < 100; i++)
            {
                HashSet<(double x, int y)> newTiles = new();

                int yMin = Tiles.Min(x => x.y) - 1;
                double xMin = Tiles.Min(x => x.x) - .5;
                int yMax = Tiles.Max(x => x.y) + 1;
                double xMax = Tiles.Max(x => x.x) + .5;

                foreach (var tile in Tiles)
                {
                    int count = 0;
                    foreach (var neighbor in tile.Neighbors())
                    {
                        if (Tiles.Contains(neighbor))
                        {
                            count++;
                        }

                        if (count > 1)
                        {
                            break;
                        }
                    }

                    if (count == 1)
                    {
                        newTiles.Add(tile);
                    }
                }

                for (double x = xMin; x <= xMax; x += 0.5)
                {
                    for (int y = yMin; y <= yMax; y++)
                    {
                        int count = 0;
                        foreach (var neighbor in (x, y).Neighbors())
                        {
                            if (Tiles.Contains(neighbor))
                            {
                                count++;
                            }

                            if (count > 2)
                            {
                                break;
                            }
                        }

                        if (count == 2)
                        {
                            newTiles.Add((x, y));
                        }
                    }
                }

                Tiles = newTiles;
            }

            return Tiles.Count.ToString();
        }
    }
}
