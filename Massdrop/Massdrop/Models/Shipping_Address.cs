using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using ExtendedObservableCollection;

namespace Massdrop.Models
{
	public sealed class Shipping_Address : ExtendedNotifyPropertyChanged, IModel
	{
		private int id;

		public int ID
		{
			get { return id; }
			set { SetField(this, ref id, value); }
		}

		private string address;

		public string Address
		{
			get { return address; }
			set { SetField(this, ref address, value); }
		}

		private User user;

		public User User
		{
			get { return user; }
			set { SetField(this, ref user, value); }
		}

		private string city;

		public string City
		{
			get { return city; }
			set { SetField(this, ref city, value); }
		}

		private string province;

		public string Province
		{
			get { return province; }
			set { SetField(this, ref province, value); }
		}

		private string postalcode;

		public string PostalCode
		{
			get { return postalcode; }
			set { SetField(this, ref postalcode, value); }
		}

		private long phonenumber;

		public long PhoneNumber
		{
			get { return phonenumber; }
			set { SetField(this, ref phonenumber, value); }
		}

		public Shipping_Address(string address, string city, string province, string postalcode, User user, long phonenumber = 0)
		{
			this.Address = address;
			this.City = city;
			this.PostalCode = postalcode;
			this.PhoneNumber = phonenumber;
			this.Province = province;
			this.User = user;
		}
	}
}