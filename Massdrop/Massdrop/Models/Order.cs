using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExtendedObservableCollection;

namespace Massdrop.Models
{
	public sealed class Order : ExtendedNotifyPropertyChanged, IModel
	{
		private DateTime date;

		public DateTime Date
		{
			get { return date; }
			set { SetField(this, ref date, value); }
		}

		private decimal price;
		public decimal Price
		{
			get { return price; }
			set { SetField(this, ref price, value); }
		}
		private Massdrop massdrop;
		public Massdrop Massdrop
		{
			get { return massdrop; }
			set { SetField(this, ref massdrop, value); }
		}

		private User user;
		public User User
		{
			get { return user; }
			set { SetField(this, ref user, value); }
		}

		private Payment_Method payment_Method;
		public Payment_Method Payment_Method
		{
			get { return payment_Method; }
			set { SetField(this, ref payment_Method, value); }
		}

		private int id;

		public int ID
		{
			get { return id; }
			set { SetField(this, ref id, value); }
		}

		public Order(DateTime date, decimal price, User user, Payment_Method payment_method, Massdrop massdrop)
		{
			this.Date = date;
			this.Price = price;
			this.User = user;
			this.Payment_Method = payment_method;
			this.Massdrop = massdrop;
		}
	}
}