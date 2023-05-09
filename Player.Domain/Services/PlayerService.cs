using Microsoft.EntityFrameworkCore;
using Player.Domain.Model.Player;
using Player.Domain.Model.Statistic;
using Player.Infraestructure.Contexts;
using Player.Infraestructure.Model;
using System.Text.RegularExpressions;

namespace Player.Domain.Services
{
	public class PlayerService : BaseService
	{
		public readonly PlayersContext _playersContext;
		public PlayerService()
		{
			_playersContext = new PlayersContext();
		}

		public async Task<List<PlayerInformation>> GetPlayers()
		{
			var response = await _playersContext.Players.ToListAsync();

			var listInformation = new List<PlayerInformation>();

			var playerInfos = from player in _playersContext.Players
							  join func in _playersContext.PlayerFunctions on player.FkFunctionId equals func.Id
							  select new PlayerInformation
							  {
								  Id = player.Id,
								  Email = player.Email,
								  Age = player.Age,
								  Cpf = player.Cpf,
								  DateOfBirth = player.DateOfBirth,
								  FullName = player.FullName,
								  Function = func.NameFunction,
								  AcronymFunction = func.AcronymFunction,
								  Nationality = player.Nationality,
								  Nickname = player.Nickname,
								  PhoneNumber = player.PhoneNumber,
								  RepresentativeName = player.RepresentativeName ?? "N/A",
								  RepresentativePhoneNumber = player.RepresentativePhoneNumber ?? "N/A",
								  ShirtNumber = player.ShirtNumber
							  };

			listInformation = playerInfos.ToList();
			return listInformation;
		}

		public async Task<PlayerInformation?> GetPlayers(string document)
		{
			var hasPlayer = await _playersContext.Players.Where(x => x.Cpf == document).SingleOrDefaultAsync();

			if (hasPlayer == null)
				return null;

			var playerInfo = from player in _playersContext.Players
							 join func in _playersContext.PlayerFunctions on player.FkFunctionId equals func.Id
							 where player.Cpf == document
							 select new PlayerInformation
							 {
								 Id = player.Id,
								 Email = player.Email,
								 Age = player.Age,
								 Cpf = player.Cpf,
								 DateOfBirth = player.DateOfBirth,
								 FullName = player.FullName,
								 Function = func.NameFunction,
								 AcronymFunction = func.AcronymFunction,
								 Nationality = player.Nationality,
								 Nickname = player.Nickname,
								 PhoneNumber = player.PhoneNumber,
								 RepresentativeName = player.RepresentativeName ?? "N/A",
								 RepresentativePhoneNumber = player.RepresentativePhoneNumber ?? "N/A",
								 ShirtNumber = player.ShirtNumber
							 };


			return playerInfo.Single();
		}

		public int? GetPlayerId(Statistic statistic)
		{
			var player = _playersContext.Players.Where(x => x.Cpf == statistic.Player.Document).FirstOrDefault();

			if (player == null)
				return null;

			return player.Id;
		}

		public int? GetPlayerId(string cpf)
		{
			var player = _playersContext.Players.Where(x => x.Cpf == cpf).FirstOrDefault();

			if (player == null)
				return null;

			return player.Id;
		}

		public async Task<PlayerModel> CreatePlayer(RegisterPlayer player)
		{
			PlayerModel newPlayer = ValidatePlayerInformation(player);
			_playersContext.Players.Add(newPlayer);
			await _playersContext.SaveChangesAsync();
			return _playersContext.Entry(newPlayer).Entity;
		}

