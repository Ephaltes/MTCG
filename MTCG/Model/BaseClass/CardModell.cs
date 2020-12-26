using System;
using MTCG.Interface;
using Newtonsoft.Json;

namespace MTCG.Model.BaseClass
{

    
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
