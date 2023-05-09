using Microsoft.EntityFrameworkCore;
using Player.Infraestructure.Contexts;
using Player.Infraestructure.Model;

namespace Player.Domain.Services
{
	public class PositionService : BaseService
	{
		public readonly PlayersContext _playersContext;
		public PositionService()
		{
			_playersContext = new PlayersContext();
		}

		public async Task<IEnumerable<PlayerFunction>> GetPositions()
		{
			return await _playersContext.PlayerFunctions.ToListAsync();
		}

		public async Task<PlayerFunction> GetPositions(string acronym)
		{
			return await _playersContext.PlayerFunctions.Where(x => x.AcronymFunction == acronym).SingleOrDefaultAsync();
		}

		public async Task<PlayerFunction> CreatePosition(PlayerFunction function)
		{
			_playersContext.PlayerFunctions.Add(function);
			await _playersContext.SaveChangesAsync();
			return function;
		}

		public async Task DeletePosition(int id)
		{
			var functionToDelete = await _playersContext.PlayerFunctions.FindAsync(id);
			_playersContext.PlayerFunctions.Remove(functionToDelete);
			await _playersContext.SaveChangesAsync();
		}

		public async Task UpdatePosition(PlayerFunction function)
		{
			_playersContext.Entry(function).State = EntityState.Modified;
			await _playersContext.SaveChangesAsync();
		}

	}
}
