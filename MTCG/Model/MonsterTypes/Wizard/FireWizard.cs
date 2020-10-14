using System;
using MTCG.Model.BaseClass;

namespace MTCG.Model.MonsterTypes.Wizard
{
    public class FireWizard:BaseWizardModell
    {
        public FireWizard()
        {
            Description = "Story Fire Wizard";
            Name = "Fire Wizard";
            ElementType = CardType.Fire;
            Damage = 1;
        }
        
        public override double CalculateDamge(CardModell enemyCard)
        {
            if (enemyCard.GetType().IsSubclassOf(typeof(SpellCardModell)))
            {
                return 0;
            }
            
            Random rand = new Random();
            return Damage * AttackSpeed * rand.NextDouble();
        }
    }
}