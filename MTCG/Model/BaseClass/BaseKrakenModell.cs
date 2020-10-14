using System;

namespace MTCG.Model.BaseClass
{
    public abstract class BaseKrakenModell : MonsterCardModell
    {
        public BaseKrakenModell()
        {
            MonsterType = MonsterType.Kraken;
            Health = 175;
            Damage = 7.5;
            AttackSpeed = 1.2;
        }

        public abstract override double CalculateDamge(CardModell enemyCard);
    }
}
