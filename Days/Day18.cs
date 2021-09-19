using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    class Day18 : BaseDay
    {
        private readonly IEnumerable<string> input;
        public Day18()
        {
            input = File.ReadAllText(InputFilePath).Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Replace(" ", ""));
        }

        public override string Solve_1()
        {
            long total = 0;
            foreach (var line in input)
            {
                Stack<long> sums = new(new long[] { 0 });
                Stack<char> ops = new(new[] { '+' });

                foreach (var c in line)
                {
                    if (c == '(')
                    {
                        ops.Push('+');
                        sums.Push(0);
                    }
                    else if (c == ')')
                    {
                        long val = sums.Pop();
                        switch (ops.Pop())
                        {
                            case '+':
                                sums.Push(sums.Pop() + val);
                                break;
                            case '*':
                                sums.Push(sums.Pop() * val);
                                break;
                        }
                    }
                    else if (c == '+' || c == '*')
                    {
                        ops.Push(c);
                    }
                    else
                    {
                        switch (ops.Pop())
                        {
                            case '*':
                                sums.Push(sums.Pop() * int.Parse(c.ToString()));
                                break;
                            case '+':
                                sums.Push(sums.Pop() + int.Parse(c.ToString()));
                                break;
                        }
                    }
                }

                total += sums.Pop();
            }

            return total.ToString();
        }

        public override string Solve_2()
        {
            long total = 0;
            foreach (var line in input)
            {
                Stack<long> vals = new();
                Stack<char> ops = new(new[] { '(' });

                void Reduce()
                {
                    while (ops.Peek() != '(')
                    {
                        if (ops.Pop() == '*')
                        {
                            vals.Push(vals.Pop() * vals.Pop());
                        }
                        else
                        {
                            vals.Push(vals.Pop() + vals.Pop());
                        }
                    }
                }

                foreach (var c in line)
                {
                    switch (c)
                    {
                        case '(':
                        case '+':
                            ops.Push(c);
                            break;
                        case ')':
                            Reduce();
                            ops.Pop();
                            break;
                        case '*':
                            Reduce();
                            ops.Push(c);
                            break;
                        default:
                            vals.Push(int.Parse(c.ToString()));
                            break;
                    }
                }

                Reduce();
                total += vals.Single();
            }

            return total.ToString();
        }
    }
}
