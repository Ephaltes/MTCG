using System.Collections.Generic;
using MTCG.Entity;
using MTCG.Model.BaseClass;

namespace MTCG.Interface
{
    public interface IDatabase
    {
        public bool CreateUser(UserEntity userEntity);

        public bool UserExists(string username);

        public UserEntity GetUserByToken(string token);


        public bool AddCardsToDatabase(List<CardModell> cardsToAdd);

        public bool AddCardToDatabase(CardModell cardsToAdd);
    }
}