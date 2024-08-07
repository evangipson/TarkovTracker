﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;

using TarkovTracker.Base.DependencyInjection;
using TarkovTracker.Logic.Builders.Interfaces;
using TarkovTracker.Logic.Services.Interfaces;
using TarkovTracker.View.Controllers.Interfaces;

namespace TarkovTracker.View
{
	internal class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		/// <param name="args">
		/// A list of arguments provided to the application.
		/// </param>
		private static void Main(string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;
			Console.Title = "Tarkov Tracker";

			// setup our DI
			var serviceCollection = new ServiceCollection().AddLogging(cfg => cfg.AddConsole());

			// register TarkovTracker services from Services and View using reflection
			serviceCollection.AddServicesFromAssembly(Assembly.GetAssembly(typeof(IApplicationController)));
			serviceCollection.AddServicesFromAssembly(Assembly.GetAssembly(typeof(IQueryBuilder)));
			serviceCollection.AddServicesFromAssembly(Assembly.GetAssembly(typeof(IQueryService)));

			// register IHttpClientFactory
			serviceCollection.AddHttpClient();

			// register in-memory caching
			serviceCollection.AddMemoryCache();

			// instantiate dependency injection concrete object
			var serviceProvider = serviceCollection.BuildServiceProvider();

			// start the application by getting the Application
			// class from the required services, and run it.
			serviceProvider.GetRequiredService<IApplicationController>().Run();

			return;
		}
	}
}