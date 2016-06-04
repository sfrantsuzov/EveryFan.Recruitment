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
            var payingPositions = new List<PayingPosition>();

            var positions = tournament.Entries
                                    .GroupBy(w => w.Chips)
                                    .Select(w => new { Chips = w.Key, Count = w.Count() })
                                    .OrderByDescending(x => x.Chips);

            var positionCount = positions.Count();

            var isOddPositionCount = positionCount == 1 || positionCount % 2 != 0;

            var topPositionsCount = positionCount / 2;
            var middlePositionCount = (isOddPositionCount) ? 1 : 0;

            var winningPositionsCount = topPositionsCount + middlePositionCount;

            var winningPositions = positions.Take(winningPositionsCount).ToList();

            var prizePool = tournament.PrizePool;

            var stakes = prizePool / (2 * topPositionsCount + middlePositionCount);
            var stakesReminder = prizePool % (2 * topPositionsCount + middlePositionCount);

            for (var i = 0; i < winningPositions.Count; i++)
            {
                var positionIndex = i + 1;
                var payOutAmmount = stakes * 2;

                var isLastPosition = positionIndex == winningPositionsCount;
                var isSingleStake = isOddPositionCount && isLastPosition ;

                if (isSingleStake)
                {
                    payOutAmmount = stakes;
                }

                var winner = winningPositions[i];

                for (var j = 0; j < winner.Count; j++)
                {
                    var payoutPosition = new PayingPosition()
                    {
                        Position = positionIndex,
                        Payout = payOutAmmount / winner.Count
                    };

                    payingPositions.Add(payoutPosition);
                    positionIndex++;
                }
            }

            var existsReminder = stakesReminder != 0;

            if (existsReminder)
            {
                var randomNumber = new Random((int)DateTime.Now.Ticks);
                var positionsCount = payingPositions.Count();
                var randomIndex = randomNumber.Next(0, positionsCount - 1);

                payingPositions[randomIndex].Payout += stakesReminder;
            }

            return payingPositions;
        }
    }
}
