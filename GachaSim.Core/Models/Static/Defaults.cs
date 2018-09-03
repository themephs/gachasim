using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GachaSim.Core.Models
{
	public static class Defaults
	{
		public static readonly List<UnitRate> DefaultRate_Standard = new List<UnitRate>
		{
			new UnitRate
			{
				Name = "Off Banner Rainbow",
				Rating = UnitRating.Rainbow,
				Relevance = UnitRelevance.OffBanner,
				Rate = 0.02
			},
			new UnitRate
			{
				Name = "On Banner Rainbow",
				Rating = UnitRating.Rainbow,
				Relevance = UnitRelevance.OnBanner,
				Rate = 0.01
			},
			new UnitRate
			{
				Name = "Gold",
				Rating = UnitRating.Gold,
				Rate = 0.19
			},
			new UnitRate
			{
				Name = "Blue",
				Rating = UnitRating.Blue,
				Rate = 0.78
			}
		};

		public static readonly List<IGacha> Gachas = new List<IGacha>
		{
			new Banner
			{
				Code = "STD",
				Name = "Solo Feature Banner",
				Rates = DefaultRate_Standard
			},

			//new Banner
			//{
			//	Code = "SPL",
			//	Name = "Split Feature Banner",
			//	Rates = new List<UnitRate>
			//	{
			//		new UnitRate
			//		{
			//			Name = "Off Banner Rainbow",
			//			Rating = UnitRating.Rainbow,
			//			Relevance = UnitRelevance.OffBanner,
			//			Rate = 0.02
			//		},
			//		new UnitRate
			//		{
			//			Name = "On Banner Rainbow 1",
			//			Rating = UnitRating.Rainbow,
			//			Relevance = UnitRelevance.OnBanner,
			//			Rate = 0.005
			//		},
			//		new UnitRate
			//		{
			//			Name = "On Banner Rainbow 2",
			//			Rating = UnitRating.Rainbow,
			//			Relevance = UnitRelevance.OnBanner,
			//			Rate = 0.005
			//		},
			//		new UnitRate
			//		{
			//			Name = "Gold",
			//			Rating = UnitRating.Gold,
			//			Relevance = UnitRelevance.Undefined,
			//			Rate = 0.19
			//		},
			//		new UnitRate
			//		{
			//			Name = "Blue",
			//			Rating = UnitRating.Blue,
			//			Relevance = UnitRelevance.Undefined,
			//			Rate = 0.78
			//		}
			//	}
			//},

			new Banner
			{
				Code = "5RR",
				Name = "Rate-Up Banner (5% Rainbow Rate, One Feature Unit)",
				Rates = new List<UnitRate>
				{
					new UnitRate
					{
						Name = "Off Banner Rainbow",
						Rating = UnitRating.Rainbow,
						Relevance = UnitRelevance.OffBanner,
						Rate = 0.0125
					},
					new UnitRate
					{
						Name = "On Banner Rainbow",
						Rating = UnitRating.Rainbow,
						Relevance = UnitRelevance.OnBanner,
						Rate = 0.0375
					},
					new UnitRate
					{
						Name = "Gold",
						Rating = UnitRating.Gold,
						Relevance = UnitRelevance.Undefined,
						Rate = 0.19
					},
					new UnitRate
					{
						Name = "Blue",
						Rating = UnitRating.Blue,
						Relevance = UnitRelevance.Undefined,
						Rate = 0.76
					}
				}
			},

			new StepUp
			{
				Code = "25S",
				Name = "25K Lapis Step-Up (Split Banner)",
				Rates = DefaultRate_Standard,
				Steps = new List<Pull>
				{
					new Pull
					{
						Name = "Step 1 (10 + 1 Pull)",
						Modifiers = new List<PullModifier>
						{
							new PullModifier
							{
								UnitCount = 10,								
							},
							new PullModifier
							{
								UnitCount = 1,
								Modifications = new List<UnitRate>
								{
									new UnitRate
									{
										Name = "On Banner Rainbow",
										Rating = UnitRating.Rainbow,
										Relevance = UnitRelevance.OnBanner,
										Rate = 0.02
									},
									new UnitRate
									{
										Name = "Off Banner Rainbow",
										Rating = UnitRating.Rainbow,
										Relevance = UnitRelevance.OnBanner,
										Rate = 0.03
									},
									new UnitRate
									{
										Name = "Gold",
										Rating = UnitRating.Gold,										
										Rate = 0.95
									}
								}
							},
						}
					},
					new Pull
					{
						Name = "Step 2 (10 Pulls, +1 Guaranteed Banner Gold)",
						Modifiers = new List<PullModifier>
						{
							new PullModifier
							{
								UnitCount = 10,
							},
							new PullModifier
							{
								UnitCount = 1,
								Modifications = new List<UnitRate>
								{									
									new UnitRate
									{
										Name = "Gold",
										Rating = UnitRating.Gold,
										Rate = 1.0
									},
									new UnitRate
									{
										Name = "On Banner Rainbow",
										Rating = UnitRating.Rainbow,
										Relevance = UnitRelevance.OnBanner,
										Rate = 0
									},
									new UnitRate
									{
										Name = "Off Banner Rainbow",
										Rating = UnitRating.Rainbow,
										Relevance = UnitRelevance.OffBanner,
										Rate = 0
									}
								}
							},
						}
					},
					new Pull
					{
						Name = "Step 3 (10 Pulls, +1 Guaranteed Rainbow)",
						Modifiers = new List<PullModifier>
						{
							new PullModifier
							{
								UnitCount = 10,
							},
							new PullModifier
							{
								UnitCount = 1,
								Modifications = new List<UnitRate>
								{
									new UnitRate
									{
										Name = "Off Banner Rainbow",
										Rating = UnitRating.Rainbow,
										Relevance = UnitRelevance.OffBanner,
										Rate = 1.0
									},
									new UnitRate
									{
										Name = "Gold",
										Rating = UnitRating.Gold,
										Rate = 0
									},
									new UnitRate
									{
										Name = "On Banner Rainbow",
										Rating = UnitRating.Rainbow,
										Relevance = UnitRelevance.OnBanner,
										Rate = 0
									},
								}
							},
						}
					},
					new Pull
					{
						Name = "Step 4 (10 + 1 Pull)",
						Modifiers = new List<PullModifier>
						{
							new PullModifier
							{
								UnitCount = 10
							},
							new PullModifier
							{
								UnitCount = 1,
								Modifications = new List<UnitRate>
								{
									new UnitRate
									{
										Name = "On Banner Rainbow",
										Rating = UnitRating.Rainbow,
										Relevance = UnitRelevance.OnBanner,
										Rate = 0.02
									},
									new UnitRate
									{
										Name = "Off Banner Rainbow",
										Rating = UnitRating.Rainbow,
										Relevance = UnitRelevance.OnBanner,
										Rate = 0.03
									},
									new UnitRate
									{
										Name = "Gold",
										Rating = UnitRating.Gold,
										Rate = 0.95
									}
								}
							},
						}
					},
					new Pull
					{
						Name = "Step 5 (10 Pulls, +1 Guaranteed Banner Rainbow)",
						Modifiers = new List<PullModifier>
						{
							new PullModifier
							{
								UnitCount = 10
							},
							new PullModifier
							{
								UnitCount = 1,
								Modifications = new List<UnitRate>
								{
									new UnitRate
									{
										Name = "On Banner Rainbow",
										Rating = UnitRating.Rainbow,
										Relevance = UnitRelevance.OnBanner,
										Rate = 1.0
									},
									new UnitRate
									{
										Name = "Gold",
										Rating = UnitRating.Gold,
										Rate = 0
									},
									new UnitRate
									{
										Name = "Off Banner Rainbow",
										Rating = UnitRating.Rainbow,
										Relevance = UnitRelevance.OffBanner,
										Rate = 0
									},
								}
							}
						}
					}
				}
			},

			new StepUp
			{
				Code = "11S",
				Name = "11K Lapis Step-Up",				
				Rates = DefaultRate_Standard,
				Steps = new List<Pull>
				{
					new Pull
					{
						Name = "Step 1 (1 Pull: 1.5x Rate)",
						Modifiers = new List<PullModifier>
						{
							new PullModifier
							{
								UnitCount = 1,
								Modifications = new List<UnitRate>
								{
									new UnitRate
									{
										Name = "Off Banner Rainbow",
										Rating = UnitRating.Rainbow,
										Relevance = UnitRelevance.OffBanner,
										Rate = 0.02
									},
									new UnitRate
									{
										Name = "On Banner Rainbow",
										Rating = UnitRating.Rainbow,
										Relevance = UnitRelevance.OnBanner,
										Rate = 0.015
									},
									new UnitRate
									{
										Name = "Blue",
										Rating = UnitRating.Blue,
										Relevance = UnitRelevance.Undefined,
										Rate = 0.775
									},
									new UnitRate
									{
										Name = "Gold",
										Rating = UnitRating.Gold,
										Relevance = UnitRelevance.Undefined,
										Rate = 0.19
									}
								}
							}
						}
					},
					new Pull
					{
						Name = "Step 2 (2 Pulls)",
						Modifiers = new List<PullModifier>
						{
							new PullModifier
							{
								UnitCount = 2								
							}
						}
					},
					new Pull
					{
						Name = "Step 3 (4 Pulls)",
						Modifiers = new List<PullModifier>
						{
							new PullModifier
							{
								UnitCount = 4
							}
						}
					},
					new Pull
					{
						Name = "Step 4 (6 Pulls)",
						Modifiers = new List<PullModifier>
						{
							new PullModifier
							{
								UnitCount = 6
							}
						}
					},
					new Pull
					{
						Name = "Step 5 (11 Pulls, 5x Rate Up)",						
						Modifiers = new List<PullModifier>
						{
							new PullModifier
							{
								UnitCount = 10,
								Modifications = new List<UnitRate>
								{
									new UnitRate
									{
										Name = "Off Banner Rainbow",
										Rating = UnitRating.Rainbow,
										Relevance = UnitRelevance.OffBanner,
										Rate = 0.02
									},
									new UnitRate
									{
										Name = "On Banner Rainbow",
										Rating = UnitRating.Rainbow,
										Relevance = UnitRelevance.OnBanner,
										Rate = 0.05
									}								
								}
							},
							new PullModifier
							{
								UnitCount = 1,
								Modifications = new List<UnitRate>
								{
									new UnitRate
									{
										Name = "Off Banner Rainbow",
										Rating = UnitRating.Rainbow,
										Relevance = UnitRelevance.OffBanner,
										Rate = 0.0125
									},
									new UnitRate
									{
										Name = "On Banner Rainbow",
										Rating = UnitRating.Rainbow,
										Relevance = UnitRelevance.OnBanner,
										Rate = 0.1875
									},									
									new UnitRate
									{
										Name = "Gold",
										Rating = UnitRating.Gold,
										Relevance = UnitRelevance.Undefined,
										Rate = 0.8
									}
								}
							}
						}
					}
				}
			}
		};

		public static readonly List<Pull> PullTypes = new List<Pull>
		{
			new Pull
			{
				Code = "3*",
				Name = "Rare Summon Ticket / Single Pull",				
				Modifiers = new List<PullModifier>
				{
					new PullModifier
					{
						UnitCount = 1
					}
				}
			},

			new Pull
			{
				Code = "4*",
				Name = "Guaranteed 4+ Ticket (5% Rainbow)",
				Modifiers = new List<PullModifier>
				{
					new PullModifier
					{
						UnitCount = 1,
						Modifications = new List<UnitRate>
						{
							new UnitRate
							{
								Name = "On Banner Rainbow",
								Rating = UnitRating.Rainbow,
								Relevance = UnitRelevance.OnBanner,
								Rate = 0.02
							},
							new UnitRate
							{
								Name = "Off Banner Rainbow",
								Rating = UnitRating.Rainbow,
								Relevance = UnitRelevance.OnBanner,
								Rate = 0.03
							},
							new UnitRate
							{
								Name = "Gold",
								Rating = UnitRating.Gold,
								Rate = 0.95
							}
						}
					}
				}
			},

			new Pull
			{
				Code = "10%",
				Name = "10% 5+ Ticket",
				Modifiers = new List<PullModifier>
				{
					new PullModifier
					{
						UnitCount = 1,
						Modifications = new List<UnitRate>
						{
							new UnitRate
							{
								Name = "On Banner Rainbow",
								Rating = UnitRating.Rainbow,
								Relevance = UnitRelevance.OnBanner,
								Rate = 0.067
							},
							new UnitRate
							{
								Name = "Off Banner Rainbow",
								Rating = UnitRating.Rainbow,
								Relevance = UnitRelevance.OnBanner,
								Rate = 0.033
							},
						}
					}
				}
			},

			new Pull
			{
				Code = "30%",
				Name = "30% 5+ Ticket",
				Modifiers = new List<PullModifier>
				{
					new PullModifier
					{
						UnitCount = 1,
						Modifications = new List<UnitRate>
						{
							new UnitRate
							{
								Name = "On Banner Rainbow",
								Rating = UnitRating.Rainbow,
								Relevance = UnitRelevance.OnBanner,
								Rate = 0.20
							},
							new UnitRate
							{
								Name = "Off Banner Rainbow",
								Rating = UnitRating.Rainbow,
								Relevance = UnitRelevance.OnBanner,
								Rate = 0.10
							},
						}
					}
				}
			},

			new Pull
			{
				Code = "10+1",
				Name = "10 + 1 Summon",
				Modifiers = new List<PullModifier>
				{
					new PullModifier
					{
						UnitCount = 10
					},
					new PullModifier
					{
						UnitCount = 1,
						Modifications = new List<UnitRate>
						{
							new UnitRate
							{
								Name = "On Banner Rainbow",
								Rating = UnitRating.Rainbow,
								Relevance = UnitRelevance.OnBanner,
								Rate = 0.02
							},
							new UnitRate
							{
								Name = "Off Banner Rainbow",
								Rating = UnitRating.Rainbow,
								Relevance = UnitRelevance.OnBanner,
								Rate = 0.03
							},
							new UnitRate
							{
								Name = "Gold",
								Rating = UnitRating.Gold,
								Rate = 0.95
							}
						}
					}
				}
			},

			new Pull
			{
				Code = "10+1_5%",
				Name = "10 + 1 Summon (5% Rainbow Rate)",
				Modifiers = new List<PullModifier>
				{
					new PullModifier
					{
						UnitCount = 10,
						Modifications = new List<UnitRate>
						{
							new UnitRate
							{
								Name = "On Banner Rainbow",
								Rating = UnitRating.Rainbow,
								Relevance = UnitRelevance.OnBanner,
								Rate = 0.01667
							},
							new UnitRate
							{
								Name = "Off Banner Rainbow",
								Rating = UnitRating.Rainbow,
								Relevance = UnitRelevance.OnBanner,
								Rate = 0.03333
							}
						}
					},
					new PullModifier
					{
						UnitCount = 1,
						Modifications = new List<UnitRate>
						{
							new UnitRate
							{
								Name = "On Banner Rainbow",
								Rating = UnitRating.Rainbow,
								Relevance = UnitRelevance.OnBanner,
								Rate = 0.06375
							},
							new UnitRate
							{
								Name = "Off Banner Rainbow",
								Rating = UnitRating.Rainbow,
								Relevance = UnitRelevance.OnBanner,
								Rate = 0.02125
							},
							new UnitRate
							{
								Name = "Gold",
								Rating = UnitRating.Gold,
								Rate = 0.915
							}
						}
					}
				}
			}
		};

	}
}
