using System;

namespace MTCG.Model.BaseClass
{
    public abstract class BaseOrcModell : MonsterCardModell
    {
        public BaseOrcModell()
        {
            Health = 120;
            Damage = 2.5;
            AttackSpeed = 5;
        }

        public override double CalculateDamge(CardModell enemyCard)
        {
            if (enemyCard.GetType().IsSubclassOf(typeof(BaseWizardModell)))
            {
                return 0;
            }
            return base.CalculateDamge(enemyCard);
        }
    }
}
