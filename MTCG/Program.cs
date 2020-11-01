using System;
using MTCG.Helpers;
using MTCG.Model;
using MTCG.Model.BaseClass;
using MTCG.Model.MonsterTypes.Dragon;
using MTCG.Model.MonsterTypes.Kraken;
using MTCG.Model.SpellCards.Fire;
using Npgsql;

namespace MTCG
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string token = UserModell.CreateTokenForUser("test", "test");
            UserModell model = new UserModell("test-mtcgToken");
        }
    }
}
