using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExtendedObservableCollection;

namespace Massdrop.Models
{
	public class Discussion : ExtendedNotifyPropertyChanged, IModel
	{
		#region Full Properties

		// The message of this comment
		private string message;

		public string Message
		{
			get { return message; }
			set { SetField(this, ref message, value); }
		}


		// The amount of likes this comment has
		private int likes;

		public int Likes
		{
			get { return likes; }
			set { SetField(this, ref likes, value); }
		}

		// The date this comment was placed
		private DateTime date;

		public DateTime Date
		{
			get { return date; }
			set { SetField(this, ref date, value); }
		}

		// The user that placed this comment
		private User user;

		public User User
		{
			get { return user; }
			set { SetField(this, ref user, value); }
		}

		// The massdrop this comment was placed on
		private Massdrop massdrop;

		public Massdrop Massdrop
		{
			get { return massdrop; }
			set { SetField(this, ref massdrop, value); }
		}

		// The id of the comment
		private int id;

		public int ID
		{
			get { return id; }
			set { SetField(this, ref id, value); }
		}
		#endregion

		#region Properties
		// The replies to this comment
		public ExtendedBindingList<Discussion> Replies { get; set; }
		#endregion

		#region Constructors

		/// <summary>
		/// This constructor is used to make new discussions
		/// </summary>
		/// <param name="id">The id of the discussion</param>
		/// <param name="message">The message of the discussion</param>
		/// <param name="likes">The amount of likes of the discussion</param>
		/// <param name="date">The date the discussion was placed</param>
		/// <param name="user">The user who placed the comment</param>
		/// <param name="massdrop">The massdrops this discussion belongs to</param>
		/// <param name="replies">The replies of this discussion</param>
		public Discussion(int id, string message, int likes, DateTime date, User user, Massdrop massdrop, List<Discussion> replies = null)
		{
			this.ID = id;
			this.Message = message;
			this.Likes = likes;
			this.Date = date;
			this.User = user;
			if (replies != null)
				this.Replies = new ExtendedBindingList<Discussion>(replies);
			else
				this.Replies = new ExtendedBindingList<Discussion>();
			this.Massdrop = massdrop;
		}

		/// <summary>
		/// This constructor is used for making discussions for testdata
		/// </summary>
		/// <param name="message">The message of the discussion</param>
		/// <param name="likes">The amount of likes of the discussion</param>
		/// <param name="date">The date the discussion was placed</param>
		/// <param name="user">The user who placed the comment</param>
		/// <param name="massdrop">The massdrops this discussion belongs to</param>
		/// <param name="replies">The replies of this discussion</param>
		public Discussion(string message, int likes, DateTime date, User user, Massdrop massdrop, List<Discussion> replies = null)
		{
			this.Message = message;
			this.Likes = likes;
			this.Date = date;
			this.User = user;
			if (replies != null)
				this.Replies = new ExtendedBindingList<Discussion>(replies);
			else
				this.Replies = new ExtendedBindingList<Discussion>();
			this.Massdrop = massdrop;
		}
		#endregion
	}
}