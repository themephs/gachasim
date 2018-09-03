using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GachaSim.Cli
{
	public class HelpException : Exception
	{
		public string Topic { get; private set; }

		public HelpException(string topic, string message) : base(message)
		{
			this.Topic = topic;
		}
	}
}
