using System;
using MTCG.Model.BaseClass;

namespace MTCG.Model.MonsterTypes.Orc
{
    public class OrcWarrior:BaseOrcModell
    {
        public OrcWarrior()
        {
            Description = "Story Orc Warrior";
            Name = "Orc Warrior";
            ElementType = CardType.Normal;
            Damage = 1;
        }
        
        public override double CalculateDamge(CardModell enemyCard)
        {
            if (enemyCard.GetType().IsSubclassOf(typeof(BaseWizardModell)))
            {
                return 0;
            }
            
            Random rand = new Random();
            return Damage * AttackSpeed * rand.NextDouble();
        }
    }
}