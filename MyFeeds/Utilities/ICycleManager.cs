using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFeeds.Utilities
{
    public interface ICycleManager
    {
        bool CanRun(int index, int count);
        Task ReccordRun();
    }
}
