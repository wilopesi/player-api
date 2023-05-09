using Player.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Player.Domain.Model;
using System.Net;

namespace Player.Test
{
	[TestClass]
	public class PositionTest
	{
		private PositionService _positionService = new PositionService();

		[TestMethod]
		public void TestGetPositionList()
		{
			BaseResponse response = new BaseResponse();
			try
			{
				response.Data = _positionService.GetPositions().Result;
				response.Message = "Task Finished";
				response.HttpStatusCode = HttpStatusCode.OK;
			}
			catch(Exception exception)
			{
				response.Data = null;
				response.Message = exception.Message;
				response.HttpStatusCode = HttpStatusCode.InternalServerError;
			}
		}


		[TestMethod]
		public void TestGetPosition()
		{
			BaseResponse response = new BaseResponse(); 
			try
			{
				response.Data = _positionService.GetPositions("MEI").Result;
				response.Message = "Task Finished";
				response.HttpStatusCode = HttpStatusCode.OK;
			}
			catch (Exception exception)
			{
				response.Data = null;
				response.Message = exception.Message;
				response.HttpStatusCode = HttpStatusCode.InternalServerError;
			}
		}


	}
}