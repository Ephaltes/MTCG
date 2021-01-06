using MTCG.Interface;
using MTCG.Model;
using WebServer;
using WebServer.API;
using WebServer.Interface;

namespace MTCG.API
{
    public class CardsHandler : DefaultRessourceHandler
    {
        public CardsHandler(IRequestContext req, IDatabase database) : base(req, database)
        {
        }

        protected override ResponseContext HandleGet()
        {
            var model = new UserModell(Database);

            RequestContext.HttpHeader.TryGetValue("Authorization", out var token);
            var authorization = ConvertToAuthorizationEntity(token);

            if (authorization == null || !model.VerifyToken(authorization.Value)) return NotAuthorized();

            var stack = model.Stack;

            if (stack?.Count > 0) return SuccessObject(stack, StatusCodes.OK);
            return CustomError("No Cards found", StatusCodes.NotFound);
        }
    }
}