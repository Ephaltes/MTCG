using System;
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
        public PackageModell Package { get; }

        private IDatabase _database;

        public UserModell(IDatabase db)
        {

            _database = db;
            Stack = new StackModell();
            Deck = new DeckModell();
            Package = new PackageModell();
        }

        public bool VerifyToken(string token)
        {
            if (!token.Contains("-mtcgToken"))
                throw new AuthenticationException("Wrong Token");
            
            UserEntity = _database.GetUserByToken(token);
            return true;
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
            
            throw new Exception("Error: Create UserToken");
        }
    }
}