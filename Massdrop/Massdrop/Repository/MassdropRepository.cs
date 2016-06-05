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
		public GenericRepository<Product> ProductRepo;
		public GenericRepository<Models.Massdrop> MassdropRepo;
		public GenericRepository<Discussion> DiscussionRepo;

		public MassdropRepository(IContext<Product> productContext, IContext<Models.Massdrop> massdropContext, IContext<Discussion> discussionContext)
		{
			this.ProductRepo = new GenericRepository<Product>(productContext);
			this.MassdropRepo = new GenericRepository<Models.Massdrop>(massdropContext);
			this.DiscussionRepo = new GenericRepository<Discussion>(discussionContext);

			 foreach (Models.Massdrop massdrop in MassdropRepo.Collection)
			{
				foreach(Discussion discussion in DiscussionRepo.Collection.Where(x => x.Massdrop.ID == massdrop.ID))
				{
					massdrop.Discussion = discussion;
					discussion.Massdrop = massdrop;
				}
				
				foreach(Product product in ProductRepo.Collection.Where(x => x.ID == massdrop.Product.ID))
				{
					massdrop.Product = product;
					product.Massdrop = massdrop;
				}
			}

			ProductRepo.EnableListener();
			MassdropRepo.EnableListener();
			DiscussionRepo.EnableListener();
		}
	}
}