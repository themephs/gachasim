using GachaSim.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GachaSim.Cli
{
	public static class Help
	{
		public static void Help_All()
		{
			Console.WriteLine();
			Console.WriteLine("\tgachasim (command) -[ipbMR]<code>");
			Console.WriteLine();
			Console.WriteLine("Commands----------------");
			Console.WriteLine();
			Console.WriteLine("\thelp <topic>\t\tGets info on how to use a command or flag, or shows generic help if <topic> omitted");
			Console.WriteLine();
			Console.WriteLine("\trun\t\t\tExecutes a gacha simulation with any specified flags");
			Console.WriteLine();
			Console.WriteLine();

			Console.WriteLine("Option flags----------------");
			Console.WriteLine();
			Console.WriteLine("\t-i<number>\t\tSets the number of iterations to run of the gacha");
			Console.WriteLine();
			Console.WriteLine("\t-p<code>\t\tSets the pull type to use for the gacha (each iteration will multiply multi-unit pulls)");			
			Console.WriteLine();
			Console.WriteLine("\t-b<code>\t\tSets the banner type to pull from");
			Console.WriteLine();
			Console.WriteLine("\t-M<code>\t\tSets the randomizer method");
			Console.WriteLine();
			Console.WriteLine("\t-R<code>\t\tSets the randomizer refresh style");
		}

		public static void Help_Iterations()
		{
			Console.WriteLine();
			Console.WriteLine("\tIteration flag (-i<number>)\t(example: gachasim run -i150)");
			Console.WriteLine();
			Console.WriteLine("\tSets the number of times the simulation will be repeated using the given parameters.");
			Console.WriteLine("\tExample would run the simulation 150 times.");
		}

		public static void Help_Groups()
		{
			Console.WriteLine();
			Console.WriteLine("\tGrouping flag (-g<number>)\t(example: gachasim run -g20)");
			Console.WriteLine();
			Console.WriteLine("\tSets the number of groups of iterations the simulation will run to find cumulative statistical odds.");
			Console.WriteLine("\tExample would run the simulation 20 times the number of iterations.");
		}

		public static void Help_Pulls()
		{
			Console.WriteLine();
			Console.WriteLine("\tPull type flag (-p<code>)\t(example: gachasim run -p10+1)");
			Console.WriteLine();
			Console.WriteLine("\tSpecifies the type of pull to use in each iteration.  The available pull types in CLI mode are shown below:");
			Console.WriteLine();
			
			foreach (var pullType in Defaults.PullTypes)
			{
				Console.WriteLine($"\t{pullType.Code}\t{pullType.Name}");
				Console.WriteLine();
			}
		}

		public static void Help_Banners()
		{
			Console.WriteLine();
			Console.WriteLine("\tBanner type flag (-b<code>)\t(example: gachasim run -bSTD)");
			Console.WriteLine();
			Console.WriteLine("\tSpecifies the type of banner to perform the specified pull from in each iteration.  Step-Up banners");
			Console.WriteLine("\twill increment one step per iteration.  The available banner styles and their codes are shown below:");
			Console.WriteLine();

			foreach (var gacha in Defaults.Gachas)
			{
				Console.WriteLine($"\t{gacha.Code}\t{gacha.Name}");
				Console.WriteLine();
			}
		}

		public static void Help_Methods()
		{
			Console.WriteLine();
			Console.WriteLine("\tRandomization seeding method (-M<code>)\t(example: gachasim run -MGUID)");
			Console.WriteLine();
			Console.WriteLine("\tSpecifies the method that will be used to determine how the random number generator is seeded.");
			Console.WriteLine("\tThe following codes are shown below:");
			Console.WriteLine();
			Console.WriteLine("\tCLOCK\tUses current system clock time to seed the randomizer (default seed method)");
			Console.WriteLine();
			Console.WriteLine("\tGUID\tUses a randomly generated unique GUID to seed the randomizer");
			Console.WriteLine();
			Console.WriteLine("\tINC\tUses an incremental seed that persists between iterations and runs of this application");

		}

		public static void Help_Refreshes()
		{
			Console.WriteLine();
			Console.WriteLine("\tRandomization re-seeding rate (-R<code>)\t(example: gachasim run -RPULL)");
			Console.WriteLine();
			Console.WriteLine("\tSpecifies the rate at which the random number generator will re-seed.");
			Console.WriteLine("\tThe following codes are shown below:");
			Console.WriteLine();
			Console.WriteLine("\tPULL\tSeeds a new randomizer before each unique pull (default)");
			Console.WriteLine();
			Console.WriteLine("\tUNIT\tSeeds a new randomizer every unit that is pulled");			
			Console.WriteLine();
			Console.WriteLine("\tTIMED\tOnly reseeds the randomizer every 10 minutes");
			Console.WriteLine();
			Console.WriteLine("\tNEVER\tUses the same seed once the application starts until its lifetime ends");


		}
	}
}
