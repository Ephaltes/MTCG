using System;
using MTCG.Interface;
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
    
    public class CardModell : ICard
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
        
        public void GenerateRandomId()
        {
            Entity.Id = Guid.NewGuid().ToString();
        }

    }
}
