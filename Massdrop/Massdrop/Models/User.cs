using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExtendedObservableCollection;

namespace Massdrop.Models
{
	public sealed class User : ExtendedNotifyPropertyChanged, IModel
	{
		private int id;

		public int ID
		{
			get { return id; }
			set { SetField(this, ref id, value); }
		}

		private string emailaddress;

		public string EmailAddress
		{
			get { return emailaddress; }
			set { SetField(this, ref emailaddress, value); }
		}

		private string name;

		public string Name
		{
			get { return name; }
			set { SetField(this, ref name, value); }
		}

		private string username;

		public string UserName
		{
			get { return username; }
			set { SetField(this, ref username, value); }
		}
		private string password;

		public string Password
		{
			get { return password; }
			set { SetField(this, ref password, value); }
		}

		private Shipping_Address shipping_address;

		public Shipping_Address Shipping_Address
		{
			get { return shipping_address; }
			set { SetField(this, ref shipping_address, value); }
		}

		public User(int id)
		{
			this.ID = id;
		}
		public User(string emailadress, string name, string username, string password)
		{
			this.EmailAddress = emailadress;
			this.Name = name;
			this.UserName = username;
			this.Password = password;
		}

		public User(string emailaddress, string password)
		{
			this.EmailAddress = emailaddress;
			this.Password = password;
		}

		public void ChangeUserInfo(string userName, string name, string email, string password)
		{
			if (userName != null)
				this.UserName = userName;
			if (name != null)
				this.Name = name;
			if (email != null)
				this.EmailAddress = email;
			if (password != null)
				this.Password = password;
		}

		public void ChangeShippingAddress(string address, string city, string province, string postalcode)
		{
			
		}

		//public void RemoveShipping
	}
}