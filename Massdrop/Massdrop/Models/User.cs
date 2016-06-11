using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExtendedObservableCollection;

namespace Massdrop.Models
{
	public class User : ExtendedNotifyPropertyChanged, IModel
	{
		#region Full Properties
		//The id of the user
		private int id;

		public int ID
		{
			get { return id; }
			set { SetField(this, ref id, value); }
		}

		// The emailaddress of the user
		private string emailaddress;

		public string EmailAddress
		{
			get { return emailaddress; }
			set { SetField(this, ref emailaddress, value); }
		}

		// The name of the user
		private string name;

		public string Name
		{
			get { return name; }
			set { SetField(this, ref name, value); }
		}

		// The username of the user
		private string username;

		public string UserName
		{
			get { return username; }
			set { SetField(this, ref username, value); }
		}

		// The imageurl of the user
		private string imageUrl;

		public string ImageUrl
		{
			get { return imageUrl; }
			set { SetField(this, ref imageUrl, value); }
		}

		// The password of the user
		private string password;

		public string Password
		{
			get { return password; }
			set { SetField(this, ref password, value); }
		}

		// The list of shipping_addresses addressed to this user
		public ExtendedBindingList<Shipping_Address> Shipping_Addresses { get; set; }
		#endregion

		#region Constructors
		/// <summary>
		/// This constructor will be used to link users to for example massdrops
		/// </summary>
		/// <param name="id">The id of the user</param>
		public User(int id)
		{
			this.ID = id;
			this.Shipping_Addresses = new ExtendedBindingList<Shipping_Address>();
		}

		/// <summary>
		/// This constructor will be used to create new users
		/// </summary>
		/// <param name="id">The id of the user</param>
		/// <param name="emailaddres">The emailadress of the user</param>
		/// <param name="password">The password of the user</param>
		public User(int id, string emailaddres, string password)
		{
			this.ID = id;
			this.EmailAddress = emailaddres;
			this.Password = password;
			this.Shipping_Addresses = new ExtendedBindingList<Shipping_Address>();
		}

		/// <summary>
		/// This constructor will be used for testdata to make sure we have all the fields covered
		/// </summary>
		/// <param name="id">The id of the user</param>
		/// <param name="emailadress">The email of the user</param>
		/// <param name="name">The name of the user</param>
		/// <param name="username">The username of the user</param>
		/// <param name="password">The password of the user</param>
		/// <param name="imageurl">The imageurl of the user</param>
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

		/// <summary>
		/// This constructor will be used to create facebookusers
		/// </summary>
		/// <param name="name">The name of the facebookuser</param>
		/// <param name="emailaddress">The email of the facebookuser</param>
		/// <param name="imageurl">The imageurl of the facebookuser</param>
		public User(string name, string emailaddress, string imageurl)
		{
			this.Name = name;
			this.EmailAddress = emailaddress;
			this.ImageUrl = imageurl;
			this.Shipping_Addresses = new ExtendedBindingList<Shipping_Address>();
		}

		#endregion

		#region Methods
		/// <summary>
		/// This function changes the current information of the user
		/// </summary>
		/// <param name="userName">The new username of the user, if this is null then it't not being changed</param>
		/// <param name="name">The new name of the user, if this is null then it't not being changed</param>
		/// <param name="email">The new email of the user, if this is null then it't not being changed</param>
		/// <param name="password">The new password of the user, if this is null then it't not being changed</param>
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

		/// <summary>
		/// This function adds a shipping address to this user
		/// </summary>
		/// <param name="address">The address of the shipping address</param>
		/// <param name="city">The city of the shipping address</param>
		/// <param name="province">The province of the shipping address</param>
		/// <param name="postalcode">The postalcode of the shipping address</param>
		public string AddShippingAddress(string address, string city, string province, string postalcode)
		{
			this.Shipping_Addresses.Add(new Shipping_Address(address, city, province, postalcode, this));
			return "1";
		}


		/// <summary>
		/// This removes a shipping address from this user
		/// </summary>
		/// <param name="id">The id of the shipping address</param>
		public void RemoveShippingAddres(int id)
		{
			this.Shipping_Addresses.Remove(this.Shipping_Addresses.First(x => x.ID == id));
		}

		#endregion
	}
}