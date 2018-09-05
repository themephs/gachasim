using GachaSim.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GachaSim.Cli.Commands
{
	public class CommandService
	{
		private readonly Dictionary<string, ICommand> Commands;

		public CommandService(IOutputService outputService, ConfigService cfgService)
		{

			var commandTypes = Assembly.GetExecutingAssembly()
									   .GetTypes()
									   .Where(t => !t.IsAbstract 
												&& t.IsClass
												&& typeof(ICommand).IsAssignableFrom(t) 
												&& t.GetCustomAttribute<CommandKeyAttribute>() != null);

			Commands = commandTypes.ToDictionary(
				ct => ct.GetCustomAttribute<CommandKeyAttribute>().Key,
				ct => (ICommand)Activator.CreateInstance(ct, outputService, cfgService)
			);
		}


		public void HandleCommand(string cmd, params string[] args)
		{			
			if (!string.IsNullOrWhiteSpace(cmd) && this.Commands.ContainsKey(cmd.ToLower()))
			{				
				var commandInstance = this.Commands[cmd.ToLower()];
				commandInstance.Execute(args);
			}
		}
	}
}
