using System;
using System.Collections.Generic;

namespace MemTest
{
    class Program
    {
        static Random rnd = new Random();

        static byte[] AllocateBuffer(int size)
        {
            byte[] buffer = new byte[size];
            //InitBuffer(buffer);
            return buffer;
        }

        static void InitBuffer(byte[] buffer)
        {
            rnd.NextBytes(buffer);
        }

        static void Main(string[] args)
        {
            DateTime start;

            List<byte[]> buffer1 = new List<byte[]>();
            List<byte[]> buffer2 = new List<byte[]>();
            List<byte[]> buffer3 = new List<byte[]>();

            //            
            //    allocate             
            //            
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("1. Allocate 64mb block(s) as more as possible...");
            start = DateTime.Now;
            try
            {
                while (true)
                {
                    buffer1.Add(AllocateBuffer(64 * 1024 * 1024));
                    Console.Write("#");
                    buffer2.Add(AllocateBuffer(64 * 1024 * 1024));
                    Console.Write("#");
                }
            }
            catch (OutOfMemoryException)
            {
            }
            Console.WriteLine();
            Console.WriteLine("   Complete.");
            Console.WriteLine("   - total {0} x 64mb blocks = {1} MB were allocated.", (buffer1.Count + buffer2.Count), (buffer1.Count + buffer2.Count) * 64);
            Console.WriteLine("   - total execute time: {0} sec", (DateTime.Now - start).TotalSeconds);

            //        
            //    free  
            //        
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("2. Free Blocks...");
            start = DateTime.Now;
            {
                //        
                //  de-reference and GC  
                //            
                buffer2.Clear();
                GC.Collect(GC.MaxGeneration);
            }
            Console.WriteLine("   Complete.");
            Console.WriteLine("   - total {0} x 64mb blocks = {1} MB were allocated.", buffer1.Count, buffer1.Count * 64);
            Console.WriteLine("   - total execute time: {0} sec", (DateTime.Now - start).TotalSeconds);


            //           
            //    allocate  
            //          
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("3. Allocate 72mb block(s) as more as possible...");
            start = DateTime.Now;
            try
            {
                while (true)
                {
                    buffer3.Add(AllocateBuffer(72 * 1024 * 1024));
                    Console.Write("#");
                }
            }
            catch (OutOfMemoryException)
            {
            }
            Console.WriteLine();
            Console.WriteLine("   Complete.");
            Console.WriteLine("   - total: 64mb x {0} + 72mb x {1} = {2} MB were allocated.", buffer1.Count, buffer3.Count, buffer1.Count * 64 + buffer3.Count * 72);
            Console.WriteLine("   - total execute time: {0} sec", (DateTime.Now - start).TotalSeconds);


            Console.WriteLine("[Enter] to exit...");
            Console.ReadLine();
        }
    }

}
