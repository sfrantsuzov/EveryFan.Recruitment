using System;
using System.Collections.Generic;
using System.Linq;

namespace EveryFan.Recruitment.PayoutCalculators
{
    /// <summary>
    /// FiftyFifty payout calculator. The 50/50 payout scheme returns double the tournament buyin to people
    /// who finish in the top half of the table. If the number of runners is odd the player in the middle position
    /// should get their stake back. Any tied positions should have the sum of the amount due to those positions
    /// split equally among them.
    /// </summary>
    public class FiftyFiftyPayoutCalculator : BasePayoutCalculator
    {
            
        public override IReadOnlyList<PayingPosition> GetPayingPositions(Tournament tournament)
        {
            // Result list
            List<PayingPosition> result = new List<PayingPosition>();

            // Amount of player
            var playerAmount = tournament.Entries.Select(x => x.UserId).Distinct().Count();
                        
            // Get the top half
            List<TournamentEntry> topHalf = tournament.Entries.OrderByDescending(x => x.Chips)
                // Take an extra player if there are an odd number of players
                .Take((int) playerAmount / 2 + (playerAmount % 2 != 0 ? 1 : 0))
                .ToList<TournamentEntry> ();


            // Populate the intermediate list
            for ( var i =0;  i < topHalf.Count(); i++)
            {
                var thied = tournament.Entries.Where(x => x.Chips == topHalf[i].Chips).Count();

                if (thied > 1)
                {
                    result.Add(new PayingPosition() { Payout = (int)  tournament.PrizePool / thied, Position = i + 1 });
                }
                else
                {

                    // Is number of player odd?
                    if (topHalf[i].Equals(topHalf.Last()) && playerAmount % 2 != 0)
                    {
                        result.Add(new PayingPosition() { Payout = (int)tournament.BuyIn, Position = i + 1 });
                    }
                    else
                    {
                        result.Add(new PayingPosition() { Payout = (int)tournament.BuyIn * 2, Position = i + 1 });
                    }
                }
            }

            return result;
        }
    }
}
