using System.Collections.Generic;
using MTCG.Entity;
using MTCG.Model;
using MTCG.Model.BaseClass;

namespace MTCG.Interface
{
    public interface IDatabase
    {
        public bool CreateUser(UserEntity userEntity);

        public bool UserExists(string username);

        public UserEntity GetUserByToken(string token);


        public bool AddCardsToDatabase(List<ICard> cardsToAdd);

        public bool AddCardToDatabase(ICard cardsToAdd);

        public bool AddCardToStack(ICard card, UserEntity user);

        public bool UpdateCardStatus(ICard card, UserEntity user, CardPlace cardPlace);

        public List<IPackage> GetPackages();

        public IPackage GetPackage(string packageid);

        public List<ICard> GetCardsInPackage(string packageid);
    }
}