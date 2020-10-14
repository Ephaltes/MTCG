using System;
using MTCG.Model.BaseClass;

namespace MTCG.Model.MonsterTypes.Goblin
{
    public class GoblinLackey:BaseGoblinModell
    {
        public GoblinLackey()
        {
            Description = "Story Goblin Lackey";
            Name = "Goblin Lackey";
            ElementType = CardType.Normal;

        }
        
        public override double CalculateDamge(CardModell enemyCard)
        {
            if (enemyCard.GetType().IsSubclassOf(typeof(BaseDragonModell)) 
                || enemyCard.GetType().IsSubclassOf(typeof(SpellCardModell)))
            {
                return 0;
            }
            
            Random rand = new Random();
            return Damage * AttackSpeed * rand.NextDouble();
        }
    }
}