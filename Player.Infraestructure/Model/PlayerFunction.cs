using System;
using System.Collections.Generic;

namespace Player.Infraestructure.Model
{
    public partial class PlayerFunction
    {
        public int Id { get; set; }
        public string NameFunction { get; set; } = null!;
        public string AcronymFunction { get; set; } = null!;
    }
}
