using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace EveryFan.Recruitment.UnitTests
{
    
    public class WinnerTakesAllPayoutCalculatorTests
    {
        
        [Test]
        public void TwoEntries()
        {
            Tournament tournament = new Tournament()
            {
                BuyIn = 250,
                PrizePool = 500,
                Entries = new List<TournamentEntry>()
                {
                    new TournamentEntry()
                    {
                        Chips = 10000,
                        UserId = "roger"
                    },
                    new TournamentEntry()
                    {
                        Chips = 1000,
                        UserId = "jennifer"
                    }
                }
            };

            PayoutEngine calculator = new PayoutEngine(Factory.Get(PayoutScheme.WINNER_TAKES_ALL));
            IReadOnlyList<TournamentPayout> payouts = calculator.Calculate(tournament);

            Assert.AreEqual(1, payouts.Count);
            Assert.AreEqual(500, payouts.Sum(p => p.Payout));
        }

        [Test]
        public void ThreeEntries()
        {
            Tournament tournament = new Tournament()
            {
                BuyIn = 250,
                PrizePool = 750,
                Entries = new List<TournamentEntry>()
                {
                    new TournamentEntry()
                    {
                        Chips = 5000,
                        UserId = "roger"
                    },
                    new TournamentEntry()
                    {
                        Chips = 3000,
                        UserId = "jennifer"
                    },
                    new TournamentEntry()
                    {
                        Chips = 1000,
                        UserId = "billy"
                    },
                }
            };

            PayoutEngine calculator = new PayoutEngine(Factory.Get(PayoutScheme.WINNER_TAKES_ALL));
            IReadOnlyList<TournamentPayout> payouts = calculator.Calculate(tournament);

            Assert.AreEqual(1, payouts.Count);
            Assert.AreEqual(750, payouts.Sum(p => p.Payout));
            Assert.AreEqual(750, payouts[0].Payout);
        }

        [Test]
        public void SplitWinnings()
        {
            Tournament tournament = new Tournament()
            {
                BuyIn = 250,
                PrizePool = 750,
                Entries = new List<TournamentEntry>()
                {
                    new TournamentEntry()
                    {
                        Chips = 5000,
                        UserId = "roger"
                    },
                    new TournamentEntry()
                    {
                        Chips = 5000,
                        UserId = "jennifer"
                    },
                    new TournamentEntry()
                    {
                        Chips = 1000,
                        UserId = "billy"
                    },
                }
            };

            PayoutEngine calculator = new PayoutEngine(Factory.Get(PayoutScheme.WINNER_TAKES_ALL));
            IReadOnlyList<TournamentPayout> payouts = calculator.Calculate(tournament);

            Assert.AreEqual(2, payouts.Count);
            Assert.AreEqual(750, payouts.Sum(p => p.Payout));
            Assert.AreEqual(375, payouts[0].Payout);
            Assert.AreEqual(375, payouts[1].Payout);
        }
      }
}
