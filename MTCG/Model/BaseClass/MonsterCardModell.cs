using System;
using System.IO;
using Newtonsoft.Json;

namespace MTCG.Model.BaseClass
{

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
    
    public class MonsterCardModell : CardModell
    {
        public Race Race { get;protected set; }

        public MonsterCardModell(CardEntity cardEntity)
        {
            if(cardEntity.CardType != CardType.MonsterCard)
                throw new InvalidDataException("Card is not a MonsterCard");
            
            Id = cardEntity.Id;
            Name = cardEntity.Name;
            Damage = cardEntity.Damage;
            Description = cardEntity.Description;
            ElementType = cardEntity.ElementType;
            CardType = cardEntity.CardType;
            Race = cardEntity.Race;
        }

        public MonsterCardModell()
        {
        }
        
        public override double CalculateDamge(CardModell enemyCard)
        {
            if (enemyCard.CardType == CardType.MonsterCard)
            {
                if (Race == Race.Goblin && ((MonsterCardModell)enemyCard).Race == Race.Dragon)
                {
                    return 0;
                }
                
                if (Race == Race.Orc && ((MonsterCardModell)enemyCard).Race == Race.Wizard)
                {
                    return 0;
                }
                
                if (Race == Race.Dragon && ((MonsterCardModell)enemyCard).Race == Race.FireElf)
                {
                    return 0;
                }
            }
            return Damage;
        }
    }
}
