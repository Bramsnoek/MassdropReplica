using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExtendedObservableCollection;

namespace Massdrop.Models
{
	public enum PaymentType
	{
		Ideal,
		Paypal
	}
	public sealed class Payment_Method : ExtendedNotifyPropertyChanged, IModel
	{

		private decimal price;

		public decimal Price
		{
			get { return price; }
			set { SetField(this, ref price, value); }
		}

		private PaymentType type;

		public PaymentType Type
		{
			get { return type; }
			set { SetField(this, ref type, value); }
		}

		private int id;

		public int ID
		{
			get { return id; }
			set { SetField(this, ref id, value); }
		}


		public Payment_Method(PaymentType type)
		{
			this.Type = type;
		}

		public void GetTransactionCost()
		{

		}
	}
}