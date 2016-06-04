using EveryFan.Recruitment.PayoutCalculators;
using System;

namespace EveryFan.Recruitment.Factories
{
    public class PayoutCalculatroFactory
    {
        public IPayoutCalculator Get(PayoutScheme calculator)
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

