using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingExercise
{
    internal class PalindromePermutationII
    {
        internal IList<string> GeneratePalindromes(string s)
        {
            List<string> res = new List<string>();

            Dictionary<string, int> count = new Dictionary<string, int>();

            string temp = "";
            if (MoreThanOneOdd(s, count, ref temp))
                return res;

            GetAllResults(count, res, temp, s.Length);

            return res;
        }

        private void GetAllResults(Dictionary<string, int> count, List<string> res, string temp, int len)
        {
            if (temp.Length == len)
            {
                res.Add(String.Copy(temp));
                return;
            }
            if (temp.Length * 2 == len)
            {
                res.Add(String.Copy(Reverse(temp) + temp));
                return;
            }

            if (temp.Length * 2 - 1 == len)
            {
                res.Add(String.Copy(Reverse(temp.Substring(1, temp.Length - 1)) + temp));
                return;
            }

            foreach (string key in count.Keys.ToList())
            {
                if (count[key] > 0)
                {
                    temp += key;
                    count[key] -= 2;
                    GetAllResults(count, res, temp, len);
                    count[key] += 2;
                    Console.WriteLine("{0}: {1}", temp, temp.Length);
                    temp = temp.Remove(temp.Length - 1, 1);
                    Console.WriteLine("{0}: {1}", temp, temp.Length);
                }
            }
        }

        private string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private bool MoreThanOneOdd(string str, Dictionary<string, int> count, ref string temp)
        {
            for (int i = 0; i < str.Length; i++)
            {
                string cur = str.Substring(i, 1);
                if (count.ContainsKey(cur))
                    count[cur]++;
                else
                    count[cur] = 1;
            }

            foreach (string key in count.Keys.ToList())
            {
                if (count[key] % 2 != 0 && temp != "")
                    return true;
                else if (count[key] % 2 != 0)
                {
                    temp = key;
                    count[key]--;
                }            
            }

            return false;
        }

        public void Test()
        {
            string str = "aab";
            IList<string> res = GeneratePalindromes(str);

            for (int i = 0; i < res.Count; i++)
            {
                Console.WriteLine(res[i]);
            }
        }
    }
}
