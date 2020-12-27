using System.Collections.Generic;
using MTCG.Helpers;
using MTCG.Interface;
using MTCG.Model;
using Newtonsoft.Json;
using WebServer;
using WebServer.API;
using WebServer.Interface;
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
            var model = new UserModell(Database);

            RequestContext.HttpHeader.TryGetValue("Authorization", out var token);
            var authorization = ConvertToAuthorizationEntity(token);

            if (authorization == null || !model.VerifyToken(authorization.Value)) return NotAuthorized();

            var deck = model.Deck;

            if (deck.Count > 0)
            {
                if (RequestContext.HttpRequest.Count < 2)
                    return SuccessObject(deck, StatusCodes.OK);

                if (RequestContext.HttpRequest.Count > 1 && RequestContext.HttpRequest[1] == Constant.PLAINTEXT)
                    return SuccessObject(deck.ToStringForCardList(), StatusCodes.OK);
            }

            return CustomError("No Cards found", StatusCodes.NotFound);
        }

        protected override ResponseContext HandlePut()
        {
            var model = new UserModell(Database);

            RequestContext.HttpHeader.TryGetValue("Authorization", out var token);
            var authorization = ConvertToAuthorizationEntity(token);

            if (authorization == null || !model.VerifyToken(authorization.Value)) return NotAuthorized();

            if (string.IsNullOrEmpty(RequestContext.HttpBody))
                return EmptyBody();

            var cardids = JsonConvert.DeserializeObject<List<string>>(RequestContext.HttpBody);

            if (model.SetDeckByCardIds(cardids))
                return SuccessObject("Cards added successful", StatusCodes.OK);

            return SomeThingWrong();
        }

        protected override ResponseContext HandlePost()
        {
            var model = new UserModell(Database);

            RequestContext.HttpHeader.TryGetValue("Authorization", out var token);
            var authorization = ConvertToAuthorizationEntity(token);

            if (authorization == null || !model.VerifyToken(authorization.Value)) return NotAuthorized();

            if (string.IsNullOrEmpty(RequestContext.HttpBody))
                return EmptyBody();

            var cardid = JsonConvert.DeserializeObject<string>(RequestContext.HttpBody);

            if (model.AddCardToDeckByCardId(cardid))
                return SuccessObject("Cards added successful", StatusCodes.OK);

            return SomeThingWrong();
        }

        protected override ResponseContext HandleDelete()
        {
            var model = new UserModell(Database);

            RequestContext.HttpHeader.TryGetValue("Authorization", out var token);
            var authorization = ConvertToAuthorizationEntity(token);

            if (authorization == null || !model.VerifyToken(authorization.Value)) return NotAuthorized();

            if (string.IsNullOrEmpty(RequestContext.HttpBody))
                return EmptyBody();

            var cardid = JsonConvert.DeserializeObject<string>(RequestContext.HttpBody);

            if (model.RemoveCardFromDeckByCardId(cardid))
                return SuccessObject("Cards added successful", StatusCodes.OK);

            return SomeThingWrong();
        }
    }
}