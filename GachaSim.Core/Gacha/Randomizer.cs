using System;
using System.Collections.Generic;
using System.Linq;

using GachaSim.Core.Models;

namespace GachaSim.Core
{
	public static class Randomizer
	{
		public static SeedingMethod Method { get; set; }
		public static SeedingRefresh Refresh { get; set; }

		private static int Incrementer = 1;
		private static int TimedRefreshMinutes = 10;
		private static DateTime LastRefresh = DateTime.Now;

		private static bool RequiresRefresh
		{
			get
			{
				return Current == null ||
					   (
							Refresh == SeedingRefresh.EveryUnit ||
							(
								Refresh == SeedingRefresh.Timed &&
								LastRefresh.AddMinutes(TimedRefreshMinutes) < DateTime.Now
							)
					   );
			}
		}

		private static Random Current { get; set; }

		public static double GetNextRandomValue(bool forceRefresh = false)
		{
			if (forceRefresh || RequiresRefresh)
			{
				Current = GenerateRandomizer();
			}

			return Current.NextDouble();
		}

		private static Random GenerateRandomizer()
		{
			switch (Method)
			{
				case SeedingMethod.Clock: return new Random();
				case SeedingMethod.Guid: return new Random(Guid.NewGuid().GetHashCode());
				case SeedingMethod.Incremental: return new Random(Incrementer++ % int.MaxValue);
				default:
					throw new Exception($"Unknown seeding method {Method.ToString()}");
			}
		}
	}
}
