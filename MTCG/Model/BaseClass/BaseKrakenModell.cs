﻿using System;

namespace MTCG.Model.BaseClass
{
    public class BaseKrakenModell : MonsterCardModell
    {
        public BaseKrakenModell()
        {
            MonsterType = MonsterType.Kraken;
        }
        public override double CalculateDamge()
        {
            throw new NotImplementedException();
        }
    }
}
