using TennisScoreboard.Data;
using TennisScoreboard.Models;

namespace TennisScoreboard.Services
{
    public class PlayerSerise : IPlayerSerise
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerSerise(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }
        public Player GetPlayerByName(string playerName)
        {
            Player player = new();
            try
            {
                player = _playerRepository.GetPlayer(playerName).Result;
            }
            catch (Exception)
            {
                player = AddNewPlayer(playerName);
            }
            return player;
        }
        public Player GetPlayerById(int id)
        {
            var player = _playerRepository.GetPlayer(id).Result;
            return player;
        }
        private Player AddNewPlayer(string playerName)
        {
            var player = new Player { Name = playerName };
            _playerRepository.AddNewPlayer(player);
            return player;
        }
    }
}
