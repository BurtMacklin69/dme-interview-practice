using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dme.Services.UsersStoreClient.Models
{
	internal class StoreResponse
	{
		public UserDto[] Results { get; set; }
		public InfoDto Info { get; set; }
	}
}
