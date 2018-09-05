using GachaSim.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GachaSim.Cli.Commands
{
	public class RunArguments
	{
		public int Groups { get; set; }
		public int Iterations { get; set; }
		public string Banner { get; set; }
		public string Pull { get; set; }
		public SeedingRefresh RngRefresh { get; set; }
		public SeedingMethod RngMethod { get; set; }

		public static RunArguments From(params string[] args)
		{
			RunArguments arguments = new RunArguments();

			foreach (var arg in args.Where(a => a.StartsWith("-")).Select(a => a.Substring(1)))
			{
				switch (arg.Substring(0, 1))
				{
					case "i": arguments.Iterations = int.Parse(arg.Substring(1)); break;
					case "g": arguments.Groups = int.Parse(arg.Substring(1)); break;
					case "p": arguments.Pull = arg.Substring(1).ToUpper(); break;
					case "b": arguments.Banner = arg.Substring(1).ToUpper(); break;
					case "M": arguments.RngMethod = ParseSeedMethod(arg.Substring(1).ToUpper()); break;
					case "R": arguments.RngRefresh = ParseSeedRefresh(arg.Substring(1).ToUpper()); break;
				}
			}

			return arguments;
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
