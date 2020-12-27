using System;
using MTCG.Interface;

namespace MTCG.Model.BaseClass
{
    public class CardModell : ICard
    {
        public CardModell(CardEntity entity)
        {
            Entity = entity;
        }

        public CardModell()
        {
            Entity = new CardEntity();
        }

        public CardEntity Entity { get; set; }

        public void GenerateRandomId()
        {
            Entity.Id = Guid.NewGuid().ToString("N");
        }
    }
}