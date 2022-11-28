using BackgroundTaskWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using BackgroundTaskWPF.Tasks;

namespace BackgroundTaskWPF.Controller
{
    public class FactorialController
    {
        private CancellationTokenSource tokenSource;
        private readonly IFactorialTask factorialTask;

        public FactorialController(IFactorialTask factorialTaskInstace)
        {
            factorialTask = factorialTaskInstace;
        }

        public async void Start(FactorialViewModel viewModel, Action<string> resultAction)
        {
            viewModel.EnableStart = false;
            viewModel.EnableCancel = true;

            IProgress<int> progress = new Progress<int>(value => viewModel.ProgressPercentage = value);
            tokenSource = new CancellationTokenSource();

            try
            {
                Dictionary<int, int> results
                = await Task.Run(()
                    => factorialTask.Calculate(
                        viewModel.InputTxt.Split(';').Select(values => Convert.ToInt32(values)),
                        progress,
                        tokenSource.Token));

                foreach (var res in results)
                {
                    resultAction($"input: {res.Key}\toutput: {res.Value}{Environment.NewLine}");
                }
            }
            catch (OperationCanceledException)
            {
                resultAction("Calculation cancelled!");
            }

            viewModel.InputTxt = string.Empty;
            viewModel.EnableStart = true;
            viewModel.EnableCancel = false;
        }

        public void Cancel()
        {
            tokenSource?.Cancel();
        }
    }
}
