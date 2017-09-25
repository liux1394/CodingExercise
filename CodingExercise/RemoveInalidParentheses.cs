using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CodingExercise
{
    internal class RemoveInalidParentheses
    {
        public static string GetOne(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            Stack<int> leftParenthesisIdx = new Stack<int>();

            HashSet<int> indexToRemove = new HashSet<int>();
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    leftParenthesisIdx.Push(i);
                }
                else if (input[i] == ')')
                {
                    if (leftParenthesisIdx.Count == 0)
                    {
                        indexToRemove.Add(i);
                    }
                    else
                    {
                        leftParenthesisIdx.Pop();
                    }
                }
            }

            foreach (int value in leftParenthesisIdx)
            {
                indexToRemove.Add(value);
            }

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                if (!indexToRemove.Contains(i))
                {
                    builder.Append(input[i]);
                }
            }

            return builder.ToString();
        }

        public void Test()
        {
            string input = "(a)())()";
            Console.WriteLine("{0}", RemoveInalidParentheses.GetOne(input));

            input = "()())()";
            Console.WriteLine("{0}", RemoveInalidParentheses.GetOne(input));

            input = ")(";
            Console.WriteLine("{0}", RemoveInalidParentheses.GetOne(input));
        }
    }

}
