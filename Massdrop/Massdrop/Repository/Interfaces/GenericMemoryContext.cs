using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExtendedObservableCollection;
using Massdrop.Models;

namespace Massdrop.Repository.Interfaces
{
	public class GenericMemoryContext<T> : IContext<T>
		where T : IModel
	{
		#region Fields
		private List<T> collection;
		#endregion

		#region Constructors
		public GenericMemoryContext(List<T> collection = null)
		{
			if (collection == null)
			{
				collection = new List<T>();
			}
			else
			{
				this.collection = collection;
			}
		}
		#endregion

		#region Methods
		public T Get(int id)
		{
			return collection.Find(x => x.ID == id);
		}

		public IEnumerable<T> GetAll()
		{
			return collection;
		}

		public bool Insert(T entity)
		{
			entity.ID = collection[collection.Count - 1].ID + 1;
			collection.Add(entity);
			return true;
		}

		public bool Remove(T entity)
		{
			collection.Remove(entity);
			return true;
		}

		public bool Update(T entity)
		{
			T item = collection.Find(x => x.ID == entity.ID);
			item = entity;
			return true;
		}
		#endregion
	}
}