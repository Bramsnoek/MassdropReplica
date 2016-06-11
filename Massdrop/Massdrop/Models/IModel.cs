using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massdrop.Models
{
	/// <summary>
	/// This interface makes sure every model class has an ID
	/// </summary>
	public interface IModel
	{
		// The id all model classes will have
		int ID { get; set; }
	}
}
