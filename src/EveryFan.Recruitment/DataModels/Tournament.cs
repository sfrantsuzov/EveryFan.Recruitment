using System.Collections.Generic;

namespace EveryFan.Recruitment
{
    public class Tournament
    {
        public int PrizePool { get; set; }
        public int BuyIn { get; set; }
        public PayoutScheme PayoutScheme { get; set; }
        public IReadOnlyList<TournamentEntry> Entries { get; set; }
    }
}
