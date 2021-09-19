using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Helpers.Day24
{
    static class Extensions
    {
        public static IEnumerable<(double x, int y)> Neighbors(this (double x, int y) source)
        {
            double x = source.x;
            int y = source.y;

            yield return (x - 0.5, y + 1);
            yield return (x - 1, y);
            yield return (x - 0.5, y - 1);
            yield return (x + 0.5, y - 1);
            yield return (x + 1, y);
            yield return (x + 0.5, y + 1);
        }
    }
}
