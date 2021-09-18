using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Helpers.Day19
{
    class Rule
    {
        private char _terminal;
        public char Terminal
        {
            get
            {
                return _terminal;
            }
            set
            {
                IsTerminal = true;
                _terminal = value;
            }
        }
        public bool IsTerminal { get; private set; }
        public List<List<int>> Sequences = new();

        public static Rule FromString(string s)
        {
            Rule r = new();

            if (s.Contains("\""))
            {
                r.Terminal = s[1];
            }
            else
            {
                foreach (var seq in s.Split(" | "))
                {
                    r.Sequences.Add(seq.Split(" ").Select(x => int.Parse(x)).ToList());
                }
            }

            return r;
        }
    }
}
