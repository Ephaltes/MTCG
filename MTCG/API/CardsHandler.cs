using System;
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
    public class CardsHandler : DefaultRessourceHandler
    {

        public CardsHandler(IRequestContext req, IDatabase database) : base(req, database)
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

            var stack = model.Stack;

            if (stack.Count > 0)
            {
                return SuccessObject(stack, StatusCodes.OK);
            }
            return CustomError("No Cards found", StatusCodes.NotFound);
        }

    }
}