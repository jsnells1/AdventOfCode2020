using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    class Day22 : BaseDay
    {
        private readonly string[] input;
        private readonly Queue<int> Player1 = new();
        private readonly Queue<int> Player2 = new();
        private int HighestCard { get; set; } = -1;
        public Day22()
        {
            input = File.ReadAllText(InputFilePath).Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        private void SetupDecks()
        {
            Player1.Clear();
            Player2.Clear();
            var cards = input[0].Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)[1..].Select(x => int.Parse(x));

            foreach (var card in cards)
            {
                HighestCard = Math.Max(HighestCard, card);
                Player1.Enqueue(card);
            }

            cards = input[1].Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)[1..].Select(x => int.Parse(x));

            foreach (var card in cards)
            {
                HighestCard = Math.Max(HighestCard, card);
                Player2.Enqueue(card);
            }
        }

        public override string Solve_1()
        {
            SetupDecks();
            while (Player1.Count != 0 && Player2.Count != 0)
            {
                if (Player1.Peek() > Player2.Peek())
                {
                    Player1.Enqueue(Player1.Dequeue());
                    Player1.Enqueue(Player2.Dequeue());
                }
                else
                {
                    Player2.Enqueue(Player2.Dequeue());
                    Player2.Enqueue(Player1.Dequeue());
                }
            }

            return GetScore().ToString();
        }

        public override string Solve_2()
        {
            SetupDecks();
            PlayGame(Player1, Player2);

            return GetScore().ToString();
        }

        /// <returns>Returns true if Player1 won or false if Player2 won</returns>
        private bool PlayGame(Queue<int> p1, Queue<int> p2)
        {
            if (p1 != Player1 && p1.Contains(HighestCard))
            {
                return true;
            }

            List<int[]> Player1Seen = new();
            List<int[]> Player2Seen = new();

            while (p1.Count != 0 && p2.Count != 0)
            {
                // Exact same config in both decks, Player1 wins
                if (Player1Seen.Any(seen => p1.SequenceEqual(seen)) && Player2Seen.Any(seen => p2.SequenceEqual(seen)))
                {
                    return true;
                }

                Player1Seen.Add(p1.ToArray());
                Player2Seen.Add(p2.ToArray());

                bool p1Wins = false;
                if (p1.Count - 1 >= p1.Peek() && p2.Count - 1 >= p2.Peek())
                {
                    p1Wins = PlayGame(new Queue<int>(p1.Skip(1).Take(p1.Peek())), new Queue<int>(p2.Skip(1).Take(p2.Peek())));
                }
                else
                {
                    p1Wins = p1.Peek() > p2.Peek();
                }

                if (p1Wins)
                {
                    p1.Enqueue(p1.Dequeue());
                    p1.Enqueue(p2.Dequeue());
                }
                else
                {
                    p2.Enqueue(p2.Dequeue());
                    p2.Enqueue(p1.Dequeue());
                }
            }

            return p1.Count > 0;
        }

        private int GetScore()
        {
            int multi = 1;
            int score = 0;
            Queue<int> queue = Player1.Count > Player2.Count ? Player1 : Player2;
            foreach (var card in queue.Reverse())
            {
                score += card * multi;
                multi++;
            }

            return score;
        }
    }
}
