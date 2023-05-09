using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.Utils.AppSettings
{
	public static class ConfigurationManager
	{
		public static IConfiguration AppSettings { get; }

		static ConfigurationManager()
		{
			AppSettings = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();
		}

	}
}
