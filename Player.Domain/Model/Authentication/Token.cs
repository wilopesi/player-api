using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.Domain.Model.Authentication
{
    public class Token
    {
        public string token_type { get; set; }

        public string token { get; set; }

        public int expires_in { get; set; }
    }
}
