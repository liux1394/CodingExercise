using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingExercise
{
    internal class BlackOrWhiteSquare
    {
        internal char color; // 'W' = white, 'B' = black, 'N' = nothing
        internal List<BlackOrWhiteSquare> next;

        internal BlackOrWhiteSquare(char _color)
        {
            this.color = _color;
            next = null;
        }
    }

    internal class SquareOperations
    {
        public static bool Equals(BlackOrWhiteSquare sq1, BlackOrWhiteSquare sq2)
        {
            if (sq1 == null && sq2 == null)
            {
                return true;
            }

            if (sq1 == null || sq2 == null)
            {
                return false;
            }

            if (sq1.color != sq2.color)
            {
                return false;
            }

            if (sq1.color == 'W' || sq1.color == 'B')
            {
                return true;
            }

            for (int i = 0; i < sq1.next.Count; i++)
            {
                if (!Equals(sq1.next[i], sq2.next[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static BlackOrWhiteSquare Merge(BlackOrWhiteSquare sq1, BlackOrWhiteSquare sq2)
        {
            if (sq1.color == 'B' || sq2.color == 'B')
            {
                return new BlackOrWhiteSquare('B');
            }

            if (sq1.color == 'W')
            {
                return sq2;
            }

            if (sq2.color == 'W')
            {
                return sq1;
            }

            BlackOrWhiteSquare cur = new BlackOrWhiteSquare('N');
            cur.next = new List<BlackOrWhiteSquare>();
            for (int i = 0; i < 4; i++)
            {
                cur.next.Add( Merge( sq1.next[i], sq2.next[i] ) );
            }

            for (int i = 1; i < 4; i++)
            {
                if (cur.next[i - 1].color != cur.next[i].color)
                {
                    return cur;
                }
            }
            cur.color = cur.next[0].color;
            cur.next = null;
            return cur;
        }

        internal void Test()
        {
            BlackOrWhiteSquare sq1 = null;
            BlackOrWhiteSquare sq2 = null;
            Console.WriteLine("{0}, should be true", SquareOperations.Equals(sq1, sq2));

            sq1 = new BlackOrWhiteSquare('W');
            Console.WriteLine("{0}, should be false", SquareOperations.Equals(sq1, sq2));

            sq2 = new BlackOrWhiteSquare('W');
            BlackOrWhiteSquare sq3; 
            sq3 = SquareOperations.Merge(sq1, sq2);
            Console.WriteLine("{0}, should be true", SquareOperations.Equals(sq1, sq2));

            sq2 = new BlackOrWhiteSquare('B');
            sq3 = SquareOperations.Merge(sq1, sq2);
            Console.WriteLine("{0}, should be false", SquareOperations.Equals(sq1, sq2));

            sq1.color = 'N';
            sq1.next = new List<BlackOrWhiteSquare>();
            sq1.next.Add(new BlackOrWhiteSquare('B'));
            sq1.next.Add(new BlackOrWhiteSquare('W'));
            sq1.next.Add(new BlackOrWhiteSquare('W'));
            sq1.next.Add(new BlackOrWhiteSquare('B'));
            sq3 = SquareOperations.Merge(sq1, sq2);
            Console.WriteLine("{0}, should be false", SquareOperations.Equals(sq1, sq2));

            sq2.color = 'N';
            sq2.next = new List<BlackOrWhiteSquare>();
            sq2.next.Add(new BlackOrWhiteSquare('B'));
            sq2.next.Add(new BlackOrWhiteSquare('W'));
            sq2.next.Add(new BlackOrWhiteSquare('W'));
            sq2.next.Add(new BlackOrWhiteSquare('B'));
            sq3 = SquareOperations.Merge(sq1, sq2);
            Console.WriteLine("{0}, should be true", SquareOperations.Equals(sq1, sq2));

            sq2.next[1].color = 'B';
            sq3 = SquareOperations.Merge(sq1, sq2);
            Console.WriteLine("{0}, should be false", SquareOperations.Equals(sq1, sq2));

            sq1.next[1].color = 'N';
            sq1.next[1].next = new List<BlackOrWhiteSquare>();
            sq1.next[1].next.Add(new BlackOrWhiteSquare('B'));
            sq1.next[1].next.Add(new BlackOrWhiteSquare('B'));
            sq1.next[1].next.Add(new BlackOrWhiteSquare('W'));
            sq1.next[1].next.Add(new BlackOrWhiteSquare('W'));
            sq3 = SquareOperations.Merge(sq1, sq2);
            Console.WriteLine("{0}, should be false", SquareOperations.Equals(sq1, sq2));

            sq2.next[1].color = 'N';
            sq2.next[1].next = new List<BlackOrWhiteSquare>();
            sq2.next[1].next.Add(new BlackOrWhiteSquare('B'));
            sq2.next[1].next.Add(new BlackOrWhiteSquare('B'));
            sq2.next[1].next.Add(new BlackOrWhiteSquare('W'));
            sq2.next[1].next.Add(new BlackOrWhiteSquare('W'));
            sq3 = SquareOperations.Merge(sq1, sq2);
            Console.WriteLine("{0}, should be true", SquareOperations.Equals(sq1, sq2));

            sq2.next[1].next[2].color = 'B';
            sq3 = SquareOperations.Merge(sq1, sq2);
            Console.WriteLine("{0}, should be false", SquareOperations.Equals(sq1, sq2));

            sq2.next[1].next[0].color = 'W';
            sq2.next[1].next[1].color = 'W';
            sq2.next[1].next[2].color = 'B';
            sq2.next[1].next[3].color = 'B';
            sq3 = SquareOperations.Merge(sq1, sq2);
        }
    }

}
