using System;
using MTCG.Model.BaseClass;

namespace MTCG.Model.MonsterTypes.Wizard
{
    public class FireWizard:MonsterCardModell
    {
        public FireWizard()
        {
            Description = "Story Fire Wizard";
            Name = "Fire Wizard";
            ElementType = BaseClass.ElementType.Fire;
            Health = 125;
            Damage = 15;
            AttackSpeed = 1;
            Race = Race.Wizard;
            CardId = CardId.FireWizard;
        }
    }
}