using System;
using System.Collections.Generic;
using EveryFan.Recruitment.PayoutCalculators;
using EveryFan.Recruitment.Factories;

namespace EveryFan.Recruitment
{
    public class PayoutEngine
    {
        private PayoutCalculatroFactory _calculatorFactory;

        public PayoutEngine(PayoutCalculatroFactory calculatorFactory)
        {
            this._calculatorFactory = calculatorFactory;
        }

        public IReadOnlyList<TournamentPayout> Calculate(Tournament tournament)
        {
            return _calculatorFactory.Get(tournament.PayoutScheme).Calculate(tournament);
        }
    }
}
