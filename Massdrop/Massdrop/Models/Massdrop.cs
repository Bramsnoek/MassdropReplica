using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExtendedObservableCollection;

namespace Massdrop.Models 
{
	public class Massdrop : ExtendedNotifyPropertyChanged, IModel
	{
		#region Full Properties

		// The first discounted price of this massdrop
		private decimal first_DroppedPrice;

		public decimal First_DroppedPrice
		{
			get { return first_DroppedPrice; }
			set { SetField(this, ref first_DroppedPrice, value); }
		}

		// The second discounted price of this massdrop
		private decimal second_DroppedPrice;

		public decimal Second_DroppedPrice
		{
			get { return second_DroppedPrice; }
			set { SetField(this, ref second_DroppedPrice, value); }
		}

		// The startdate of this massdrop
		private DateTime startDate;

		public DateTime StartDate
		{
			get { return startDate; }
			set { SetField(this, ref startDate, value); }
		}

		// The enddate of this massdrop
		private DateTime endDate;

		public DateTime EndDate
		{
			get { return endDate; }
			set { SetField(this, ref endDate, value); }
		}

		// The product associated to this massdrop
		private Product product;
		public Product Product
		{
			get { return product; }
			set { SetField(this, ref product, value); }
		}

		// The id of this product
		private int id;

		public int ID
		{
			get { return id; }
			set { SetField(this, ref id, value); }
		}

		// The comments to this massdrop
		public ExtendedBindingList<Discussion> Discussions { get; set; }
		#endregion

		#region Constructor

		/// <summary>
		/// This constructor is used when linking massdrops to for example products and orders
		/// </summary>
		/// <param name="id">The id of the massdrop</param>
		public Massdrop(int id)	
		{
			this.ID = id;
			this.Discussions = new ExtendedBindingList<Models.Discussion>();
		}
		/// <summary>
		/// This constructor is used when creating massdrops
		/// </summary>
		/// <param name="id">The id of the massdrop</param>
		/// <param name="first_droppedprice">The fist discounted price of the massdrop</param>
		/// <param name="second_droppedprice">The discount price of the massdrop</param>
		/// <param name="startdate">The startdate of the massdrop</param>
		/// <param name="enddate">The enddate of the massdrop</param>
		/// <param name="product">The product of this massdrop</param>
		public Massdrop(int id, decimal first_droppedprice, decimal second_droppedprice, DateTime startdate, DateTime enddate, Product product)
		{
			this.ID = id;
			this.First_DroppedPrice = first_droppedprice;
			this.Second_DroppedPrice = second_droppedprice;
			this.StartDate = startdate;
			this.EndDate = enddate;
			this.Product = product;
			this.Discussions = new ExtendedBindingList<Models.Discussion>();
		}
		#endregion

		#region Methods

		/// <summary>
		/// This function is used to add a comment to a massdrop
		/// </summary>
		/// <param name="commenttext">The text of the comment you want to add</param>
		/// <param name="user">The user that placed the discussion</param>
		public void AddComment(string commenttext, User user)
		{
			this.Discussions.Add(new Discussion(commenttext, 0, DateTime.Now, user, this));
		}
		#endregion
	}
}