using System.Runtime.Serialization;
using System.ServiceModel;

namespace PumpService
{
	
	public interface IStatistics
	{
		
		int AllTacts { get; set; }
		int ErrorTacts { get; set; }
		int SuccessTacts { get; set; }
	}
}