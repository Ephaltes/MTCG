using System;
using System.Collections.Generic;
using System.Linq;
using MTCG.Entity;
using MTCG.Interface;
using MTCG.Model.BaseClass;

namespace MTCG.Model
{
    public class PackageModell
    {
        protected PackageEntity Entity;

        public int CardCount => Entity.CardsInPackage.Count;
        public int PackageAmount => Entity.Amount;
        
        public string Id => Entity.Id;

        private IDatabase _database;

        public PackageModell(PackageEntity entity,IDatabase database)
        {
            if (entity.Id == null || entity.Amount == 0 || entity.CardsInPackage.Count < Constant.MAXCARDSPERPACKAGE)
                throw new MissingMemberException("Packages doesnt have Id, Amount or Cards");

            Entity = entity;
            _database = database;
            _database.AddCardsToDatabase(Entity.CardsInPackage);
        }

        public bool AddCardToPackage(CardModell card)
        {
            try
            {
                Entity.CardsInPackage.Add(card);
                _database.AddCardToDatabase(card);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Entity.CardsInPackage.Remove(card);
                return false;
            }
        }
        public bool AddCardsToPackage(List<CardModell> cards)
        {
            try
            {
                Entity.CardsInPackage.AddRange(cards);
                _database.AddCardsToDatabase(cards);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Entity.CardsInPackage = Entity.CardsInPackage.Except(cards).ToList();
                return false;
            }
        }

        public List<CardModell> Open()
        {
            Entity.Amount--;
            var list = Entity.CardsInPackage.ToList();
            foreach (var card in list)
            {
                card.GenerateRandomId();
            }

            _database.AddCardsToDatabase(list);

            return list;
        }
        
    }
}