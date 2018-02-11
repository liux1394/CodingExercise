using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingExercise
{
    public class GeneratelLowestNumber
    {
        public string Solution(string num, int n)
        {
            int len = num.Length - n;

            Stack<char> res = new Stack<char>(); 
            for (int i = 0; i < num.Length; i++)
            {
                while (res.Count > 0 && num[i] < res.Peek() && res.Count + num.Length - i > len)
                {
                    res.Pop();
                }

                res.Push(num[i]);
            }

            StringBuilder builder = new StringBuilder();
            while (res.Count > 0)
            {
                if (res.Count <= len)
                {
                    builder.Append(res.Peek());
                }

                res.Pop();
            }

            int s = 0;
            int t = builder.Length - 1;
            while (s < t)
            {
                char temp = builder[s];
                builder[s] = builder[t];
                builder[t] = temp;
                s++;
                t--;
            }

            return builder.ToString();
        }

        public void Test()
        {
            string input1 = "123456";
            Console.WriteLine(this.Solution(input1, 3));

            string input2 = "4205123";
            Console.WriteLine(this.Solution(input2, 4));

            string input3 = "216504";
            Console.WriteLine(this.Solution(input3, 3));

            string input4 = "2165224";
            Console.WriteLine(this.Solution(input4, 4));
        }
    }   
}
