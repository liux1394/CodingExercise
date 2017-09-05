using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingExercise
{
    internal class DictionaryMatch
    {
        internal List<string> SolutionUsingHash(string str, List<string> dictionary)
        {
            if (str == null || str.Length == 0)
            {
                return dictionary;
            }

            Dictionary<char, int> characters = new Dictionary<char, int>();
            for (int i = 0; i < str.Length; i++)
            {
                if (characters.ContainsKey(str[i]))
                {
                    characters[str[i]]++;
                }
                else
                {
                    characters[str[i]] = 1;
                }
            }

            List<string> res = new List<string>();

            for (int i = 0; i < dictionary.Count; i++)
            {
                Dictionary<char, int> lookUp = new Dictionary<char, int>();
                for (int j = 0; j < dictionary[i].Length; j++)
                {
                    if (lookUp.ContainsKey(dictionary[i][j]))
                    {
                        lookUp[dictionary[i][j]]++;
                    }
                    else
                    {
                        lookUp[dictionary[i][j]] = 1;
                    }
                }
                bool isIncluded = true;
                foreach (KeyValuePair<char, int> pair in lookUp)
                {
                    if (!characters.ContainsKey(pair.Key) || characters[pair.Key] < pair.Value)
                    {
                        isIncluded = false;
                        break;
                    }
                }
                if (isIncluded)
                {
                    res.Add(dictionary[i]);
                }
            }
            return res;
        }

        internal List<string> SolutionUsingTrie(string str, List<string> dictionary)
        {
            if (str == null || str.Length == 0)
            {
                return dictionary;
            }

            List<string> allSubsets = new List<string>();
            StringBuilder builder = new StringBuilder();
            string sortedStr = string.Concat(str.OrderBy(ch => ch));
            GetAllSubSets(sortedStr, 0, allSubsets, builder);

            TrieNode root = Trie.CreateTrie(dictionary);

            List<string> result = new List<string>();

            List<string> allStrings = new List<string>();
            for (int i = 0; i < allSubsets.Count; i++)
            {
                StringBuilder sb = new StringBuilder();
                bool[] used = new bool[allSubsets[i].Length];
                GetAllPermutations(allSubsets[i], allStrings, sb, used);
            }
            for (int i = 0; i < allStrings.Count; i++)
            {
                if (Trie.IsExisted(allStrings[i], root))
                {
                    result.Add(allStrings[i]);
                }
            }

            return result;
        }

        private void GetAllSubSets(string str, int start, List<string> result, StringBuilder builder)
        {   
            for (int i = start; i < str.Length; i++)
            {
                if (i == start || str[i] != str[i - 1])
                {
                    builder.Append(str[i]);
                    result.Add(builder.ToString());
                    GetAllSubSets(str, i + 1, result, builder);
                    builder.Remove(builder.Length - 1, 1);
                }
            }
        }

        private void GetAllPermutations(string str, List<string> result, StringBuilder builder, bool[] used)
        {
            if (builder.Length == str.Length)
            {
                result.Add(builder.ToString());
                return;
            }

            for (int i = 0; i < str.Length; i++)
            {
                if ((i == 0 || str[i] != str[i - 1]) && !used[i])
                {
                    builder.Append(str[i]);
                    used[i] = true;
                    GetAllPermutations(str, result, builder, used);
                    used[i] = false;
                    builder.Remove(builder.Length-1, 1);
                }
            }
        }

        internal void Test()
        {
            string str = "aabdtc";
            List<string> dict = new List<string> { "bat", "dag", "cad", "ab", "at", "date", "calos" };
            List<string> result1 = SolutionUsingHash(str, dict);
            List<string> result2 = SolutionUsingTrie(str, dict);

            Console.WriteLine("Results of the method using hash table are as follows:");
            for (int i = 0; i < result1.Count; i++)
            {
                Console.Write("{0} ", result1[i]);
            }
            Console.Write("\n");
            Console.WriteLine("Results of the method using Trie are as follows:");
            for (int i = 0; i < result2.Count; i++)
            {
                Console.Write("{0} ", result2[i]);
            }
        }
    }
}
