using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GachaSim.Core.Models
{
	/// <summary>
	/// Represents a generic unit type and relevance that can be pulled from a gacha
	/// </summary>
    public class Unit : IUnit
    {
		/// <summary>
		/// A name to identify the unit type
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// The rating of the unit (blue, gold, or rainbow)
		/// </summary>
		public UnitRating Rating { get; set; }
		/// <summary>
		/// The relevance to current feature (is the unit off or on banner?)
		/// </summary>
		public UnitRelevance Relevance { get; set; }

		public string DictionaryKey => string.Format("{0},{1}", (int)this.Rating, (int)this.Relevance);
	}
}
