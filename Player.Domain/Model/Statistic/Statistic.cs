using Player.Domain.Model.Player;
using System.ComponentModel.DataAnnotations;

namespace Player.Domain.Model.Statistic
{
	public class Statistic
	{ 
		public ShortPlayerInformation Player { get; set; }

		public int? Goals { get; set; }
		public int? Assistance { get; set; }
		public int? YellowCard { get; set; }
		public int? RedCard { get; set; }
		public int? Matches { get; set; }
	}
}
