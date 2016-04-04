using System;
using System.Collections.Generic;
using System.Linq;

namespace EveryFan.Recruitment.PayoutCalculators
{
    /// <summary>
    /// Winner takes all payout calculator, the winner recieves the entire prize pool. In the event of a tie for the winning position the
    /// prize pool is split equally between the tied players.
    /// </summary>
    public class WinnerTakesAllPayoutCalculator : BasePayoutCalculator
    {
        public override IReadOnlyList<PayingPosition> GetPayingPositions(Tournament tournament)
        {
            // Result list
            List<PayingPosition> result = new List<PayingPosition>();

            // Query to select winners (users with maximum amoint of chips)
            var query = tournament.Entries.Where(x => x.Chips == tournament.Entries.Max(a => a.Chips));
                      
            // Get the winners
            List<TournamentEntry> winners = query.ToList<TournamentEntry>();

            // Populate the intermediate list
            foreach(var tour in winners)
            {
                result.Add(new PayingPosition() { Payout = (int) tournament.PrizePool / winners.Count(), Position = 1 });
            }

            return (IReadOnlyList <PayingPosition>)result;
        }
    }
}
