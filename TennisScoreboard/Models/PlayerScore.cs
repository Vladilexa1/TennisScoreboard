namespace TennisScoreboard.Models
{
    public class PlayerScore
    {
        public int Id { get; set; }
        public int Set { get; set; } = 0;
        public int Game { get; set; } = 0;
        public int Point { get; set; } = 0;
    }
}
