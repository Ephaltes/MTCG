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
        
        public PackageModell(PackageEntity entity,IDatabase database)
        {
            if (entity.Id == null || entity.Amount == 0 || entity.CardsInPackage.Count < Constant.MAXCARDSPERPACKAGE)
                throw new MissingMemberException("Packages doesnt have Id, Amount or Cards");

            Entity = entity;
            _database = database;

        }

        public PackageModell(IDatabase database)
        {
            Entity = new PackageEntity();
            _database = database;
        }

        public bool AddCardToPackage(ICard card)
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
        public bool AddCardsToPackage(List<ICard> cards)
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

        public List<ICard> Open()
        {
            Entity.Amount--;
            var list = new List<ICard>();
            foreach (var card in Entity.CardsInPackage)
            {
                ICard temp = card.CloneJson();
                temp.GenerateRandomId();
                list.Add(temp);
            }

            _database.AddCardsToDatabase(list);

            return list;
        }
        
    }
}