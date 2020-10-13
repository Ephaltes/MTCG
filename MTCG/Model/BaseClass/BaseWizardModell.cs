using System;

namespace MTCG.Model.BaseClass
{
    public abstract class BaseWizardModell : MonsterCardModell
    {
        public BaseWizardModell()
        {
            MonsterType = MonsterType.Wizard;
        }

        public abstract override double CalculateDamge(CardModell enemyCard);
    }
}
