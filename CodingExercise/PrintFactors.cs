using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingExercise
{
    class PrintFactors
    {
        internal List<List<int>> Solution1Dfs(int num)
        {
            List<List<int>> res = new List<List<int>>();
            List<int> temp = new List<int>();
            DfsFactor(num, res, temp, num);
            res[0].Add(1);
            return res;
        }

        private void DfsFactor(int num, List<List<int>> res, List<int> temp, int max)
        {
            if (num == 1)
            {
                res.Add(new List<int>(temp));
                return;
            }
           
            for (int i = max; i > 1; i--)
            {
                if (num % i == 0)
                {
                    temp.Add(i);
                    DfsFactor(num / i, res, temp, Math.Min(max, i));
                    temp.RemoveAt(temp.Count-1);
                }
            }
        }

        internal List<List<int>> Solution2Dp(int num)
        {
            List<List<List<int>>> dp = new List<List<List<int>>>(num);

            dp.Add(new List<List<int>> { new List<int> { 1 } });
            for (int i = 1; i < num; i++)
            {
                dp.Add(new List<List<int>> { new List<int> { i + 1 } });
            }
            for (int i = 1; i < num; i++)
            {
                for (int j = i; j > 1; j--)
                {
                    if ((i + 1) % j == 0)
                    {
                        foreach (List<int> factor in dp[(i + 1) / j - 1])
                        {
                            bool isGood = true;
                            foreach (int n in factor)
                            {
                                if (n > j)
                                    isGood = false;
                            }
                            if (isGood)
                            {
                                List<int> temp = new List<int>(factor);
                                temp.Add(j);
                                dp[i].Add(temp);
                            }
                        }
                    }
                }
            }
            if(num != 1)
                dp[num - 1][0].Add(1);
            return dp[num - 1];
            
        }

        internal void Test(int num)
        {
            //List<List<int>> result = Solution1Dfs(num);
            List<List<int>> result = Solution2Dp(num);

            Console.WriteLine("Print Factors for {0}:", num);
            for (int i = 0; i < result.Count; i++)
            {

                for (int j = 0; j < result[i].Count; j++)
                {
                    Console.Write("{0} ", result[i][j]);
                }
                Console.Write("\n");
            }

        }

    }
}
