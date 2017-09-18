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
            int startTime1 = Convert.ToInt32(start1[2]);
         
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
        }
        

    }
}
