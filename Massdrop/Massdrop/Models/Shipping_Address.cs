using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using ExtendedObservableCollection;

namespace Massdrop.Models
{
	public class Shipping_Address : ExtendedNotifyPropertyChanged, IModel
	{
		#region Full Properties

		// The id of the shipping_address
		private int id;

		public int ID
		{
			get { return id; }
			set { SetField(this, ref id, value); }
		}

		// The address of the shipping_address
		private string address;

		public string Address
		{
			get { return address; }
			set { SetField(this, ref address, value); }
		}

		// The user of the shipping_address
		private User user;

		public User User
		{
			get { return user; }
			set { SetField(this, ref user, value); }
		}

		// The city of the shipping_address
		private string city;

		public string City
		{
			get { return city; }
			set { SetField(this, ref city, value); }
		}

		// The province of the shipping_address
		private string province;

		public string Province
		{
			get { return province; }
			set { SetField(this, ref province, value); }
		}

		// The postalcode of the shipping_address
		private string postalcode;

		public string PostalCode
		{
			get { return postalcode; }
			set { SetField(this, ref postalcode, value); }
		}

		// The phonenumber of the shipping_address
		private long phonenumber;

		public long PhoneNumber
		{
			get { return phonenumber; }
			set { SetField(this, ref phonenumber, value); }
		}
		#endregion

		#region Constructor

		/// <summary>
		/// This constructor will be used to create a shipping_address
		/// </summary>
		/// <param name="address">The address of the shipping_address</param>
		/// <param name="city">The city of the shipping_address</param>
		/// <param name="province">The province of the shipping_address</param>
		/// <param name="postalcode">The postalcode of the shipping_address</param>
		/// <param name="user">The user of the shipping_addres</param>
		/// <param name="phonenumber">The phonenumber of the shipping_addres</param>
		public Shipping_Address(string address, string city, string province, string postalcode, User user, long phonenumber = 0)
		{
			this.Address = address;
			this.City = city;
			this.PostalCode = postalcode;
			this.PhoneNumber = phonenumber;
			this.Province = province;
			this.User = user;
		}
		#endregion
	}
}