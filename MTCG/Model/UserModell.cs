using System.Collections.Generic;
using MTCG.Entity;
using MTCG.Helpers;
using MTCG.Interface;

namespace MTCG.Model
{
    public class UserModell
    {
        private readonly IDatabase _database;

        public UserModell(IDatabase db)
        {
            _database = db;
        }

        public UserEntity UserEntity { get; set; }
        public List<CardEntity> Stack => GetStack();

        public List<CardEntity> Deck => GetDeck();

        public bool VerifyToken(string token)
        {
            if (!token.Contains("-mtcgToken"))
                return false;

            UserEntity = _database.GetUserByToken(token);

            if (UserEntity == null)
                return false;

            return true;
        }

        public UserEntity GetUserByUsername(string username)
        {
            return _database.GetUserByUsername(username);
        }

        public bool UpdateUser()
        {
            return _database.UpdateUser(UserEntity);
        }

        public string CreateTokenForUser(string username, string password)
        {
            if (_database.UserExists(username))
                return null;

            var newUser = new UserEntity();
            newUser.Username = username;
            var hash = Cryptography.GenerateSaltedHash(password);
            newUser.Password = hash.Hash;
            newUser.Salt = hash.Salt;
            newUser.Token = username + "-mtcgToken";

            if (_database.CreateUser(newUser))
                return newUser.Token;

            return null;
        }

        public bool VerifyLogin()
        {
            var entity = GetUserByUsername(UserEntity.Username);

            if (Cryptography.VerifyPassword(UserEntity.Password, entity.Password, entity.Salt))
            {
                UserEntity = entity;
                return true;
            }

            return false;
        }

        protected List<CardEntity> GetStack()
        {
            return _database.GetStackFromUser(UserEntity);
        }

        protected List<CardEntity> GetDeck()
        {
            return _database.GetDeckFromUser(UserEntity);
        }

        public bool SetDeckByCardIds(List<string> cardIds)
        {
            return _database.SetDeckByCardIds(cardIds, UserEntity);
        }

        public bool AddCardToDeckByCardId(string cardId)
        {
            return _database.AddCardToDeckByCardId(cardId, UserEntity);
        }

        public bool RemoveCardFromDeckByCardId(string cardId)
        {
            return _database.RemoveCardFromDeckByCardId(cardId, UserEntity);
        }

        public void WonFightAgainst(UserEntity enemy)
        {
            _database.UpdateElo(UserEntity, enemy);
        }

        public void LostFightAgainst(UserEntity enemy)
        {
            _database.UpdateElo(UserEntity, enemy, false);
        }
    }
}