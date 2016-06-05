using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExtendedObservableCollection;

namespace Massdrop.Models 
{
	public sealed class Massdrop : ExtendedNotifyPropertyChanged, IModel
	{
		private decimal first_DroppedPrice;

		public decimal First_DroppedPrice
		{
			get { return first_DroppedPrice; }
			set { SetField(this, ref first_DroppedPrice, value); }
		}

		private decimal second_DroppedPrice;

		public decimal Second_DroppedPrice
		{
			get { return second_DroppedPrice; }
			set { SetField(this, ref second_DroppedPrice, value); }
		}

		private DateTime startDate;

		public DateTime StartDate
		{
			get { return startDate; }
			set { SetField(this, ref startDate, value); }
		}

		private DateTime endDate;

		public DateTime EndDate
		{
			get { return endDate; }
			set { SetField(this, ref endDate, value); }
		}

		private Product product;
		public Product Product
		{
			get { return product; }
			set { SetField(this, ref product, value); }
		}

		private int id;

		public int ID
		{
			get { return id; }
			set { SetField(this, ref id, value); }
		}

		private Discussion discussion;

		public Discussion Discussion
		{
			get { return discussion; }
			set { SetField(this, ref discussion, value); }
		}

		public Massdrop(int id)
		{
			this.ID = id;
		}

		public Massdrop(int id, decimal first_droppedprice, decimal second_droppedprice, DateTime startdate, DateTime enddate, Product product)
		{
			this.ID = id;
			this.First_DroppedPrice = first_droppedprice;
			this.Second_DroppedPrice = second_droppedprice;
			this.StartDate = startdate;
			this.EndDate = enddate;
			this.Product = product;
		}
	}
}