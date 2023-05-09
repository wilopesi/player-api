using System;
using System.Collections.Generic;

namespace Player.Infraestructure.Model
{
    public partial class PlayerStat
    {
        public int Id { get; set; }
        public int Goals { get; set; }
        public int Assistance { get; set; }
        public int YellowCard { get; set; }
        public int RedCard { get; set; }
        public int Matches { get; set; }
        public int FkPlayerId { get; set; }

        public virtual PlayerModel FkPlayer { get; set; } = null!;
    }
}
