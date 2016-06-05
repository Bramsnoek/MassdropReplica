using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExtendedObservableCollection;

namespace Massdrop.Models
{
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

	public sealed class Product : ExtendedNotifyPropertyChanged, IModel
	{
		private int id;

		public int ID
		{
			get { return id; }
			set { SetField(this, ref id, value); }
		}

		private decimal price;

		public decimal Price
		{
			get { return price; }
			set { SetField(this, ref price, value); }
		}

		private ProductCategory category;

		public ProductCategory Category
		{
			get { return category; }
			set { SetField(this, ref category, value); }
		}

		private string name;

		public string Name
		{
			get { return name; }
			set { SetField(this, ref name, value); }
		}

		private Massdrop massdrop;

		public Massdrop Massdrop
		{
			get { return massdrop; }
			set { SetField(this, ref massdrop, value); }
		}

		public Product(int id)
		{
			this.ID = id;
		}

		public Product(int id, string name, decimal price, ProductCategory category)
		{
			this.id = id;
			this.Category = category;
			this.Name = name;
			this.Price = price;
		}
	}
}