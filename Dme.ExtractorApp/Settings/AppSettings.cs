using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dme.Services.UsersStoreClient.Settings;

namespace Dme.ExtractorApp.Settings
{
	internal class AppSettings
	{
		public string ConnectionString { get; set; }
		public UsersStoreClientSettings UsersStoreClient { get; set; }
	}
}
