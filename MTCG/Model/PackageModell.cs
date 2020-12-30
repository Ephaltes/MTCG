using System;
using System.Collections.Generic;
using System.Linq;
using MTCG.Entity;
using MTCG.Helpers;
using MTCG.Interface;

namespace MTCG.Model
{
    public class PackageModell : IPackage
    {
        private readonly IDatabase _database;
        public PackageEntity Entity { get; set; }


        public PackageModell(IDatabase database)
        {
            _database = database;
        }

        public int AddPackage(PackageEntity entity)
        {
            Entity = entity;

            if (Entity.CardsInPackage == null || Entity.CardsInPackage.Count < 1)
                return 1;
            
            if (string.IsNullOrEmpty(Entity.Id))
                Entity.Id = Guid.NewGuid().ToString("N");
            
            foreach (var card in Entity.CardsInPackage)
            {
                if (string.IsNullOrEmpty(card.Id))
                    card.GenerateIdForCard();

                if ( card.CardType == CardType.Unknown
                     || card.CardType == CardType.MonsterCard && card.Race == Race.Unknow
                     || card.Damage <= 0)
                    return 2;
            }

            if(_database.AddPackage(Entity))
                return 0;

            return 3;
        }
        

        public List<CardEntity> Open(UserEntity user)
        {
            Entity.Amount--;
            user.Coins -= 5;

            foreach (var card in Entity.CardsInPackage)
            {
                card.GenerateIdForCard();
                card.CardPlace = CardPlace.Stack;
            }

            if (_database.OpenPackage(Entity, user))
                return Entity.CardsInPackage;

            return null;
        }
    }
}