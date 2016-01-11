using System;
using System.Collections.Generic;
//using System.Linq;
using System.Threading.Tasks;

namespace CalcPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int threads = 4;
            int digits = 10000;

            if (args.Length > 0) threads = int.Parse(args[0]);
            if (args.Length > 1) digits = int.Parse(args[1]);

            Console.WriteLine("thread count: {0}", threads);
            Console.WriteLine("digits count: {0}", digits);

            DateTime start = DateTime.Now;
            List<Task<Result>> tasks = new List<Task<Result>>();
            for (int i = 0; i < threads; i++)
            {
                tasks.Add(Task<Result>.Factory.StartNew(() => { return CalculatePi(digits); }));
            }
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine("================================================================================");
            Console.WriteLine("Compute PI complete!");
            Console.WriteLine("Total Excute Time:   {0} (ms)", (DateTime.Now - start).TotalMilliseconds);
            Console.WriteLine("Average Excute Time: {0} (ms)", GetAverage(tasks).TotalMilliseconds);
        }

        static public TimeSpan GetAverage(List<Task<Result>> tasks)
        {
            TimeSpan total = TimeSpan.Zero;

            foreach(Task<Result> t in tasks)
            {
                total += t.Result.Time;
            }

            return TimeSpan.FromMilliseconds(total.TotalMilliseconds / tasks.Count);
        }

        public class Result
        {
            public string PI;
            public TimeSpan Time;
        }

        static public Result CalculatePi(int digits)
        {
            DateTime startTime = DateTime.Now;
            string result = "";
            digits++;

            uint[] x = new uint[digits * 3 + 2];
            uint[] r = new uint[digits * 3 + 2];

            for (int j = 0; j < x.Length; j++)
                x[j] = 20;

            for (int i = 0; i < digits; i++)
            {
                uint carry = 0;
                for (int j = 0; j < x.Length; j++)
                {
                    uint num = (uint)(x.Length - j - 1);
                    uint dem = num * 2 + 1;

                    x[j] += carry;

                    uint q = x[j] / dem;
                    r[j] = x[j] % dem;

                    carry = q * num;
                }
                if (i < digits - 1)
                    result += (x[x.Length - 1] / 10).ToString();
                r[x.Length - 1] = x[x.Length - 1] % 10; ;
                for (int j = 0; j < x.Length; j++)
                    x[j] = r[j] * 10;
            }

            return new Result()
            {
                PI = result,
                Time = DateTime.Now - startTime
            };
        }
    }
}
