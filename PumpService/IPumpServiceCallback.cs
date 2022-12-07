using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace PumpService

{	[ServiceContract()]


	public interface IPumpServiceCallback
	{
		[OperationContract]
		
		void UpdateStatistics(IStatistics statistics);

	}
}