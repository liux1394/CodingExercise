using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintFactors inst = new PrintFactors();
            inst.Test(32);
            inst.Test(12);
            inst.Test(1);
            Console.ReadKey();
        }
    }
}
