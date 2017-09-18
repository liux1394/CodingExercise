using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CodingExercise
{
    internal class FunctionTime
    {
        public int TotalTime;
        public int ExclusiveTime;
    }

    internal class FunctionExecutionTime
    {
        public static FunctionTime GetFunctionTimeForOne(List<string> jobs, string job)
        {
            if (job == null || jobs == null || jobs.Count < 2)
            {
                throw new ArgumentException("Invalid arguments");
            }
            int index = 0;
            string[] tokens = FunctionExecutionTime.FindJob(jobs, job, ref index);
            if (tokens == null)
            {
                throw new Exception("The given job is not found");
            }

            string start1 = tokens[0];
            int startTime1 = Convert.ToInt32(tokens[2]);
         
            tokens = jobs[index + 1].Split(',');
            if (tokens[0] == job)
            {
                return new FunctionTime {TotalTime = Convert.ToInt32(tokens[2]) - startTime1, ExclusiveTime = Convert.ToInt32(tokens[2]) - startTime1 };
            }

            string start2 = tokens[0];
            int startTime2 = Convert.ToInt32(tokens[2]);
            index = index + 2;
            tokens = FunctionExecutionTime.FindJob(jobs, start2, ref index);

            int endTime2 = Convert.ToInt32(tokens[2]);
            int endTime1 = Convert.ToInt32(jobs[index + 1].Split(',')[2]);
            
            return new FunctionTime {ExclusiveTime = endTime1 - startTime1- (endTime2 - startTime2), TotalTime = endTime1 - startTime1 };
        }

        private static string[] FindJob(List<string> jobs, string job, ref int index)
        {
            string[] tokens = null;
            for ( ; index < jobs.Count; index++)
            {
                tokens = jobs[index].Split(',');
                if (tokens[0] == job)
                {
                    break;
                }
            }
            if (tokens[0] == job)
            {
                return tokens;
            }

            return null;
        }


        public static Dictionary<string, FunctionTime> GetFunctionTimeForAll(List<string> jobs)
        {
            if (jobs == null || jobs.Count < 2)
            {
                throw new Exception("Invalid input");
            }

            Dictionary<string, FunctionTime> res = new Dictionary<string, FunctionTime>();
            Stack<Tuple<string, int>> functions = new Stack<Tuple<string, int>>();
            int prevTime = 0;
            for (int i = 0; i < jobs.Count; i++)
            {
                string[] tokens = jobs[i].Split(',');
                if (functions.Count == 0 || functions.Peek().Item1 != tokens[0])
                {
                    functions.Push(new Tuple<string, int>(tokens[0], Convert.ToInt32(tokens[2])));
                }
                else
                {
                    int totalTime = Convert.ToInt32(tokens[2]) - functions.Pop().Item2;
                    int exclusiveTime = totalTime - prevTime;
                    prevTime = totalTime;
                    res[tokens[0]] = new FunctionTime { TotalTime = totalTime, ExclusiveTime = exclusiveTime };
                }
            }

            return res;
        }

        public void Test()
        {
            List<string> jobs1 = new List<string> { "abc,START,100", "abc,END,200" };
            List<string> jobs2 = new List<string> { "abc,START,100", "def,START,150", "def,END,180", "abc,END,200" };
            List<string> jobs3 = new List<string> { "abc,START,75", "def,START,100", "ghi,START,160", "ghi,END,190", "def,END,223", "abc,END,264" };

            FunctionTime res1 = FunctionExecutionTime.GetFunctionTimeForOne(jobs1, "abc");
            Console.WriteLine("res1: TotalTime = {0}, ExclusiveTime = {1}", res1.TotalTime, res1.ExclusiveTime);

            Dictionary<string, FunctionTime> res11 = FunctionExecutionTime.GetFunctionTimeForAll(jobs1);
            Console.WriteLine("Print all results for job1:");
            foreach (KeyValuePair<string, FunctionTime> pair in res11)
            {
                Console.WriteLine("job: {0}, TotalTime = {1}, ExclusiveTime = {2}", pair.Key, pair.Value.TotalTime, pair.Value.ExclusiveTime);
            }

            FunctionTime res2 = FunctionExecutionTime.GetFunctionTimeForOne(jobs2, "abc");
            Console.WriteLine("res2: TotalTime = {0}, ExclusiveTime = {1}", res2.TotalTime, res2.ExclusiveTime);

            Dictionary<string, FunctionTime> res22 = FunctionExecutionTime.GetFunctionTimeForAll(jobs2);
            Console.WriteLine("Print all results for job2:");
            foreach (KeyValuePair<string, FunctionTime> pair in res22)
            {
                Console.WriteLine("job: {0}, TotalTime = {1}, ExclusiveTime = {2}", pair.Key, pair.Value.TotalTime, pair.Value.ExclusiveTime);
            }

            FunctionTime res3 = FunctionExecutionTime.GetFunctionTimeForOne(jobs3, "def");
            Console.WriteLine("res3: TotalTime = {0}, ExclusiveTime = {1}", res3.TotalTime, res3.ExclusiveTime);

            Dictionary<string, FunctionTime> res33 = FunctionExecutionTime.GetFunctionTimeForAll(jobs3);
            Console.WriteLine("Print all results for job3:");
            foreach (KeyValuePair<string, FunctionTime> pair in res33)
            {
                Console.WriteLine("job: {0}, TotalTime = {1}, ExclusiveTime = {2}", pair.Key, pair.Value.TotalTime, pair.Value.ExclusiveTime);
            }
        }
    }
}
