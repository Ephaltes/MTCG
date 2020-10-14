using System;
using MTCG.Model.BaseClass;

namespace MTCG.Model.MonsterTypes.Knight
{
    public class GalaxyKnight:BaseKnightModell
    {
        public GalaxyKnight()
        {
            Description = "Story Galaxy Knight";
            Name = "Galaxy Knight";
            ElementType = CardType.Normal;
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