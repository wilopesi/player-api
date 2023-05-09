using System;
using System.Collections.Generic;

namespace Player.Infraestructure.Model
{
    public partial class PlayerModel
	{
        public PlayerModel()
        {
            PlayerStats = new HashSet<PlayerStat>();
        }

        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; } = null!;
        public string Cpf { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? RepresentativeName { get; set; }
        public string? RepresentativePhoneNumber { get; set; }
        public int ShirtNumber { get; set; }
        public string Nickname { get; set; } = null!;
        public int FkFunctionId { get; set; }

        public virtual PlayerFunction FkFunction { get; set; } = null!;
        public virtual ICollection<PlayerStat> PlayerStats { get; set; }
    }
}
