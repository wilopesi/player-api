using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Player.Domain.Model
{
	public class BaseResponse
	{
		public string? Message { get; set; }
		public HttpStatusCode HttpStatusCode { get; set; }
		public virtual object? Data { get; set; }	
	}

}
