namespace TennisScoreboard.Contracts
{
    public record PlayerScoreContracts
    {
        public int Id { get; set; }
        public int Set { get; set; } = 0;
        public int Game { get; set; } = 0;
        public string Point { get; set; } = string.Empty;
      
    }
}
