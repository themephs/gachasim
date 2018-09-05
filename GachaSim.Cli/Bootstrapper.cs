using System.Linq;

using GachaSim.Cli.Commands;
using GachaSim.Core.Services;

namespace GachaSim.Cli
{
	public class Bootstrapper
	{
		private readonly ConfigService _configService;
		private readonly CommandService _commandService;

		public Bootstrapper(ConfigService cfgService, CommandService cmdService)
		{
			this._configService = cfgService;
			this._commandService = cmdService;
		}

		public void Start(params string[] args)
		{
			try
			{
				if (args.Length == 0)
				{
					this._commandService.HandleCommand("help", "ALL");
				}
				else
				{
					this._commandService.HandleCommand(args.First(), args.Skip(1).ToArray());
				}
			}
			catch (HelpException hex)
			{
				this._commandService.HandleCommand("help", hex.Topic, hex.Message);
			}
		}
	}
}
