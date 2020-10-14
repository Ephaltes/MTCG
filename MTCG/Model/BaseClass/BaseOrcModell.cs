using System;

namespace MTCG.Model.BaseClass
{
    public abstract class BaseOrcModell : MonsterCardModell
    {
        public BaseOrcModell()
        {
            MonsterType = MonsterType.Orc;
            Health = 120;
            Damage = 2.5;
            AttackSpeed = 5;
        }

        public abstract override double CalculateDamge(CardModell enemyCard);
    }
}
