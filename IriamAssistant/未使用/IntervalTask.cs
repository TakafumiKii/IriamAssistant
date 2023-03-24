using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IriamAssistant.未使用
{
    internal class IntervalTask
    {
        public delegate Task TaskFunction();
        public int IntervalMs { get; set; } = 5000;

        CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public async Task Run(TaskFunction taskFunction)
        {
            var cancellationToken = _cancellationTokenSource.Token;
            while (!cancellationToken.IsCancellationRequested && IntervalMs > 0)
            {
                await taskFunction();
                await Task.Delay(IntervalMs, cancellationToken);
            }
        }

        public void Cancel()
        {
            if(!_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource = new CancellationTokenSource();
            }
        }

    }
}
