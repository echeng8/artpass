namespace artpass.Models
{
    public class DeadlockMatch
    {
        public int Id { get; set; }
        public bool IsWin { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Username { get; set; }

        public DeadlockMatch(int id, bool isWin, DateTime timestamp, string? username = null)
        {
            Id = id;
            IsWin = isWin;
            Timestamp = timestamp;
            Username = username;
        }
        public override string ToString()
        {
            return $"Match {Id} - {Username} - IsWin: {IsWin} - {Timestamp}";
        }
    }
}
