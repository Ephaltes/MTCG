using System;

namespace MTCG.Model.BaseClass
{
    public class SpellCardModell : CardModell
    {
        public override double CalculateDamge(CardModell enemyCard)
        {
            Random rand = new Random();

            if (enemyCard.GetType().IsSubclassOf(typeof(BaseKrakenModell)))
            {
                return 0;
            }
            
            if ( ElementType == CardType.Fire &&  enemyCard.ElementType == CardType.Normal)
            {
                return Damage*Constant.SPELLMULTIPLIER*rand.NextDouble();
            }
            
            if ( ElementType == CardType.Water &&  enemyCard.ElementType == CardType.Fire)
            {
                return Damage*Constant.SPELLMULTIPLIER*rand.NextDouble();
            }
            
            if ( ElementType == CardType.Normal &&  enemyCard.ElementType == CardType.Water)
            {
                return Damage*Constant.SPELLMULTIPLIER*rand.NextDouble();
            }
            
            return Damage * rand.NextDouble();
        }
    }
}
