using System;
using System.Collections.Generic;
using System.Threading;

namespace BackgroundTaskWPF.Tasks
{
    public interface IFactorialTask
    {
        Dictionary<int, int> Calculate(IEnumerable<int> values,
            IProgress<int> progressHandler,
            CancellationToken token);
    }
}
