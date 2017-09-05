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

    public class SquareOperations
    {
        public static bool Equals()
        { }

        public static BlackOrWhiteSquare Merge(BlackOrWhiteSquare sq1, BlackOrWhiteSquare sq2)
        { } 
    }

}
