using System;
using System.Security.Authentication;
using MTCG.Model.BaseClass;

namespace MTCG.Model
{
    public class UserModell
    {
        public string Username { get;}
        public double Elo { get; set; }
        public int Win { get; set; }
        public int Lose { get; set; }
        public int Draw { get; set; }
        public StackModell Stack { get; }
        public DeckModell Deck { get; }
        public PackageModell Package { get; }

        public UserModell(string token)
        {
            if (!token.Contains("-mtcgToken"))
                throw new AuthenticationException("Wrong Token");

            //get everything from DB
            Username = "username";
            Elo = 1000;
            Win = 10;
            Lose = 10;
            Draw = 5;
            Stack = new StackModell();
            Deck = new DeckModell();
            Package = new PackageModell();
        }

      public static string CreateTokenForUser(string username, string password)
        {
            //check database if username exists
            //if user exists & passwort same  return token
            // else return user exists
            return username + "-mtcgToken";
            
        }
        
        
    }
}