using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ski.Net
{
    class Topography
    {
        public int[][] Map { get; private set; }

        public List<MappedCordinate[]> Paths { get; private set; }

        public int _count { get; private set; }
        public int _lastLength { get; private set; }


        private T[] Add2Array<T>(T[] arr, T item) => arr.Concat(new[] { item }).ToArray();

        public IEnumerable<MappedCordinate[]> getPathsByLength(int length) => (length > 0)
            ? Paths.Where(list => list.Length >= length)
            : Paths.Where(list => list.Length >= _lastLength);


        public Topography(string filePath)
        {
            _count = 0; _lastLength = 0;
            Paths = new List<MappedCordinate[]>();

            string[] txtLines = File.ReadAllLines(filePath);
            Map = new int[txtLines.Length][];

            for (int i = 0; i < txtLines.Length; i++)
            {
                Map[i] = Array.ConvertAll(txtLines[i].Split(' '), s => int.Parse(s));
                _count += Map[i].Length;
            }

        }

        public void StepTrace(int currStep, int prev, MapCordinate point, List<MappedCordinate> trace)
        {
            try
            {
                int value = this.Map[point.y][point.x];
                currStep++;

                if (value < prev)
                {
                    trace.Add(new MappedCordinate(currStep, prev, value, point.y, point.x));

                    this.StepTrace(currStep, value, new MapCordinate(point.y - 1, point.x), new List<MappedCordinate>(trace));
                    this.StepTrace(currStep, value, new MapCordinate(point.y, point.x + 1), new List<MappedCordinate>(trace));
                    this.StepTrace(currStep, value, new MapCordinate(point.y + 1, point.x), new List<MappedCordinate>(trace));
                    this.StepTrace(currStep, value, new MapCordinate(point.y, point.x - 1), new List<MappedCordinate>(trace));
                }
                else if (trace.Count >= _lastLength)
                {
                    _lastLength = trace.Count;

                    if (!Paths.Any(x => x.SequenceEqual(trace)))
                        Paths.Add(trace.ToArray());
                }
            }
            catch // Out of Bounds 
            {
                // Trace End
            }
        }
    }
}
