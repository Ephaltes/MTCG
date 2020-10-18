using System;

namespace MTCG.Model.BaseClass
{
    public class MonsterCardModell : CardModell
    {
        public double Health=100;
        public double AttackSpeed = 1;

        public MonsterCardModell()
        {
            ElementType = CardType.Normal;
        }
        public override double CalculateDamge(CardModell enemyCard)
        {
            Random rand = new Random();
            return Damage * AttackSpeed * rand.NextDouble();
        }

    }
}
