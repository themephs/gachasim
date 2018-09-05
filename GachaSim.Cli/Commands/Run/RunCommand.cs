using System;
using System.Collections.Generic;
using System.Linq;

using GachaSim.Core;
using GachaSim.Core.Models;
using GachaSim.Core.Services;

namespace GachaSim.Cli.Commands
{
	[CommandKey("run")]
	public class RunCommand : ICommand
	{
		private readonly IOutputService _outputService;
		private readonly ConfigService _configService;

		private const string DEFAULT_BANNER = "STD";
		private const string DEFAULT_PULL = "3*";

		private IGacha SelectedGacha { get; set; }
		private Pull SelectedPull { get; set; }

		public RunCommand(IOutputService outputService, ConfigService cfgService)
		{
			this._outputService = outputService;
			this._configService = cfgService;
		}

		public void Execute(params string[] args)
		{
			var arguments = RunArguments.From(args);
						
			Randomizer.Refresh = arguments.RngRefresh;
			Randomizer.Method = arguments.RngMethod;

			this.SelectedGacha = this._configService.GetGacha(arguments.Banner ?? "STD");
			this.SelectedPull = this._configService.GetPull(arguments.Pull ?? "3*");

			if (this.SelectedGacha == null)
				throw new HelpException("-b", $"Unknown banner code {arguments.Banner}");

			if (this.SelectedPull == null)
				throw new HelpException("-p", $"Unknown pull code {arguments.Banner}");

			int groups = Math.Max(0, arguments.Groups);

			if (groups > 0)
			{
				this._outputService.Write("Running simulation, group {0} of {1}, iteration {2} of {3}", 0, arguments.Groups, 0, arguments.Iterations);
				CumulativeResult result = new CumulativeResult();

				for (int i = 0; i < groups; i++)
				{
					result.Groups.Add(this.RunGroup(arguments, false, i + 1));
				}

				this.PrintCumulative(arguments, result);
			}
			else
			{
				this._outputService.Write("Running simulation, iteration {0} of {1}", 0, arguments.Iterations);
				this.RunGroup(arguments);
			}
		}


		private void PrintCumulative(RunArguments args, CumulativeResult result)
		{			
			this._outputService.Write("\r                                                                                                     \n");
			this._outputService.WriteLine($"Results out of {args.Groups} sets of {args.Iterations}");
			this._outputService.WriteLine("-----------------------------------------------------------");
			this._outputService.WriteLine($"Banner: {this.SelectedGacha.Name}");
			this._outputService.WriteLine($"Pull: {this.SelectedPull.Name}");
			this._outputService.WriteLine("-----------------------------------------------------------");
			this._outputService.WriteLine($"Min Rainbows: {result.MinRainbows}");
			this._outputService.WriteLine($"Min On-Banner Rainbows: {result.MinOnBanner} ({result.MinOnBannerPercentage}%) - ({result.MinOnBannerCount} times)");
			this._outputService.WriteLine($"Min Off-Banner Rainbows: {result.MinOffBanner} ({result.MinOffBannerPercentage}%)");

			this._outputService.WriteLine("-----------------------------------------------------------");
			this._outputService.WriteLine($"Max Rainbows: {result.MaxRainbows}");
			this._outputService.WriteLine($"Max On-Banner Rainbows: {result.MaxOnBanner} ({result.MaxOnBannerPercentage}%)");
			this._outputService.WriteLine($"Max Off-Banner Rainbows: {result.MaxOffBanner} ({result.MaxOffBannerPercentage}%)");
		}

		private GroupResult RunGroup(RunArguments args, bool output = true, int groupNum = 1)
		{			
			int iterations = Math.Max(1, args.Iterations);

			Dictionary<string, int> results = new Dictionary<string, int>
			{
				{ "3,0", 0 },
				{ "4,0", 0 },
				{ "5,1", 0 },
				{ "5,2", 0 }
			};

			for (int i = 0; i < iterations; i++)
			{
				if (!output)
				{
					this._outputService.Write("\rRunning simulation, group {0} of {1}, iteration {2} of {3}", groupNum, args.Groups, i+1, iterations);
				}
				else
				{
					this._outputService.Write("\rRunning simulation, iteration {0} of {1}", i+1, iterations);
				}

				var pullResults = this.SelectedGacha.Pull(this.SelectedPull);

				foreach (var pullResult in pullResults)
				{
					results[pullResult.DictionaryKey]++;
				}
			}

			GroupResult result = new GroupResult
			{
				Blues = results["3,0"],
				Golds = results["4,0"],
				Rainbows = results["5,1"] + results["5,2"],
				OnBannerRainbows = results["5,2"],
				Number = groupNum
			};

			if (output)
			{
				this._outputService.Write("\r                                                                                                     \n");
				this._outputService.WriteLine($"Results out of {args.Iterations} iterations ({result.Total} units pulled)...");
				this._outputService.WriteLine("-----------------------------------------------------------");
				this._outputService.WriteLine($"Banner: {this.SelectedGacha.Name}");
				this._outputService.WriteLine($"Pull: {this.SelectedPull.Name}");
				this._outputService.WriteLine("-----------------------------------------------------------");
				this._outputService.WriteLine($"Blues: {result.Blues} ({result.BluePercentage}%)");
				this._outputService.WriteLine($"Golds: {result.Golds} ({result.GoldPercentage}%)");
				this._outputService.WriteLine($"Rainbows: {result.Rainbows} ({result.RainbowPercentage}%)");
				this._outputService.WriteLine("-----------------------------------------------------------");
				this._outputService.WriteLine($"Rainbow (Off Banner): {result.OffBannerRainbows} ({result.OffBannerPercentage}%)");
				this._outputService.WriteLine($"Rainbow (On Banner): {result.OnBannerRainbows} ({result.OnBannerPercentage}%)");
			}

			return result;
		}
	}
}
