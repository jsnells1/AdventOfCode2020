using AdventOfCode.Helpers.Day19;
using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    class Day19 : BaseDay
    {
        private readonly Dictionary<int, Rule> rules = new();
        private readonly string[] messages;

        public Day19()
        {
            var input = File.ReadAllText(InputFilePath).Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            messages = input[1].Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            var rulesRaw = input[0].Split("\r\n");

            foreach (var rule in rulesRaw)
            {
                string[] split = rule.Split(": ");
                int ruleNum = int.Parse(split[0]);

                rules.Add(ruleNum, Rule.FromString(split[1]));
            }
        }

        public override string Solve_1()
        {
            int count = 0;
            foreach (var msg in messages)
            {
                if (Run(0, msg).Any(m => string.IsNullOrEmpty(m)))
                {
                    count++;
                }
            }

            return count.ToString();
        }

        public override string Solve_2()
        {
            rules[8] = Rule.FromString("42 | 42 8");
            rules[11] = Rule.FromString("42 31 | 42 11 31");

            int count = 0;
            foreach (var msg in messages)
            {
                if (Run(0, msg).Any(m => string.IsNullOrEmpty(m)))
                {
                    count++;
                }
            }

            return count.ToString();
        }

        #region Solution Methods
        private IEnumerable<string> Run(int rule, string s)
        {
            Rule r = rules[rule];

            if (r.IsTerminal)
            {
                if (!string.IsNullOrEmpty(s) && r.Terminal == s[0])
                {
                    yield return s[1..];
                }
            }
            else
            {
                foreach (var thing in Run_Alt(r.Sequences, s))
                {
                    yield return thing;
                }
            }
        }

        private IEnumerable<string> Run_Seq(List<int> seq, string s)
        {
            if (seq.Count == 0)
            {
                yield return s;
            }
            else
            {
                int k = seq[0];
                seq = seq.Skip(1).ToList();

                foreach (var msg in Run(k, s))
                {
                    foreach (var r in Run_Seq(seq, msg))
                    {
                        yield return r;
                    }
                }
            }
        }

        private IEnumerable<string> Run_Alt(List<List<int>> alt, string s)
        {
            foreach (var seq in alt)
            {
                foreach (var r in Run_Seq(seq, s))
                {
                    yield return r;
                }
            }
        }
        #endregion
    }
}
