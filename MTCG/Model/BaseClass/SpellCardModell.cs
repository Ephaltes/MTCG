using System;

namespace MTCG.Model.BaseClass
{
    public abstract class SpellCardModell : CardModell
    {
        public override double CalculateDamge(CardModell enemyCard)
        {
            Random rand = new Random();

            if (enemyCard.GetType().IsSubclassOf(typeof(BaseKrakenModell)))
            {
                return 0;
            }

            return Damage * rand.NextDouble();
        }
    }
}
