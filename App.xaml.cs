using R_III_WPF.Services.Implemenatations;
using R_III_WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using Unity.Injection;

namespace R_III_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IUnityContainer container = new UnityContainer();
            container.RegisterType<IBillsService, BillsService>();
            container.RegisterType<IInfoService, InfoService>();
            container.RegisterType<IUsersService, UsersService>();
            container.RegisterType<MainWindow>(new InjectionConstructor(container.Resolve<IInfoService>(), container.Resolve<IBillsService>(), container.Resolve<IUsersService>()));
            container.RegisterType<MainWindow, MainWindow>();
            container.RegisterType<Login>(new InjectionConstructor(container.Resolve<MainWindow>(), container.Resolve<IUsersService>()));
            container.RegisterType<Login, Login>();



            var drv = container.Resolve<Login>();
            drv.Show();
        }
    }
}
