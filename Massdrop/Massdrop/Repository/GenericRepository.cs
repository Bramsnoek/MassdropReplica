using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExtendedObservableCollection;
using Massdrop.Repository.Interfaces;
using System.Collections.Specialized;

namespace Massdrop.Repository
{
	public class GenericRepository<T>
		where T : ExtendedNotifyPropertyChanged
	{
		#region Fields
		private IContext<T> context;
		#endregion

		#region Constructor
		// The collection that will hold the models for a certain Type (T)
		public ExtendedObservableCollection<T> Collection { get; set; }
		#endregion

		#region Constructor 
		/// <summary>
		/// This constructor requires one context, it will be used to fill the collection with the right data
		/// </summary>
		/// <param name="context">The context used to retrieve the data</param>
		public GenericRepository(IContext<T> context)
		{
			this.context = context;
			Collection = new ExtendedObservableCollection<T>(context.GetAll());
		}
		#endregion

		#region Methods
		public void EnableListener()
		{
			Collection.CollectionChanged += Collection_CollectionChanged;
			Collection.ExtendedPropertyChanged += Collection_ExtendedPropertyChanged;
			Collection.ExtendedPropertyListChanged += Collection_ExtendedPropertyListChanged;
		}
		#endregion

		#region Event Handlers
		private void Collection_ExtendedPropertyListChanged(object sender, ExtendedListChangedEventArgs e)
		{
			context.Insert((T)sender);
		}

		private void Collection_ExtendedPropertyChanged(object sender, ExtendedPropertyChangedEventArgs e)
		{
			context.Update((T)sender);
		}

		private void Collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					context.Insert((T)e.NewItems[0]);
					break;
				case NotifyCollectionChangedAction.Remove:
					context.Remove((T)e.OldItems[0]);
					break;
				default:
					break;
			}
		}
		#endregion
	}
}