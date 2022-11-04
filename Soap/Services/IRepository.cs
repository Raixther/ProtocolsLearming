using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soap.Services
{
	public interface IRepository<T, TId> 
	{
		T GetById(TId Id);
		IList<T> GetAll();
		TId Add(T item);
		int Update(T item);
		int Delete(T item);
	}
}
