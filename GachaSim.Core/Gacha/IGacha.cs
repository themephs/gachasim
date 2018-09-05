using System.Collections.Generic;

using GachaSim.Core.Models;

namespace GachaSim.Core
{
	public interface IGacha : IConfigurable
	{
		List<Unit> Pull(params Pull[] pulls);
	}
}
