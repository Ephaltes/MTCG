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
    public enum Race
    {
        Unknow,
        Dragon,
        FireElf,
        Goblin,
        Knight,
        Kraken,
        Orc,
        Wizard
    }
    
    public class CardModell
    {
        public CardEntity Entity { get; set; }

        public CardModell(CardEntity entity)
        {
            Entity = entity;
        }

        public CardModell()
        {
            Entity = new CardEntity();
        }
        public double CalculateDamge(CardModell enemyCard)
        {
            if (enemyCard.Entity.CardType == CardType.MonsterCard && Entity.CardType == CardType.MonsterCard)
            {
                if (Entity.Race == Race.Goblin && enemyCard.Entity.Race == Race.Dragon)
                {
                    return 0;
                }
                
                if (Entity.Race == Race.Orc && enemyCard.Entity.Race == Race.Wizard)
                {
                    return 0;
                }
                
                if (Entity.Race == Race.Dragon && enemyCard.Entity.Race == Race.FireElf)
                {
                    return 0;
                }
            }

            if (Entity.CardType == CardType.SpellCard && enemyCard.Entity.CardType == CardType.MonsterCard)
            {
                if (enemyCard.Entity.Race == Race.Kraken)
                {
                    return 0;
                }
            
                if (enemyCard.Entity.Race == Race.Knight)
                {
                    return 9999;
                }

                if (EnemyIsWeakAgainstThisElement(enemyCard))
                {
                    return Entity.WeakDamage;
                }
            }
            return Entity.Damage;
        }
        
        protected bool EnemyIsWeakAgainstThisElement(CardModell enemyCard)
        {
            if (Entity.ElementType == ElementType.Fire && enemyCard.Entity.ElementType == ElementType.Normal)
                return true;
            
            if (Entity.ElementType == ElementType.Water && enemyCard.Entity.ElementType == ElementType.Fire)
                return true;
            
            if (Entity.ElementType == ElementType.Normal && enemyCard.Entity.ElementType == ElementType.Water)
                return true;

            return false;
        }

        public void GenerateRandomId()
        {
            Entity.Id = Guid.NewGuid().ToString();
        }

    }
}
