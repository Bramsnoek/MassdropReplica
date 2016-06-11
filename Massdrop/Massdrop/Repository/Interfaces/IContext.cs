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
		#region Methods
		/// <summary>
		/// The method that will be used to insert data into a data source
		/// </summary>
		/// <param name="source">The source class instance that will be inserted</param>
		/// <returns>If the insert was succesfull, true or false</returns>
		bool Insert(T source);

		/// <summary>
		/// This function will be used to get all the data of a collection
		/// </summary>
		/// <returns>All the data of this type</returns>
		IEnumerable<T> GetAll();

		/// <summary>
		/// This function will be used to delete data from a collection
		/// </summary>
		/// <param name="source">The source class instance that will be removed</param>
		/// <returns>If the removal was succesfull, true or false</returns>
		bool Remove(T source);

		/// <summary>
		/// This function will be used to update data from a collection
		/// </summary>
		/// <param name="source">The source class instance that will be updated</param>
		/// <returns>If the update was succesfull, true or false</returns>
		bool Update(T source);
		#endregion
	}
}
