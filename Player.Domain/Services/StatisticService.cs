using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Player.Domain.Model.Player;
using Player.Domain.Model.Statistic;
using Player.Infraestructure.Contexts;
using Player.Infraestructure.Model;

namespace Player.Domain.Services
{
	public class StatisticService : BaseService
	{
		public readonly PlayersContext _playersContext;
		public StatisticService()
		{
			_playersContext = new PlayersContext();
		}

		public async Task<Statistic?> GetStatistic(string document)
		{
			var hasPlayer = await _playersContext.Players.Where(x => x.Cpf == document).SingleOrDefaultAsync();

			if (hasPlayer == null)
				return null;

			var statisticPlayer = await _playersContext.PlayerStats.Where(x => x.FkPlayerId == hasPlayer.Id).SingleAsync();

			ShortPlayerInformation shortPlayerInfo = new ShortPlayerInformation
			{
				Nickname = hasPlayer.Nickname,
				Document = document,
			};

			Statistic response = new Statistic
			{
				Player = shortPlayerInfo,
				Assistance = statisticPlayer.Assistance,
				Goals = statisticPlayer.Goals,
				Matches = statisticPlayer.Matches,
				RedCard = statisticPlayer.RedCard,
				YellowCard = statisticPlayer.YellowCard,
			};

			return response;
		}

		public async Task UpdateStatistic(Statistic statistic, int playerId)
		{
			PlayerStat playerStats = _playersContext.PlayerStats.Where(x => x.FkPlayerId == playerId).Single();

			playerStats.Assistance += Math.Max(statistic.Assistance ?? 0, 0);
			playerStats.Matches += Math.Max(statistic.Matches ?? 0, 0);
			playerStats.RedCard += Math.Max(statistic.RedCard ?? 0, 0);
			playerStats.YellowCard += Math.Max(statistic.YellowCard ?? 0, 0);
			playerStats.Goals += Math.Max(statistic.Goals ?? 0, 0);

			await _playersContext.SaveChangesAsync();
		}



	}
}
