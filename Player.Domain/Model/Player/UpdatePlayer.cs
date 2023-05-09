using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.Domain.Model.Player
{
	public class UpdatePlayer
	{
		public string? FullName { get; set; }

		public DateTime? DateOfBirth { get; set; }

		public string? Nationality { get; set; }

		public string? Cpf { get; set; }

		public string? PhoneNumber { get; set; }

		public string? Email { get; set; }

		public int? ShirtNumber { get; set; }

		public string? Nickname { get; set; }

		public string? AcronymFunction { get; set; }

		public string? RepresentativeName { get; set; }

		public string? RepresentativePhoneNumber { get; set; }
	}
}
