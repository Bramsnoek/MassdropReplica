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
		//The collection of the type
		private List<T> collection;
		#endregion

		#region Constructors
		/// <summary>
		/// This constructor will either make a new collection or use an existing one
		/// </summary>
		/// <param name="collection">If this is given it will use the existing collection</param>
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

		/// <summary>
		/// This function will get one instance of T from the collection
		/// </summary>
		/// <param name="id">The id of the class instance</param>
		/// <returns>The class instance</returns>
		public T Get(int id)
		{
			return collection.Find(x => x.ID == id);
		}


		/// <summary>
		/// This function will get all the class instances of T
		/// </summary>
		/// <returns>IEnumerable of T</returns>
		public IEnumerable<T> GetAll()
		{
			return collection;
		}

		/// <summary>
		/// This function will insert a class instance of T to the collection
		/// </summary>
		/// <param name="entity">The class instance of T</param>
		/// <returns>If the insertion was succesfull, a true or false</returns>
		public bool Insert(T entity)
		{
			entity.ID = collection[collection.Count - 1].ID + 1;
			collection.Add(entity);
			return true;
		}

		/// <summary>
		/// This function will remove a class instance of T to the collection
		/// </summary>
		/// <param name="entity">The class instance of T</param>
		/// <returns>If the removal was succesfull, a true or false</returns>
		public bool Remove(T entity)
		{
			collection.Remove(entity);
			return true;
		}

		/// <summary>
		/// This function will update a class instance of T to the collection
		/// </summary>
		/// <param name="entity">The class instance of T</param>
		/// <returns>If the update was succesfull, a true or false</returns>
		public bool Update(T entity)
		{
			T item = collection.Find(x => x.ID == entity.ID);
			item = entity;
			return true;
		}
		#endregion
	}
}