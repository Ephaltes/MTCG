using System;

namespace MTCG.Model.BaseClass
{
    public abstract class BaseGoblinModell : MonsterCardModell
    {
        public BaseGoblinModell()
        {
            MonsterType = MonsterType.Goblin;
            Health = 100;
            Damage = 5;
            AttackSpeed = 5;
        }

        public abstract override double CalculateDamge(CardModell enemyCard);
    }
}
