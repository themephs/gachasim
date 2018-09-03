using GachaSim.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GachaSim.Cli
{
	public class Arguments
	{
		public int Groups { get; set; }
		public int Iterations { get; set; }
		public string Banner { get; set; }
		public string Pull { get; set; }
		public SeedingRefresh RngRefresh { get; set; }
		public SeedingMethod RngMethod { get; set; }
	}
}
