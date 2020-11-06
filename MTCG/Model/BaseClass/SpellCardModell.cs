using System;
using System.IO;

namespace MTCG.Model.BaseClass
{
    public class SpellCardModell : CardModell
    {
        public double WeakDamage { get;protected set; }
        public SpellCardModell(CardEntity cardEntity)
        {
            if(cardEntity.CardType != CardType.SpellCard)
                throw new InvalidDataException("Card is not a SpellCard");
            
            Id = cardEntity.Id;
            Name = cardEntity.Name;
            Damage = cardEntity.Damage;
            Description = cardEntity.Description;
            ElementType = cardEntity.ElementType;
            CardType = cardEntity.CardType;
            WeakDamage = cardEntity.WeakDamage;
        }
        
        public override double CalculateDamge(CardModell enemyCard)
        {
            
            if (enemyCard.CardType == CardType.MonsterCard && ((MonsterCardModell) enemyCard).Race == Race.Kraken)
            {
                return 0;
            }
            
            if (enemyCard.CardType == CardType.MonsterCard && ((MonsterCardModell) enemyCard).Race == Race.Knight)
            {
                return 9999;
            }

            if (EnemyIsWeakAgainstThisElement(enemyCard))
            {
                return WeakDamage;
            }

            return Damage;
        }
    }
}
