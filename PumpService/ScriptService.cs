using Microsoft.CSharp;

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace PumpService
{
	public class ScriptService : IScriptService
	{
		private readonly IStatistics _statisticsService;

		private readonly SettingsService _settingsService;

		private readonly IPumpServiceCallback _serviceCallback;

		private CompilerResults _compilerResults = null;
		public ScriptService(IStatistics statisticsService, SettingsService settingsService, IPumpServiceCallback serviceCallback)
		{
			_statisticsService = statisticsService;
			_settingsService = settingsService;
			_serviceCallback = serviceCallback;
		}
		public bool Compile()
		{
			try
			{
			CompilerParameters compilerParameters = new CompilerParameters();
			compilerParameters.GenerateInMemory = true;
			compilerParameters.ReferencedAssemblies.Add("System.dll");
			compilerParameters.ReferencedAssemblies.Add("System.Core.dll");
			compilerParameters.ReferencedAssemblies.Add("System.Data.dll");
			compilerParameters.ReferencedAssemblies.Add("Microsoft.CSharp.dll");
			compilerParameters.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().Location);

			FileStream fileStream = new FileStream(_settingsService.FileName, FileMode.Open);
			byte[] buffer;
			try
			{
				int lenght = (int)fileStream.Length;
				buffer = new byte[lenght];
				int count;
				int sum = 0;
				while ((count=fileStream.Read(buffer, sum, lenght-sum))>0)
					sum+=count;
			}
			finally
			{
				fileStream.Close();
			}
			CSharpCodeProvider provider = new CSharpCodeProvider();
			_compilerResults = provider.CompileAssemblyFromSource(compilerParameters, Encoding.UTF8.GetString(buffer));
			if (!(_compilerResults.Errors is null)||_compilerResults.Errors.HasErrors)
			{
				return false;
			}
			return true;
		}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return false;
			}
		}
		public void Run(int count)
		{
			if (_compilerResults is null||(!(_compilerResults is null)&&!(_compilerResults.Errors is null)&&_compilerResults.Errors.HasErrors))
			{
				if (Compile() is false)
				{
					return;
				}
			}

			

			Type t = _compilerResults.CompiledAssembly.GetType("Sample.SampleScript");
			if (t is null)
			{
				return;
			}

			MethodInfo entryPoint = t.GetMethod("EntryPoint");

			Task.Run(() => {
				for (int i = 0; i < count; i++)
				{
					if (entryPoint is null)
					{
						return;
					}
					if ((bool)entryPoint.Invoke(Activator.CreateInstance(t), null))
					{
						_statisticsService.SuccessTacts++;
					}
					else
					{
						_statisticsService.ErrorTacts++;
					}
					_statisticsService.AllTacts++;
					_serviceCallback.UpdateStatistics(_statisticsService);
					Thread.Sleep(800);
				}
			});
		}
	}
}