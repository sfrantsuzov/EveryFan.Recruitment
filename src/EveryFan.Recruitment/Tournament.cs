using EveryFan.Recruitment.PayoutCalculators;
using System;
using System.Collections.Generic;

namespace EveryFan.Recruitment
{
    public class Tournament
    {
        public int PrizePool { get; set; }
        public int BuyIn { get; set; }
        //public PayoutScheme PayoutScheme { get; set; }
        public IReadOnlyList<TournamentEntry> Entries { get; set; }
    }

    public static class Factory
    {
        public static IPayoutCalculator Get(PayoutScheme calculator)
        {
            switch (calculator)
            {
                case PayoutScheme.FIFTY_FIFY:
                    return new FiftyFiftyPayoutCalculator();
                case PayoutScheme.WINNER_TAKES_ALL:
                    return new WinnerTakesAllPayoutCalculator();
                default:
                    throw new ArgumentOutOfRangeException(nameof(calculator));
            }
        }
    }

}
