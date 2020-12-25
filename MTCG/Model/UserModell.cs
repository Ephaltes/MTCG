using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using MTCG.Entity;
using MTCG.Helpers;
using MTCG.Interface;
using MTCG.Model.BaseClass;

namespace MTCG.Model
{
    public class UserModell
    {
        public UserEntity UserEntity { get; set; }
        public List<CardEntity> Stack => GetStack();
        public List<CardEntity> Deck
        {
            get => GetDeck();
        }

        private IDatabase _database;

        public UserModell(IDatabase db)
        {
            _database = db;
        }

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

            UserEntity newUser = new UserEntity();
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
            return _database.SetDeckByCardIds(cardIds,UserEntity);
        }
    }
}