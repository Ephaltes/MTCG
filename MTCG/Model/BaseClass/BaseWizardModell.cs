using System;

namespace MTCG.Model.BaseClass
{
    public class BaseWizardModell : MonsterCardModell
    {
        public BaseWizardModell()
        {
            MonsterType = MonsterType.Wizard;
        }
        public override double CalculateDamge(CardModell enemyCard)
        {
            throw new NotImplementedException();
        }
    }
}
