using System;
using System.IO.MemoryMappedFiles;
using System.Linq;

namespace MTCG.Model
{
    public enum GameEnd
    {
        Draw,
        Player1,
        Player2
    }
    public class GameModell
    {
        public static Tuple<GameEnd,string> Fight(DeckModell player1Deck, DeckModell player2Deck)
        {
            string log="Fight started:\n\n";
            int i = 0;
            while (player1Deck.DeckList.Count > 0 && player2Deck.DeckList.Count > 0 && i < Constant.MAXROUND)
            {
                
                //Get Random Card in Deck
                var player1Card = player1Deck.DeckList.OrderBy(x => Guid.NewGuid()).ToList()[0];
                var player2Card = player2Deck.DeckList.OrderBy(x => Guid.NewGuid()).ToList()[0];

                var player1dmg = player1Card.CalculateDamge(player2Card);
                var player2dmg = player2Card.CalculateDamge(player1Card);

                log += $"Round {i + 1}: \n";
                log += $"Player1 dealing: {player1dmg}\n";
                log += $"Player2 dealing: {player2dmg}\n";

                if (player1dmg > player2dmg)
                {
                    player1Deck.DeckList.Add(player2Card);
                    player2Deck.DeckList.Remove(player2Card);
                    log += $"Player1 won the round dealing {player1dmg} Damage\n";
                }
                else if (player2dmg > player1dmg)
                {
                    player2Deck.DeckList.Add(player1Card);
                    player1Deck.DeckList.Remove(player1Card);
                    log += $"Player2 won the round dealing {player2dmg} Damage\n";
                }

                log += "\n";
                i++;
            }

            if (player1Deck.DeckList.Count == 0)
            {
                log += "END: Winner Player2";
                return new Tuple<GameEnd, string>(GameEnd.Player2,log);
            }
            if (player2Deck.DeckList.Count == 0)
            {
                log += "END: Winner Player1";
                return new Tuple<GameEnd, string>(GameEnd.Player1,log);
            }
            
            log += "END: Draw";
            return new Tuple<GameEnd, string>(GameEnd.Draw,log);

        }
    }
}