using System.Collections.Generic;

namespace MTCG.Interface
{
    public interface IPackage
    {
        public int CardCount { get; }
        public int PackageAmount { get; }
        public string Id { get; }

        public bool AddCardToPackage(ICard card);

        public bool AddCardsToPackage(List<ICard> cards);

        public List<ICard> Open();

    }
}