using System;
using System.Collections.Generic;
using System.Linq;

using GachaSim.Core.Models;

namespace GachaSim.Core
{
    public class Banner : IGacha
    {
		public string Name { get; set; }
		public string Code { get; set; }		
		public bool IsDefault { get; set; }

		public List<UnitRate> Rates { get; set; }

		public virtual List<Unit> Pull(params Pull[] pulls)
		{
			List<Unit> results = new List<Unit>();

			foreach (var pull in pulls)
			{
				results.AddRange(this.ExecutePull(pull));
			}

			return results;
		}

		protected List<Unit> ExecutePull(Pull pull)
		{
			List<Unit> result = new List<Unit>();

			foreach (var mod in pull.Modifiers)
			{				
				var rates = this.GetModifiedRates(mod.Modifications);

				for (int i = 0; i < mod.UnitCount; i++)
				{
					result.Add(this.PullUnit(i == 0, rates));
				}
			}

			return result;
		}

		private Unit PullUnit(bool forceRandomRefresh, params UnitRate[] rates)
		{
			double floor = 0;
			double roll = Randomizer.GetNextRandomValue(Randomizer.Refresh == SeedingRefresh.EveryPull && forceRandomRefresh);

			foreach (var rate in rates)
			{
				floor += rate.Rate;

				if (roll <= floor)
					return rate as Unit;				
			}

			throw new Exception($"Failed to hit a unit within expected rates.  Floor was {floor} when pull finished");
		}

		private UnitRate[] GetModifiedRates(List<UnitRate> modifications)
		{
			Dictionary<string, UnitRate> rates = this.Rates.ToDictionary(ur => ur.DictionaryKey, ur => new UnitRate(ur));

			foreach(var mod in modifications.OrderByDescending(m => m.Rating).ThenBy(m => m.Relevance))
			{
				string modkey = mod.DictionaryKey;

				if (rates.ContainsKey(modkey))
				{
					rates[modkey].Rate = mod.Rate;					
				}
				else
				{
					throw new Exception($"Invalid pull modifier key {modkey}");
				}
			}

			var blueRate = 1.0 - rates.Where(ent => ent.Key != "3,0").Sum(ent => ent.Value.Rate);

			if (blueRate < 0)
				throw new Exception("Rating overflow, total rating of all modifiers cannot exceed 100%");

			rates["3,0"].Rate = blueRate;

			return rates.Values.OrderBy(m => m.Rating).ThenBy(m => m.Relevance).ToArray();
		}
    }
}
