using BackgroundTaskWPF.Controller;
using BackgroundTaskWPF.Tasks;
using BackgroundTaskWPF.Views;
using IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace BackgroundTaskWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider serviceProvider;

        private IServiceProvider ConfigureIoC()
        {
            IServiceCollection services = new ServiceCollection();
            Register.Business(services);
            View(services);
            return services.BuildServiceProvider();
        }

        private void View(IServiceCollection services)
        {
            services.AddTransient<FactorialController>();
            services.AddTransient<IFactorialTask, FactorialTask>();
            services.AddTransient<FactorialView>();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            serviceProvider = ConfigureIoC();

            serviceProvider.GetService<FactorialView>().Show();
        }
    }
}
