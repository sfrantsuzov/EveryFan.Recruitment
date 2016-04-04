using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace EveryFan.Recruitment.UnitTests
{
    public class FiftyFiftyPayoutCalculatorTests
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

            PayoutEngine calculator = new PayoutEngine(Factory.Get(PayoutScheme.FIFTY_FIFY));
            IReadOnlyList<TournamentPayout> payouts = calculator.Calculate(tournament );

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

            PayoutEngine calculator = new PayoutEngine(Factory.Get(PayoutScheme.FIFTY_FIFY));
            IReadOnlyList<TournamentPayout> payouts = calculator.Calculate(tournament);

            Assert.AreEqual(2, payouts.Count);
            Assert.AreEqual(750, payouts.Sum(p => p.Payout));
            Assert.AreEqual(500, payouts[0].Payout);
            Assert.AreEqual(250, payouts[1].Payout);
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

            PayoutEngine calculator = new PayoutEngine(Factory.Get(PayoutScheme.FIFTY_FIFY));
            IReadOnlyList<TournamentPayout> payouts = calculator.Calculate(tournament);

            Assert.AreEqual(2, payouts.Count);
            Assert.AreEqual(750, payouts.Sum(p => p.Payout));
            Assert.AreEqual(375, payouts[0].Payout);
            Assert.AreEqual(375, payouts[1].Payout);
        }

        [Test]
        public void OddSplitWinnings()
        {
            Tournament tournament = new Tournament()
            {
                BuyIn = 333,
                PrizePool = 999,
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

            PayoutEngine engine = new PayoutEngine(Factory.Get(PayoutScheme.FIFTY_FIFY));
            IReadOnlyList<TournamentPayout> payouts = engine.Calculate(tournament);

            Assert.AreEqual(2, payouts.Count);
            Assert.That(payouts.Sum(p => p.Payout) == 999 || payouts.Sum(p => p.Payout) == 998);
            Assert.That(payouts[0].Payout == 500 || payouts[0].Payout == 499);
            Assert.That(payouts[1].Payout == 500 || payouts[1].Payout == 499);
        }
    }
}
