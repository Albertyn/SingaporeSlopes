using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ski.Net
{
    class MapCordinate
    {
        public int y { get; private set; }
        public int x { get; private set; }

        public MapCordinate(int y, int x)
        {
            this.y = y;
            this.x = x;
        }
    }
    class MappedCordinate : MapCordinate
    {
        public int step { get; private set; }
        public int parent { get; private set; }
        public int value { get; private set; }

        public MappedCordinate(int step, int parent, int value, int y, int x) : base(y, x)
        {
            this.value = value;
            this.parent = parent;
            this.step = step;
        }
    }
}
