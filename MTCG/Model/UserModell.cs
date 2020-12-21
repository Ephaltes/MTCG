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
        public StackModell Stack { get; }
        public DeckModell Deck { get; }
        
        private IDatabase _database;

        public UserModell(IDatabase db)
        {

            _database = db;
            Stack = new StackModell();
            Deck = new DeckModell();
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
                return "User already exists";
            
            UserEntity newUser = new UserEntity();
            newUser.Username = username;
            var hash = Cryptography.GenerateSaltedHash(password);
            newUser.Password = hash.Hash;
            newUser.Salt = hash.Salt;
            newUser.Token = username + "-mtcgToken";

            if(_database.CreateUser(newUser))
                return newUser.Token;

            return null;
        }
      }
}