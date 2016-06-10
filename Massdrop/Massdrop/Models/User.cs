using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExtendedObservableCollection;

namespace Massdrop.Models
{
	public enum UserType
	{
		Normal,
		Facebook
	};

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

		private string imageUrl;

		public string ImageUrl
		{
			get { return imageUrl; }
			set { SetField(this, ref imageUrl, value); }
		}

		private string password;

		public string Password
		{
			get { return password; }
			set { SetField(this, ref password, value); }
		}

		private ExtendedBindingList<Shipping_Address> shipping_address;

		public ExtendedBindingList<Shipping_Address> Shipping_Addresses
		{
			get { return shipping_address; }
			set { SetField(this, ref shipping_address, value); }
		}

		public User(int id)
		{
			this.ID = id;
			this.Shipping_Addresses = new ExtendedBindingList<Shipping_Address>();
		}

		public User(int id, string emailaddres, string password)
		{
			this.ID = id;
			this.EmailAddress = emailaddres;
			this.Password = password;
			this.Shipping_Addresses = new ExtendedBindingList<Shipping_Address>();
		}

		public User(int id, string emailadress, string name, string username, string password, string imageurl)
		{
			this.ID = id;
			this.EmailAddress = emailadress;
			this.Name = name;
			this.UserName = username;
			this.Password = password;
			this.ImageUrl = imageurl;
			this.Shipping_Addresses = new ExtendedBindingList<Shipping_Address>();
		}

		public User(string name, string emailaddress, string imageurl)
		{
			this.Name = name;
			this.EmailAddress = emailaddress;
			this.ImageUrl = imageurl;
			this.Shipping_Addresses = new ExtendedBindingList<Shipping_Address>();
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
	}
}