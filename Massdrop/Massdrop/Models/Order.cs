using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExtendedObservableCollection;

namespace Massdrop.Models
{
	public sealed class Order : ExtendedNotifyPropertyChanged, IModel
	{
		#region Full Properties

		// The date that the order was placed
		private DateTime date;

		public DateTime Date
		{
			get { return date; }
			set { SetField(this, ref date, value); }
		}

		// The price of this order
		private decimal price;
		public decimal Price
		{
			get { return price; }
			set { SetField(this, ref price, value); }
		}

		// The massdrop this order belongs to
		private Massdrop massdrop;
		public Massdrop Massdrop
		{
			get { return massdrop; }
			set { SetField(this, ref massdrop, value); }
		}

		// The user who ordered this ordered
		private User user;
		public User User
		{
			get { return user; }
			set { SetField(this, ref user, value); }
		}

		// The payment_method used to pay for this order
		private Payment_Method payment_Method;
		public Payment_Method Payment_Method
		{
			get { return payment_Method; }
			set { SetField(this, ref payment_Method, value); }
		}

		// The id of this order
		private int id;

		public int ID
		{
			get { return id; }
			set { SetField(this, ref id, value); }
		}
		#endregion

		#region Constructor
		/// <summary>
		/// This constructor will be used to place new orders
		/// </summary>
		/// <param name="date">The date this order was place</param>
		/// <param name="price">The price of this order</param>
		/// <param name="user">The user who placed the order</param>
		/// <param name="payment_method">The payment_method the order was payed with</param>
		/// <param name="massdrop">The massdrop the order belongs to</param>
		public Order(DateTime date, decimal price, User user, Payment_Method payment_method, Massdrop massdrop)
		{
			this.Date = date;
			this.Price = price;
			this.User = user;
			this.Payment_Method = payment_method;
			this.Massdrop = massdrop;
		}
		#endregion
	}
}