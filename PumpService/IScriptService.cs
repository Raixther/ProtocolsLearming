using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PumpService
{
	public interface IScriptService
	{
		bool Compile();
		void Run(int count);
	}
}