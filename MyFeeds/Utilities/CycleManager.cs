using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyFeeds.Utilities
{
    public class CycleManager
    {
        public long CycleNumber { get; set; }
        public CycleManager() { }

        public bool CanRun(int index, int count)
        {
            var modulo = CycleNumber % count;

            if (modulo == index) return true;
            return false;
        }
    }
}
