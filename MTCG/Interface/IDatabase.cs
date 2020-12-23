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


        public bool AddCardsToDatabase(List<CardEntity> cardsToAdd);

        public bool AddCardToDatabase(CardEntity cardsToAdd);

        public UserEntity GetUserByUsername(string username);

        public bool UpdateUser(UserEntity user);

        public bool AddCardToStack(ICard card, UserEntity user);

        public bool UpdateCardStatus(ICard card, UserEntity user, CardPlace cardPlace);

        public List<IPackage> GetPackages();

        public IPackage GetPackage(string packageid);

        public List<CardEntity> GetCardsInPackage(string packageid);

        public bool AddPackage(PackageEntity packageEntity);
        public bool AddCardToPackage(PackageEntity packageEntity);
    }
}