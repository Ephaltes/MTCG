using System;

namespace MTCG.Model.BaseClass
{
    public abstract class BaseDragonModell : MonsterCardModell
    {

        public BaseDragonModell()
        {
            Health = 200;
            Damage = 20;
            AttackSpeed = 1;
        }

        public new double CalculateDamge(CardModell enemyCard)
        {
            if (enemyCard.GetType().IsSubclassOf(typeof(BaseFireElfModell)))
            {
                return 0;
            }

            return base.CalculateDamge(enemyCard);

        }
    }
}
