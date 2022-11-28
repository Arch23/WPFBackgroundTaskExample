using BackgroundTaskWPF.Controller;
using BackgroundTaskWPF.Tasks;
using BackgroundTaskWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BackgroundTaskWPF.Views
{
    /// <summary>
    /// Interaction logic for FactorialView.xaml
    /// </summary>
    public partial class FactorialView : Window
    {
        private readonly FactorialController controller;
        private FactorialViewModel viewModel;

        public FactorialView(FactorialController controllerInstance)
        {
            InitializeComponent();
            viewModel = DataContext as FactorialViewModel;
            controller = controllerInstance;
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            controller.Start(viewModel, (line) => ResultTxtBlock.Text += line);
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            controller.Cancel();
        }
    }
}
