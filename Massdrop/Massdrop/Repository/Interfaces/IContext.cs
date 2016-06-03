using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtendedObservableCollection;

namespace Massdrop.Repository.Interfaces
{
	public interface IContext<T>
	{
		bool Insert(T source);
		IEnumerable<T> GetAll();
		bool Remove(T source);
		bool Update(T source);
	}
}
