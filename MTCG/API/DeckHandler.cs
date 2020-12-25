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

            if (token == null || !model.VerifyToken(authorization.Value))
            {
                return NotAuthorized();
            }

            var deck = model.Deck;

            if (deck.Count > 0)
            {
                return SuccessObject(deck, StatusCodes.OK);
            }
            return CustomError("No Cards found", StatusCodes.NotFound);
        }
        
        protected override ResponseContext HandlePut()
        {
            UserModell model = new UserModell(Database);

            RequestContext.HttpHeader.TryGetValue("Authorization", out string token);
            var authorization = ConvertToAuthorizationEntity(token);

            if (token == null || !model.VerifyToken(authorization.Value))
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

    }
}