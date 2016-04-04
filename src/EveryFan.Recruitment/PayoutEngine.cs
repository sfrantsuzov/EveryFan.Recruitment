using System;
using System.Collections.Generic;
using EveryFan.Recruitment.PayoutCalculators;

namespace EveryFan.Recruitment
{
   

    public class PayoutEngine
    {

        IPayoutCalculator calculator;

        public PayoutEngine(IPayoutCalculator calculator)
        {
            this.calculator = calculator;
        }


        public IReadOnlyList<TournamentPayout> Calculate(Tournament tournament)
        {
            return calculator.Calculate(tournament);
        }
        
    }
}
