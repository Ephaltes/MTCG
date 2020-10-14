using System;
using MTCG.Model.BaseClass;

namespace MTCG.Model.MonsterTypes.Dragon
{
    public class RedDragon:BaseDragonModell
    {
        public RedDragon()
        {
            Description = "Story Red Dragon";
            Name = "Red Dragon";
            ElementType = CardType.Fire;
        }
        
        public override double CalculateDamge(CardModell enemyCard)
        {
            if (enemyCard.GetType().IsSubclassOf(typeof(BaseFireElfModell)) 
                || enemyCard.GetType().IsSubclassOf(typeof(SpellCardModell)) )
            {
                return 0;
            }
            
            Random rand = new Random();

            return Damage * AttackSpeed * rand.NextDouble();

        }
    }
}