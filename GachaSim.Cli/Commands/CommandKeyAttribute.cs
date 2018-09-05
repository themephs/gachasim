using System;
using System.Collections.Generic;
using System.Text;

namespace GachaSim.Cli.Commands
{
	public class CommandKeyAttribute : Attribute
	{
		public string Key { get; private set; }

		public CommandKeyAttribute(string key)
		{
			this.Key = key;
		}
	}
}
