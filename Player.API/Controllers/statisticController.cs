using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Player.Domain.Model.Statistic;
using Player.Domain.Services;


namespace Player.API.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	[EnableCors]

	public class statisticController : ControllerBase
	{
		private StatisticService _statisticService;
		private PlayerService _playerService;

		public statisticController()
		{
			_statisticService = new StatisticService();
			_playerService = new PlayerService();
		}

		[HttpGet]
		[Route("cpf")]
		public async Task<ActionResult<Statistic>> GetStatistic(string cpf)
		{
			var response = await _statisticService.GetStatistic(cpf);

			if (response == null)
				return NotFound();

			return response;
		}

		[HttpPut]
		[Route("cpf")]
		public async Task<ActionResult<Statistic>> UpdateStatistic(string cpf,[FromBody] Statistic statistic)
		{
			if (cpf != statistic.Player.Document)
				return BadRequest("The entered key cpf is different from the object's body key.");

			var playerId = _playerService.GetPlayerId(statistic);

			if(playerId == null)
				return NotFound();
			else
				await _statisticService.UpdateStatistic(statistic, playerId.Value);

			return StatusCode(200);
		}

	};
}
