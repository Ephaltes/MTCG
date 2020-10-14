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
            
            Random rand = new Random();
            return Damage * AttackSpeed * rand.NextDouble();
        }
    }
}