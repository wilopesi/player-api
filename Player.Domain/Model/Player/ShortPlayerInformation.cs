using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Player.Domain.Model.Player
{
	public class ShortPlayerInformation
	{
		public string? Nickname { get; set; }

		[Required]
		public string Document { get; set; }
	}
}
