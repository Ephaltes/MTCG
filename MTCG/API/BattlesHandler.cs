using MTCG.Interface;
using MTCG.Model;
using WebServer;
using WebServer.API;
using WebServer.Interface;
using Constant = MTCG.Model.Constant;

namespace MTCG.API
{
    public class BattlesHandler : DefaultRessourceHandler
    {
        public BattlesHandler(IRequestContext req, IDatabase database) : base(req, database)
        {
        }

        protected override ResponseContext HandlePost()
        {
            var model = new UserModell(Database);

            RequestContext.HttpHeader.TryGetValue("Authorization", out var token);
            var authorization = ConvertToAuthorizationEntity(token);

            if (authorization == null || !model.VerifyToken(authorization.Value)) return NotAuthorized();

            if (model.Deck.Count != Constant.MAXCARDSINDECK)
                return CustomError("Not Enough Cards in Deck", StatusCodes.BadRequest);

            var gameModell = new GameModell(model);

            return SuccessObject(gameModell.GetLog(), StatusCodes.OK);
        }
    }
}