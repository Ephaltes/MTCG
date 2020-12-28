using System.Collections.Generic;
using MTCG.Entity;
using MTCG.Helpers;
using MTCG.Interface;

namespace MTCG.Model
{
    public class TradingModell
    {
        private readonly IDatabase _database;
        
        public TradingModell(IDatabase db)
        {
            _database = db;
        }

        public List<TradingEntity> GetAllTradingDeals()
        {
            return _database.GetAllTradingDeals();
        }

        public bool DeleteTradeByUserRequest(string tradeId, UserEntity userEntity)
        {
            return _database.RemoveTradeByUserRequest(tradeId, userEntity.Id);
        }

        public bool Add(TradingEntity entity)
        {
            return _database.AddTrade(entity);
        }

        public bool Trade(string tradeid, string cardToTrade, UserEntity userEntity)
        {
            return _database.TradeCards(tradeid, cardToTrade,userEntity);
        }
    }
}