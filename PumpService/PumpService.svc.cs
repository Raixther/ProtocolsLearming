using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PumpService
{
	// ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
	// ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.


	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession,

		IncludeExceptionDetailInFaults = true)]

	public class PumpService : IPumpService
	{
		
		private readonly IScriptService _scriptService;
		private readonly IStatistics _statisticsService;
		private readonly SettingsService _serviceSettings;
		private IPumpServiceCallback Callback => OperationContext.Current.GetCallbackChannel<IPumpServiceCallback>();
				
			
		public PumpService()
		{
			
			_statisticsService=new Statistics();
			_serviceSettings =new SettingsService();
			_scriptService= new ScriptService(_statisticsService, _serviceSettings, Callback);

		}

		public void RunScript()
		{
			_scriptService.Run(10);
		}

		public void UpdateAndCompileScript(string fileName)
		{
			_serviceSettings.FileName = fileName;
			_scriptService.Compile();
		}
	}
}
