using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.Domain.Model.Player
{
	public class RegisterPlayer
	{
		public string FullName { get; set; } = null!;

		public DateTime DateOfBirth { get; set; }

		public string Nationality { get; set; } = null!;

		[Required]
		public string Cpf { get; set; } = null!;

		public string PhoneNumber { get; set; } = null!;

		public string Email { get; set; } = null!;

		public int ShirtNumber { get; set; }

		public string Nickname { get; set; } = null!;

		[MaxLength(3)]
		public string AcronymFunction { get; set; } = null!;

		public string? RepresentativeName { get; set; }

		public string? RepresentativePhoneNumber { get; set; }
	}
}
