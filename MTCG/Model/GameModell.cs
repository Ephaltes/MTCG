using System;
using System.IO.MemoryMappedFiles;
using System.Linq;

namespace MTCG.Model
{
    public class GameModell
    {
        public static Tuple<string,string> Fight(DeckModell player1Deck, DeckModell player2Deck)
        {
            string log="Fight started:\n";
            for (int i=0;
                player1Deck.DeckList.Count > 0 && player2Deck.DeckList.Count > 0 && i < Constant.MAXROUND;
                i++ )
            {
                
                //Get Random Card in Deck
                var player1Card = player1Deck.DeckList.OrderBy(x => Guid.NewGuid()).ToList()[0];
                var player2Card = player1Deck.DeckList.OrderBy(x => Guid.NewGuid()).ToList()[0];

                var player1dmg = player1Card.CalculateDamge(player2Card);
                var player2dmg = player2Card.CalculateDamge(player1Card);

                if (player1dmg > player2dmg)
                {
                    player1Deck.DeckList.Add(player2Card);
                    player2Deck.DeckList.Remove(player2Card);
                    log += "Player1 won the round dea"
                }
                else if (player2dmg > player1dmg)
                {
                    player2Deck.DeckList.Add(player1Card);
                    player1Deck.DeckList.Remove(player1Card);
                }

            }
            return new Tuple<string, string>(" "," ");
        }
    }
}