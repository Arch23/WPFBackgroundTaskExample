using Busines.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BackgroundTaskWPF.Tasks
{
    public class FactorialTask : IFactorialTask
    {
        private const int ERROR_VALUE = -1;
        private readonly IFactorial factorial;

        public FactorialTask(IFactorial factorialInstance)
        {
            factorial = factorialInstance;
        }

        public Dictionary<int, int> Calculate (
            IEnumerable<int> values, 
            IProgress<int> progressHandler, 
            CancellationToken token)
        {
            Dictionary<int, int> retValues = new Dictionary<int, int>();
            var distinctValues = values.Distinct();
            int progress = 0;
            foreach (int val in distinctValues)
            {
                if (token != null)
                {
                    token.ThrowIfCancellationRequested();
                }

                try
                {
                    retValues.Add(val, factorial.Calculate(val));
                    Thread.Sleep(1000);
                }
                catch (Exception ex) when (ex is ArgumentException || ex is ArithmeticException)
                {
                    retValues.Add(val, ERROR_VALUE);
                }
                progress++;
                progressHandler.Report((int)(((decimal)progress / (decimal)distinctValues.Count()) * 100));
            }
            return retValues;
        }
    }
}
