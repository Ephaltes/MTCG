using System.Text.Json.Serialization;
using MTCG.Entity;
using MTCG.Interface;
using MTCG.Model;
using Newtonsoft.Json;
using WebServer;
using WebServer.API;
using WebServer.Interface;

namespace MTCG.API
{
    public class TradingsHandler : DefaultRessourceHandler
    {
        public TradingsHandler(IRequestContext req, IDatabase database) : base(req, database)
        {
        }

        protected override ResponseContext HandleGet()
        {
            var model = new UserModell(Database);

            RequestContext.HttpHeader.TryGetValue("Authorization", out var token);
            var authorization = ConvertToAuthorizationEntity(token);

            if (authorization == null || !model.VerifyToken(authorization.Value)) return NotAuthorized();

            var tradingModel = new TradingModell(Database);

            var retList = tradingModel.GetAllTradingDeals();

            if (retList.Count < 1)
                return CustomError("No Trading Deals found", StatusCodes.NotFound);

            return SuccessObject(retList, StatusCodes.OK);
        }

        protected override ResponseContext HandlePost()
        {
            var model = new UserModell(Database);

            RequestContext.HttpHeader.TryGetValue("Authorization", out var token);
            var authorization = ConvertToAuthorizationEntity(token);

            if (authorization == null || !model.VerifyToken(authorization.Value)) return NotAuthorized();

            if (string.IsNullOrEmpty(RequestContext.HttpBody)) return EmptyBody();
            
            var tradingModel = new TradingModell(Database);

            if (RequestContext.HttpRequest.Count < 2)
            {
                var tradingEntity = JsonConvert.DeserializeObject<TradingEntity>(RequestContext.HttpBody);
                tradingEntity.UserEntity = model.UserEntity;

                if (tradingEntity.WantMinDamage < 1)
                    return CustomError("Missing WantMinDamage", StatusCodes.NotFound);

            
                if (tradingModel.Add(tradingEntity))
                    return SuccessObject("Added Successful", StatusCodes.Created);
            }
            else
            {
                string tradeid = RequestContext.HttpRequest[1];
                string cardToTrade = JsonConvert.DeserializeObject<string>(RequestContext.HttpBody);
                
                if (string.IsNullOrEmpty(tradeid))
                    return CustomError("No Id provided", StatusCodes.NotFound);

                if (tradingModel.Trade(tradeid, cardToTrade, model.UserEntity))
                    return SuccessObject("Trade Successful", StatusCodes.OK);
            }
            return SomeThingWrong();
        }

        protected override ResponseContext HandleDelete()
        {
            var model = new UserModell(Database);

            RequestContext.HttpHeader.TryGetValue("Authorization", out var token);
            var authorization = ConvertToAuthorizationEntity(token);

            if (authorization == null || !model.VerifyToken(authorization.Value)) return NotAuthorized();

            if (RequestContext.HttpRequest.Count < 2 || string.IsNullOrEmpty(RequestContext.HttpRequest[1])) 
                return CustomError("Missing Parameter", StatusCodes.BadRequest);

            var tradeid = RequestContext.HttpRequest[1];

            var tradingModel = new TradingModell(Database);

            if (tradingModel.DeleteTradeByUserRequest(tradeid, model.UserEntity))
                return SuccessObject("Deleted Successful", StatusCodes.OK);

            return SomeThingWrong();
        }
    }
}