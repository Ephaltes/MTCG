using System;

namespace MTCG.Model.BaseClass
{
    public abstract class BaseWizardModell : MonsterCardModell
    {
        public BaseWizardModell()
        {
            Health = 125;
            Damage = 15;
            AttackSpeed = 1;
        }
    }
}
