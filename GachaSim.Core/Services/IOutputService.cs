using System;
using System.Collections.Generic;
using System.Text;

namespace GachaSim.Core.Services
{
	public interface IOutputService
	{
		void Write(string format, params object[] args);
		void WriteLine();
		void WriteLine(string format, params object[] args);		
	}
}
