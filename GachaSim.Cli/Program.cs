using System;

using Microsoft.Extensions.DependencyInjection;

using GachaSim.Cli.Commands;
using GachaSim.Cli.Services;
using GachaSim.Core.Services;

namespace GachaSim.Cli
{
	class Program
	{
		private static ServiceProvider _services;

		static void Main(string[] args)
		{
			try
			{
				_services = new ServiceCollection()
									.AddSingleton<Bootstrapper>()
									.AddSingleton<IOutputService, ConsoleOutputService>()
									.AddSingleton<CommandService>()
									.AddSingleton<ConfigService>()
									.BuildServiceProvider();

				_services.GetService<Bootstrapper>()
						 .Start(args);
			}
			catch (Exception ex)
			{
#if DEBUG
				Console.WriteLine($"**FATAL ERROR:\r\n\r\n {ex.ToString()}");
#else
				Console.WriteLine($"**FATAL ERROR: {ex.Message}");
#endif
			}
			finally
			{
				_services.Dispose();
			}
		}	
	}
}
