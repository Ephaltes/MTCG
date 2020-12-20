using System;
using System.IO.MemoryMappedFiles;
using System.Linq;
using MTCG.Entity;
using MTCG.Interface;
using MTCG.Model.BaseClass;

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
        public static ReportEntity Fight(DeckModell player1Deck, DeckModell player2Deck)
        {
            string log="Fight started:\n\n";
            int i = 0;
            while (player1Deck.DeckList.Count > 0 && player2Deck.DeckList.Count > 0 && i < Constant.MAXROUND)
            {
                
                //Get Random Card in Deck
                var player1Card = player1Deck.DeckList.OrderBy(x => Guid.NewGuid()).ToList()[0];
                var player2Card = player2Deck.DeckList.OrderBy(x => Guid.NewGuid()).ToList()[0];

                var player1dmg = CalculateDamge(player1Card,player2Card);
                var player2dmg = CalculateDamge(player2Card,player1Card);

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
                return new ReportEntity(){GameEnd = GameEnd.Player2,Log = log};
            }
            if (player2Deck.DeckList.Count == 0)
            {
                log += "END: Winner Player1";
                return new ReportEntity(){GameEnd = GameEnd.Player1,Log = log};
            }
            
            log += "END: Draw";
            return new ReportEntity(){GameEnd = GameEnd.Draw,Log = log};

        }
        
        public static double CalculateDamge(ICard attackingCard,ICard defendingCard)
        {
            if (defendingCard.Entity.CardType == CardType.MonsterCard && attackingCard.Entity.CardType == CardType.MonsterCard)
            {
                if (attackingCard.Entity.Race == Race.Goblin && defendingCard.Entity.Race == Race.Dragon)
                {
                    return 0;
                }
                
                if (attackingCard.Entity.Race == Race.Orc && defendingCard.Entity.Race == Race.Wizard)
                {
                    return 0;
                }
                
                if (attackingCard.Entity.Race == Race.Dragon && defendingCard.Entity.Race == Race.FireElf)
                {
                    return 0;
                }
            }

            if (attackingCard.Entity.CardType == CardType.SpellCard && defendingCard.Entity.CardType == CardType.MonsterCard)
            {
                if (defendingCard.Entity.Race == Race.Kraken)
                {
                    return 0;
                }
            
                if (defendingCard.Entity.Race == Race.Knight)
                {
                    return 9999;
                }
            }
            
            if (attackingCard.Entity.CardType == CardType.MonsterCard && defendingCard.Entity.CardType == CardType.SpellCard 
                || attackingCard.Entity.CardType == CardType.SpellCard && defendingCard.Entity.CardType == CardType.MonsterCard)
            {
                var weak = WeakAgainstCardElement(attackingCard, defendingCard);

                switch (weak)
                {
                    case WeakAgainst.AttackingCard:
                        return attackingCard.Entity.Damage * 0.5;
                    case WeakAgainst.DefendingCard:
                        return attackingCard.Entity.Damage * 2.0;
                }
                
            }
            return attackingCard.Entity.Damage;
        }

        public enum WeakAgainst
        {
            Normal,
            AttackingCard,
            DefendingCard
        }
        public static WeakAgainst WeakAgainstCardElement(ICard attackingCard, ICard defendingCard)
        {
            if (attackingCard.Entity.ElementType == ElementType.Fire && defendingCard.Entity.ElementType == ElementType.Normal)
                return WeakAgainst.DefendingCard;
            
            if (attackingCard.Entity.ElementType == ElementType.Water && defendingCard.Entity.ElementType == ElementType.Fire)
                return WeakAgainst.DefendingCard;
            
            if (attackingCard.Entity.ElementType == ElementType.Normal && defendingCard.Entity.ElementType == ElementType.Water)
                return WeakAgainst.DefendingCard;
            
            if (attackingCard.Entity.ElementType == ElementType.Fire && defendingCard.Entity.ElementType == ElementType.Water)
                return WeakAgainst.AttackingCard;
            
            if (attackingCard.Entity.ElementType == ElementType.Water && defendingCard.Entity.ElementType == ElementType.Normal)
                return WeakAgainst.AttackingCard;
            
            if (attackingCard.Entity.ElementType == ElementType.Normal && defendingCard.Entity.ElementType == ElementType.Fire)
                return WeakAgainst.AttackingCard;
            
            return WeakAgainst.Normal;
        }

    }
}