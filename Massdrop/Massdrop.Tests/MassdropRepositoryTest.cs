using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExtendedObservableCollection;
using Massdrop;
using Massdrop.Repository;
using Massdrop.Repository.Interfaces;
using Massdrop.Models;

namespace Massdrop.Tests
{
	[TestClass]
	public class MassdropRepositoryTest
	{
		private MassdropRepository massdropRepo;

		[TestInitialize]
		public void Constructor()
		{
			massdropRepo = new MassdropRepository(new GenericMemoryContext<Product>(TestData.GetProducts()), new GenericMemoryContext<Models.Massdrop>(TestData.GetMassdrops()), new GenericMemoryContext<Discussion>(TestData.GetDiscussions()));

			massdropRepo.DiscussionRepo.EnableListener();
			massdropRepo.MassdropRepo.EnableListener();
			massdropRepo.ProductRepo.EnableListener();
		}

		#region Discussion Tests
		[TestMethod]
		public void TestAddDiscussion()
		{
			int currentCount = massdropRepo.DiscussionRepo.Collection.Count;

			Discussion newDiscussion = new Discussion(6, "Dit is een nieuwe comment", 0, DateTime.Now, TestData.GetUsers()[1], TestData.GetMassdrops()[2]);

			massdropRepo.DiscussionRepo.Collection.Add(newDiscussion);

			Assert.AreEqual(massdropRepo.DiscussionRepo.Collection.Count, currentCount + 1);
		}

		[TestMethod]
		public void TestRemoveDiscussion()
		{
			int currentCount = massdropRepo.DiscussionRepo.Collection.Count;

			Discussion toBeRemovedDiscussion = massdropRepo.DiscussionRepo.Collection[0];

			massdropRepo.DiscussionRepo.Collection.Remove(toBeRemovedDiscussion);

			Assert.AreEqual(massdropRepo.DiscussionRepo.Collection.Count, currentCount - 1);
		}
		#endregion

		#region Product Tests
		[TestMethod]
		public void TestAddProduct()
		{
			int currentCount = massdropRepo.ProductRepo.Collection.Count;

			Product newProduct = new Product(1, "test", 123, ProductCategory.Audiophile);

			massdropRepo.ProductRepo.Collection.Add(newProduct);

			Assert.AreEqual(massdropRepo.ProductRepo.Collection.Count, currentCount + 1);
		}

		[TestMethod]
		public void TestRemoveProduct()
		{
			int currentCount = massdropRepo.ProductRepo.Collection.Count;

			Product toBeRemovedProduct = massdropRepo.ProductRepo.Collection[0];

			massdropRepo.ProductRepo.Collection.Remove(toBeRemovedProduct);

			Assert.AreEqual(massdropRepo.ProductRepo.Collection.Count, currentCount - 1);
		}
		#endregion

		#region Massdrop Tests
		[TestMethod]
		public void TestAddMassdrop()
		{
			int currentCount = massdropRepo.MassdropRepo.Collection.Count;

			Models.Massdrop newMassdrop = new Models.Massdrop(1, 123, 124, DateTime.Now, DateTime.Now.AddDays(30), TestData.GetProducts()[0]);

			massdropRepo.MassdropRepo.Collection.Add(newMassdrop);

			Assert.AreEqual(massdropRepo.MassdropRepo.Collection.Count, currentCount + 1);
		}

		[TestMethod]
		public void TestRemoveMassdrop()
		{
			int currentCount = massdropRepo.MassdropRepo.Collection.Count;

			Models.Massdrop toBeRemovedMassdrop = massdropRepo.MassdropRepo.Collection[0];

			massdropRepo.MassdropRepo.Collection.Remove(toBeRemovedMassdrop);

			Assert.AreEqual(massdropRepo.MassdropRepo.Collection.Count, currentCount - 1);
		}
		#endregion
	}
}
