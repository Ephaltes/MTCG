using System.Collections.Generic;
using MTCG.Entity;

namespace MTCG.Interface
{
    public interface IPackage
    {
        public int CardCount { get; }
        public int PackageAmount { get; }
        public string Id { get; }

        public bool AddCardToPackage(CardEntity card);

        public bool AddCardsToPackage(List<CardEntity> cards);

        public List<CardEntity> Open(UserEntity user);
    }
}