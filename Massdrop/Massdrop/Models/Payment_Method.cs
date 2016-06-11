using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExtendedObservableCollection;

namespace Massdrop.Models
{
	// This enum will determine the type of payment
	public enum PaymentType
	{
		Ideal,
		Paypal
	}
	public class Payment_Method : ExtendedNotifyPropertyChanged, IModel
	{
		#region Full Properties
		// The price of using this payment method
		private decimal price;

		public decimal Price
		{
			get { return price; }
			set { SetField(this, ref price, value); }
		}

		// The type of payment
		private PaymentType type;

		public PaymentType Type
		{
			get { return type; }
			set { SetField(this, ref type, value); }
		}

		// The id of this payment_method
		private int id;

		public int ID
		{
			get { return id; }
			set { SetField(this, ref id, value); }
		}
		#endregion

		#region Constructor
		/// <summary>
		/// This constructor is used for creating payment method instances
		/// </summary>
		/// <param name="type">The type of payment</param>
		public Payment_Method(PaymentType type)
		{
			this.Type = type;
		}
		#endregion
	}
}