using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
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
        protected static ConcurrentBag<UserModell> PlayerList { get; set; } = 
            new ConcurrentBag<UserModell>();
        protected static ConcurrentDictionary<string, ReportEntity> LogList { get; set; } =
            new ConcurrentDictionary<string, ReportEntity>();
        
        protected UserModell _player;
        private static object _lockobject = new object();
      
        public GameModell(UserModell player)
        {
            PlayerList.Add(player);
            _player = player;
        }

        public ReportEntity GetLog()
        {
            ReportEntity report = null;
            while (!LogList.TryRemove(_player.UserEntity.Username, out report))
            {
                Thread.Sleep(1000);

                TryFight();
            }

            return report;
        }

        protected void TryFight()
        {
            UserModell player1=null, player2=null;
            lock (_lockobject)
            {
                if (PlayerList.Count < 2 )
                {
                    return;
                }

                if (PlayerList.TryTake(out player1))
                {
                    if (!PlayerList.TryTake(out player2))
                    {
                        PlayerList.Add(player1);
                        return;
                    }
                }
                else
                    return;
            }

            Fight(player1, player2);
        }
        
        protected void Fight(UserModell player1, UserModell player2)
        {
            var player1Deck = player1.Deck;
            var player2Deck = player2.Deck;
            
            
            string log="Fight started:\n\n";
            int i = 0;
            while (player1Deck.Count > 0 && player2Deck.Count > 0 && i < Constant.MAXROUND)
            {
                
                //Get Random Card in Deck
                var player1Card = player1Deck.OrderBy(x => Guid.NewGuid()).ToList()[0];
                var player2Card = player2Deck.OrderBy(x => Guid.NewGuid()).ToList()[0];

                var player1dmg = CalculateDamge(player1Card,player2Card);
                var player2dmg = CalculateDamge(player2Card,player1Card);

                log += $"Round {i + 1}: \n";
                log += $"Player1 dealing: {player1dmg}\n";
                log += $"Player2 dealing: {player2dmg}\n";

                if (player1dmg > player2dmg)
                {
                    player1Deck.Add(player2Card);
                    player2Deck.Remove(player2Card);
                    log += $"Player1 won the round dealing {player1dmg} Damage\n";
                }
                else if (player2dmg > player1dmg)
                {
                    player2Deck.Add(player1Card);
                    player1Deck.Remove(player1Card);
                    log += $"Player2 won the round dealing {player2dmg} Damage\n";
                }

                log += "\n";
                i++;
            }

            ReportEntity report = null;
            if (player1Deck.Count == 0)
            {
                log += "END: Winner Player2";
                report =  new ReportEntity()
                {
                    GameEnd = GameEnd.Player2, Log = log, Player1 = player1.UserEntity.DisplayName, Player2 = player2.UserEntity.DisplayName
                    ,Winner = player2.UserEntity.DisplayName
                };

                player1.LostFightAgainst(player2.UserEntity);
                player2.WonFightAgainst(player1.UserEntity);
                
            }
            if (player2Deck.Count == 0)
            {
                log += "END: Winner Player1";
                report =  new ReportEntity()
                {
                    GameEnd = GameEnd.Player1, Log = log, Player1 = player1.UserEntity.DisplayName, Player2 = player2.UserEntity.DisplayName,
                    Winner = player1.UserEntity.DisplayName
                };

                player1.WonFightAgainst(player2.UserEntity);
                player2.LostFightAgainst(player1.UserEntity);
            }

            if (player1Deck.Count > 0 && player2Deck.Count > 0)
            {
                log += "END: Draw";
                report =  new ReportEntity()
                {
                    GameEnd = GameEnd.Draw, Log = log, Player1 = player1.UserEntity.DisplayName, Player2 = player2.UserEntity.DisplayName,
                };
                //No Elo change when Draw
            }
            
            LogList.TryAdd(player1.UserEntity.DisplayName, report);
            LogList.TryAdd(player2.UserEntity.DisplayName, report);
            
        }
        
        public static double CalculateDamge(CardEntity attackingCard,CardEntity defendingCard)
        {
            if (defendingCard.CardType == CardType.MonsterCard && attackingCard.CardType == CardType.MonsterCard)
            {
                if (attackingCard.Race == Race.Goblin && defendingCard.Race == Race.Dragon)
                {
                    return 0;
                }
                
                if (attackingCard.Race == Race.Orc && defendingCard.Race == Race.Wizard)
                {
                    return 0;
                }
                
                if (attackingCard.Race == Race.Dragon && defendingCard.Race == Race.FireElf)
                {
                    return 0;
                }
            }

            if (attackingCard.CardType == CardType.SpellCard && defendingCard.CardType == CardType.MonsterCard)
            {
                if (defendingCard.Race == Race.Kraken)
                {
                    return 0;
                }
            
                if (defendingCard.Race == Race.Knight)
                {
                    return 9999;
                }
            }
            
            if (attackingCard.CardType == CardType.MonsterCard && defendingCard.CardType == CardType.SpellCard 
                || attackingCard.CardType == CardType.SpellCard && defendingCard.CardType == CardType.MonsterCard)
            {
                var weak = WeakAgainstCardElement(attackingCard, defendingCard);

                switch (weak)
                {
                    case WeakAgainst.AttackingCard:
                        return attackingCard.Damage * 0.5;
                    case WeakAgainst.DefendingCard:
                        return attackingCard.Damage * 2.0;
                }
                
            }
            return attackingCard.Damage;
        }

        public enum WeakAgainst
        {
            Normal,
            AttackingCard,
            DefendingCard
        }
        public static WeakAgainst WeakAgainstCardElement(CardEntity attackingCard, CardEntity defendingCard)
        {
            if (attackingCard.ElementType == ElementType.Fire && defendingCard.ElementType == ElementType.Normal)
                return WeakAgainst.DefendingCard;
            
            if (attackingCard.ElementType == ElementType.Water && defendingCard.ElementType == ElementType.Fire)
                return WeakAgainst.DefendingCard;
            
            if (attackingCard.ElementType == ElementType.Normal && defendingCard.ElementType == ElementType.Water)
                return WeakAgainst.DefendingCard;
            
            if (attackingCard.ElementType == ElementType.Fire && defendingCard.ElementType == ElementType.Water)
                return WeakAgainst.AttackingCard;
            
            if (attackingCard.ElementType == ElementType.Water && defendingCard.ElementType == ElementType.Normal)
                return WeakAgainst.AttackingCard;
            
            if (attackingCard.ElementType == ElementType.Normal && defendingCard.ElementType == ElementType.Fire)
                return WeakAgainst.AttackingCard;
            
            return WeakAgainst.Normal;
        }

    }
}