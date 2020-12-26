using System;
using System.Collections.Generic;
using MTCG.Entity;
using MTCG.Helpers;
using MTCG.Interface;
using MTCG.Model;
using MTCG.Model.BaseClass;
using Newtonsoft.Json;
using WebServer;
using WebServer.API;
using WebServer.Interface;
using WebServer.RessourceHandler;
using Constant = MTCG.Model.Constant;

namespace MTCG.API
{
    public class DeckHandler : DefaultRessourceHandler
    {

        public DeckHandler(IRequestContext req, IDatabase database) : base(req, database)
        {
        }

        protected override ResponseContext HandleGet()
        {
            UserModell model = new UserModell(Database);

            RequestContext.HttpHeader.TryGetValue("Authorization", out string token);
            var authorization = ConvertToAuthorizationEntity(token);

            if (authorization == null || !model.VerifyToken(authorization.Value))
            {
                return NotAuthorized();
            }

            var deck = model.Deck;

            if (deck.Count > 0)
            {
                if(RequestContext.HttpRequest.Count < 2)
                    return SuccessObject(deck, StatusCodes.OK);

                if (RequestContext.HttpRequest.Count > 1 && RequestContext.HttpRequest[1] == Constant.PLAINTEXT)
                    return SuccessObject(deck.ToStringForCardList(), StatusCodes.OK);
            }
            return CustomError("No Cards found", StatusCodes.NotFound);
        }
        
        protected override ResponseContext HandlePut()
        {
            UserModell model = new UserModell(Database);

            RequestContext.HttpHeader.TryGetValue("Authorization", out string token);
            var authorization = ConvertToAuthorizationEntity(token);

            if (authorization == null || !model.VerifyToken(authorization.Value))
            {
                return NotAuthorized();
            }

            if (string.IsNullOrEmpty(RequestContext.HttpBody))
                return EmptyBody();

            var cardids = JsonConvert.DeserializeObject<List<string>>(RequestContext.HttpBody);

            if (model.SetDeckByCardIds(cardids))
                return SuccessObject("Cards added successful", StatusCodes.OK);
            
            return SomeThingWrong();
        }
        
        protected override ResponseContext HandlePost()
        {
            UserModell model = new UserModell(Database);

            RequestContext.HttpHeader.TryGetValue("Authorization", out string token);
            var authorization = ConvertToAuthorizationEntity(token);

            if (authorization == null || !model.VerifyToken(authorization.Value))
            {
                return NotAuthorized();
            }

            if (string.IsNullOrEmpty(RequestContext.HttpBody))
                return EmptyBody();

            var cardid = JsonConvert.DeserializeObject<string>(RequestContext.HttpBody);

            if (model.AddCardToDeckByCardId(cardid))
                return SuccessObject("Cards added successful", StatusCodes.OK);
            
            return SomeThingWrong();
        }
        
        protected override ResponseContext HandleDelete()
        {
            UserModell model = new UserModell(Database);

            RequestContext.HttpHeader.TryGetValue("Authorization", out string token);
            var authorization = ConvertToAuthorizationEntity(token);

            if (authorization == null || !model.VerifyToken(authorization.Value))
            {
                return NotAuthorized();
            }

            if (string.IsNullOrEmpty(RequestContext.HttpBody))
                return EmptyBody();

            var cardid = JsonConvert.DeserializeObject<string>(RequestContext.HttpBody);

            if (model.RemoveCardFromDeckByCardId(cardid))
                return SuccessObject("Cards added successful", StatusCodes.OK);
            
            return SomeThingWrong();
        }

    }
}