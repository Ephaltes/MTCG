using System;

namespace MTCG.Model.BaseClass
{
    public abstract class BaseFireElfModell:MonsterCardModell
    {
        public BaseFireElfModell()
        {
            MonsterType = MonsterType.FireElve;
            Health = 125;
            Damage = 5;
            AttackSpeed = 3;
        }

        public abstract override double CalculateDamge(CardModell enemyCard);
    }
}
