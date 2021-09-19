using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    class Day04 : BaseDay
    {
        public readonly string[] input;
        private readonly List<Dictionary<string, string>> Passports = new();
        public Day04()
        {
            input = File.ReadAllText(InputFilePath).Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public override string Solve_1()
        {
            string[] Keys = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
            int count = 0;
            foreach (var line in input)
            {
                Dictionary<string, string> items = new();
                foreach (var item in line.Replace("\r\n", " ").Split(" "))
                {
                    var split = item.Split(":");

                    if (Keys.Contains(split[0]))
                        items.Add(split[0], split[1]);
                }

                if (!Keys.Except(items.Keys).Any())
                {
                    Passports.Add(items);
                    count++;
                }
            }

            return count.ToString();
        }

        public override string Solve_2()
        {
            int count = 0;
            foreach (var passport in Passports)
            {
                if (passport.All(x => Validate(x.Key, x.Value)))
                {
                    count++;
                }
            }

            return count.ToString();
        }

        private static bool Validate(string key, string value)
        {
            switch (key)
            {
                case "byr":
                    if (int.TryParse(value, out int result))
                    {
                        return result >= 1920 && result <= 2002;
                    }

                    return false;
                case "iyr":
                    if (int.TryParse(value, out result))
                    {
                        return result >= 2010 && result <= 2020;
                    }

                    return false;
                case "eyr":
                    if (int.TryParse(value, out result))
                    {
                        return result >= 2020 && result <= 2030;
                    }

                    return false;
                case "hgt":
                    if (value[^2..] == "cm")
                    {
                        value = value[..^2];
                        if (int.TryParse(value, out result))
                        {
                            return result >= 150 && result <= 193;
                        }
                    }
                    else if (value[^2..] == "in")
                    {
                        value = value[..^2];
                        if (int.TryParse(value, out result))
                        {
                            return result >= 59 && result <= 76;
                        }
                    }

                    return false;
                case "hcl":
                    return value.Length == 7 && value[0] == '#' && value[1..].All(x => char.IsDigit(x) || (x >= 'a' && x <= 'f'));
                case "ecl":
                    return new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(value);
                case "pid":
                    return value.Length == 9 && value.All(x => char.IsDigit(x));
            }

            return true;
        }
    }
}
