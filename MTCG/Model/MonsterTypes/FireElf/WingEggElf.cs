using System;
using MTCG.Model.BaseClass;

namespace MTCG.Model.MonsterTypes.FireElf
{
    public class WingEggElf:BaseFireElfModell
    {
        public WingEggElf()
        {
            Description = "Story Elf";
            Name = "Wing Egg Elf";
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