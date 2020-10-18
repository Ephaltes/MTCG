using System;

namespace MTCG.Model.BaseClass
{
    public enum ElementType
    {
        Fire,
        Water,
        Normal
    }

    public enum CardType
    {
        Unknown,
        MonsterCard,
        SpellCard
    }

    public abstract class CardModell
    {
        private static readonly Random _random = new Random();
        public string Name { get; set; }
        public double Damage { get; set; }
        public string Description { get; set; }

        public ElementType ElementType { get; set; } = ElementType.Normal;
        public CardType CardType { get; set; } = CardType.Unknown;

        public abstract double CalculateDamge(CardModell enemyCard);
        
        protected bool EnemyIsWeakAgainstThisElement(CardModell enemyCard)
        {
            if (ElementType == ElementType.Fire && enemyCard.ElementType == ElementType.Normal)
                return true;
            
            if (ElementType == ElementType.Water && enemyCard.ElementType == ElementType.Fire)
                return true;
            
            if (ElementType == ElementType.Normal && enemyCard.ElementType == ElementType.Water)
                return true;

            return false;
        }

        protected static double DnDDiceRoll()
        {
            return _random.NextDouble() * (Constant.MAXDICEROLL - Constant.MINDICEROLL) + Constant.MINDICEROLL;
        }
    }
}
