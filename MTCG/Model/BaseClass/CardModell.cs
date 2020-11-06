using System;
using Newtonsoft.Json;

namespace MTCG.Model.BaseClass
{
    public enum ElementType
    {
        Normal,
        Fire,
        Water,
    }

    public enum CardType
    {
        Unknown,
        MonsterCard,
        SpellCard
    }
    
    
    public abstract class CardModell
    {
        public string Id { get; protected set; } = Guid.NewGuid().ToString();
        public string Name { get; protected set; }
        public double Damage { get;protected set; }
        public string Description { get;protected set; }
        public ElementType ElementType { get;protected set; } = ElementType.Normal;
        public CardType CardType { get;protected set; } = CardType.Unknown;

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

        public void GenerateRandomId()
        {
            Id = Guid.NewGuid().ToString();
        }

    }
}
