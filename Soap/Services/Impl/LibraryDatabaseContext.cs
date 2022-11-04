using Soap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Text;

namespace Soap.Services.Impl
{
	public class LibraryDatabaseContext : ILibraryDatabaseContextService
	{

		private IList<Book> _libraryDatabase;
		public IList<Book> Books => _libraryDatabase;

		public LibraryDatabaseContext()
		{
			Initialize();
		}

		private void Initialize()
		{	
			_libraryDatabase = (List<Book>)JsonConvert.DeserializeObject(Encoding.UTF8.GetString(Properties.Resources.books), typeof(List<Book>));
		}
	}
}