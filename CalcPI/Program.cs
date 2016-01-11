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
            int threads = 32;
            int digits = 30000;

            if (args.Length > 0) digits = int.Parse(args[0]);
            if (args.Length > 1) threads = int.Parse(args[1]);

            Console.WriteLine("thread count: {0}", threads);
            Console.WriteLine("digits count: {0}", digits);

            DateTime start = DateTime.Now;
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < threads; i++)
            {
                tasks.Add(Task.Factory.StartNew(() => { CalculatePi(digits); }));
            }
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine(DateTime.Now - start);
        }


        static public string CalculatePi(int digits)
        {
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

            return result;
        }
    }
}
