﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WpfTestGhActions
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Window? ActiveWindow => Current.Windows
            .OfType<Window>()
            .FirstOrDefault(w => w.IsActive);

        public static Window? FocusedWindow => Current.Windows
            .OfType<Window>()
            .FirstOrDefault(w => w.IsFocused);

        public static Window? CurrentWindow => FocusedWindow ?? ActiveWindow;

        private static IHost? _Host;

        public static IHost Host => _Host
            ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IServiceProvider Services => Host.Services;

        internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            var host = Host;
            
            base.OnStartup(e);
            await host.StartAsync();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using var host = Host;
            base.OnExit(e);
            await host.StopAsync();
        }
    }
}