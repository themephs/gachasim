using System.Collections.Generic;

using GachaSim.Core.Models;

namespace GachaSim.Core
{
	/// <summary>
	/// Represents a single pull type that can be used in simulations (can represent 10+1, tickets etc)...
	/// </summary>
    public class Pull : IConfigurable
    {
		/// <summary>
		/// The label to display for this pull
		/// </summary>
		public string Name { get; set; }

		public string Code { get; set; }

		public bool IsDefault { get; set; }

		/// <summary>
		/// A collection of <see cref="PullModifier"/> entities which determine the number and any rate changes implemented by this pull
		/// </summary>
		public List<PullModifier> Modifiers { get; set; }

		public Pull()
		{
			this.Modifiers = new List<PullModifier>();
		}
    }
}
