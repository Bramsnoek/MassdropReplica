using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExtendedObservableCollection;

namespace Massdrop.Models
{
	// This enum holds all the categories a product can be in
	public enum ProductCategory
	{
		Ultralight,
		Audiophile,
		MKeyboards,
		ProAudio,
		HobbyShop,
		Mensstyle,
		EverydayCarry,
		Quilting,
		Tech,
		Writing,
		Photography,
		RCCars,
		Auto
	}

	public class Product : ExtendedNotifyPropertyChanged, IModel
	{
		#region Full Properties
		// The id of the product
		private int id;

		public int ID
		{
			get { return id; }
			set { SetField(this, ref id, value); }
		}

		// The imageurl of the product
		private string imageurl;

		public string ImageUrl
		{
			get { return imageurl; }
			set { SetField(this, ref imageurl, value); }
		}

		// The description of the product
		private string description;

		public string Description
		{
			get { return description; }
			set { SetField(this, ref description, value); }
		}

		// The price of the product
		private decimal price;

		public decimal Price
		{
			get { return price; }
			set { SetField(this, ref price, value); }
		}

		// The category of the the product
		private ProductCategory category;

		public ProductCategory Category
		{
			get { return category; }
			set { SetField(this, ref category, value); }
		}

		// The name of the product
		private string name;

		public string Name
		{
			get { return name; }
			set { SetField(this, ref name, value); }
		}

		// The massdrop this product belongs to
		private Massdrop massdrop;

		public Massdrop Massdrop
		{
			get { return massdrop; }
			set { SetField(this, ref massdrop, value); }
		}

		// The non discounted price of this product
		public decimal NormalPrice { get; set; }
		#endregion
		
		#region Constructors

		/// <summary>
		/// This constructor is used to make product instances used for linking
		/// </summary>
		/// <param name="id">The id of the product</param>
		public Product(int id)
		{
			this.ID = id;
		}



		/// <summary>
		/// This constructor will be used for testdata, to make sure we have all the fields covered
		/// </summary>
		/// <param name="id">The id of the product</param>
		/// <param name="name">The name of the product</param>
		/// <param name="price"> The price of the product</param>
		/// <param name="category">The category of the product</param>
		public Product(int id, string name, decimal price, ProductCategory category)
		{
			this.id = id;
			this.Category = category;
			this.Name = name;
			this.Price = price;
			imageurl = "http://placehold.it/310x310";
			this.price = price * 1.50m;
		}

		/// <summary>
		/// This constructor is used to make new products that will be inserted into the datbaase
		/// </summary>
		/// <param name="id">The id of the product</param>
		/// <param name="name">The name of the product</param>
		/// <param name="price"> The price of the product</param>
		/// <param name="category">The category of the product</param>
		/// <param name="imageurl">The imageurl of the product</param>
		/// <param name="description">The description of the product</param>
		public Product(int id, string name, decimal price, ProductCategory category, string imageurl, string description)
		{
			this.ImageUrl = imageurl;
			this.id = id;
			this.Category = category;
			this.Name = name;
			this.Price = price;
			this.NormalPrice = price * 1.50m;
			this.Description = description;
		}
		#endregion
	}
}