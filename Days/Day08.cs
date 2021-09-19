using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    class Day08 : BaseDay
    {
        private readonly (string ins, int val)[] input;
        public Day08()
        {
            input = File.ReadAllText(InputFilePath)
                .Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(" "))
                .Select(x => (x[0], int.Parse(x[1])))
                .ToArray();
        }

        public override string Solve_1()
        {
            HashSet<int> seenInstructions = new();

            int acc = 0;
            int i = 0;

            while (seenInstructions.Add(i))
            {
                if (input[i].ins == "acc")
                {
                    acc += input[i].val;
                }
                else if (input[i].ins == "jmp")
                {
                    i += input[i].val - 1;
                }

                i++;
            }

            return acc.ToString();
        }

        public override string Solve_2()
        {
            HashSet<int> seenInstructions = new();
            for (int testIndex = 0; testIndex < input.Length; testIndex++)
            {
                if (input[testIndex].ins == "acc")
                    continue;

                seenInstructions.Clear();
                input[testIndex].ins = input[testIndex].ins == "nop" ? "jmp" : "nop";

                int acc = 0;
                int i = 0;
                while (seenInstructions.Add(i) && i < input.Length)
                {
                    if (input[i].ins == "acc")
                    {
                        acc += input[i].val;
                    }
                    else if (input[i].ins == "jmp")
                    {
                        i += input[i].val - 1;
                    }

                    i++;
                }

                if (i == input.Length)
                {
                    return acc.ToString();
                }

                input[testIndex].ins = input[testIndex].ins == "nop" ? "jmp" : "nop";
            }

            throw new NotImplementedException();
        }
    }
}
