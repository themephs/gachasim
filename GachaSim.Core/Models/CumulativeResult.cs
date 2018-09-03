using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GachaSim.Core.Models
{
	public class CumulativeResult
	{
		public List<GroupResult> Groups { get; set; }

		public int MinRainbows => this.Groups.Min(grp => grp.Rainbows);
		public int MinOnBanner => this.Groups.Min(grp => grp.OnBannerRainbows);

		public int MinOnBannerCount => this.Groups.Count(grp => grp.OnBannerRainbows == this.MinOnBanner);

		public int MinOffBanner => this.Groups.Min(grp => grp.Rainbows - grp.OnBannerRainbows);

		public double MinOnBannerPercentage => this.Groups.Min(grp => grp.OnBannerPercentage);
		public double MinOffBannerPercentage => this.Groups.Min(grp => grp.OffBannerPercentage);


		public int MaxRainbows => this.Groups.Max(grp => grp.Rainbows);
		public int MaxOnBanner => this.Groups.Max(grp => grp.OnBannerRainbows);
		public int MaxOffBanner => this.Groups.Max(grp => grp.Rainbows - grp.OnBannerRainbows);
		public double MaxOnBannerPercentage => this.Groups.Max(grp => grp.OnBannerPercentage);
		public double MaxOffBannerPercentage => this.Groups.Max(grp => grp.OffBannerPercentage);

		public CumulativeResult()
		{
			this.Groups = new List<GroupResult>();
		}

	}
}
