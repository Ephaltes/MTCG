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
        protected PackageEntity Entity;

        public PackageModell(PackageEntity entity, IDatabase database)
        {
            if (entity.CardsInPackage.Count < Constant.MAXCARDSPERPACKAGE)
                throw new MissingMemberException("Packages doesnt have Id, Amount or Cards");

            if (string.IsNullOrEmpty(entity.Id))
                entity.Id = Guid.NewGuid().ToString("N");

            Entity = entity;
            _database = database;
        }

        public PackageModell(IDatabase database)
        {
            Entity = new PackageEntity();
            _database = database;
        }

        public int CardCount => Entity.CardsInPackage.Count;
        public int PackageAmount => Entity.Amount;

        public string Id => Entity.Id;

        public bool AddCardToPackage(CardEntity entity)
        {
            try
            {
                if (string.IsNullOrEmpty(entity.Id))
                    entity.Id = Guid.NewGuid().ToString("N");

                entity.CardPlace = CardPlace.Pack;


                Entity.CardsInPackage.Add(entity);
                _database.AddCardToDatabase(entity);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Entity.CardsInPackage.Remove(entity);
                return false;
            }
        }

        public bool AddCardsToPackage(List<CardEntity> entity)
        {
            try
            {
                foreach (var card in entity)
                {
                    if (string.IsNullOrEmpty(card.Id))
                        card.Id = Guid.NewGuid().ToString("N");

                    card.CardPlace = CardPlace.Pack;
                }

                Entity.CardsInPackage.AddRange(entity);
                _database.AddCardsToDatabase(entity);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Entity.CardsInPackage = Entity.CardsInPackage.Except(entity).ToList();
                return false;
            }
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