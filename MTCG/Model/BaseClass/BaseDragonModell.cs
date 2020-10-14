using System;

namespace MTCG.Model.BaseClass
{
    public abstract class BaseDragonModell : MonsterCardModell
    {

        public BaseDragonModell()
        {
            MonsterType = MonsterType.Dragon;
            Health = 200;
            Damage = 20;
            AttackSpeed = 1;
        }

        public abstract override double CalculateDamge(CardModell enemyCard);
    }
}
