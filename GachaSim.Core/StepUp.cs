using System;
using System.Collections.Generic;
using System.Linq;

using GachaSim.Core.Models;

namespace GachaSim.Core
{
    public class StepUp : Banner
    {
		public List<Pull> Steps { get; set; }

		private Pull CurrentStep { get; set; }

		public override List<Unit> Pull(params Pull[] pulls)
		{
			if (this.Steps.Count == 0)
				throw new Exception($"Step-Up {this.Name} has no steps");

			if (this.CurrentStep == null)
			{
				this.CurrentStep = this.Steps.First();
			}

			var result = this.ExecutePull(this.CurrentStep);

			int nextIndex = (Steps.IndexOf(this.CurrentStep) + 1) % this.Steps.Count;
			this.CurrentStep = this.Steps[nextIndex];

			return result;
		}
    }
}
