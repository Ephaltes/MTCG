using System;
using MTCG.Model.BaseClass;

namespace MTCG.Model.MonsterTypes.Kraken
{
    public class DemonKraken:BaseKrakenModell
    {
        public DemonKraken()
        {
            Description = "Story Demon Kraken";
            Name = "Demon Kraken";
            ElementType = CardType.Water;
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