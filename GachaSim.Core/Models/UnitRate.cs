using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GachaSim.Core.Models
{
	public class UnitRate : Unit
	{

		/// <summary>
		/// The default pull rate of this unit type.
		/// </summary>
		public double Rate { get; set; }		

		public UnitRate() { }

		public UnitRate(UnitRate rateToClone)
		{
			this.Rate = rateToClone.Rate;
			this.Name = rateToClone.Name;
			this.Rating = rateToClone.Rating;
			this.Relevance = rateToClone.Relevance;
		}
	}
}