		public PlayerModel ValidatePlayerInformation(RegisterPlayer player)
		{
			int age, functionId;
			string phoneNumber;
			string? phoneNumberRepresentative;

			ValidateCpf(player.Cpf);
			ValidateFullName(player.FullName);
			ValidateDateOfBirth(player.DateOfBirth, out age);
			ValidateNationality(player.Nationality);
			ValidatePhoneNumber(player.PhoneNumber, out phoneNumber);
			ValidateEmail(player.Email);
			ValidateShirtNumber(player.ShirtNumber);
			ValidateNickname(player.Nickname);
			ValidateAcronym(player.AcronymFunction, out functionId);
			ValidateRepresentativePhoneNumber(player.RepresentativePhoneNumber, out phoneNumberRepresentative);
			ValidateRepresentativeName(player.RepresentativeName);

			var newModel = new PlayerModel
			{
				Email = player.Email,
				Age = age,
				Cpf = player.Cpf,
				DateOfBirth = player.DateOfBirth,
				FkFunctionId = functionId,
				FullName = player.FullName,
				Nationality = player.Nationality,
				Nickname = player.Nickname,
				PhoneNumber = phoneNumber,
				RepresentativeName = player.RepresentativeName,
				RepresentativePhoneNumber = player.RepresentativePhoneNumber,
				ShirtNumber = player.ShirtNumber
			};

			return newModel;
		}

		public async Task UpdatePlayer(UpdatePlayer player, int playerId)
		{
			int age, functionId;
			string phoneNumber;
			string? phoneNumberRepresentative;

			var playerUpdated = _playersContext.Players.Single(x => x.Id == playerId);

			var validationDict = new Dictionary<string, Action>
			{
				{ nameof(player.Nationality), () => {
					ValidateNationality(player.Nationality);
					playerUpdated.Nationality = player.Nationality;
				}},
				{ nameof(player.Cpf), () => {
					ValidateCpf(player.Cpf);
					playerUpdated.Cpf = player.Cpf;
				}},
				{ nameof(player.FullName), () => {
					ValidateFullName(player.FullName);
					playerUpdated.FullName = player.FullName;
				}},
				{ nameof(player.DateOfBirth), () => {
					ValidateDateOfBirth(player.DateOfBirth.Value, out age);
					var birthDay = (DateTime)player.DateOfBirth;
					playerUpdated.DateOfBirth = birthDay;
					playerUpdated.Age = age;
				}},
				{ nameof(player.PhoneNumber), () => {
					ValidatePhoneNumber(player.PhoneNumber, out phoneNumber);
					playerUpdated.PhoneNumber = player.PhoneNumber;
				}},
				{ nameof(player.Email), () => {
					ValidateEmail(player.Email);
					playerUpdated.Email = player.Email;
				}},
				{ nameof(player.ShirtNumber), () => {
					ValidateShirtNumber(player.ShirtNumber);
					playerUpdated.ShirtNumber = player.ShirtNumber.Value;
				}},
				{ nameof(player.Nickname), () => {
					ValidateNickname(player.Nickname);
					playerUpdated.Nickname = player.Nickname;
				}},
				{ nameof(player.AcronymFunction), () => {
					ValidateAcronym(player.AcronymFunction, out functionId);
					playerUpdated.FkFunctionId = functionId;
				}},
				{ nameof(player.RepresentativePhoneNumber), () => {
					ValidateRepresentativePhoneNumber(player.RepresentativePhoneNumber, out phoneNumberRepresentative);
					playerUpdated.RepresentativePhoneNumber = player.RepresentativePhoneNumber;
				}},
				{ nameof(player.RepresentativeName), () => {
					ValidateRepresentativeName(player.RepresentativeName);
					playerUpdated.RepresentativeName = player.RepresentativeName;
				}}
			};

			foreach (var entry in validationDict)
			{
				var propertyValue = typeof(UpdatePlayer).GetProperty(entry.Key)?.GetValue(player);

				if (propertyValue != null)
				{
					entry.Value();
				}
			}

			await _playersContext.SaveChangesAsync();
		}

		public async Task DeletePlayer(int playerId)
		{
			var playerToDelete = await _playersContext.Players.FindAsync(playerId);
			_playersContext.Players.Remove(playerToDelete);
			await _playersContext.SaveChangesAsync();
		}

		private bool ValidateFullName(string fullName)
		{
			if (string.IsNullOrEmpty(fullName))
				throw new Exception("The fullname field is required.");

			if (fullName.Length > 100 || !Regex.IsMatch(fullName, @"^[a-zA-Z ]+$"))
				throw new Exception("The fullName field is invalid. It must be a string or array type with a maximum length of '100'.");

			return true;
		}

