using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GachaSim.Core.Models
{
	/// <summary>
	/// Defines a number of units in a pull series and attaches an optional modifier to a specific rating of unit
	/// </summary>
    public class PullModifier
    {
		/// <summary>
		/// The number of units to implement in the parent pull
		/// </summary>
		public int UnitCount { get; set; }

		/// <summary>
		/// List of changed rates that will replace the default gacha rates when the pull this modifier is attached to is used
		/// </summary>
		public List<UnitRate> Modifications { get; set; }

		public PullModifier()
		{
			this.Modifications = new List<UnitRate>();
		}
    }
}
