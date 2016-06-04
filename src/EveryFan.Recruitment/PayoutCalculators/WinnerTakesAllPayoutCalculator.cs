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
            var payingPositions = new List<PayingPosition>();

            var winningChipsAmmount = tournament.Entries.Max(a => a.Chips);

            var winners = tournament.Entries
                                    .Where(x => x.Chips == winningChipsAmmount)
                                    .ToList();

            var winnersCount = winners.Count();
            var prizePool = tournament.PrizePool;
            var payout = prizePool / winnersCount;
            var payoutReminder = prizePool % winnersCount;

            for (var i = 0; i < winnersCount; i++)
            {
                var payoutPosition = new PayingPosition()
                {
                    Payout = payout,
                    Position = 1
                };

                payingPositions.Add(payoutPosition);
            }

            if (payoutReminder != 0)
            {
                var randomNumber = new Random((int)DateTime.Now.Ticks);
                var positionsCount = payingPositions.Count();
                var randomIndex = randomNumber.Next(0, positionsCount - 1);

                payingPositions[randomIndex].Payout += payoutReminder;
            }

            return payingPositions;
        }
    }
}
