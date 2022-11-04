using Soap.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soap.Services
{
	public interface ILibraryDatabaseContextService
	{
		IList<Book> Books{ get;}
	}
}
