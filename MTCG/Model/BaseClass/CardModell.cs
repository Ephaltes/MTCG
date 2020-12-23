using System;
using MTCG.Interface;
using Newtonsoft.Json;

namespace MTCG.Model.BaseClass
{
    public enum ElementType
    {
        Normal = 0,
        Fire = 1,
        Water = 2,
    }

    public enum CardType
    {
        Unknown,
        MonsterCard = 1,
        SpellCard = 2
    }
    public enum Race
    {
        Unknow = 0,
        Dragon = 1,
        FireElf = 2,
        Goblin = 3,
        Knight = 4,
        Kraken = 5,
        Orc = 6,
        Wizard = 7
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
            Entity.Id = Guid.NewGuid().ToString("N");
        }

    }
}
