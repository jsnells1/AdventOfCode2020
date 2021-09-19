using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    class Day25 : BaseDay
    {
        public readonly string[] input;
        public Day25()
        {
            input = File.ReadAllText(InputFilePath).Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public override string Solve_1()
        {
            int card = int.Parse(input[0]);
            int door = int.Parse(input[1]);

            int loopSize = 0;
            int subjectNum = 7;
            long value = 1;
            while (value != card)
            {
                loopSize++;
                value *= subjectNum;
                value %= 20201227;
            }

            value = 1;
            subjectNum = door;

            for (int i = 0; i < loopSize; i++)
            {
                value *= subjectNum;
                value %= 20201227;
            }

            return value.ToString();
        }

        public override string Solve_2()
        {
            throw new NotImplementedException();
        }
    }
}
