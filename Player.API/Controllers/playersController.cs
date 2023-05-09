using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Player.Domain.Model.Player;
using Player.Domain.Model.Statistic;
using Player.Domain.Services;
using Player.Infraestructure.Model;

namespace Player.API.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	[EnableCors]

	public class playersController : ControllerBase
	{
		private PlayerService _playerService;

		public playersController()
		{
			_playerService = new PlayerService();
		}


		[HttpGet]
		public async Task<List<PlayerInformation>> GetPlayers()
		{
			return await _playerService.GetPlayers();
		}

		[HttpGet]
		[Route("cpf")]
		public async Task<ActionResult<PlayerInformation>> GetPlayers(string cpf)
		{
			var response = await _playerService.GetPlayers(cpf);

			if (response == null)
				return NotFound();

			return response;
		}

		[HttpPost]
		public async Task<ActionResult<PlayerModel>> CreatePlayer([FromBody] RegisterPlayer player)
		{
			try
			{
				var newPlayer = await _playerService.CreatePlayer(player);
				return CreatedAtAction(nameof(CreatePlayer), new { id = newPlayer.Id }, newPlayer);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		[Route("cpf")]
		public async Task<ActionResult<Statistic>> UpdatePlayer(string cpf, [FromBody] UpdatePlayer player)
		{
			var playerId = _playerService.GetPlayerId(cpf);

			if (playerId == null)
				return NotFound();
			else
				await _playerService.UpdatePlayer(player, playerId.Value);

			return StatusCode(200);
		}

		[HttpDelete]
		[Route("cpf")]
		public async Task<ActionResult> Delete(string cpf)
		{
			var playerId = _playerService.GetPlayerId(cpf);

			if (playerId == null)
				return NotFound();

			await _playerService.DeletePlayer(playerId.Value);
			return StatusCode(200);
		}


	};
}