		private bool ValidateDateOfBirth(DateTime dateOfBirth, out int age)
		{
			if (dateOfBirth > DateTime.Now)
			{
				age = -1;
				throw new Exception("The field dateOfBirth is invalid.");
			}

			age = DateTime.Today.Year - dateOfBirth.Year;
			if (dateOfBirth.Date > DateTime.Today.AddYears(-age))
				age--;

			return true;
		}

		private bool ValidateNationality(string nationality)
		{
			if (string.IsNullOrEmpty(nationality))
				throw new Exception("The field nationality is required.");

			if (nationality.Length > 30 || !Regex.IsMatch(nationality, @"^[a-zA-Z\s]+$"))
				throw new Exception("The field nationality is invalid. It must be a string or array type with a maximum length of '30'.");

			return true;
		}

		private bool ValidateCpf(string cpf)
		{
			if (string.IsNullOrEmpty(cpf))
				throw new Exception("The field cpf is required");

			if (cpf.Length != 11 || !Regex.IsMatch(cpf, @"^\d+$"))
				throw new Exception("The field cpf is invalid.It must contain only digits and have a length of '11'.");

			var exist = _playersContext.Players.Where(x => x.Cpf == cpf).FirstOrDefault();

			if (exist != null)
				throw new Exception("The informed cpf is already registered.");

			return true;
		}

		public static bool ValidatePhoneNumber(string phoneNumber, out string phone)
		{
			if (string.IsNullOrWhiteSpace(phoneNumber))
				throw new Exception("The field phoneNumber is required.");

			phoneNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());
			phone = phoneNumber;

			if (phoneNumber.Length > 20 || phoneNumber.Length <= 0)
				throw new Exception("The field phoneNumber is invalid.It must contain only digits with a maximum length of '20'.");

			return true;
		}

		public static bool ValidateRepresentativePhoneNumber(string? phoneNumber, out string? phone)
		{
			phone = phoneNumber;

			if (string.IsNullOrWhiteSpace(phoneNumber))
				return true;

			phoneNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());
			phone = phoneNumber;

			var regex = new Regex("^[a-zA-Z0-9 ]*$");
			var match = regex.IsMatch(phoneNumber);

			if (phoneNumber.Length <= 0 || phoneNumber.Length > 20)
				throw new Exception("The field representativePhoneNumber is invalid.It must contain only digits with a maximum length of '20'.");

			return true;
		}

		public static bool ValidateEmail(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
				throw new Exception("The field email is required.");

			var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
			var match = emailRegex.IsMatch(email);

			if (!match || email.Length > 30)
				throw new Exception("The email provided is invalid or exceeds the maximum length of 30 characters.");

			return true;
		}

		public static bool ValidateRepresentativeName(string? representativeName)
		{
			if (string.IsNullOrWhiteSpace(representativeName))
				return true;

			var regex = new Regex("^[a-zA-Z0-9 ]*$");
			var match = regex.IsMatch(representativeName);

			if (!match || representativeName.Length > 100)
				throw new Exception("Invalid representative name. It allows a maximum of 100 characters and no special characters are allowed.");

			return true;
		}

		public bool ValidateShirtNumber(int? shirtNumber)
		{
			if (shirtNumber == null)
				throw new Exception("The field shirtNumber is required.");

			if (shirtNumber <= 0 || shirtNumber > 999)
				throw new Exception("The shirtNumber must be greater than 0 and less than or equal to 999.");

			return true;
		}

		public bool ValidateNickname(string nickname)
		{
			if (string.IsNullOrEmpty(nickname))
				throw new Exception("Nickname cannot be null or empty.");

			if (nickname.Length > 20)
				throw new Exception("Nickname length cannot exceed 20 characters.");

			return true;
		}

		public void ValidateAcronym(string acronym, out int id)
		{
			var response = _playersContext.PlayerFunctions.Where(x => x.AcronymFunction == acronym).FirstOrDefault();

			if (response == null)
				throw new Exception("Position/Function does not exist! Search the positions endpoint for existing acronyms.");

			id = response.Id;
		}

	}
}
