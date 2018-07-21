using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Ski.Net
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Skiing in Singapore! \nPress any key to continue...");
            Console.ReadKey(true);

            Stopwatch watch = Stopwatch.StartNew();

            Topography Slope = new Topography("data.txt");
            int ix = 0;
            int iy = 0;
            int start = 1501; //  int.MaxValue;

            foreach (var y in Slope.Map)
            {
                Console.WriteLine($"Process row: [ {iy} ] length: {y.Length}");
                foreach (var x in y)
                {
                    Slope.StepTrace(0, start, new MapCordinate(iy, ix), new List<MappedCordinate>());
                    ix++;
                }
                ix = 0;
                iy++;
            }

            Console.WriteLine($"\nElapsed Time: {watch.ElapsedMilliseconds}ms | t:{watch.ElapsedTicks}\nPress any key for Results...");
            Console.ReadKey(true);

            IEnumerable<MappedCordinate[]> results = Slope.getPathsByLength(0);

            // Winner 
            // var w = results.OrderByDescending(list => list.Max(p => p.value) - list.Min(p => p.value)).First();

            int pos = 0;
            foreach (var arr in results.OrderByDescending(list => list.Max(p => p.value) - list.Min(p => p.value)))
            {
                var max = arr.Max(x => x.value);
                var min = arr.Min(x => x.value);

                if (pos > 0) Console.WriteLine("\nOther path with same length:");
                else Console.WriteLine($"\nWinner!! >> The longset route with steepest drop:")
                        ;
                Console.WriteLine($"Length: {arr.Length}, Drop: {max}-{min} = {max - min}\n");

                foreach (var item in arr.OrderBy(x => x.step).ThenBy(p => p.parent))
                {
                    Console.WriteLine($"[{item.step}] parent: {item.parent}, value: {item.value}, x: {item.x}, y: {item.y}");
                }
                pos++;
            }
            Console.WriteLine("\nPress any key to Exit...");
            Console.ReadKey();
        }
    }
}
