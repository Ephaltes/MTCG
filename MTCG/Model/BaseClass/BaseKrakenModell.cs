using System;

namespace MTCG.Model.BaseClass
{
    public abstract class BaseKrakenModell : MonsterCardModell
    {
        public BaseKrakenModell()
        {
            MonsterType = MonsterType.Kraken;
        }

        public abstract override double CalculateDamge(CardModell enemyCard);
    }
}
