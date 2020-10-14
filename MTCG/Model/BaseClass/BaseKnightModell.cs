using System;

namespace MTCG.Model.BaseClass
{
    public abstract class BaseKnightModell : MonsterCardModell
    {
        public BaseKnightModell()
        {
            MonsterType = MonsterType.Knight;
            Health = 150;
            Damage = 5;
            AttackSpeed = 2;
        }

        public abstract override double CalculateDamge(CardModell enemyCard);
    }
}
