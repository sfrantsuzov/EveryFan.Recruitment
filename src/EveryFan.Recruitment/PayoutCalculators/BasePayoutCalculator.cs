using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryFan.Recruitment.PayoutCalculators
{
    /// <summary>
    /// Base payout calculator. This additional abstract base class allows you to change the interface (IPayoutCalculator) without breaking the implementation of sub-classes.
    /// </summary>
    public abstract class BasePayoutCalculator :  IPayoutCalculator
    {
        public virtual IReadOnlyList<PayingPosition> GetPayingPositions(Tournament tournament)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<TournamentPayout> Calculate(Tournament tournament)
        {
            IReadOnlyList<PayingPosition> payingPositions = this.GetPayingPositions(tournament);
            IReadOnlyList<TournamentEntry> orderedEntries = tournament.Entries.OrderByDescending(p => p.Chips).ToList();

            List<TournamentPayout> payouts = new List<TournamentPayout>();
            payouts.AddRange(payingPositions.Select((p, i) => new TournamentPayout()
            {
                Payout = p.Payout,
                UserId = orderedEntries[i].UserId
            }));

            return payouts;
        }
    }
}
