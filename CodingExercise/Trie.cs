using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingExercise
{
    internal class TrieNode
    {
        internal char val;
        internal bool isLeaf;
        internal TrieNode[] next = new TrieNode[26];

        internal TrieNode()
        {
            isLeaf = false;
        }
    }

    internal class Trie
    {
        internal static TrieNode CreateTrie(List<string> dict)
        {
            if (dict == null && dict.Count == 0)
                return null;
            TrieNode root = new TrieNode() { val = '#' };
            foreach (string str in dict)
            {
                TrieNode cur = root;
                int i;
                for (i = 0; i < str.Length && cur.next[str[i]-'a'] != null; i++)
                {
                    cur = cur.next[str[i] - 'a'];
                }

                while(i < str.Length)
                {
                    cur.next[str[i] - 'a'] = new TrieNode() { val = str[i] };
                    cur = cur.next[str[i] - 'a'];
                    i++;
                }
                cur.isLeaf = true;
            }
            return root;
        }

        internal static bool IsExisted(string str, TrieNode root)
        {
            if (str == null || str.Length == 0)
            {
                return true;
            }

            if (root == null)
            {
                return false;
            }

            for (int i = 0; i < str.Length; i++)
            {
                if (root.next[str[i] - 'a'] == null)
                    return false;
                root = root.next[str[i] - 'a'];
            }

            return root.isLeaf;
        }
    }


}
