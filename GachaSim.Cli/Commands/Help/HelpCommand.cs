using GachaSim.Core.Models;
using GachaSim.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GachaSim.Cli.Commands
{
	[CommandKey("help")]
	public class HelpCommand : ICommand
	{
		private readonly IOutputService _outputService;
		private readonly ConfigService _configService;

		public HelpCommand(IOutputService outputService, ConfigService cfgService)
		{
			this._outputService = outputService;
			this._configService = cfgService;
		}

		public void Execute(params string[] args)
		{
			string topic = args.FirstOrDefault() ?? "ALL";
			string prefixMessage = args.Skip(1).FirstOrDefault();

			if (!string.IsNullOrEmpty(prefixMessage))
			{
				this._outputService.WriteLine($"\t{prefixMessage}");
				this._outputService.WriteLine();
			}

			switch (topic)
			{
				case "ALL": this.All(); break;
				case "-b": this.Banners(); break;
				case "-i": this.Iterations(); break;
				case "-g": this.Groups(); break;
				case "-p": this.Pulls(); break;
				case "-M": this.SeedMethods(); break;
				case "-R": this.SeedRefreshes(); break;
				default:
					this._outputService.WriteLine($"ERROR: The topic \"{topic}\" does not have a help entry");
					this.All(); break;
			}
		}

		private void All()
		{
			this._outputService.WriteLine();
			this._outputService.WriteLine("\tgachasim (command) -[ipbgMR]<code>");
			this._outputService.WriteLine();
			this._outputService.WriteLine("Commands----------------");
			this._outputService.WriteLine();
			this._outputService.WriteLine("\thelp <topic>\t\tGets info on how to use a command or flag, or shows generic help if <topic> omitted");
			this._outputService.WriteLine();
			this._outputService.WriteLine("\trun\t\t\tExecutes a gacha simulation with any specified flags");
			this._outputService.WriteLine();
			this._outputService.WriteLine();

			this._outputService.WriteLine("Option flags----------------");
			this._outputService.WriteLine();
			this._outputService.WriteLine("\t-i<number>\t\tSets the number of iterations to run of the gacha");
			this._outputService.WriteLine();
			this._outputService.WriteLine("\t-p<code>\t\tSets the pull type to use for the gacha (each iteration will multiply multi-unit pulls)");			
			this._outputService.WriteLine();
			this._outputService.WriteLine("\t-b<code>\t\tSets the banner type to pull from");
			this._outputService.WriteLine();
			this._outputService.WriteLine("\t-M<code>\t\tSets the randomizer method");
			this._outputService.WriteLine();
			this._outputService.WriteLine("\t-R<code>\t\tSets the randomizer refresh style");
		}

		private void Iterations()
		{
			this._outputService.WriteLine();
			this._outputService.WriteLine("\tIteration flag (-i<number>)\t(example: gachasim run -i150)");
			this._outputService.WriteLine();
			this._outputService.WriteLine("\tSets the number of times the simulation will be repeated using the given parameters.");
			this._outputService.WriteLine("\tExample would run the simulation 150 times.");
		}

		private void Groups()
		{
			this._outputService.WriteLine();
			this._outputService.WriteLine("\tGrouping flag (-g<number>)\t(example: gachasim run -g20)");
			this._outputService.WriteLine();
			this._outputService.WriteLine("\tSets the number of groups of iterations the simulation will run to find cumulative statistical odds.");
			this._outputService.WriteLine("\tExample would run the simulation 20 times the number of iterations.");
		}

		private void Pulls()
		{
			this._outputService.WriteLine();
			this._outputService.WriteLine("\tPull type flag (-p<code>)\t(example: gachasim run -p10+1)");
			this._outputService.WriteLine();
			this._outputService.WriteLine("\tSpecifies the type of pull to use in each iteration.  The available pull types in CLI mode are shown below");
			this._outputService.WriteLine("\t(custom pull types are surrounded by square brackets, dont include the brackets when using the code):");
			this._outputService.WriteLine();
			
			foreach (var pullType in this._configService.GetAllPulls())
			{
				string codeDisplay = pullType.IsDefault ? pullType.Code : $"[{pullType.Code}]";
				this._outputService.WriteLine($"\t{codeDisplay}\t\t{pullType.Name}");				
				this._outputService.WriteLine();
			}
		}

		private void Banners()
		{
			this._outputService.WriteLine();
			this._outputService.WriteLine("\tBanner type flag (-b<code>)\t(example: gachasim run -bSTD)");
			this._outputService.WriteLine();
			this._outputService.WriteLine("\tSpecifies the type of banner to perform the specified pull from in each iteration.  Step-Up banners");
			this._outputService.WriteLine("\twill increment one step per iteration.  The available banner styles and their codes are as follows");
			this._outputService.WriteLine("\t(custom banners are surrounded by square brackets, don't include the brackets when using custom codes):");
			this._outputService.WriteLine();

			foreach (var gacha in this._configService.GetAllGachas())
			{
				string codeDisplay = gacha.IsDefault ? gacha.Code : $"[{gacha.Code}]";
				this._outputService.WriteLine($"\t{codeDisplay}\t\t{gacha.Name}");
				this._outputService.WriteLine();
			}
		}

		private void SeedMethods()
		{
			this._outputService.WriteLine();
			this._outputService.WriteLine("\tRandomization seeding method (-M<code>)\t(example: gachasim run -MGUID)");
			this._outputService.WriteLine();
			this._outputService.WriteLine("\tSpecifies the method that will be used to determine how the random number generator is seeded.");
			this._outputService.WriteLine("\tThe following codes are shown below:");
			this._outputService.WriteLine();
			this._outputService.WriteLine("\tCLOCK\tUses current system clock time to seed the randomizer (default seed method)");
			this._outputService.WriteLine();
			this._outputService.WriteLine("\tGUID\tUses a randomly generated unique GUID to seed the randomizer");
			this._outputService.WriteLine();
			this._outputService.WriteLine("\tINC\tUses an incremental seed that persists between iterations and runs of this application");

		}

		private void SeedRefreshes()
		{
			this._outputService.WriteLine();
			this._outputService.WriteLine("\tRandomization re-seeding rate (-R<code>)\t(example: gachasim run -RPULL)");
			this._outputService.WriteLine();
			this._outputService.WriteLine("\tSpecifies the rate at which the random number generator will re-seed.");
			this._outputService.WriteLine("\tThe following codes are shown below:");
			this._outputService.WriteLine();
			this._outputService.WriteLine("\tPULL\tSeeds a new randomizer before each unique pull (default)");
			this._outputService.WriteLine();
			this._outputService.WriteLine("\tUNIT\tSeeds a new randomizer every unit that is pulled");			
			this._outputService.WriteLine();
			this._outputService.WriteLine("\tTIMED\tOnly reseeds the randomizer every 10 minutes");
			this._outputService.WriteLine();
			this._outputService.WriteLine("\tNEVER\tUses the same seed once the application starts until its lifetime ends");
		}
	}
}
