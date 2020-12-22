using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MTCG.Entity;
using MTCG.Helpers;
using MTCG.Interface;
using MTCG.Model.BaseClass;

namespace MTCG.Model
{
    public class PackageModell : IPackage
    {
        protected PackageEntity Entity;

        public int CardCount => Entity.CardsInPackage.Count;
        public int PackageAmount => Entity.Amount;

        public string Id => Entity.Id;

        private IDatabase _database;

        public PackageModell(PackageEntity entity, IDatabase database)
        {
            if (entity.Amount == 0 || entity.CardsInPackage.Count < Constant.MAXCARDSPERPACKAGE)
                throw new MissingMemberException("Packages doesnt have Id, Amount or Cards");

            if (string.IsNullOrEmpty(Entity.Id))
                Entity.Id = Guid.NewGuid().ToString();

            Entity = entity;
            _database = database;
        }

        public PackageModell(IDatabase database)
        {
            Entity = new PackageEntity();
            _database = database;
        }

        public bool AddCardToPackage(CardEntity entity)
        {
            try
            {
                if (string.IsNullOrEmpty(entity.Id))
                    entity.Id = Guid.NewGuid().ToString();


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
                        card.Id = Guid.NewGuid().ToString();
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

        public List<CardEntity> Open()
        {
            Entity.Amount--;
            var list = new List<CardEntity>();
            foreach (var card in Entity.CardsInPackage)
            {
                CardEntity temp = card.CloneJson();
                temp.Id = Guid.NewGuid().ToString();
                list.Add(temp);
            }

            _database.AddCardsToDatabase(list);

            return list;
        }
    }
}