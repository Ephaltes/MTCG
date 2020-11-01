using System;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using MTCG.Entity;
using MTCG.Helpers;
using MTCG.Model.BaseClass;

namespace MTCG.Model
{
    public class UserModell
    {
        public UserEntity UserEntity { get; set; }
        public StackModell Stack { get; }
        public DeckModell Deck { get; }
        public PackageModell Package { get; }

        public UserModell(string token)
        {
            if (!token.Contains("-mtcgToken"))
                throw new AuthenticationException("Wrong Token");

            //TODO: Mock database connection for UnitTest
            DatabaseModell database = new DatabaseModell();
            UserEntity = database.GetUserByToken(token);
            Stack = new StackModell();
            Deck = new DeckModell();
            Package = new PackageModell();
        }

      public static string CreateTokenForUser(string username, string password)
        {
            DatabaseModell database = new DatabaseModell();

            if (database.UserExists(username))
                return "User already exists";
            
            UserEntity newUser = new UserEntity();
            newUser.Username = username;
            var hash = Cryptography.GenerateSaltedHash(password);
            newUser.Password = hash.Hash;
            newUser.Salt = hash.Salt;
            newUser.Token = username + "-mtcgToken";

            if(database.CreateUser(newUser))
                return newUser.Token;
            
            throw new Exception("Error: Create UserToken");
        }
    }
}