
namespace Player.Domain.Services
{
	public abstract class BaseService
	{
		public DateTime BrasiliaTime() => TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));

	}
}
