using System;

namespace MTCG.Model.BaseClass
{
    public abstract class BaseGoblinModell : MonsterCardModell
    {
        public BaseGoblinModell()
        {
            Health = 100;
            Damage = 5;
            AttackSpeed = 5;
        }

        public new double CalculateDamge(CardModell enemyCard)
        {
            if (enemyCard.GetType().IsSubclassOf(typeof(BaseDragonModell)))
            {
                return 0;
            }

            return base.CalculateDamge(enemyCard);
        }
    }
}
