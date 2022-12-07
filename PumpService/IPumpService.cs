using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PumpService
{
	[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IPumpServiceCallback))]

	public interface IPumpService
	{	
		[OperationContract(IsOneWay = true)]
		void RunScript();

		[OperationContract(IsOneWay = true)]
		void UpdateAndCompileScript(string fileName);

	}
}
