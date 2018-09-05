using System;
using System.Collections.Generic;
using System.Text;

namespace GachaSim.Cli.Commands
{
	public interface ICommand
	{
		void Execute(params string[] args);
	}
}
