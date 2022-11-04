using Soap.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Soap.Services.Impl
{
	public class LibraryRepository : ILibraryRepositoryService
	{
		private readonly ILibraryDatabaseContextService _dbContext;
		public LibraryRepository(ILibraryDatabaseContextService dbContext)
		{
			_dbContext = dbContext;
		}
		public Book GetById(string Id)
		{
			throw new NotImplementedException();
		}
		public IList<Book> GetAll()
		{
			throw new NotImplementedException();
		}
		public IList<Book> GetByAuthor(string authorName)
		{
			return _dbContext.Books.Where(book => book.Authors.Where(author => author.Name.ToLower().Contains(authorName.ToLower())).Count()>0).ToList();
			
		}

		public IList<Book> GetByCategory(string category)
		{
			try
			{
				return _dbContext.Books.Where(book => book.Title.ToLower().Contains(category.ToLower())).ToList();
			}
			catch (Exception ex)
			{

				return null;
			}
			
		}
		public IList<Book> GetByTitle(string title)
		{
			try
			{
				return _dbContext.Books.Where(book => book.Title.ToLower().Contains(title.ToLower())).ToList();
			}
			catch (Exception ex)
			{

				return null;
			}
			
		}
		public string Add(Book item)
		{
			throw new NotImplementedException();
		}

		public int Update(Book item)
		{
			throw new NotImplementedException();
		}
		public int Delete(Book item)
		{
			throw new NotImplementedException();
		}	
	}
}