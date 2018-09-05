using System;
using System.Collections.Generic;
using System.Text;

using GachaSim.Core.Services;

namespace GachaSim.Cli.Services
{
	public class ConsoleOutputService : IOutputService
	{		
		public void Write(string format, params object[] args) => Console.Write(format, args);

		public void WriteLine() => Console.WriteLine();
		public void WriteLine(string format, params object[] args) => Console.WriteLine(format, args);
	}
}
