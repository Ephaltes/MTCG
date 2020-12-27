using System.Linq;
using MTCG.Interface;
using MTCG.Model;
using WebServer;
using WebServer.API;
using WebServer.Interface;

namespace MTCG.API
{
    public class TransactionsHandler : DefaultRessourceHandler
    {
        public TransactionsHandler(IRequestContext req, IDatabase database) : base(req, database)
        {
        }

        protected override ResponseContext HandlePost()
        {
            var responseContext = new ResponseContext();
            var model = new UserModell(Database);

            if (RequestContext.HttpRequest[1].ToLower() != "packages")
                return CustomError("Wrong Parameter", StatusCodes.BadRequest);


            RequestContext.HttpHeader.TryGetValue("Authorization", out var token);
            var authorization = ConvertToAuthorizationEntity(token);

            if (authorization == null || !model.VerifyToken(authorization.Value)) return NotAuthorized();

            if (model.UserEntity.Coins < 5)
                return CustomError("Not enough coins", StatusCodes.BadRequest);

            var packages = Database.GetPackages().OrderBy(x => x.PackageAmount).ToList();

            var cards = packages[0].Open(model.UserEntity);

            if (cards == null)
                return CustomError("No Packages available", StatusCodes.NotFound);

            return SuccessObject(cards, StatusCodes.OK);
        }
    }
}