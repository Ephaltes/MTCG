using System.Collections.Generic;
using MTCG.Entity;

namespace MTCG.Interface
{
    public interface IDatabase
    {
        public bool CreateUser(UserEntity userEntity);

        public bool UserExists(string username);

        public UserEntity GetUserByToken(string token);


        public bool AddCardsToDatabase(List<CardEntity> cardsToAdd);

        public bool AddCardToDatabase(CardEntity cardsToAdd);

        public UserEntity GetUserByUsername(string username);

        public bool UpdateUser(UserEntity user);

        public bool AddCardToStack(CardEntity card, UserEntity user);

        public bool UpdateCardStatus(CardEntity card, UserEntity user, CardPlace cardPlace);

        public List<IPackage> GetPackages();

        public IPackage GetPackage(string packageid);

        public List<CardEntity> GetCardsInPackage(string packageid);

        public bool AddPackage(PackageEntity packageEntity);
        public bool AddCardToPackage(PackageEntity packageEntity);

        public bool OpenPackage(PackageEntity packageEntity, UserEntity userEntity);

        public List<CardEntity> GetStackFromUser(UserEntity userEntity);
        public List<CardEntity> GetDeckFromUser(UserEntity userEntity);
        public bool SetDeckByCardIds(List<string> cardIDs, UserEntity userEntity);

        public bool AddCardToDeckByCardId(string cardId, UserEntity userEntity);
        public bool RemoveCardFromDeckByCardId(string cardId, UserEntity userEntity);
        public List<UserEntity> LoadScoreBoard();

        public bool UpdateElo(UserEntity me, UserEntity enemy, bool won = true);
        public CardEntity GetCardById(string cardid);
        public List<TradingEntity> GetAllTradingDeals();
        public UserEntity GetUserById(int id);
        public bool RemoveTradeByUserRequest(string tradeId, int userid);
        public bool AddTrade(TradingEntity entity);
        public bool TradeCards(string tradeid, string cardToTrade, UserEntity userEntity);
        public TradingEntity GetTradeById(string tradeid);
    }
}