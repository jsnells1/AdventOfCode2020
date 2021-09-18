using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    class Day21 : BaseDay
    {
        private readonly List<string> AllIngredients = new();
        private readonly Dictionary<string, HashSet<string>> PossibleMappings = new();
        public Day21()
        {
            var input = File.ReadAllText(InputFilePath).Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var line in input)
            {
                var split = line.Split(" (contains ");

                var ingredients = split[0].Split(" ");
                var allergens = split[1][..^1].Split(", ");

                AllIngredients.AddRange(ingredients);
                foreach (var allergen in allergens)
                {
                    if (PossibleMappings.TryGetValue(allergen, out HashSet<string> value))
                    {
                        value.IntersectWith(ingredients);
                    }
                    else
                    {
                        PossibleMappings[allergen] = ingredients.ToHashSet();
                    }
                }
            }
        }

        public override string Solve_1()
        {
            var values = PossibleMappings.Values.SelectMany(x => x).ToHashSet();
            return AllIngredients.Count(x => !values.Contains(x)).ToString();
        }

        public override string Solve_2()
        {
            SortedDictionary<string, string> RealMapping = new();

            while (RealMapping.Keys.Count < PossibleMappings.Keys.Count)
            {
                foreach (var pair in PossibleMappings)
                {
                    if (pair.Value.Count == 1)
                    {
                        RealMapping[pair.Key] = pair.Value.First();
                    }
                }

                foreach (var val in PossibleMappings.Values)
                {
                    val.RemoveWhere(x => RealMapping.Values.Contains(x));
                }
            }

            return string.Join(",", RealMapping.Values);
        }
    }
}
