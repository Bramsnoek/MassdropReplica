using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Massdrop.Repository.Interfaces;
using Massdrop.Repository.Contexts;
using Massdrop.Models;
using ExtendedObservableCollection;

namespace Massdrop.Repository
{
	public class MassdropRepository
	{
		#region Properties
		// The repository that will be used to get the product information
		public GenericRepository<Product> ProductRepo { get; set; }

		// The repository that will be used to get the massdrop information
		public GenericRepository<Models.Massdrop> MassdropRepo { get; set;  }

		// The repository that will be used to get the discussion information
		public GenericRepository<Discussion> DiscussionRepo { get; set; }
		#endregion

		#region Constructor
		/// <summary>
		/// This constructor requires 3 context parameters, these will be used to instantiate the repositories and get the data. The data will also be linked to eachother in this constructor
		/// </summary>
		/// <param name="productContext">The context that will be used to instantiate the productrepository</param>
		/// <param name="massdropContext">The context that will be used to instantiate the massdroprepository</param>
		/// <param name="discussionContext">The context that will be used to instantiate the discussionrepository</param>
		public MassdropRepository(IContext<Product> productContext, IContext<Models.Massdrop> massdropContext, IContext<Discussion> discussionContext)
		{
			this.ProductRepo = new GenericRepository<Product>(productContext);
			this.MassdropRepo = new GenericRepository<Models.Massdrop>(massdropContext);
			this.DiscussionRepo = new GenericRepository<Discussion>(discussionContext);

			foreach (Models.Massdrop massdrop in MassdropRepo.Collection)
			{
				foreach (Discussion discussion in DiscussionRepo.Collection.Where(x => x.Massdrop.ID == massdrop.ID))
				{
					massdrop.Discussions.Add(discussion);
					discussion.Massdrop = massdrop;
				}

				foreach (Product product in ProductRepo.Collection.Where(x => x.ID == massdrop.Product.ID))
				{
					massdrop.Product = product;
					product.Massdrop = massdrop;
				}
			}
		}
		#endregion
	}
}