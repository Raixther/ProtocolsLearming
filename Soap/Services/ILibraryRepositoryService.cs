using Soap.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soap.Services
{
	public interface ILibraryRepositoryService :IRepository<Book, string>
	{
		IList<Book> GetByTitle(string title);

		IList<Book> GetByCategory(string category);

		IList<Book> GetByAuthor(string authorName);

	}
}
