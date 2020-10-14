using System;

namespace MTCG.Model.BaseClass
{
    public abstract class BaseWizardModell : MonsterCardModell
    {
        public BaseWizardModell()
        {
            MonsterType = MonsterType.Wizard;
            Health = 125;
            Damage = 15;
            AttackSpeed = 1;
        }

        public abstract override double CalculateDamge(CardModell enemyCard);
    }
}
