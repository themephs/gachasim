using System.Collections.Generic;

using GachaSim.Core.Models;

namespace GachaSim.Core
{
	public interface IConfigurable
	{
		string Name { get; set; }
		string Code { get; }

		bool IsDefault { get; set; }
	}
}
