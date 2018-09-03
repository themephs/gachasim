using System.Collections.Generic;

using GachaSim.Core.Models;

namespace GachaSim.Core
{
	public interface IGacha
	{
		string Name { get; }

		string Code { get; }

		List<Unit> Pull(params Pull[] pulls);
	}
}
