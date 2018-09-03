using GachaSim.Core;
using GachaSim.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GachaSim.Cli
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				if (args.Length == 0)
				{
					HandleCommand_Help("ALL");
				}
				else
				{
					HandleCommand(args.First(), args.Skip(1).ToArray());
				}
			}
			catch (HelpException hex)
			{
				HandleCommand_Help(hex.Topic, hex.Message);
			}			
		}

		private static void HandleCommand(string cmd, params string[] args)
		{
			switch (cmd)
			{
				case "help": HandleCommand_Help(args.FirstOrDefault()); break;
				case "run": HandleCommand_Run(ReadArguments(args)); break;
			}
		}

		private static void HandleCommand_Help(string topic, string prefixMessage = null)
		{
			if (string.IsNullOrEmpty(topic))
			{
				topic = "ALL";
			}

			switch (topic)
			{
				case "ALL": Help.Help_All(); break;
				case "-b": Help.Help_Banners(); break;
				case "-i": Help.Help_Iterations(); break;
				case "-g": Help.Help_Groups(); break;
				case "-p": Help.Help_Pulls(); break;
				case "-M": Help.Help_Methods(); break;
				case "-R": Help.Help_Refreshes(); break;				
			}
		}

		private static void HandleCommand_Run(Arguments args)
		{
			Console.WriteLine("Running simulation, this may take a while.  Hang on...");
			Console.WriteLine();

			Randomizer.Refresh = args.RngRefresh;
			Randomizer.Method = args.RngMethod;
						
			int groups = Math.Max(0, args.Groups);		
			
			if (groups > 0)
			{
				CumulativeResult result = new CumulativeResult();

				for (int i = 0; i < groups; i++)
				{
					result.Groups.Add(RunGroup(args, false, i+1));
				}

				PrintCumulative(args, result);
			}
			else
			{
				RunGroup(args);
			}
		}

		private static void PrintCumulative(Arguments args, CumulativeResult result)
		{
			var banner = Defaults.Gachas.FirstOrDefault(g => g.Code == (args.Banner ?? "STD"));
			var pull = Defaults.PullTypes.FirstOrDefault(p => p.Code == (args.Pull ?? "3*"));

			Console.WriteLine($"Results out of {args.Groups} sets of {args.Iterations}");
			Console.WriteLine("-----------------------------------------------------------");
			Console.WriteLine($"Banner: {banner.Name}");
			Console.WriteLine($"Pull: {pull.Name}");
			Console.WriteLine("-----------------------------------------------------------");
			Console.WriteLine($"Min Rainbows: {result.MinRainbows}");
			Console.WriteLine($"Min On-Banner Rainbows: {result.MinOnBanner} ({result.MinOnBannerPercentage}%) - ({result.MinOnBannerCount} times)");
			Console.WriteLine($"Min Off-Banner Rainbows: {result.MinOffBanner} ({result.MinOffBannerPercentage}%)");

			Console.WriteLine("-----------------------------------------------------------");
			Console.WriteLine($"Max Rainbows: {result.MaxRainbows}");
			Console.WriteLine($"Max On-Banner Rainbows: {result.MaxOnBanner} ({result.MaxOnBannerPercentage}%)");
			Console.WriteLine($"Max Off-Banner Rainbows: {result.MaxOffBanner} ({result.MaxOffBannerPercentage}%)");
		}

		private static GroupResult RunGroup(Arguments args, bool output = true, int groupNum = 1)
		{
			var banner = Defaults.Gachas.FirstOrDefault(g => g.Code == (args.Banner ?? "STD"));
			var pull = Defaults.PullTypes.FirstOrDefault(p => p.Code == (args.Pull ?? "3*"));
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
				var pullResults = banner.Pull(pull);

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
				Console.WriteLine($"Results out of {args.Iterations} iterations ({result.Total} units pulled)...");
				Console.WriteLine("-----------------------------------------------------------");
				Console.WriteLine($"Banner: {banner.Name}");
				Console.WriteLine($"Pull: {pull.Name}");
				Console.WriteLine("-----------------------------------------------------------");
				Console.WriteLine($"Blues: {result.Blues} ({result.BluePercentage}%)");
				Console.WriteLine($"Golds: {result.Golds} ({result.GoldPercentage}%)");
				Console.WriteLine($"Rainbows: {result.Rainbows} ({result.RainbowPercentage}%)");
				Console.WriteLine("-----------------------------------------------------------");
				Console.WriteLine($"Rainbow (Off Banner): {result.OffBannerRainbows} ({result.OffBannerPercentage})");
				Console.WriteLine($"Rainbow (On Banner): {result.OnBannerRainbows} ({result.OnBannerPercentage}%)");
			}

			return result;
		}
		
		private static Arguments ReadArguments(params string[] args)
		{
			Arguments arguments = new Arguments();

			foreach (var arg in args.Where(a => a.StartsWith("-")).Select(a => a.Substring(1)))
			{
				switch (arg.Substring(0, 1))
				{
					case "i": arguments.Iterations = int.Parse(arg.Substring(1)); break;
					case "g": arguments.Groups = int.Parse(arg.Substring(1)); break;
					case "p": arguments.Pull = CheckPullCode(arg.Substring(1).ToUpper()); break;
					case "b": arguments.Banner = CheckBannerCode(arg.Substring(1).ToUpper()); break;
					case "M": arguments.RngMethod = ParseSeedMethod(arg.Substring(1).ToUpper()); break;
					case "R": arguments.RngRefresh = ParseSeedRefresh(arg.Substring(1).ToUpper()); break;
				}
			}

			return arguments;
		}

		private static string CheckPullCode(string code)
		{
			if (!Defaults.PullTypes.Any(pull => pull.Code == code))
				throw new HelpException("-p", "Unknown pull code, the following pulls are available:");

			return code;
		}

		private static string CheckBannerCode(string code)
		{
			if (!Defaults.Gachas.Any(pull => pull.Code == code))
				throw new HelpException("-b", "Unknown banner code, the following banners are available:");

			return code;
		}

		private static SeedingMethod ParseSeedMethod(string code)
		{
			switch (code)
			{
				case "C":
				case "CLOCK":
				case "TICKS":
				case "TICK":
				case "TIME":
					return SeedingMethod.Clock;
				case "G":
				case "ID":
				case "GUID":
					return SeedingMethod.Guid;
				case "I":
				case "INC":
				case "INCREMENT":
				case "INCREMENTAL":
					return SeedingMethod.Incremental;
				default:
					throw new HelpException("-M", "Invalid method code.  The following options are available:");
			}
		}

		private static SeedingRefresh ParseSeedRefresh(string code)
		{
			switch (code)
			{
				case "U":
				case "UNIT":
				case "E":
				case "EVERY":
					return SeedingRefresh.EveryUnit;
				case "P":
				case "PULL":
					return SeedingRefresh.EveryPull;
				case "T":
				case "TIME":
				case "TIMED":
					return SeedingRefresh.Timed;
				case "N":
				case "NEV":
				case "NEVER":
					return SeedingRefresh.Never;
				default:
					throw new HelpException("-R", "Invalid refresh code.  The following options are available:");
			}
		}
	}
}
