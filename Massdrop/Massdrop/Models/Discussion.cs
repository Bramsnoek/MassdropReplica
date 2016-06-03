using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExtendedObservableCollection;

namespace Massdrop.Models
{
	public sealed class Discussion : ExtendedNotifyPropertyChanged, IModel
	{

		private string message;

		public string Message
		{
			get { return message; }
			set { SetField(this, ref message, value); }
		}

		private int likes;

		public int Likes
		{
			get { return likes; }
			set { SetField(this, ref likes, value); }
		}

		private DateTime date;

		public DateTime Date
		{
			get { return date; }
			set { SetField(this, ref date, value); }
		}

		private User user;

		public User User
		{
			get { return user; }
			set { SetField(this, ref user, value); }
		}

		private Models.Massdrop massdrop;

		public Models.Massdrop Massdrop
		{
			get { return massdrop; }
			set { SetField(this, ref massdrop, value); }
		}


		private ExtendedBindingList<Discussion> replies;

		public ExtendedBindingList<Discussion> Replies
		{
			get { return replies; }
			set { SetField(this, ref replies, value); }
		}

		private int id;

		public int ID
		{
			get { return id; }
			set { SetField(this, ref id, value); }
		}
		public Discussion(int id, string message, int likes, DateTime date, User user, Massdrop massdrop, List<Discussion> replies = null)
		{
			this.ID = id;
			this.Message = message;
			this.Likes = likes;
			this.Date = date;
			this.User = user;
			this.Replies = new ExtendedBindingList<Discussion>(replies);
			this.Massdrop = massdrop;
		}

		public void LikeMessage(Discussion discussion)
		{

		}


	}
}