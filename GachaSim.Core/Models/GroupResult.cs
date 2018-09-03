using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GachaSim.Core.Models
{
	public class GroupResult
	{
		public int Number { get; set; }

		public int Blues { get; set; }
		public int Golds { get; set; }
		public int Rainbows { get; set; }
		public int OnBannerRainbows { get; set; }

		public int OffBannerRainbows => this.Rainbows - this.OnBannerRainbows;

		public int Total => this.Blues + this.Golds + this.Rainbows;

		public double BluePercentage => this.GetPercentageOf(this.Blues);
		public double GoldPercentage => this.GetPercentageOf(this.Golds);
		public double RainbowPercentage => this.GetPercentageOf(this.Rainbows);
		public double OffBannerPercentage => this.GetPercentageOf(this.Rainbows - this.OnBannerRainbows);
		public double OnBannerPercentage => this.GetPercentageOf(this.OnBannerRainbows);

		private double GetPercentageOf(int percentageOf) => (percentageOf / (double)this.Total) * 100;

	}
}
