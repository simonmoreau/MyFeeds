using MyFeeds.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFeedsTests.Mocks
{
    public class CycleManagerMock : ICycleManager
    {
        public bool CanRun(int index, int count)
        {
            return index == 1;
        }
    }
}
